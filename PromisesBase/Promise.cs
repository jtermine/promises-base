using System;
using System.Threading.Tasks;
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
	/// <typeparam name="TE">a promise response object</typeparam>
	public sealed class Promise<TC, TW, TR, TE> : IHandlePromiseActions
        where TC: class, IHandlePromiseConfig, new()
        where TW : class, IAmAPromiseWorkload, new()
        where TR: class, IAmAPromiseRequest, new()
        where TE: class, IAmAPromiseResponse, new()
    {
        /// <summary>
        /// When a promise initialized, the PromiseId is set to a Guid
        /// </summary>
        public Promise()
        {
            PromiseId = Guid.NewGuid().ToString("N");
            Config = new TC();
            Request = new TR();
            Workload = new TW();
            Response = new TE();
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
            public readonly WorkloadHandlerQueue<TC, TW, TR, TE> AuthChallengers = new WorkloadHandlerQueue<TC, TW, TR, TE>();
            
            /// <summary>
            /// a dictionary of Validators -- these are executed in sequenced to determine whether the promise workload contains valid information
            /// </summary>
            public readonly WorkloadHandlerQueue<TC, TW, TR, TE> Validators = new WorkloadHandlerQueue<TC, TW, TR, TE>();

            /// <summary>
            /// a dictionary of executors -- these are executed in sequence to perform the action(s) that the promise makes
            /// </summary>
            public readonly WorkloadHandlerQueue<TC, TW, TR, TE> Executors = new WorkloadHandlerQueue<TC, TW, TR, TE>();

            /// <summary>
            /// a dictionary of actions that execute in sequence before a promise starts
            /// </summary>
            public readonly WorkloadHandlerQueue<TC, TW, TR, TE> PreStartActions = new WorkloadHandlerQueue<TC, TW, TR, TE>();

            /// <summary>
            /// a dictionary of actions that execute in sequence after a promise ends
            /// </summary>
            public readonly WorkloadHandlerQueue<TC, TW, TR, TE> PostEndActions = new WorkloadHandlerQueue<TC, TW, TR, TE>();

            /// <summary>
            /// a dictionary of actions that execute in sequence when transmitting a promise to a services
            /// </summary>
            public readonly WorkloadHandlerQueue<TC, TW, TR, TE> XferActions = new WorkloadHandlerQueue<TC, TW, TR, TE>();

            /// <summary>
            /// a dictionary of block handlers -- there are executed when a promise is blocked
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> BlockHandlers { get; }
            
            /// <summary>
            /// a dictionary of trace handlers -- these are executed whe na promise is tracing
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> TraceHandlers { get; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> DebugHandlers { get; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> InfoHandlers { get; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> WarnHandlers { get; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> ErrorHandlers { get; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> FatalHandlers { get; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> AbortHandlers { get; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> AbortOnAccessDeniedHandlers { get; }
            
            /// <summary>
            /// 
            /// </summary>
            public PromiseHandlerQueue<IHandlePromiseActions> SuccessHandlers { get; }

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
        public Promise<TC, TW, TR, TE> DeserializeWorkload(string json)
        {
            Workload = JsonConvert.DeserializeObject<TW>(json, _jsonSerializerSettings);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBlocked { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsTerminated { get; private set; }

       
        /// <summary>
        /// Serializes the workload of this promise to Json
        /// </summary>
        /// <returns>a string containing the workload serialized to Json</returns>
        public string SerializeWorkload()
        {
            return JsonConvert.SerializeObject(Workload, _jsonSerializerSettings);
        }

        /// <summary>
        /// Deserializes the request of this promise from Json
        /// </summary>
        /// <param name="json">workload of the promise in Json</param>
        public Promise<TC, TW, TR, TE> DeserializeRequest(string json)
        {
            Request = JsonConvert.DeserializeObject<TR>(json, _jsonSerializerSettings);
            return this;
        }

        /// <summary>
        /// Serializes the request of this promise to Json
        /// </summary>
        /// <returns>a string containing the workload serialized to Json</returns>
        public string SerializeRequest()
        {
            return JsonConvert.SerializeObject(Request, _jsonSerializerSettings);
        }

        /// <summary>
        /// Deserializes the config of this promise from Json
        /// </summary>
        /// <param name="json">workload of the promise in Json</param>
        public Promise<TC, TW, TR, TE> DeserializeConfig(string json)
        {
            Config = JsonConvert.DeserializeObject<TC>(json, _jsonSerializerSettings);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Promise<TC, TW, TR, TE> DeserializeResponse(string json)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Serializes the config of this promise to Json
        /// </summary>
        /// <returns>a string containing the workload serialized to Json</returns>
        public string SerializeConfig()
        {
            return JsonConvert.SerializeObject(Config, _jsonSerializerSettings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SerializeResponse()
        {
            return JsonConvert.SerializeObject(Response, _jsonSerializerSettings);
        }

        /// <summary>
        /// The Id for the promise.  Gets sent to the requestId of the workload if one isn't already provided.
        /// </summary>
        public string PromiseId { get; }

		/// <summary>
		/// The application groupId that the promise will run under -- used when passing events across applications.
		/// </summary>
		public int ApplicationGroupId { get; set; }

        /// <summary>
        /// Exposes the promise's configuration
        /// </summary>
        private TC Config { get; set; }

        /// <summary>
        /// Exposes the promise's workload
        /// </summary>
        private TW Workload { get; set; }

        /// <summary>
        /// Exposes the promise's request.
        /// </summary>
        private TR Request { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private TE Response { get; }

        /// <summary>
        /// the number of authChallenger actions established on this promise
        /// </summary>
        public int AuthChallengersCount => _context.AuthChallengers.Count;

	    /// <summary>
        /// the number of validator actions established on this promise
        /// </summary>
        public int ValidatorsCount => _context.Validators.Count;

		/// <summary>
        /// the number of executor actions established on this promise
        /// </summary>
        public int ExecutorsCount => _context.Executors.Count;

	    /// <summary>
        /// Orders a promise to execute ('run') its validator, authChallenger, and exector tasks asynchronously
        /// </summary>
        /// <returns>the async task that returns an instance of this promise object when it completes</returns>
	    // ReSharper disable once UnusedMember.Global
        public Task<Promise<TC, TW, TR, TE>> RunAsync(int applicationGroupId)
        {
            return Task.Run(() => Run(applicationGroupId));
        }

        /// <summary>
        /// Orders a promise to execute ('run') its validator, authChallenger, and exector tasks synchronously
        /// </summary>
        /// <returns>the instance of this promise object</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public Promise<TC, TW, TR, TE> Run(int applicationGroupId)
        {
            try
            {
	            ApplicationGroupId = applicationGroupId;

                Trace(PromiseMessages.PromiseStarted);

                _context.PreStartActions.Invoke(this, Config, Workload, Request, Response);
                _context.AuthChallengers.Invoke(this, Config, Workload, Request, Response);
                _context.Validators.Invoke(this, Config, Workload, Request, Response);
                _context.Executors.Invoke(this, Config, Workload, Request, Response);
                _context.XferActions.Invoke(this, Config, Workload, Request, Response);
                _context.SuccessHandlers.Invoke(this, PromiseMessages.PromiseSuccess);
                _context.PostEndActions.Invoke(this, Config, Workload, Request, Response);

                Trace(PromiseMessages.PromiseSuccess);

            }
            catch (Exception ex)
            {
                Error(new GenericEventMessage(ApplicationGroupId, ex));
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
            IsTerminated = true;
            IsBlocked = true;

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
            IsTerminated = true;
            _context.ErrorHandlers.Invoke(this, message);
        }

        /// <summary>
        /// Submits a message to the 'fatal' instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Fatal(IHandleEventMessage message)
        {
            IsTerminated = true;
            _context.FatalHandlers.Invoke(this, message);
        }

        /// <summary>
        /// Submits a message to the 'abort' instrumentation handler.
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Abort(IHandleEventMessage message)
        {
            IsTerminated = true;
            _context.AbortHandlers.Invoke(this, message);
        }

        /// <summary>
        /// Submits a message to the 'abortOnAccessDenied' instrumentation handler.
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void AbortOnAccessDenied(IHandleEventMessage message)
        {
            IsTerminated = true;
            _context.AbortOnAccessDeniedHandlers.Invoke(this, message);
        }

        /// <summary>
        /// Submits a message to the 'block' instrumentation handler.
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="ex"></param>
        public void Block(Exception ex)
        {
            Block(new GenericEventMessage(ApplicationGroupId, ex));
        }

        /// <summary>
        /// Submits an exception to the 'trace' instrumentation handler
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Trace(Exception ex)
        {
            Trace(new GenericEventMessage(ApplicationGroupId, ex));
        }

        /// <summary>
        /// Submits an exception to the 'debug' instrumentation handler
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Debug(Exception ex)
        {
            Debug(new GenericEventMessage(ApplicationGroupId, ex));
        }

        /// <summary>
        /// Submits an exception to the 'info' instrumentation handler
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Info(Exception ex)
        {
            Info(new GenericEventMessage(ApplicationGroupId, ex));
        }

        /// <summary>
        /// Submits an exception to the 'warn' instrumentation handler
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Warn(Exception ex)
        {
            Warn(new GenericEventMessage(ApplicationGroupId, ex));
        }

        /// <summary>
        /// Submits an exception to the 'error' instrumentation handler
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Error(Exception ex)
        {
            Error(new GenericEventMessage(ApplicationGroupId, ex));
        }

        /// <summary>
        /// Submits an exception to the 'fatal' instrumentation handler
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Fatal(Exception ex)
        {
            Fatal(new GenericEventMessage(ApplicationGroupId, ex));
        }

        /// <summary>
        /// Submits an exception to the 'abort' instrumentation handler
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void Abort(Exception ex)
        {
            Abort(new GenericEventMessage(ApplicationGroupId, ex));
        }

        /// <summary>
        /// Submits an exception to the 'abortOnAccessDenied' instrumentation handler
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="ex">an exception object</param>
        public void AbortOnAccessDenied(Exception ex)
        {
            AbortOnAccessDenied(new GenericEventMessage(ApplicationGroupId, ex));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Promise<TC, TW, TR, TE> WithPreStart(string actionId, Action<IHandlePromiseActions, TC, TW, TR, TE> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.PreStartActions.Enqueue(new WorkloadHandler<TC, TW, TR, TE>
            {
                Action = action,
                EndMessage = PromiseMessages.PreStartActionStopped(ApplicationGroupId, actionId),
                StartMessage = PromiseMessages.PreStartActionStarted(ApplicationGroupId, actionId),
                HandlerName = actionId
            });

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public Promise<TC, TW, TR, TE> WithPostEnd(string actionId, Action<IHandlePromiseActions, TC, TW, TR, TE> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.PostEndActions.Enqueue(new WorkloadHandler<TC, TW, TR, TE>
            {
                Action = action,
                EndMessage = PromiseMessages.PostEndActionStopped(ApplicationGroupId, actionId),
                StartMessage = PromiseMessages.PostEndActionStarted(ApplicationGroupId, actionId),
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithValidator(string actionId, Action<IHandlePromiseActions, TC, TW, TR, TE> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.Validators.Enqueue(new WorkloadHandler<TC, TW, TR, TE>
            {
                Action = action,
                EndMessage = PromiseMessages.ValidatorStopped(ApplicationGroupId, actionId),
                StartMessage = PromiseMessages.ValidatorStarted(ApplicationGroupId, actionId),
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithAuthChallenger(string actionId, Action<IHandlePromiseActions, TC, TW, TR, TE> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.AuthChallengers.Enqueue(new WorkloadHandler<TC, TW, TR, TE>
            {
                Action = action,
                EndMessage = PromiseMessages.AuthChallengerStopped(ApplicationGroupId, actionId),
                StartMessage = PromiseMessages.AuthChallengerStarted(ApplicationGroupId, actionId),
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithExecutor(string actionId, Action<IHandlePromiseActions, TC, TW, TR, TE> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.Executors.Enqueue(new WorkloadHandler<TC, TW, TR, TE>
            {
                Action = action,
                EndMessage = PromiseMessages.ExecutorStopped(ApplicationGroupId, actionId),
                StartMessage = PromiseMessages.ExecutorStarted(ApplicationGroupId, actionId),
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithXferAction(string actionId, Action<IHandlePromiseActions, TC, TW, TR, TE> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.XferActions.Enqueue(new WorkloadHandler<TC, TW, TR, TE>
            {
                Action = action,
                EndMessage = PromiseMessages.XferActionStopped(ApplicationGroupId, actionId),
                StartMessage = PromiseMessages.XferActionStarted(ApplicationGroupId, actionId),
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithBlockHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.BlockHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithTraceHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.TraceHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithDebugHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.DebugHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithInfoHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.InfoHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithWarnHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.WarnHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithErrorHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.ErrorHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithFatalHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.FatalHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithAbortHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.AbortHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithAbortOnAccessDeniedHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.AbortOnAccessDeniedHandlers.Enqueue(new PromiseHandler<IHandlePromiseActions>
            {
                Action = action,
                HandlerName = actionId
            });

            return this;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="action"></param>
		/// <returns></returns>
        public Promise<TC, TW, TR, TE> WithSuccessHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action)
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