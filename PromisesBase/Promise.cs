using System;
using System.Collections.Generic;
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
        }

        private readonly PromiseContext _context = new PromiseContext();
        private readonly TW _workload = new TW();

        public PromiseContext Context { get { return _context; } }

        public TW Workload { get { return _workload; } }

        public int AuthChallengersCount { get { return Context.AuthChallengers.Count; } }
        public int ValidatorsCount { get { return Context.Validators.Count; } }
        public int ExecutorsCount { get { return Context.Executors.Count; } }

        public IAmAPromise<TW> RunAsync()
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

            return this;
        }

        public void Trace()
        {
        }

        public void Debug()
        {
        }

        public void Info()
        {
        }

        public void Warn()
        {
        }

        public void Error()
        {
        }

        public void Fatal()
        {
        }

        public void Abort()
        {
        }

        public void AbortOnAccessDenied()
        {
            _workload.TerminateProcessing = true;
        }
    }
}
