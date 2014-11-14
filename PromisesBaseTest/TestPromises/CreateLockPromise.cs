using Termine.Promises.Base.Test.TestObjects;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class CreateLockPromise : Promise<PromiseRequest, PromiseResponse>
    {
        public int AuthChallengerChecksum { get; set; }
        public int ValidatorChecksum { get; set; }
        public int ExecutorChecksum { get; set; }

        public CreateLockPromise()
        {
            this.WithAuthChallenger(new PromiseActionInstance<PromiseRequest, PromiseResponse>("1", AuthChallenger));
            this.WithValidator(new PromiseActionInstance<PromiseRequest, PromiseResponse>("2", Validator));
            this.WithExecutor(new PromiseActionInstance<PromiseRequest, PromiseResponse>("3", Executor));
        }

        private void Executor(IPromise promise, PromiseWorkload<PromiseRequest, PromiseResponse> workload)
        {
            ExecutorChecksum = AuthChallengerChecksum + ValidatorChecksum;
        }

        private void Validator(IPromise promise, PromiseWorkload<PromiseRequest, PromiseResponse> workload)
        {
            ValidatorChecksum = AuthChallengerChecksum + 1;
        }

        private void AuthChallenger(IPromise promise, PromiseWorkload<PromiseRequest, PromiseResponse> workload)
        {
            AuthChallengerChecksum = 1;
        }
    }
}
