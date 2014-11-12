using Termine.Promises.Base.Test.TestObjects;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class FalseLockPromise : Promise<PromiseWorkload, PromiseRequest, PromiseResponse>
    {
        public int AuthChallengerChecksum { get; set; }
        public int ValidatorChecksum { get; set; }
        public int ExecutorChecksum { get; set; }

        public FalseLockPromise()
        {
            WithAuthChallenger(new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("4", AuthChallenger));
            WithValidator(new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("4", Validator));
            WithExecutor(new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("6", Executor));
        }

        private void Executor(PromiseWorkload promiseWorkload)
        {
            ExecutorChecksum = AuthChallengerChecksum + ValidatorChecksum;
        }

        private void Validator(PromiseWorkload promiseWorkload)
        {
            ValidatorChecksum = AuthChallengerChecksum + 1;
            promiseWorkload.TerminateProcessing = true;
        }

        private void AuthChallenger(PromiseWorkload promiseWorkload)
        {
            AuthChallengerChecksum = 1;
        }
    }
}
