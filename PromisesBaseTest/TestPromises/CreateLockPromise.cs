using Termine.Promises.Base.Test.CreateLockPromiseObjects;
using Termine.Promises.Interfaces;
using Termine.Promises.NLogInstrumentation;
using Termine.Promises.ZMQ;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class CreateLockPromise : Promise<CreateLockWorkload>
    {
        public int AuthChallengerChecksum { get; set; }
        public int ValidatorChecksum { get; set; }
        public int ExecutorChecksum { get; set; }

        public CreateLockPromise()
        {
            this.WithNLogInstrumentation();
            //this.WithRest();
            //this.WithRabbitMQ();
        }

        public override void Init()
        {
            this.WithNLogInstrumentation()
                .WithAuthChallenger("1", AuthChallenger)
                .WithValidator("2", Validator)
                .WithExecutor("3", Executor);
        }

        private void Executor(IHandlePromiseActions handlePromiseActions, CreateLockWorkload createLockWorkload)
        {
            ExecutorChecksum = AuthChallengerChecksum + ValidatorChecksum;
        }

        private void Validator(IHandlePromiseActions handlePromiseActions, CreateLockWorkload createLockWorkload)
        {
            ValidatorChecksum = AuthChallengerChecksum + 1;
        }

        private void AuthChallenger(IHandlePromiseActions handlePromiseActions, CreateLockWorkload createLockWorkload)
        {
            AuthChallengerChecksum = 1;
        }
    }
}
