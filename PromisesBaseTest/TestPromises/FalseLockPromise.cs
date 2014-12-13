using Termine.Promises.Base.Test.FalseLockPromiseObjects;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class FalseLockPromise : Promise<FalseLockWorkload>
    {
        public int AuthChallengerChecksum { get; set; }
        public int ValidatorChecksum { get; set; }
        public int ExecutorChecksum { get; set; }

        public override void Init()
        {
            this.WithNlogInstrumentation();
            
            WithAuthChallenger(new PromiseActionInstance<FalseLockWorkload>("4", AuthChallenger));
            WithValidator(new PromiseActionInstance<FalseLockWorkload>("5", Validator));
            WithExecutor(new PromiseActionInstance<FalseLockWorkload>("6", Executor));
        }

        private void Executor(FalseLockWorkload lockWorkload)
        {
            ExecutorChecksum = AuthChallengerChecksum + ValidatorChecksum;
        }

        private void Validator(FalseLockWorkload lockWorkload)
        {
            ValidatorChecksum = AuthChallengerChecksum + 1;
            lockWorkload.IsTerminated = true;
        }

        private void AuthChallenger(FalseLockWorkload lockWorkload)
        {
            AuthChallengerChecksum = 1;
        }
    }
}
