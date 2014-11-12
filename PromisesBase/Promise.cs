using System;
using System.Collections.Generic;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    public class Promise<TT, TA, TW> where TT:IAmAPromiseWorkload<TA, TW>, new() where TA : IAmAPromiseRequest, new() where TW : IAmAPromiseResponse, new()
    {
        private class PromiseContext
        {
            public readonly Dictionary<string, Action<TT>> AuthChallengers = new Dictionary<string, Action<TT>>();
            public readonly Dictionary<string, Action<TT>> Validators = new Dictionary<string, Action<TT>>();
            public readonly Dictionary<string, Action<TT>> Executors = new Dictionary<string, Action<TT>>();
        }

        private readonly PromiseContext _context = new PromiseContext();
        private TT _workload = new TT();

        public int AuthChallengersCount { get { return _context.AuthChallengers.Count; } }
        public int ValidatorsCount { get { return _context.Validators.Count; } }
        public int ExecutorsCount { get { return _context.Executors.Count; } }

        public Promise<TT, TA, TW> WithWorkload(TT workload)
        {
            _workload = workload;
            return this;
        }

        public Promise<TT, TA, TW> WithValidator(IAmAPromiseAction<TT, TA, TW> validator)
        {
            if (string.IsNullOrEmpty(validator.ActionId) || validator.PromiseAction == default(Action<TT>)) return this;

            if (_context.Validators.ContainsKey(validator.ActionId)) return this;
            _context.Validators.Add(validator.ActionId, validator.PromiseAction);

            return this;
        }

        public Promise<TT, TA, TW> WithAuthChallenger(IAmAPromiseAction<TT, TA, TW> authChallenger)
        {
            if (string.IsNullOrEmpty(authChallenger.ActionId) || authChallenger.PromiseAction == default(Action<TT>)) return this;

            if (_context.AuthChallengers.ContainsKey(authChallenger.ActionId)) return this;
            _context.AuthChallengers.Add(authChallenger.ActionId, authChallenger.PromiseAction);

            return this;
        }

        public Promise<TT, TA, TW> WithExecutor(IAmAPromiseAction<TT, TA, TW> executor)
        {
            if (string.IsNullOrEmpty(executor.ActionId) || executor.PromiseAction == default(Action<TT>)) return this;

            if (_context.Executors.ContainsKey(executor.ActionId)) return this;
            _context.Executors.Add(executor.ActionId, executor.PromiseAction);

            return this;
        }

        public Promise<TT, TA, TW> RunAsync()
        {
            foreach (var challenger in _context.AuthChallengers)
            {
                challenger.Value.Invoke(_workload);
                if (_workload.TerminateProcessing) return this;
            }

            foreach (var validator in _context.Validators)
            {
                validator.Value.Invoke(_workload);
                if (_workload.TerminateProcessing) return this;
            }

            foreach (var executor in _context.Executors)
            {
                executor.Value.Invoke(_workload);
                if (_workload.TerminateProcessing) return this;
            }

            return this;
        }
    }
}
