using Termine.Promises.Base.Test.FalseLockPromiseObjects;
using Termine.Promises.Interfaces;
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

        private void Executor(IHandlePromiseActions handlePromiseActions, FalseLockWorkload falseLockWorkload)
        {
            ExecutorChecksum = AuthChallengerChecksum + ValidatorChecksum;
        }

        private void Validator(IHandlePromiseActions handlePromiseActions, FalseLockWorkload falseLockWorkload)
        {
            ValidatorChecksum = AuthChallengerChecksum + 1;
            falseLockWorkload.IsTerminated = true;
        }

        private void AuthChallenger(IHandlePromiseActions handlePromiseActions, FalseLockWorkload falseLockWorkload)
        {
            AuthChallengerChecksum = 1;
        }
    }
}
