using System;
using System.Collections.Generic;
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

        public PromiseContext Context
        {
            get { return _context; }
        }

        public TW Workload
        {
            get { return _workload; }
        }

        public int AuthChallengersCount
        {
            get { return Context.AuthChallengers.Count; }
        }

        public int ValidatorsCount
        {
            get { return Context.Validators.Count; }
        }

        public int ExecutorsCount
        {
            get { return Context.Executors.Count; }
        }

        public IAmAPromise<TW> RunAsync()
        {
            try
            {

                foreach (var challenger in Context.AuthChallengers)
                {
                    challenger.Value.Invoke(_workload);
                    if (_workload.TerminateProcessing) return this;
                }

                foreach (var validator in Context.Validators)
                {
                    validator.Value.Invoke(_workload);
                    if (_workload.TerminateProcessing) return this;
                }

                foreach (var executor in Context.Executors)
                {
                    executor.Value.Invoke(_workload);
                    if (_workload.TerminateProcessing) return this;
                }

            }
            catch (Exception ex)
            {
                Error(new GenericEventMessage(ex));
            }

            return this;
        }

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

        public void Trace(Exception ex)
        {
            Trace(new GenericEventMessage(ex));
        }

        public void Debug(Exception ex)
        {
            Debug(new GenericEventMessage(ex));
        }

        public void Info(Exception ex)
        {
            Info(new GenericEventMessage(ex));
        }

        public void Warn(Exception ex)
        {
            Warn(new GenericEventMessage(ex));
        }

        public void Error(Exception ex)
        {
            Error(new GenericEventMessage(ex));
        }

        public void Fatal(Exception ex)
        {
            Fatal(new GenericEventMessage(ex));
        }

        public void Abort(Exception ex)
        {
            Abort(new GenericEventMessage(ex));
        }

        public void AbortOnAccessDenied(Exception ex)
        {
            AbortOnAccessDenied(new GenericEventMessage(ex));
        }
    }
}