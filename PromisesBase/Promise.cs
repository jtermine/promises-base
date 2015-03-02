using System;
using System.Threading.Tasks;
using NClone;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Termine.Promises.Generics;
using Termine.Promises.Handlers;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    /// <summary>
    /// The basic promise model
    /// </summary>
    /// <typeparam name="TW">a promise workload</typeparam>
    /// <typeparam name="TC">a promise configuration object</typeparam>
    /// <typeparam name="TR">a promise request</typeparam>
    public sealed class Promise<TC, TW, TR> : IAmAPromise<TC, TW, TR>
        where TC: class, IHandlePromiseConfig, new()
        where TW : class, IAmAPromiseWorkload, new()
        where TR: class, IAmAPromiseRequest, new()
    {
        /// <summary>
        /// When a promise initialized, the PromiseId is set to a Guid
        /// </summary>
        public Promise()
        {
            PromiseId = Guid.NewGuid().ToString("N");
            Config = new TC();
            Request = new TR();
        }

        /// <summary>
        /// The promise context stores instances of the actions (i.e. auth challengers, validators, executors) that a promise supports
        /// </summary>
        private class PromiseContext
        {
            /*
            /// <summary>
            /// this dictionary of generic objects travels with the promise context and can be accessed by any object that inherits the promise
            /// </summary>
            public Dictionary<string, object> Objects = new Dictionary<string, object>();
             */

            /// <summary>
            /// this dictionary of AuthChallengers -- these are executed in sequence to determine whether the promise has the authority to run
            /// </summary>
            public readonly WorkloadHandlerQueue<TC, TW, TR> AuthChallengers = new WorkloadHandlerQueue<TC, TW, TR>();
            
            /// <summary>
            /// a dictionary of Validators -- these are executed in sequenced to determine whether the promise workload contains valid information
            /// </summary>
            public readonly WorkloadHandlerQueue<TC, TW, TR> Validators = new WorkloadHandlerQueue<TC, TW, TR>();

            /// <summary>
            /// a dictionary of executors -- these are executed in sequence to perform the action(s) that the promise makes
            /// </summary>
            public readonly WorkloadHandlerQueue<TC, TW, TR> Executors = new WorkloadHandlerQueue<TC, TW, TR>();

            /// <summary>
            /// a dictionary of actions that execute in sequence before a promise starts
            /// </summary>
            public readonly WorkloadHandlerQueue<TC, TW, TR> PreStartActions = new WorkloadHandlerQueue<TC, TW, TR>();

            /// <summary>
            /// a dictionary of actions that execute in sequence after a promise ends
            /// </summary>
            public readonly WorkloadHandlerQueue<TC, TW, TR> PostEndActions = new WorkloadHandlerQueue<TC, TW, TR>();

            /// <summary>
            /// a dictionary of actions that execute in sequence when transmitting a promise to a services
            /// </summary>
            public readonly WorkloadHandlerQueue<TC, TW, TR> XferActions = new WorkloadHandlerQueue<TC, TW, TR>();

            /// <summary>
            /// a dictionary of block handlers -- there are executed when a promise is blocked
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> BlockHandlers { get; private set; }
            
            /// <summary>
            /// a dictionary of trace handlers -- these are executed whe na promise is tracing
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> TraceHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> DebugHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> InfoHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> WarnHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> ErrorHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> FatalHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> AbortHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> AbortOnAccessDeniedHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> SuccessHandlers { get; private set; }

            /// <summary>
            /// when a promise context is initialized, each of the dictionaries it contains are also initialized to make it easy to add the events to the promise
            /// </summary>
            public PromiseContext()
            {
                BlockHandlers = new PromiseHandlerQueue<IHandlePromiseActions>();
                TraceHandlers = new PromiseHandlerQueue<IHandlePromiseActions>();
                DebugHandlers = new PromiseHandlerQueue<IHandlePromiseActions>();
                InfoHandlers = new PromiseHandlerQueue<IHandlePromiseActions>();
                WarnHandlers = new PromiseHandlerQueue<IHandlePromiseActions>();
                ErrorHandlers = new PromiseHandlerQueue<IHandlePromiseActions>();
                FatalHandlers = new PromiseHandlerQueue<IHandlePromiseActions>();
                AbortHandlers = new PromiseHandlerQueue<IHandlePromiseActions>();
                AbortOnAccessDeniedHandlers = new PromiseHandlerQueue<IHandlePromiseActions>();
                SuccessHandlers = new PromiseHandlerQueue<IHandlePromiseActions>();
            }
        }

        private readonly PromiseContext _context = new PromiseContext();

        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        /// <summary>
        /// Deserializes the workload of this promise from Json
        /// </summary>
        /// <param name="json">workload of the promise in Json</param>
        public void DeserializeJson(string json)
        {
            Workload = JsonConvert.DeserializeObject<TW>(json, _jsonSerializerSettings);
        }

        /// <summary>
        /// Serializes the workload of this promise to Json
        /// </summary>
        /// <returns>a string containing the workload serialized to Json</returns>
        public string SerializeJson()
        {
            return JsonConvert.SerializeObject(Workload, _jsonSerializerSettings);
        }

        /// <summary>
        /// The Id for the promise.  Gets sent to the requestId of the workload if one isn't already provided.
        /// </summary>
        public string PromiseId { get; private set; }

        /// <summary>
        /// Exposes the promise's configuration
        /// </summary>
        public TC Config { get; private set; }

        /// <summary>
        /// Exposes the promise's workload
        /// </summary>
        public TW Workload { get; private set; }

        /// <summary>
        /// Exposes the promise's request.
        /// </summary>
        public TR Request { get; private set; }

        /// <summary>
        /// Injects a workload obtained from a serializable source into this promise and resets the promiseId accordingly
        /// </summary>
        /// <param name="workload">the workload obtained from the serializable source</param>
        public void WithWorkload(TW workload)
        {
            Workload = Clone.ObjectGraph(workload);
            PromiseId = workload.RequestId;
        }

        public void WithConfig(TC config)
        {
            Config = config;
        }

        public void WithRequest(TR request)
        {
            Request = request;
        }

        /// <summary>
        /// the number of authChallenger actions established on this promise
        /// </summary>
        public int AuthChallengersCount
        {
            get { return _context.AuthChallengers.Count; }
        }

        /// <summary>
        /// the number of validator actions established on this promise
        /// </summary>
        public int ValidatorsCount
        {
            get { return _context.Validators.Count; }
        }

        /// <summary>
        /// the number of executor actions established on this promise
        /// </summary>
        public int ExecutorsCount
        {
            get { return _context.Executors.Count; }
        }

        /// <summary>
        /// Orders a promise to execute ('run') its validator, authChallenger, and exector tasks asynchronously
        /// </summary>
        /// <returns>the async task that returns an instance of this promise object when it completes</returns>
        public Task<IAmAPromise<TC, TW, TR>> RunAsync()
        {
            return Task.Run(() => Run());
        }

        /// <summary>
        /// Orders a promise to execute ('run') its validator, authChallenger, and exector tasks synchronously
        /// </summary>
        /// <returns>the instance of this promise object</returns>
        public IAmAPromise<TC, TW, TR> Run()
        {
            try
            {
                Workload.RequestId = PromiseId;

                Trace(PromiseMessages.PromiseStarted);

                _context.PreStartActions.Invoke(this, Config, Workload, Request);
                _context.AuthChallengers.Invoke(this, Config, Workload, Request);
                _context.Validators.Invoke(this, Config, Workload, Request);
                _context.Executors.Invoke(this, Config, Workload, Request);
                _context.XferActions.Invoke(this, Config, Workload, Request);
                _context.SuccessHandlers.Invoke(this, PromiseMessages.PromiseSuccess);
                _context.PostEndActions.Invoke(this, Config, Workload, Request);

                Trace(PromiseMessages.PromiseSuccess);

            }
            catch (Exception ex)
            {
                Error(new GenericEventMessage(ex));
                Trace(PromiseMessages.PromiseFail);
            }

            return this;
        }

        /// <summary>
        /// Submits a message to the 'block' instrumentation handler.
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="message"></param>
        public void Block(IHandleEventMessage message)
        {
            Workload.IsTerminated = true;
            Workload.IsBlocked = true;

            _context.BlockHandlers.Invoke(this, message);
        }

        /// <summary>
        /// Submits a message to the trace instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Trace(IHandleEventMessage message)
        {
            _context.TraceHandlers.Invoke(this, message);
        }

        /// <summary>
        /// Submits a message to the debug instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Debug(IHandleEventMessage message)
        {
            _context.DebugHandlers.Invoke(this, message);
        }

        /// <summary>
        /// Submits a message to the info instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Info(IHandleEventMessage message)
        {
            _context.InfoHandlers.Invoke(this, message);
        }

        /// <summary>
        /// Submits a message to the 'warn' instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Warn(IHandleEventMessage message)
        {

            _context.WarnHandlers.Invoke(this, message);
        }

        /// <summary>
        /// Submits a message to the 'error' instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Error(IHandleEventMessage message)
        {
            Workload.IsTerminated = true;
            _context.ErrorHandlers.Invoke(this, message);
        }

        /// <summary>
        /// Submits a message to the 'fatal' instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Fatal(IHandleEventMessage message)
        {
            Workload.IsTerminated = true;
            _context.FatalHandlers.Invoke(this, message);
        }

        /// <summary>
        /// Submits a message to the 'abort' instrumentation handler.
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Abort(IHandleEventMessage message)
        {
            Workload.IsTerminated = true;
            _context.AbortHandlers.Invoke(this, message);
        }

        /// <summary>
        /// Submits a message to the 'abortOnAccessDenied' instrumentation handler.
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void AbortOnAccessDenied(IHandleEventMessage message)
        {
            Workload.IsTerminated = true;
            _context.AbortOnAccessDeniedHandlers.Invoke(this, message);
        }

        /// <summary>
        /// Submits a message to the 'block' instrumentation handler.
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="ex"></param>
        public void Block(Exception ex)
        {
            Block(new GenericEventMessage(ex));
        }

        /// <summary>
        /// Submits an exception to the 'trace' instrumentation handler
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Trace(Exception ex)
        {
            Trace(new GenericEventMessage(ex));
        }

        /// <summary>
        /// Submits an exception to the 'debug' instrumentation handler
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Debug(Exception ex)
        {
            Debug(new GenericEventMessage(ex));
        }

        /// <summary>
        /// Submits an exception to the 'info' instrumentation handler
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Info(Exception ex)
        {
            Info(new GenericEventMessage(ex));
        }

        /// <summary>
        /// Submits an exception to the 'warn' instrumentation handler
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Warn(Exception ex)
        {
            Warn(new GenericEventMessage(ex));
        }

        /// <summary>
        /// Submits an exception to the 'error' instrumentation handler
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Error(Exception ex)
        {
            Error(new GenericEventMessage(ex));
        }

        /// <summary>
        /// Submits an exception to the 'fatal' instrumentation handler
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Fatal(Exception ex)
        {
            Fatal(new GenericEventMessage(ex));
        }

        /// <summary>
        /// Submits an exception to the 'abort' instrumentation handler
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Abort(Exception ex)
        {
            Abort(new GenericEventMessage(ex));
        }

        /// <summary>
        /// Submits an exception to the 'abortOnAccessDenied' instrumentation handler
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void AbortOnAccessDenied(Exception ex)
        {
            AbortOnAccessDenied(new GenericEventMessage(ex));
        }

        public Promise<TC, TW, TR> WithPreStart(string actionId, Action<IHandlePromiseActions, TC, TW, TR> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.PreStartActions.Enqueue(new WorkloadHandler<TC, TW, TR>
            {
                Action = action,
                EndMessage = PromiseMessages.PreStartActionStopped(actionId),
                StartMessage = PromiseMessages.PreStartActionStarted(actionId),
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithPostEnd(string actionId, Action<IHandlePromiseActions, TC, TW, TR> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.PostEndActions.Enqueue(new WorkloadHandler<TC, TW, TR>
            {
                Action = action,
                EndMessage = PromiseMessages.PostEndActionStopped(actionId),
                StartMessage = PromiseMessages.PostEndActionStarted(actionId),
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithValidator(string actionId, Action<IHandlePromiseActions, TC, TW, TR> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.Validators.Enqueue(new WorkloadHandler<TC, TW, TR>
            {
                Action = action,
                EndMessage = PromiseMessages.ValidatorStopped(actionId),
                StartMessage = PromiseMessages.ValidatorStarted(actionId),
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithAuthChallenger(string actionId, Action<IHandlePromiseActions, TC, TW, TR> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.AuthChallengers.Enqueue(new WorkloadHandler<TC, TW, TR>
            {
                Action = action,
                EndMessage = PromiseMessages.AuthChallengerStopped(actionId),
                StartMessage = PromiseMessages.AuthChallengerStarted(actionId),
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithExecutor(string actionId, Action<IHandlePromiseActions, TC, TW, TR> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.Executors.Enqueue(new WorkloadHandler<TC, TW, TR>
            {
                Action = action,
                EndMessage = PromiseMessages.ExecutorStopped(actionId),
                StartMessage = PromiseMessages.ExecutorStarted(actionId),
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithXferAction(string actionId, Action<IHandlePromiseActions, TC, TW, TR> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.XferActions.Enqueue(new WorkloadHandler<TC, TW, TR>
            {
                Action = action,
                EndMessage = PromiseMessages.XferActionStopped(actionId),
                StartMessage = PromiseMessages.XferActionStarted(actionId),
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithBlockHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.BlockHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithTraceHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.TraceHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithDebugHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.DebugHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithInfoHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.InfoHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithWarnHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.WarnHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithErrorHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.ErrorHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithFatalHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.FatalHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithAbortHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.AbortHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithAbortOnAccessDeniedHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.AbortOnAccessDeniedHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

        public Promise<TC, TW, TR> WithSuccessHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.SuccessHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }
    }
}