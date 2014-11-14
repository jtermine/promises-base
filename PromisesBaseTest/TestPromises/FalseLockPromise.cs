using Termine.Promises.Base.Test.TestObjects;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class FalseLockPromise : Promise<PromiseRequest, PromiseResponse>
    {
        public int AuthChallengerChecksum { get; set; }
        public int ValidatorChecksum { get; set; }
        public int ExecutorChecksum { get; set; }

        public FalseLockPromise()
        {
            this.WithAuthChallenger(new PromiseActionInstance<PromiseRequest, PromiseResponse>("4", AuthChallenger));
            this.WithValidator(new PromiseActionInstance<PromiseRequest, PromiseResponse>("5", Validator));
            this.WithExecutor(new PromiseActionInstance<PromiseRequest, PromiseResponse>("6", Executor));
        }

        private void Executor(IPromise promise, PromiseWorkload<PromiseRequest, PromiseResponse> workload)
        {
            ExecutorChecksum = AuthChallengerChecksum + ValidatorChecksum;
        }

        private void Validator(IPromise promise, PromiseWorkload<PromiseRequest, PromiseResponse> workload)
        {
            ValidatorChecksum = AuthChallengerChecksum + 1;
            workload.TerminateProcessing = true;
        }

        private void AuthChallenger(IPromise promise, PromiseWorkload<PromiseRequest, PromiseResponse> workload)
        {
            AuthChallengerChecksum = 1;
        }
    }
}
