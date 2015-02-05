using System.Text;
using RabbitMQ.Client;

namespace Termine.Promises.ZMQ
{
    public sealed class RabbitMqServiceBus
    {
        /// <summary>
        ///     Allocate ourselves.
        ///     We have a private constructor, so no one else can.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static readonly RabbitMqServiceBus _instance = new RabbitMqServiceBus();

        private static IConnection _connection;

        /// <summary>
        ///     This is a private constructor, meaning no outsiders have access.
        /// </summary>
        private RabbitMqServiceBus()
        {

        }

        /// <summary>
        ///     Access SiteStructure.Instance to get the singleton object.
        ///     Then call methods on that instance.
        /// </summary>
        public static RabbitMqServiceBus Instance
        {
            get { return _instance; }
        }

        public void Publish(string message)
        {
            if (IsBrokerDisconnected()) Connect();
            
            byte[] encodedMessage = Encoding.ASCII.GetBytes(message);
            _channel.BasicPublish(ExchangeName, RoutingKey, null, encodedMessage);
            //Disconnect();
        }


        private static string Username = "guest";
        private static string Password = "guest";
        private static string VirtualHost = "/";
        // "localhost" if rabbitMq is installed on the same server,
        // else enter the ip address of the server where it is installed.
        private static string HostName = "localhost";
        private static string ExchangeName = "test-exchange";
        private static string ExchangeTypeVal = ExchangeType.Direct;
        private static string QueueName = "SomeQueue";
        private static bool QueueExclusive = false;
        private static bool QueueDurable = false;
        private static bool QueueDelete = false;
        private static string RoutingKey = "yasser";

        private static IModel _channel;

        private void Connect()
        {
            if (!IsBrokerDisconnected()) return;

            var factory = new ConnectionFactory
            {
                UserName = Username,
                Password = Password,
                VirtualHost = VirtualHost,
                Protocol = Protocols.DefaultProtocol,
                HostName = HostName,
                Port = AmqpTcpEndpoint.UseDefaultPort
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, ExchangeTypeVal);
            _channel.QueueDeclare(QueueName, QueueDurable, QueueExclusive, QueueDelete, null);
            _channel.QueueBind(QueueName, ExchangeName, RoutingKey);
        }
        private static void Disconnect()
        {
            _connection.Close(200, "Goodbye");
        }

        private static bool IsBrokerDisconnected()
        {
            if (_connection == null) return true;
            return !_connection.IsOpen;
        }
    }
}