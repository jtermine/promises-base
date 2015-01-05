﻿using Termine.Promises.Base.Test.CreateLockPromiseObjects;
using Termine.Promises.NLogInstrumentation;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class CreateLockPromise : Promise<CreateLockWorkload>
    {
        public int AuthChallengerChecksum { get; set; }
        public int ValidatorChecksum { get; set; }
        public int ExecutorChecksum { get; set; }

        public override void Init()
        {
            this.WithNlogInstrumentation()
                .WithAuthChallenger("1", AuthChallenger)
                .WithValidator("2", Validator)
                .WithExecutor("3", Executor);
        }

        private void Executor(CreateLockWorkload lockWorkload)
        {
            ExecutorChecksum = AuthChallengerChecksum + ValidatorChecksum;
        }

        private void Validator(CreateLockWorkload lockWorkload)
        {
            ValidatorChecksum = AuthChallengerChecksum + 1;
        }

        private void AuthChallenger(CreateLockWorkload lockWorkload)
        {
            AuthChallengerChecksum = 1;
        }
    }
}
