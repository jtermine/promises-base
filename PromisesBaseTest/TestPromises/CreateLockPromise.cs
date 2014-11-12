﻿using Termine.Promises.Base.Test.TestObjects;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class CreateLockPromise : Promise<PromiseWorkload, PromiseRequest, PromiseResponse>
    {
        public int AuthChallengerChecksum { get; set; }
        public int ValidatorChecksum { get; set; }
        public int ExecutorChecksum { get; set; }

        public CreateLockPromise()
        {
            WithAuthChallenger(new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("1", AuthChallenger));
            WithValidator(new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("2", Validator));
            WithExecutor(new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("3", Executor));
        }

        private void Executor(PromiseWorkload promiseWorkload)
        {
            ExecutorChecksum = AuthChallengerChecksum + ValidatorChecksum;
        }

        private void Validator(PromiseWorkload promiseWorkload)
        {
            ValidatorChecksum = AuthChallengerChecksum + 1;
        }

        private void AuthChallenger(PromiseWorkload promiseWorkload)
        {
            AuthChallengerChecksum = 1;
        }
    }
}
