using Termine.Promises.Base.Test.CreateLockPromiseObjects;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class CreateLockPromise : Promise<CreateLockWorkload>
    {
        public int AuthChallengerChecksum { get; set; }
        public int ValidatorChecksum { get; set; }
        public int ExecutorChecksum { get; set; }

        public CreateLockPromise()
        {
            this.WithAuthChallenger(new PromiseActionInstance<CreateLockWorkload>("1", AuthChallenger));
            this.WithValidator(new PromiseActionInstance<CreateLockWorkload>("2", Validator));
            this.WithExecutor(new PromiseActionInstance<CreateLockWorkload>("3", Executor));
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
