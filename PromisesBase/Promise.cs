using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public class PromiseContext
        {
            public readonly Dictionary<string, Action<TW>> AuthChallengers = new Dictionary<string, Action<TW>>();
            public readonly Dictionary<string, Action<TW>> Validators = new Dictionary<string, Action<TW>>();
            public readonly Dictionary<string, Action<TW>> Executors = new Dictionary<string, Action<TW>>();

            public Dictionary<string, Action<TW, IHandleEventMessage>> TraceHandlers { get; private set; }
            public Dictionary<string, Action<TW, IHandleEventMessage>> DebugHandlers { get; private set; }
            public Dictionary<string, Action<TW, IHandleEventMessage>> InfoHandlers { get; private set; }
            public Dictionary<string, Action<TW, IHandleEventMessage>> WarnHandlers { get; private set; }
            public Dictionary<string, Action<TW, IHandleEventMessage>> ErrorHandlers { get; private set; }
            public Dictionary<string, Action<TW, IHandleEventMessage>> FatalHandlers { get; private set; }
            public Dictionary<string, Action<TW, IHandleEventMessage>> AbortHandlers { get; private set; }
            public Dictionary<string, Action<TW, IHandleEventMessage>> AbortOnAccessDeniedHandlers { get; private set; }

            public PromiseContext()
            {
                TraceHandlers = new Dictionary<string, Action<TW, IHandleEventMessage>>();
                DebugHandlers = new Dictionary<string, Action<TW, IHandleEventMessage>>();
                InfoHandlers = new Dictionary<string, Action<TW, IHandleEventMessage>>();
                WarnHandlers = new Dictionary<string, Action<TW, IHandleEventMessage>>();
                ErrorHandlers = new Dictionary<string, Action<TW, IHandleEventMessage>>();
                FatalHandlers = new Dictionary<string, Action<TW, IHandleEventMessage>>();
                AbortHandlers = new Dictionary<string, Action<TW, IHandleEventMessage>>();
                AbortOnAccessDeniedHandlers = new Dictionary<string, Action<TW, IHandleEventMessage>>();
            }
        }

        private readonly PromiseContext _context = new PromiseContext();
        private readonly TW _workload = new TW();

        public virtual void Init()
        {
        }

        /// <summary>
        /// returns a readonly reference to the promise context
        /// </summary>
        public PromiseContext Context
        {
            get { return _context; }
        }

        public TW Workload
        {
            get { return _workload; }
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
                Init();

                Trace(PromiseMessages.PromiseStarted);

                foreach (var challenger in Context.AuthChallengers)
                {
                    Trace(PromiseMessages.AuthChallengerStarted(challenger.Key));
                    challenger.Value.Invoke(_workload);
                    Trace(PromiseMessages.AuthChallengerStopped(challenger.Key));
                    if (_workload.TerminateProcessing) return this;
                }

                foreach (var validator in Context.Validators)
                {
                    Trace(PromiseMessages.ValidatorStarted(validator.Key));
                    validator.Value.Invoke(_workload);
                    Trace(PromiseMessages.ValidatorStopped(validator.Key));
                    if (_workload.TerminateProcessing) return this;
                }

                foreach (var executor in Context.Executors)
                {

                    Trace(PromiseMessages.ExecutorStarted(executor.Key));
                    executor.Value.Invoke(_workload);
                    Trace(PromiseMessages.ExecutorStopped(executor.Key));
                    if (_workload.TerminateProcessing) return this;
                }

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
        /// Submits a message to the trace instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Trace(IHandleEventMessage message)
        {
            foreach (var handler in Context.TraceHandlers)
            {
                try
                {
                    handler.Value.Invoke(Workload, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError<Promise<TW>, TW>(ex);
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
                    handler.Value.Invoke(Workload, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError<Promise<TW>, TW>(ex);
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
                    handler.Value.Invoke(Workload, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError<Promise<TW>, TW>(ex);
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
                    handler.Value.Invoke(Workload, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError<Promise<TW>, TW>(ex);
                }
            }
        }

        /// <summary>
        /// Submits a message to the 'error' instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Error(IHandleEventMessage message)
        {
            foreach (var handler in Context.ErrorHandlers)
            {
                try
                {
                    handler.Value.Invoke(Workload, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError<Promise<TW>, TW>(ex);
                }
            }
        }

        /// <summary>
        /// Submits a message to the 'fatal' instrumentation handler
        /// </summary>
        /// <param name="message">a message object implementing IHandleEventMessage</param>
        public void Fatal(IHandleEventMessage message)
        {
            foreach (var handler in Context.FatalHandlers)
            {
                try
                {
                    handler.Value.Invoke(Workload, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError<Promise<TW>, TW>(ex);
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
            foreach (var handler in Context.AbortHandlers)
            {
                try
                {
                    handler.Value.Invoke(Workload, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError<Promise<TW>, TW>(ex);
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
            _workload.TerminateProcessing = true;

            foreach (var handler in Context.AbortOnAccessDeniedHandlers)
            {
                try
                {
                    handler.Value.Invoke(Workload, message);
                }
                catch (Exception ex)
                {
                    this.HandleInstrumentationError<Promise<TW>, TW>(ex);
                }
            }
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
    }
}