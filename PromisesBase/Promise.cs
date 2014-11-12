using System;
using System.Collections.Generic;

namespace Termine.Promises
{
    public class Promise
    {
        private class PromiseContext
        {
            public readonly List<Action<PromiseWorkload>> AuthChallengers = new List<Action<PromiseWorkload>>();
            public readonly List<Action<PromiseWorkload>> Validators = new List<Action<PromiseWorkload>>();  
        }

        private readonly PromiseContext _context = new PromiseContext();
        private PromiseWorkload _workload = new PromiseWorkload();

        public int AuthChallengersCount { get { return _context.AuthChallengers.Count; } }
        public int ValidatorsCount { get { return _context.Validators.Count; } }
        
        public Promise WithWorkload(PromiseWorkload workload)
        {
            _workload = workload;
            return this;
        }

        public Promise WithValidator(Action<PromiseWorkload> validator)
        {
            _context.Validators.Add(validator);
            return this;
        }

        public Promise WithAuthChallenger(Action<PromiseWorkload> authChallenger)
        {
            _context.AuthChallengers.Add(authChallenger);
            return this;
        }

        public Promise RunAsync()
        {
            foreach (var challenger in _context.AuthChallengers)
            {
                challenger.Invoke(_workload);
            }

            foreach (var validator in _context.Validators)
            {
                validator.Invoke(_workload);
            }

            return this;
        }
    }
}
