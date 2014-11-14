﻿using System;
using System.Collections.Generic;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    /// <summary>
    /// The basic promise model
    /// </summary>
    /// <typeparam name="TA">a promise request</typeparam>
    /// <typeparam name="TW">a promise response</typeparam>
    public class Promise<TA, TW> : IAmAPromise<TA, TW>,
        IPromise
        where TA : IAmAPromiseRequest, new() 
        where TW : IAmAPromiseResponse, new()
    {
        public class PromiseContext
        {
            public readonly Dictionary<string, Action<IPromise, PromiseWorkload<TA, TW>>> AuthChallengers = new Dictionary<string, Action<IPromise, PromiseWorkload<TA, TW>>>();
            public readonly Dictionary<string, Action<IPromise, PromiseWorkload<TA, TW>>> Validators = new Dictionary<string, Action<IPromise, PromiseWorkload<TA, TW>>>();
            public readonly Dictionary<string, Action<IPromise, PromiseWorkload<TA, TW>>> Executors = new Dictionary<string, Action<IPromise, PromiseWorkload<TA, TW>>>();
        }

        private readonly PromiseContext _context = new PromiseContext();
        private readonly PromiseWorkload<TA, TW> _workload = new PromiseWorkload<TA, TW>();

        public PromiseContext Context { get { return _context; } }

        public PromiseWorkload<TA, TW> Workload { get { return _workload; } }

        public int AuthChallengersCount { get { return Context.AuthChallengers.Count; } }
        public int ValidatorsCount { get { return Context.Validators.Count; } }
        public int ExecutorsCount { get { return Context.Executors.Count; } }

        public IAmAPromise<TA, TW> WithRequest(TA request)
        {
            _workload.Request = request;
            return this;
        }
        
        public static TE Join<TE>(TE basePromise, params Promise<TA, TW>[] promises)
            where TE : Promise<TA, TW>
        {
            if (promises.Length == 0) throw new ArgumentNullException("promises");

            foreach (var thisPromise in promises)
            {
                foreach (var authChallenger in thisPromise.Context.AuthChallengers)
                {
                    basePromise.WithAuthChallenger(new PromiseActionInstance<TA, TW>(authChallenger.Key,
                        authChallenger.Value));
                }

                foreach (var validator in thisPromise.Context.Validators)
                {
                    basePromise.WithValidator(new PromiseActionInstance<TA, TW>(validator.Key,
                        validator.Value));
                }

                foreach (var executor in thisPromise.Context.Executors)
                {
                    basePromise.WithExecutor(new PromiseActionInstance<TA, TW>(executor.Key,
                        executor.Value));
                }
            }

            return basePromise;
        }

        public IAmAPromise<TA, TW> RunAsync()
        {
            foreach (var challenger in Context.AuthChallengers)
            {
                challenger.Value.Invoke(this, _workload);
                if (_workload.TerminateProcessing) return this;
            }

            foreach (var validator in Context.Validators)
            {
                validator.Value.Invoke(this, _workload);
                if (_workload.TerminateProcessing) return this;
            }

            foreach (var executor in Context.Executors)
            {
                executor.Value.Invoke(this, _workload);
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
