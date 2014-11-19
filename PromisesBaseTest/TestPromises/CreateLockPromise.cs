using Termine.Promises.Base.Test.CreateLockPromiseObjects;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class CreateLockPromise : Promise<CreateLockWorkload>
    {
        public int AuthChallengerChecksum { get; set; }
        public int ValidatorChecksum { get; set; }
        public int ExecutorChecksum { get; set; }

        public override void Init()
        {
            this.WithNlogInstrumentation<CreateLockPromise, CreateLockWorkload>()
                .WithAuthChallenger(new PromiseActionInstance<CreateLockWorkload>("1", AuthChallenger))
                .WithValidator(new PromiseActionInstance<CreateLockWorkload>("2", Validator))
                .WithExecutor(new PromiseActionInstance<CreateLockWorkload>("3", Executor));
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
