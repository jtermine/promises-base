using Termine.Promises.Base.Test.TestObjects;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class FalseLockPromise : Promise<PromiseWorkload, PromiseRequest, PromiseResponse>
    {
        public int AuthChallengerChecksum { get; set; }
        public int ValidatorChecksum { get; set; }
        public int ExecutorChecksum { get; set; }

        public FalseLockPromise()
        {
            this.WithAuthChallenger(new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("4", AuthChallenger));
            this.WithValidator(new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("5", Validator));
            this.WithExecutor(new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("6", Executor));
        }

        private void Executor(IHavePromiseMethods promise, PromiseWorkload promiseWorkload)
        {
            ExecutorChecksum = AuthChallengerChecksum + ValidatorChecksum;
        }

        private void Validator(IHavePromiseMethods promise, PromiseWorkload promiseWorkload)
        {
            ValidatorChecksum = AuthChallengerChecksum + 1;
            promiseWorkload.TerminateProcessing = true;
        }

        private void AuthChallenger(IHavePromiseMethods promise, PromiseWorkload promiseWorkload)
        {
            AuthChallengerChecksum = 1;
        }
    }
}
