using System;
using System.Collections.Generic;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    public class Promise<TT, TA, TW> where TT:IAmAPromiseWorkload<TA, TW>, new() where TA : IAmAPromiseRequest, new() where TW : IAmAPromiseResponse, new()
    {
        private class PromiseContext
        {
            public readonly List<Action<TT>> AuthChallengers = new List<Action<TT>>();
            public readonly List<Action<TT>> Validators = new List<Action<TT>>();
            public readonly List<Action<TT>> Executors = new List<Action<TT>>();
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

        public Promise<TT, TA, TW> WithValidator(Action<TT> validator)
        {
            _context.Validators.Add(validator);
            return this;
        }

        public Promise<TT, TA, TW> WithAuthChallenger(Action<TT> authChallenger)
        {
            _context.AuthChallengers.Add(authChallenger);
            return this;
        }

        public Promise<TT, TA, TW> WithExecutor(Action<TT> executor)
        {
            _context.Executors.Add(executor);
            return this;
        }

        public Promise<TT, TA, TW> RunAsync()
        {
            foreach (var challenger in _context.AuthChallengers)
            {
                challenger.Invoke(_workload);
            }

            foreach (var validator in _context.Validators)
            {
                validator.Invoke(_workload);
            }

            foreach (var executor in _context.Executors)
            {
                executor.Invoke(_workload);
            }

            return this;
        }
    }
}
