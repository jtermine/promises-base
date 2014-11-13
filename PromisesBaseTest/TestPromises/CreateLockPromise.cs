using Termine.Promises.Base.Test.TestObjects;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class CreateLockPromise : Promise<PromiseWorkload, PromiseRequest, PromiseResponse>
    {
        public int AuthChallengerChecksum { get; set; }
        public int ValidatorChecksum { get; set; }
        public int ExecutorChecksum { get; set; }

        public CreateLockPromise()
        {
            this.WithAuthChallenger(new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("1", AuthChallenger));
            this.WithValidator(new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("2", Validator));
            this.WithExecutor(new PromiseActionInstance<PromiseWorkload, PromiseRequest, PromiseResponse>("3", Executor));
        }

        private void Executor(IHavePromiseMethods promise, PromiseWorkload promiseWorkload)
        {
            ExecutorChecksum = AuthChallengerChecksum + ValidatorChecksum;
        }

        private void Validator(IHavePromiseMethods promise, PromiseWorkload promiseWorkload)
        {
            ValidatorChecksum = AuthChallengerChecksum + 1;
        }

        private void AuthChallenger(IHavePromiseMethods promise, PromiseWorkload promiseWorkload)
        {
            AuthChallengerChecksum = 1;
        }
    }
}
