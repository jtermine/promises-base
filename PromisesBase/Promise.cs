using System;
using System.Collections.Generic;
using System.Linq;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    public class Promise<TT, TA, TW> : IAmAPromise<TT, TA, TW> 
        where TT:IAmAPromiseWorkload<TA, TW>, new() 
        where TA : IAmAPromiseRequest, new() 
        where TW : IAmAPromiseResponse, new()
    {
        public class PromiseContext
        {
            public readonly Dictionary<string, Action<TT>> AuthChallengers = new Dictionary<string, Action<TT>>();
            public readonly Dictionary<string, Action<TT>> Validators = new Dictionary<string, Action<TT>>();
            public readonly Dictionary<string, Action<TT>> Executors = new Dictionary<string, Action<TT>>();
        }

        public readonly PromiseContext Context = new PromiseContext();
        private TT _workload = new TT();

        public int AuthChallengersCount { get { return Context.AuthChallengers.Count; } }
        public int ValidatorsCount { get { return Context.Validators.Count; } }
        public int ExecutorsCount { get { return Context.Executors.Count; } }

        public Promise<TT, TA, TW> WithWorkload(TT workload)
        {
            _workload = workload;
            return this;
        }

        public Promise<TT, TA, TW> WithValidator(IAmAPromiseAction<TT, TA, TW> validator)
        {
            if (string.IsNullOrEmpty(validator.ActionId) || validator.PromiseAction == default(Action<TT>)) return this;

            if (Context.Validators.ContainsKey(validator.ActionId)) return this;
            Context.Validators.Add(validator.ActionId, validator.PromiseAction);

            return this;
        }

        public Promise<TT, TA, TW> WithAuthChallenger(IAmAPromiseAction<TT, TA, TW> authChallenger)
        {
            if (string.IsNullOrEmpty(authChallenger.ActionId) || authChallenger.PromiseAction == default(Action<TT>)) return this;

            if (Context.AuthChallengers.ContainsKey(authChallenger.ActionId)) return this;
            Context.AuthChallengers.Add(authChallenger.ActionId, authChallenger.PromiseAction);

            return this;
        }

        public Promise<TT, TA, TW> WithExecutor(IAmAPromiseAction<TT, TA, TW> executor)
        {
            if (string.IsNullOrEmpty(executor.ActionId) || executor.PromiseAction == default(Action<TT>)) return this;

            if (Context.Executors.ContainsKey(executor.ActionId)) return this;
            Context.Executors.Add(executor.ActionId, executor.PromiseAction);

            return this;
        }

        public static TE Join<TE>(TE basePromise, params Promise<TT, TA, TW>[] promises)
            where TE : Promise<TT, TA, TW>
        {
            if (promises.Length == 0) throw new ArgumentNullException("promises");

            foreach (var thisPromise in promises)
            {
                foreach (var authChallenger in thisPromise.Context.AuthChallengers)
                {
                    basePromise.WithAuthChallenger(new PromiseActionInstance<TT, TA, TW>(authChallenger.Key,
                        authChallenger.Value));
                }

                foreach (var validator in thisPromise.Context.Validators)
                {
                    basePromise.WithValidator(new PromiseActionInstance<TT, TA, TW>(validator.Key,
                        validator.Value));
                }

                foreach (var executor in thisPromise.Context.Executors)
                {
                    basePromise.WithValidator(new PromiseActionInstance<TT, TA, TW>(executor.Key,
                        executor.Value));
                }
            }

            return basePromise;
        }

        public Promise<TT, TA, TW> RunAsync()
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
    }
}
