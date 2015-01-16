using Termine.Promises.Base.Test.FalseLockPromiseObjects;
using Termine.Promises.NLogInstrumentation;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class FalseLockPromise : Promise<FalseLockWorkload>
    {
        public int AuthChallengerChecksum { get; set; }
        public int ValidatorChecksum { get; set; }
        public int ExecutorChecksum { get; set; }

        public override void Init()
        {
            this.WithNLogInstrumentation()
                .WithAuthChallenger("4", AuthChallenger)
                .WithValidator("5", Validator)
                .WithExecutor("6", Executor);
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
