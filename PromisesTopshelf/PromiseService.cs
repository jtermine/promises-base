using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NLog;
using RabbitMQ.Client;

namespace PromisesTopshelf
{
    public class PromiseService
    {
        private static readonly AutoResetEvent IsRunningEvent = new AutoResetEvent(false);
        private static readonly AutoResetEvent IsConnectedEvent = new AutoResetEvent(false);
        private static readonly AutoResetEvent IsTerminatingEvent = new AutoResetEvent(false);
        private static bool _isRunning = false;
        private static bool _isConnected = true;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        
        private static Thread _mainThread;
        private static Thread _threadOne;
        private static Thread _threadTwo;
        private static Thread _threadThree;
        
        public static void Start()
        {
            _mainThread = new Thread(StartQueue);
            _mainThread.Start();
        }

        private static void FileSystemWatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            Log.Info("file changed > {0} | {1}", fileSystemEventArgs.Name, fileSystemEventArgs.ChangeType);
        }

        public static void Stop()
        {
            IsConnectedEvent.Set();
            _isConnected = false;

            IsTerminatingEvent.WaitOne(TimeSpan.FromSeconds(10));

            if (_threadOne != null && _threadOne.IsAlive) _threadOne.Abort();
            if (_threadTwo != null && _threadTwo.IsAlive) _threadTwo.Abort();
            if (_threadThree != null && _threadThree.IsAlive) _threadThree.Abort();

            if (_mainThread != null && _mainThread.IsAlive) _mainThread.Abort();
        }

        private class ConnectionState
        {
            public IConnection Connection { get; set; }
            public string QueueName { get; set; }
        }

        private static void StartQueue()
        {
            while (_isConnected)
            {
                IsConnectedEvent.Reset();
                ConnectQueue();
                IsConnectedEvent.WaitOne();
            }
        }

        private void RunPool()
        {
            while (_isRunning)
            {
                StartPool();
                IsRunningEvent.WaitOne();
                if (_appDomain != default(AppDomain)) AppDomain.Unload(_appDomain);
            }
        }

        private AppDomain _appDomain;

        private void StartPool()
        {
            _appDomain = AppDomain.CreateDomain("NewApplicationDomain");

            _appDomain.UnhandledException += (sender, args) =>
            {
                _isRunning = false;
                IsRunningEvent.Set();
            };

            _appDomain.ex

        }
        
