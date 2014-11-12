using Termine.Promises;

namespace PromisesBaseTest.TestPromises
{
    public class CreateLockPromise : Promise<PromiseWorkload, PromiseRequest, PromiseResponse>
    {
        public int AuthChallengerChecksum { get; set; }
        public int ValidatorChecksum { get; set; }
        public int ExecutorChecksum { get; set; }

        public CreateLockPromise()
        {
            WithAuthChallenger(AuthChallenger);
            WithValidator(Validator);
            WithExecutor(Executor);
        }

        private void Executor(PromiseWorkload promiseWorkload)
        {
            ExecutorChecksum = AuthChallengerChecksum + ValidatorChecksum;
        }

        private void Validator(PromiseWorkload promiseWorkload)
        {
            ValidatorChecksum = AuthChallengerChecksum + 1;
        }

        private void AuthChallenger(PromiseWorkload promiseWorkload)
        {
            AuthChallengerChecksum = 1;
        }
    }
}
