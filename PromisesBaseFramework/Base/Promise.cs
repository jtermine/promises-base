﻿using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Termine.Promises.Base.Generics;
using Termine.Promises.Base.Handlers;
using Termine.Promises.Base.Interfaces;
using Termine.Promises.Helpers;

namespace Termine.Promises.Base
{
	/// <summary>
	/// The basic promise model
	/// </summary>
	/// <typeparam name="TW">a promise workload</typeparam>
	/// <typeparam name="TC">a promise configuration object</typeparam>
	/// <typeparam name="TR">a promise request</typeparam>
	/// <typeparam name="TE">a promise response object</typeparam>
	public sealed class Promise<TC, TW, TR, TE> : IHandlePromiseActions, IHandlePromiseEvents<TC, TW, TR, TE>, IDisposable
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
	        Init();
        }

	    public Promise(bool throwExceptions)
	    {
	        Init();
	        _context.ThrowExceptions = throwExceptions;
	    }

	    private void Init()
		{
            Config = new TC();
            Request = new TR();
            Workload = new TW();
            Response = new TE();

            foreach (var configurator in PromiseConfigurator<TC, TW, TR,TE>.Instance.Configurators)
			{
				configurator.Configure(this);
			}

	        WithValidator("validateRequest", (p, c, w, rq, rx) =>
	        {
	            var result = rq.GetValidator().Validate(rq);

	            if (result.IsValid) return;

	            var errors = result.Errors.Select(f => $"[{f.PropertyName}|>|{f.AttemptedValue}|->|{f.ErrorMessage}]").ToArray();

                var errorString = new StringBuilder();

	            foreach (var error in errors)
	            {
	                errorString.Append(error);
	            }

                p.Abort($"Validation errors: {errorString.ToString()}");
                
	        });
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

		    public bool ThrowExceptions { get; set; }
             
            public CancellationTokenSource TokenSource { get; private set; } =  new CancellationTokenSource();

            /// <summary>
            /// a collection of actions that execute in sequence before a promise starts
            /// </summary>
            public readonly WorkloadHandlerQueue<TC, TW, TR, TE> WorkloadCtors = new WorkloadHandlerQueue<TC, TW, TR, TE>();

			/// <summary>
			/// a collection of AuthChallengers -- these are executed in sequence to determine whether the promise has the authority to run
			/// </summary>
			public readonly WorkloadHandlerQueue<TC, TW, TR, TE> AuthChallengers = new WorkloadHandlerQueue<TC, TW, TR, TE>();

			/// <summary>
			/// a collection of Validators -- these are executed in sequenced to determine whether the promise workload contains valid information
			/// </summary>
			public readonly WorkloadHandlerQueue<TC, TW, TR, TE> Validators = new WorkloadHandlerQueue<TC, TW, TR, TE>();

			/// <summary>
			/// a collection of executors -- these are executed in sequence to perform the action(s) that the promise makes
			/// </summary>
			public readonly WorkloadHandlerQueue<TC, TW, TR, TE> Executors = new WorkloadHandlerQueue<TC, TW, TR, TE>();

			/// <summary>
			/// a collection of actions that execute in sequence before a promise starts
			/// </summary>
			public readonly WorkloadHandlerQueue<TC, TW, TR, TE> PreStartActions = new WorkloadHandlerQueue<TC, TW, TR, TE>();

			/// <summary>
			/// a collection of actions that execute in sequence after a promise ends
			/// </summary>
			public readonly WorkloadHandlerQueue<TC, TW, TR, TE> PostEndActions = new WorkloadHandlerQueue<TC, TW, TR, TE>();

			/// <summary>
			/// a collection of actions that execute in sequence when transmitting a promise to a service
			/// </summary>
			public readonly WorkloadXferHandlerQueue<TC, TW, TR, TE> XferActions = new WorkloadXferHandlerQueue<TC, TW, TR, TE>();

            /// <summary>
			/// a collection of actions that execute Sql against a database server using Dapper
			/// </summary>
			public readonly WorkloadSqlHandlerQueue<TC, TW, TR, TE> SqlActions = new WorkloadSqlHandlerQueue<TC, TW, TR, TE>();

            /// <summary>
            /// a collection of block handlers -- there are executed when a promise is blocked
            /// </summary>
            public PromiseHandlerQueue<TC, TW, TR, TE> BlockHandlers { get; } = new PromiseHandlerQueue<TC, TW, TR, TE>();
            
			/// <summary>
			/// a collection of trace handlers -- these are executed when a promise is tracing
			/// </summary>
			public PromiseHandlerQueue<TC, TW, TR, TE> TraceHandlers { get; } = new PromiseHandlerQueue<TC, TW, TR, TE>();

			/// <summary>
			/// a collection of debug handlers -- these are executed when a promise is reporting a debug event
			/// </summary>
			public PromiseHandlerQueue<TC, TW, TR, TE> DebugHandlers { get; } = new PromiseHandlerQueue<TC, TW, TR, TE>();

			/// <summary>
			/// a collection of info handlers -- these are executed when a promise is reporting an info event (e.g. system event log)
			/// </summary>
			public PromiseHandlerQueue<TC, TW, TR, TE> InfoHandlers { get; } = new PromiseHandlerQueue<TC, TW, TR, TE>();
            
            /// <summary>
            /// a collection of warn
            ///  handlers -- these are executed when a promise is tracing
            /// </summary>
            public PromiseHandlerQueue<TC, TW, TR, TE> WarnHandlers { get; } = new PromiseHandlerQueue<TC, TW, TR, TE>();

			/// <summary>
			/// a collection of error handlers -- these are executed when a promise is reporting an error event (e.g. system event log)
			/// </summary>
			public PromiseHandlerQueue<TC, TW, TR, TE> ErrorHandlers { get; } = new PromiseHandlerQueue<TC, TW, TR, TE>();

			/// <summary>
			/// a collection of error handlers -- these are executed when a promise is reporting a fatal event (e.g. system event log)
			/// </summary>
			public PromiseHandlerQueue<TC, TW, TR, TE> FatalHandlers { get; } = new PromiseHandlerQueue<TC, TW, TR, TE>();

			/// <summary>
			/// a collection of abort handlers -- these are executed when a promise is aborting for reasons other than AccessDenied
			/// </summary>
			public PromiseHandlerQueue<TC, TW, TR, TE> AbortHandlers { get; } = new PromiseHandlerQueue<TC, TW, TR, TE>();

			/// <summary>
			/// a collection of abort handlers -- these are executed when a promise is aborting because of AccessDenied condition
			/// </summary>
			public PromiseHandlerQueue<TC, TW, TR, TE> AbortOnAccessDeniedHandlers { get; } = new PromiseHandlerQueue<TC, TW, TR, TE>();

			/// <summary>
			/// a collection of handlers that fire when a promise executed successfully
			/// </summary>
			public PromiseHandlerQueue<TC, TW, TR, TE> SuccessHandlers { get; } = new PromiseHandlerQueue<TC, TW, TR, TE>();

            public void ResetCancellationToken()
		    {
                TokenSource = new CancellationTokenSource();
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

	    public CancellationToken CancellationToken => _context.TokenSource.Token;
	    public HttpStatusCode ReturnHttpStatusCode { get; private set; } = HttpStatusCode.OK;
	    public string ReturnHttpMessage { get; private set; } = "OK";

	    /// <summary>
        /// Reports whether the promise has been blocked from executing
        /// </summary>
        public bool IsBlocked { get; private set; }
        
		/// <summary>
        /// Reports whether the promise has been terminated
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

	    public void DeserializeResponse(string json)
	    {
	        if (string.IsNullOrEmpty(json)) Warn("Attempting to deserialize a response object by providing a null or empty JSON string.  This will be skipped.");

	        Response = JsonConvert.DeserializeObject<TE>(json);
	    }

	    /// <summary>
	    /// The Id for the promise.  Gets sent to the requestId of the workload if one isn't already provided.
	    /// </summary>
	    public string PromiseId { get; } = Guid.NewGuid().ToString("N");

        public string PromiseName => $"{PromiseConfigurator<TC, TW, TR,TE>.PxConfigSection.PxApplicationGroup.Name}.{Request.RequestName}";
		public string LoggerName => $"{PromiseName}.{PromiseId}";

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
        public TE Response { get; private set; }

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
        public Task<Promise<TC, TW, TR, TE>> RunAsync(TR request = default(TR))
        {
            return Task.Run(() => Run(request), CancellationToken);
        }

        /// <summary>
        /// Orders a promise to execute ('run') its validator, authChallenger, and exector tasks synchronously
        /// </summary>
        /// <returns>the instance of this promise object</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public Promise<TC, TW, TR, TE> Run(TR request = default(TR))
        {
            if (request != default(TR)) Request = request;
            Execute();
            return this;
        }

	    private void Execute()
	    {

            _context.ResetCancellationToken();

            IsTerminated = false;
            IsBlocked = false;

            try
            {
                _context.WorkloadCtors.Invoke(this, Config, Workload, Request, Response);
                _context.PreStartActions.Invoke(this, Config, Workload, Request, Response);
            }
            catch (Exception ex)
            {
                Error(new GenericEventMessage(ex));
                if (_context.ThrowExceptions) throw;
            }

            try
	        {
	            Debug(PromiseMessages.PromiseStarted);
	            
	            _context.AuthChallengers.Invoke(this, Config, Workload, Request, Response);
	            _context.Validators.Invoke(this, Config, Workload, Request, Response);
                _context.XferActions.Invoke(this, Config, Workload, Request, Response);
                _context.SqlActions.Invoke(this, Config, Workload, Request, Response);
                _context.Executors.Invoke(this, Config, Workload, Request, Response);
                _context.SuccessHandlers.Invoke(PromiseMessages.PromiseSuccess, this, Config, Workload, Request, Response);

                Debug(PromiseMessages.PromiseSuccess);
	        }
	        catch (OperationCanceledException)
	        {
	            Info(PromiseMessages.PromiseAborted);
                
            }
            catch (Exception ex)
            {
                Error(new GenericEventMessage(ex));
                Debug(PromiseMessages.PromiseFail);
                if (_context.ThrowExceptions) throw;
            }

	        try
	        {
                _context.PostEndActions.Invoke(this, Config, Workload, Request, Response, true);
            }
	        catch (Exception ex)
	        {
                Error(new GenericEventMessage(ex));
                if (_context.ThrowExceptions) throw;
            }

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
            ReturnHttpStatusCode = HttpStatusCode.Forbidden;
            _context.BlockHandlers.Invoke(message, this, Config, Workload, Request, Response);
        }

        /// <summary>
        /// Submits a message to the trace instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Trace(IHandleEventMessage message)
        {
            _context.TraceHandlers.Invoke(message, this, Config, Workload, Request, Response);
        }

        /// <summary>
        /// Submits a message to the debug instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Debug(IHandleEventMessage message)
        {
            _context.DebugHandlers.Invoke(message, this, Config, Workload, Request, Response);
        }

        /// <summary>
        /// Submits a message to the info instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Info(IHandleEventMessage message)
        {
            _context.InfoHandlers.Invoke(message, this, Config, Workload, Request, Response);
        }

        /// <summary>
        /// Submits a message to the 'warn' instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Warn(IHandleEventMessage message)
        {

            _context.WarnHandlers.Invoke(message, this, Config, Workload, Request, Response);
        }

        /// <summary>
        /// Submits a message to the 'error' instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Error(IHandleEventMessage message)
        {
            IsTerminated = true;
            ReturnHttpStatusCode = HttpStatusCode.InternalServerError;
            ReturnHttpMessage = message.IsSensitiveMessage
                ? $"An error that contains sensitive diagnostic information has occurred > {LoggerName}"
                : $"{message.EventPublicMessage} > {LoggerName}";
            _context.ErrorHandlers.Invoke(message, this, Config, Workload, Request, Response);
        }

        /// <summary>
        /// Submits a message to the 'fatal' instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Fatal(IHandleEventMessage message)
        {
            IsTerminated = true;
            ReturnHttpStatusCode = HttpStatusCode.InternalServerError;
            ReturnHttpMessage = message.IsSensitiveMessage
                ? $"An error that contains sensitive diagnostic information has occurred > {LoggerName}"
                : $"{message.EventPublicMessage} > {LoggerName}";
            _context.FatalHandlers.Invoke(message, this, Config, Workload, Request, Response);
        }

        /// <summary>
        /// Submits a message to the 'abort' instrumentation handler.
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Abort(IHandleEventMessage message)
        {
            IsTerminated = true;
            ReturnHttpStatusCode = HttpStatusCode.InternalServerError;
            ReturnHttpMessage = message.IsSensitiveMessage
                ? $"An error that contains sensitive diagnostic information has occurred > {LoggerName}"
                : $"{message.EventPublicMessage} > {LoggerName}";
            _context.AbortHandlers.Invoke(message, this, Config, Workload, Request, Response);
            _context.TokenSource.Cancel();
        }

        /// <summary>
        /// Submits a message to the 'abortOnAccessDenied' instrumentation handler.
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void AbortOnAccessDenied(IHandleEventMessage message)
        {
            IsTerminated = true;
            ReturnHttpStatusCode = HttpStatusCode.Unauthorized;
            ReturnHttpMessage = message.IsSensitiveMessage
                ? $"An error that contains sensitive diagnostic information has occurred > {LoggerName}"
                : $"{message.EventPublicMessage} > {LoggerName}";
            _context.AbortOnAccessDeniedHandlers.Invoke(message, this, Config, Workload, Request, Response);
        }

	    public void Stop()
	    {
	        IsTerminated = true;
	        IsBlocked = true;
	    }
        
        /// <summary>
        /// Submits a message to the 'block' instrumentation handler.
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="ex"></param>
        public void Block(Exception ex)
        {
            Block(new GenericEventMessage(ex));
            _context.TokenSource.Cancel();
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

	    public void Block(string message)
	    {
	        Block(new GenericEventMessage(message));
	    }

	    public void Trace(string message)
	    {
            Trace(new GenericEventMessage(message));
        }

	    public void Debug(string message)
	    {
            Debug(new GenericEventMessage(message));
        }

	    public void Info(string message)
	    {
            Info(new GenericEventMessage(message));
        }

	    public void Warn(string message)
	    {
            Warn(new GenericEventMessage(message));
        }

	    public void Error(string message)
	    {
            Error(new GenericEventMessage(message));
        }

	    public void Fatal(string message)
	    {
            Fatal(new GenericEventMessage(message));
        }

	    public void Abort(string message)
	    {
            Abort(new GenericEventMessage(message));
        }

	    public void AbortOnAccessDenied(string message)
	    {
            AbortOnAccessDenied(new GenericEventMessage(message));
        }

	    public void Stop(string message)
	    {
            Trace(message);
            Stop();
        }

	    public void ThrowChaos()
	    {
	        Functions.CreateChaosInProperties(Config);
            Functions.CreateChaosInProperties(Workload);
            Functions.CreateChaosInProperties(Request);
            Functions.CreateChaosInProperties(Response);
        }

	    public void ThrowChaosOnConfig()
	    {
            Functions.CreateChaosInProperties(Config);
        }

	    public void ThrowChaosOnWorkload()
	    {
            Functions.CreateChaosInProperties(Workload);
        }

	    public void ThrowChaosOnRequest()
	    {
            Functions.CreateChaosInProperties(Request);
        }

	    public void ThrowChaosOnResponse()
	    {
            Functions.CreateChaosInProperties(Response);
        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public Promise<TC, TW, TR, TE> WithWorkloadCtor(string actionId, Action<IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
		{
			if (string.IsNullOrEmpty(actionId) || action == null) return this;

			_context.WorkloadCtors.Enqueue(new WorkloadHandler<TC, TW, TR, TE>
			{
				Action = action,
				EndMessage = PromiseMessages.PreStartActionStopped(actionId),
				StartMessage = PromiseMessages.PreStartActionStarted(actionId),
				HandlerName = actionId,
                Control = control
            });

			return this;
		}

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public Promise<TC, TW, TR, TE> WithPreStart(string actionId, Action<IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.PreStartActions.Enqueue(new WorkloadHandler<TC, TW, TR, TE>
            {
                Action = action,
                EndMessage = PromiseMessages.PreStartActionStopped(actionId),
                StartMessage = PromiseMessages.PreStartActionStarted(actionId),
                HandlerName = actionId,
                Control = control
            });

            return this;
        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public Promise<TC, TW, TR, TE> WithPostEnd(string actionId, Action<IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.PostEndActions.Enqueue(new WorkloadHandler<TC, TW, TR, TE>
            {
                Action = action,
                EndMessage = PromiseMessages.PostEndActionStopped(actionId),
                StartMessage = PromiseMessages.PostEndActionStarted(actionId),
                HandlerName = actionId,
                Control = control
            });

            return this;
        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public Promise<TC, TW, TR, TE> WithValidator(string actionId, Action<IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.Validators.Enqueue(new WorkloadHandler<TC, TW, TR, TE>
            {
                Action = action,
                EndMessage = PromiseMessages.ValidatorStopped(actionId),
                StartMessage = PromiseMessages.ValidatorStarted(actionId),
                HandlerName = actionId,
                Control = control
            });

            return this;
        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public Promise<TC, TW, TR, TE> WithAuthChallenger(string actionId, Action<IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.AuthChallengers.Enqueue(new WorkloadHandler<TC, TW, TR, TE>
            {
                Action = action,
                EndMessage = PromiseMessages.AuthChallengerStopped(actionId),
                StartMessage = PromiseMessages.AuthChallengerStarted(actionId),
                HandlerName = actionId
            });

            return this;
        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public Promise<TC, TW, TR, TE> WithExecutor(string actionId, Action<IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control) )
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            _context.Executors.Enqueue(new WorkloadHandler<TC, TW, TR, TE>
            {
                Action = action,
                EndMessage = PromiseMessages.ExecutorStopped(actionId),
                StartMessage = PromiseMessages.ExecutorStarted(actionId),
                HandlerName = actionId,
                Control = control
            });

            return this;
        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="configurator"></param>
	    /// <param name="action"></param>
	    /// <returns></returns>
	    public Promise<TC, TW, TR, TE> WithXferAction(string actionId, Action<WorkloadXferHandlerConfig, IHandlePromiseActions, TC, TW, TR, TE> configurator, Action<WorkloadXferHandlerConfig, IHandlePromiseActions, TC, TW, TR, TE> action = default(Action<WorkloadXferHandlerConfig,IHandlePromiseActions, TC, TW, TR, TE>))
        {
            if (string.IsNullOrEmpty(actionId) || action == configurator) return this;

	        var workloadXferHandler = new WorkloadXferHandler<TC, TW, TR, TE>
	        {
	            Configurator = configurator,
	            EndMessage = PromiseMessages.XferActionStopped(actionId),
	            StartMessage = PromiseMessages.XferActionStarted(actionId),
	            HandlerName = actionId
	        };

	        if (action != null) workloadXferHandler.Action = action;

            _context.XferActions.Enqueue(workloadXferHandler);

            return this;
        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="sqlAction"></param>
	    /// <param name="configFunc"></param>
	    /// <returns></returns>
	    public Promise<TC, TW, TR, TE> WithSqlAction(string actionId, 
            WorkloadSqlHandlerConfigDelegate<IHandlePromiseActions, TC, TW, TR, TE> configFunc,
            WorkloadSqlActionResultsDelegate sqlAction)
	    {
	        if (string.IsNullOrEmpty(actionId) ||
	            configFunc == default(WorkloadSqlHandlerConfigDelegate<IHandlePromiseActions, TC, TW, TR, TE>))
	            return this;

	        var workloadSqlHandlerConfig = configFunc.Invoke(this, Config, Workload, Request, Response);

	        var workloadSqlHandler = new WorkloadSqlHandler<TC, TW, TR, TE>
	        {
	            WorkloadSqlHandlerConfig = workloadSqlHandlerConfig,
	            EndMessage = PromiseMessages.SqlActionStopped(actionId),
	            StartMessage = PromiseMessages.SqlActionStarted(actionId),
	            HandlerName = actionId,
	            SqlAction = sqlAction
	        };
            
	        _context.SqlActions.Enqueue(workloadSqlHandler);

	        return this;
	    }

	    public void WithUserMessageHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = null)
	    {
	        WithBlockHandler($"{actionId}.block", action, control);
            WithInfoHandler($"{actionId}.info", action, control);
            WithWarnHandler($"{actionId}.warn", action, control);
            WithErrorHandler($"{actionId}.error", action, control);
            WithFatalHandler($"{actionId}.fatal", action, control);
            WithAbortOnAccessDeniedHandler($"{actionId}.accessDenied", action, control);
            WithAbortHandler($"{actionId}.abort", action, control);
            WithSuccessHandler($"{actionId}.success", action, control);
        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public void WithBlockHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return ;

            _context.BlockHandlers.Enqueue(new PromiseHandler<TC, TW, TR, TE>
            {
                Action = action,
                HandlerName = actionId
            });

        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public void WithTraceHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return ;

            _context.TraceHandlers.Enqueue(new PromiseHandler<TC, TW, TR, TE>
            {
                Action = action,
                HandlerName = actionId
            });

        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public void WithDebugHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return;

            _context.DebugHandlers.Enqueue(new PromiseHandler<TC, TW, TR, TE>
            {
                Action = action,
                HandlerName = actionId
            });

        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public void WithInfoHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return;

            _context.InfoHandlers.Enqueue(new PromiseHandler<TC, TW, TR, TE>
            {
                Action = action,
                HandlerName = actionId
            });

        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public void WithWarnHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return;

            _context.WarnHandlers.Enqueue(new PromiseHandler<TC, TW, TR, TE>
            {
                Action = action,
                HandlerName = actionId,
                Control = control
            });

        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public void WithErrorHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return ;

            _context.ErrorHandlers.Enqueue(new PromiseHandler<TC, TW, TR, TE>
            {
                Action = action,
                HandlerName = actionId,
                Control = control
            });

        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public void WithFatalHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return;

            _context.FatalHandlers.Enqueue(new PromiseHandler<TC, TW, TR, TE>
            {
                Action = action,
                HandlerName = actionId,
                Control = control
            });

        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public void WithAbortHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return;

            _context.AbortHandlers.Enqueue(new PromiseHandler<TC, TW, TR, TE>
            {
                Action = action,
                HandlerName = actionId,
                Control = control
            });

        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public void WithAbortOnAccessDeniedHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return;

            _context.AbortOnAccessDeniedHandlers.Enqueue(new PromiseHandler<TC, TW, TR, TE>
            {
                Action = action,
                HandlerName = actionId,
                Control = control
            });

        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="actionId"></param>
	    /// <param name="action"></param>
	    /// <param name="control"></param>
	    /// <returns></returns>
	    public void WithSuccessHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control))
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return;

            _context.SuccessHandlers.Enqueue(new PromiseHandler<TC, TW, TR, TE>
            {
                Action = action,
                HandlerName = actionId,
                Control = control
            });

        }

	    public void Dispose()
	    {
	        if (!IsTerminated) Abort(new OperationCanceledException("Promise disposed", _context.TokenSource.Token));
	    }
    }
}