        /*
        private static void ConnectQueue()
        {
            try
            {
                var factory = new ConnectionFactory {HostName = "localhost"};

                while (_isConnected)
                {
                    using (var connection = factory.CreateConnection())
                    {

                        //_threadOne = new Thread(OpenChannel);
                        //_threadTwo = new Thread(OpenChannel);
                        _threadThree = new Thread(OpenChannel);

                        //_threadOne.Start(new ConnectionState {Connection = connection, QueueName = "queue-1"});
                        //_threadTwo.Start(new ConnectionState {Connection = connection, QueueName = "queue-2"});
                        _threadThree.Start(new ConnectionState {Connection = connection, QueueName = "queue-2"});

                        IsConnectedEvent.WaitOne();

                        //_threadOne.Abort();
                        //_threadTwo.Abort();
                        _threadThree.Abort();

                        if (connection.IsOpen) connection.Abort(5000);
                    }
                }

                IsTerminatingEvent.Set();
            }
            catch (IOException)
            {
                Log.Info("Queue connection terminated.  Reconnecting...");

                if (_threadOne.IsAlive) _threadOne.Abort();
                if (_threadTwo.IsAlive) _threadTwo.Abort();
                if (_threadThree.IsAlive) _threadThree.Abort();

                IsConnectedEvent.Set();
            }
        }
        */
        private static void OpenChannel(object state)
        {
            try
            {
                var fileSystemWatcher = new FileSystemWatcher
                {
                    Path = AppDomain.CurrentDomain.BaseDirectory,
                    Filter = "*.txt",
                    NotifyFilter = NotifyFilters.Size | NotifyFilters.CreationTime | NotifyFilters.FileName,
                    EnableRaisingEvents = true
                };

                fileSystemWatcher.Changed += FileSystemWatcherOnChanged;
                fileSystemWatcher.Created += FileSystemWatcherOnChanged;
                fileSystemWatcher.Renamed += FileSystemWatcherOnChanged;
                fileSystemWatcher.Deleted += FileSystemWatcherOnChanged;

                var connectionState = state as ConnectionState;
                if (connectionState == null) return;

                using (var channel = connectionState.Connection.CreateModel())
                {
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicQos(0, 1000, false);
                    channel.BasicConsume(connectionState.QueueName, false, consumer);

                    Log.Trace("* - Connected {0} - *", connectionState.QueueName);

                    while (true)
                    {
                        var ea = consumer.Queue.Dequeue();
                        var promiseName = ea.RoutingKey;
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);

                        var messageParts = message.Split(Convert.ToChar("|"));

                        DateTime result;
                        Int32 eventId;
                        bool isPublicMessage;

                        var logEntry = new LogEntry
                        {
                            EventDetails = messageParts[3],
                            Body = messageParts[6],
                            DateTime = DateTime.TryParse(messageParts[0], out result) ? result : default(DateTime),
                            EventId = Int32.TryParse(messageParts[2], out eventId) ? eventId : default(Int32),
                            EventMessage = messageParts[4],
                            IsPublicMessage =
                                bool.TryParse(messageParts[6], out isPublicMessage)
                                    ? isPublicMessage
                                    : default(bool)
                        };

                        var obj = JObject.Parse(logEntry.Body);

                        Task.Factory.StartNew(() =>
                        {
                            Thread.Sleep(250);
                            Log.Trace("{0} [x] Received > {1}", connectionState.QueueName, obj);
                            channel.BasicAck(ea.DeliveryTag, false);
                        }).ContinueWith(f =>
                        {
                            if (f.Exception == null) return;

                            try
                            {
                                throw f.Exception;
                            }
                            catch (IOException)
                            {
                                Log.Trace("Connection terminated with RabbitMQ.");
                                if (_isConnected) IsConnectedEvent.Set();
                            }
                            catch (ThreadAbortException)
                            {
                                Log.Trace("Thread aborted.  Exiting channel with RabbitMQ.");
                                if (_isConnected) IsConnectedEvent.Set();
                            }

                            catch (RabbitMQ.Client.Exceptions.AlreadyClosedException)
                            {
                                Log.Trace("Thread aborted.  Exiting channel with RabbitMQ.");
                                if (_isConnected) IsConnectedEvent.Set();
                            }
                            catch (Exception ex)
                            {
                                Log.ErrorException(ex.Message, ex);
                            }

                        }, TaskContinuationOptions.OnlyOnFaulted);

                        //try
                        //{
                        //    var n = Kernel.Get<IHandlePromiseActions>(obj.Property("requestName").Value.ToString());
                        //    n.Start(logEntry.Body);
                        //}
                        //catch (Exception ex)
                        //{
                        //    Log.ErrorException(ex.Message, ex);
                        //}

                    }
                }
            }
            catch (IOException)
            {
                Log.Trace("Connection terminated with RabbitMQ.");
                if (_isConnected) IsConnectedEvent.Set();
            }
            catch (ThreadAbortException)
            {
                Log.Trace("Thread aborted.  Exiting channel with RabbitMQ.");
            }

            catch (RabbitMQ.Client.Exceptions.AlreadyClosedException)
            {
                Log.Trace("Thread aborted.  Exiting channel with RabbitMQ.");   
            }
            catch (Exception ex)
            {
                Log.ErrorException(ex.Message, ex);
                IsConnectedEvent.Set();
            }
        }
    }
}


