using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NClone;
using Termine.Promises.Generics;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    /// <summary>
    /// The basic promise model
    /// </summary>
    /// <typeparam name="TW">a promise workload</typeparam>
    public class Promise<TW> : IAmAPromise<TW>
        where TW : class, IAmAPromiseWorkload, new()
    {
        /// <summary>
        /// When a promise initialized, the PromiseId is set to a Guid
        /// </summary>
        public Promise()
        {
            PromiseId = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// The promise context stores instances of the actions (i.e. auth challengers, validators, executors) that a promise supports
        /// </summary>
        public class PromiseContext
        {
            /// <summary>
            /// this dictionary of generic objects travels with the promise context and can be accessed by any object that inherits the promise
            /// </summary>
            public Dictionary<string, object> Objects = new Dictionary<string, object>();

            /// <summary>
            /// this dictionary of AuthChallengers -- these are executed in sequence to determine whether the promise has the authority to run
            /// </summary>
            public readonly Dictionary<string, Action<TW>> AuthChallengers = new Dictionary<string, Action<TW>>();
            
            /// <summary>
            /// a dictionary of Validators -- these are executed in sequenced to determine whether the promise workload contains valid information
            /// </summary>
            public readonly Dictionary<string, Action<TW>> Validators = new Dictionary<string, Action<TW>>();

            /// <summary>
            /// a dictionary of executors -- these are executed in sequence to perform the action(s) that the promise makes
            /// </summary>
            public readonly Dictionary<string, Action<TW>> Executors = new Dictionary<string, Action<TW>>();

            /// <summary>
            /// a dictionary of actions that execute in sequence before a promise starts
            /// </summary>
            public readonly Dictionary<string, Action<TW>> PreStartActions = new Dictionary<string, Action<TW>>();

            /// <summary>
            /// a dictionary of actions that execute in sequence after a promise ends
            /// </summary>
            public readonly Dictionary<string, Action<TW>> PostEndActions = new Dictionary<string, Action<TW>>();

            /// <summary>
            /// a dictionary of actions that execute in sequence when transmitting a promise to a services
            /// </summary>
            public readonly Dictionary<string, Action<TW>> XferActions = new Dictionary<string, Action<TW>>();

            /// <summary>
            /// a dictionary of block handlers -- there are executed when a promise is blocked
            /// </summary>
            public Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>> BlockHandlers { get; private set; }
            
            /// <summary>
            /// a dictionary of trace handlers -- these are executed whe na promise is tracing
            /// </summary>
            public Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>> TraceHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>> DebugHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>> InfoHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>> WarnHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>> ErrorHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>> FatalHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>> AbortHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>> AbortOnAccessDeniedHandlers { get; private set; }
            
            /// <summary>
            /// 
            /// </summary>
            public Dictionary<string, Action<IAmAPromise<TW>>> SuccessHandlers { get; private set; }

            /// <summary>
            /// when a promise context is initialized, each of the dictionaries it contains are also initialized to make it easy to add the events to the promise
            /// </summary>
            public PromiseContext()
            {
                BlockHandlers = new Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>>();
                TraceHandlers = new Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>>();
                DebugHandlers = new Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>>();
                InfoHandlers = new Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>>();
                WarnHandlers = new Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>>();
                ErrorHandlers = new Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>>();
                FatalHandlers = new Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>>();
                AbortHandlers = new Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>>();
                AbortOnAccessDeniedHandlers = new Dictionary<string, Action<IAmAPromise<TW>, IHandleEventMessage>>();
                SuccessHandlers = new Dictionary<string, Action<IAmAPromise<TW>>>();
            }
        }

        private readonly PromiseContext _context = new PromiseContext();
        private TW _workload = new TW();

        public virtual void Init()
        {
        }

        /// <summary>
        /// The Id for the promise.  Gets sent to the requestId of the workload if one isn't already provided.
        /// </summary>
        public string PromiseId { get; private set; }

        /// <summary>
        /// returns a readonly reference to the promise context
        /// </summary>
        public PromiseContext Context
        {
            get { return _context; }
        }

        /// <summary>
        /// Exposes the promise's workload
        /// </summary>
        public TW Workload
        {
            get { return _workload; }
        }

        /// <summary>
        /// Injects a workload obtained from a serializable source into this promise and resets the promiseId accordingly
        /// </summary>
        /// <param name="workload">the workload obtained from the serializable source</param>
        public void WithWorkload(TW workload)
        {
            _workload = Clone.ObjectGraph(workload);
            PromiseId = workload.RequestId;
        }

        /// <summary>
        /// the number of authChallenger actions established on this promise
        /// </summary>
        public int AuthChallengersCount
        {
            get { return Context.AuthChallengers.Count; }
        }

        /// <summary>
        /// the number of validator actions established on this promise
        /// </summary>
        public int ValidatorsCount
        {
            get { return Context.Validators.Count; }
        }

        /// <summary>
        /// the number of executor actions established on this promise
        /// </summary>
        public int ExecutorsCount
        {
            get { return Context.Executors.Count; }
        }

        /// <summary>
        /// Orders a promise to execute ('run') its validator, authChallenger, and exector tasks asynchronously
        /// </summary>
        /// <returns>the async task that returns an instance of this promise object when it completes</returns>
        public Task<IAmAPromise<TW>> RunAsync()
        {
            return Task.Run(() => Run());
        }

        /// <summary>
        /// Orders a promise to execute ('run') its validator, authChallenger, and exector tasks synchronously
        /// </summary>
        /// <returns>the instance of this promise object</returns>
        public IAmAPromise<TW> Run()
        {
            try
            {
                Workload.WithRequestId(PromiseId);

                Init();

                Trace(PromiseMessages.PromiseStarted);

                foreach (var preStartAction in Context.PreStartActions)
                {
                    Trace(PromiseMessages.PreStartActionStarted(preStartAction.Key));
                    preStartAction.Value.Invoke(_workload);
                    Trace(PromiseMessages.PreStartActionStopped(preStartAction.Key));
                }

                foreach (var challenger in Context.AuthChallengers)
                {
                    Trace(PromiseMessages.AuthChallengerStarted(challenger.Key));
                    challenger.Value.Invoke(_workload);
                    Trace(PromiseMessages.AuthChallengerStopped(challenger.Key));
                    if (_workload.IsTerminated) return PostEnd();
                }

                foreach (var validator in Context.Validators)
                {
                    Trace(PromiseMessages.ValidatorStarted(validator.Key));
                    validator.Value.Invoke(_workload);
                    Trace(PromiseMessages.ValidatorStopped(validator.Key));
                    if (_workload.IsTerminated) return PostEnd();
                }

                if (_workload.IsBlocked) return PostEnd();

                foreach (var executor in Context.Executors)
                {
                    Trace(PromiseMessages.ExecutorStarted(executor.Key));
                    executor.Value.Invoke(_workload);
                    Trace(PromiseMessages.ExecutorStopped(executor.Key));
                    if (_workload.IsTerminated) return PostEnd();
                }

                if (_workload.IsBlocked) return PostEnd();

                foreach (var xferAction in Context.XferActions)
                {
                    Trace(PromiseMessages.XferActionStarted(xferAction.Key));
                    xferAction.Value.Invoke(_workload);
                    Trace(PromiseMessages.XferActionStopped(xferAction.Key));
                    if (_workload.IsTerminated) return PostEnd();
                }

                foreach (var successHandler in Context.SuccessHandlers)
                {

                    Trace(PromiseMessages.SuccessActionStarted(successHandler.Key));
                    successHandler.Value.Invoke(this);
                    Trace(PromiseMessages.SuccessActionStopped(successHandler.Key));
                    if (_workload.IsTerminated) return PostEnd();
                }

                Trace(PromiseMessages.PromiseSuccess);

            }
            catch (Exception ex)
            {
                Error(new GenericEventMessage(ex));
                Trace(PromiseMessages.PromiseFail);
            }

            return PostEnd();
        }

        private IAmAPromise<TW> PostEnd()
        {
            foreach (var postEndAction in Context.PostEndActions)
            {
                Trace(PromiseMessages.PostEndActionStarted(postEndAction.Key));
                postEndAction.Value.Invoke(_workload);
                Trace(PromiseMessages.PostEndActionStopped(postEndAction.Key));
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
            _workload.IsTerminated = true;
            _workload.IsBlocked = true;

            foreach (var handler in Context.BlockHandlers)
            {
                try
                {
                    handler.Value.Invoke(this, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError(ex);
                }
            }
        }

        /// <summary>
        /// Submits a message to the trace instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Trace(IHandleEventMessage message)
        {
            foreach (var handler in Context.TraceHandlers)
            {
                try
                {
                    handler.Value.Invoke(this, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError(ex);
                }
            }
        }

        /// <summary>
        /// Submits a message to the debug instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Debug(IHandleEventMessage message)
        {
            foreach (var handler in Context.DebugHandlers)
            {
                try
                {
                    handler.Value.Invoke(this, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError(ex);
                }
            }
        }

        /// <summary>
        /// Submits a message to the info instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Info(IHandleEventMessage message)
        {
            foreach (var handler in Context.InfoHandlers)
            {
                try
                {
                    handler.Value.Invoke(this, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError(ex);
                }
            }
        }

        /// <summary>
        /// Submits a message to the 'warn' instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Warn(IHandleEventMessage message)
        {
            foreach (var handler in Context.WarnHandlers)
            {
                try
                {
                    handler.Value.Invoke(this, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError(ex);
                }
            }
        }

        /// <summary>
        /// Submits a message to the 'error' instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Error(IHandleEventMessage message)
        {
            _workload.IsTerminated = true;

            foreach (var handler in Context.ErrorHandlers)
            {
                try
                {
                    handler.Value.Invoke(this, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError(ex);
                }
            }
        }

        /// <summary>
        /// Submits a message to the 'fatal' instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Fatal(IHandleEventMessage message)
        {
            _workload.IsTerminated = true;

            foreach (var handler in Context.FatalHandlers)
            {
                try
                {
                    handler.Value.Invoke(this, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError(ex);
                }
            }
        }

        /// <summary>
        /// Submits a message to the 'abort' instrumentation handler.
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Abort(IHandleEventMessage message)
        {
            _workload.IsTerminated = true;

            foreach (var handler in Context.AbortHandlers)
            {
                try
                {
                    handler.Value.Invoke(this, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError(ex);
                }
            }
        }

        /// <summary>
        /// Submits a message to the 'abortOnAccessDenied' instrumentation handler.
        /// Notifies the promise to abort processing.
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void AbortOnAccessDenied(IHandleEventMessage message)
        {
            _workload.IsTerminated = true;

            foreach (var handler in Context.AbortOnAccessDeniedHandlers)
            {
                try
                {
                    handler.Value.Invoke(this, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError(ex);
                }
            }
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

        public Promise<TW> WithPreStart(string actionId, Action<TW> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            if (Context.PreStartActions.ContainsKey(actionId)) return this;
            Context.PreStartActions.Add(actionId, action);

            return this;
        }

        public Promise<TW> WithPostEnd(string actionId, Action<TW> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            if (Context.PostEndActions.ContainsKey(actionId)) return this;
            Context.PostEndActions.Add(actionId, action);

            return this;
        }

        public Promise<TW> WithValidator(string actionId, Action<TW> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            if (Context.Validators.ContainsKey(actionId)) return this;
            Context.Validators.Add(actionId, action);

            return this;
        }

        public Promise<TW> WithAuthChallenger(string actionId, Action<TW> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            if (Context.AuthChallengers.ContainsKey(actionId)) return this;
            Context.AuthChallengers.Add(actionId, action);

            return this;
        }

        public Promise<TW> WithExecutor(string actionId, Action<TW> action)
        {
            if (string.IsNullOrEmpty(actionId) || action == null) return this;

            if (Context.Executors.ContainsKey(actionId)) return this;
            Context.Executors.Add(actionId, action);

            return this;
        }

        public Promise<TW> WithBlockHandler(string eventHandlerKey, Action<IAmAPromise<TW>, IHandleEventMessage> eventHandler)
        {
            if (eventHandler == null) return this;
            if (Context.BlockHandlers.ContainsKey(eventHandlerKey)) return this;

            Context.BlockHandlers.Add(eventHandlerKey, eventHandler);
            return this;
        }

        public Promise<TW> WithTraceHandler(string eventHandlerKey, Action<IAmAPromise<TW>, IHandleEventMessage> eventHandler)
        {
            if (eventHandler == null) return this;
            if (Context.TraceHandlers.ContainsKey(eventHandlerKey)) return this;

            Context.TraceHandlers.Add(eventHandlerKey, eventHandler);
            return this;
        }

        public Promise<TW> WithDebugHandler(string eventHandlerKey, Action<IAmAPromise<TW>, IHandleEventMessage> eventHandler)
        {
            if (eventHandler == null) return this;
            if (Context.DebugHandlers.ContainsKey(eventHandlerKey)) return this;

            Context.DebugHandlers.Add(eventHandlerKey, eventHandler);
            return this;
        }

        public Promise<TW> WithInfoHandler(string eventHandlerKey, Action<IAmAPromise<TW>, IHandleEventMessage> eventHandler)
        {
            if (eventHandler == null) return this;
            if (Context.InfoHandlers.ContainsKey(eventHandlerKey)) return this;

            Context.InfoHandlers.Add(eventHandlerKey, eventHandler);
            return this;
        }

        public Promise<TW> WithWarnHandler(string eventHandlerKey, Action<IAmAPromise<TW>, IHandleEventMessage> eventHandler)
        {
            if (eventHandler == null) return this;
            if (Context.WarnHandlers.ContainsKey(eventHandlerKey)) return this;

            Context.WarnHandlers.Add(eventHandlerKey, eventHandler);
            return this;
        }

        public Promise<TW> WithErrorHandler(string eventHandlerKey, Action<IAmAPromise<TW>, IHandleEventMessage> eventHandler)
        {
            if (eventHandler == null) return this;
            if (Context.ErrorHandlers.ContainsKey(eventHandlerKey)) return this;

            Context.ErrorHandlers.Add(eventHandlerKey, eventHandler);
            return this;
        }

        public Promise<TW> WithFatalHandler(string eventHandlerKey, Action<IAmAPromise<TW>, IHandleEventMessage> eventHandler)
        {
            if (eventHandler == null) return this;
            if (Context.FatalHandlers.ContainsKey(eventHandlerKey)) return this;

            Context.FatalHandlers.Add(eventHandlerKey, eventHandler);
            return this;
        }

        public Promise<TW> WithAbortHandler(string eventHandlerKey, Action<IAmAPromise<TW>, IHandleEventMessage> eventHandler)
        {
            if (eventHandler == null) return this;
            if (Context.AbortHandlers.ContainsKey(eventHandlerKey)) return this;

            Context.AbortHandlers.Add(eventHandlerKey, eventHandler);
            return this;
        }

        public Promise<TW> WithAbortOnAccessDeniedHandler(string eventHandlerKey,
            Action<IAmAPromise<TW>, IHandleEventMessage> eventHandler)
        {
            if (eventHandler == null) return this;
            if (Context.AbortOnAccessDeniedHandlers.ContainsKey(eventHandlerKey)) return this;

            Context.AbortOnAccessDeniedHandlers.Add(eventHandlerKey, eventHandler);
            return this;
        }

        public Promise<TW> WithSuccessHandler(string eventHandlerKey,
            Action<IAmAPromise<TW>> eventHandler)
        {
            if (eventHandler == null) return this;
            if (Context.SuccessHandlers.ContainsKey(eventHandlerKey)) return this;

            Context.SuccessHandlers.Add(eventHandlerKey, eventHandler);
            return this;
        }
    }
}