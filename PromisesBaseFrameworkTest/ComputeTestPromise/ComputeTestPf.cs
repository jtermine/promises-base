using Termine.Promises;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;
using Termine.Promises.Base.Interfaces;

namespace PromisesBaseFrameworkTest.ComputeTestPromise
{
    public class ComputeTestPf: IAmAStrongPromiseFactory<ComputeTestRq, ComputeTestRx, GenericUserIdentity>
    {
        private readonly Promise<GenericConfig, GenericUserIdentity, ComputeTestW, ComputeTestRq, ComputeTestRx>
            _promise = new Promise<GenericConfig, GenericUserIdentity, ComputeTestW, ComputeTestRq, ComputeTestRx>();

        public ComputeTestPf()
        {
            _promise.WithExecutor("e1", func =>
            {
                func.W.StandingValue = func.Rq.StartNum * func.Rq.Multiplier;
                return Resp.Success();
            });

            _promise.WithExecutor("e2", func =>
            {
                func.W.StandingValue = func.W.StandingValue * func.Rq.Multiplier;
                return Resp.Success();
            });

            _promise.WithExecutor("e3", func =>
            {
                func.W.StandingValue = func.W.StandingValue * func.Rq.Multiplier;
                return Resp.Success();
            });

            _promise.WithExecutor("e4", func =>
            {
                return Resp.Failure();
                func.W.StandingValue = func.W.StandingValue * func.Rq.Multiplier;
                return Resp.Success();
            });

            _promise.WithExecutor("e5", func =>
            {
                func.Rx.Result = func.W.StandingValue;
                return Resp.Success();
            });
        }

        public ComputeTestRx Run(ComputeTestRq request, GenericUserIdentity user, PromiseMessenger messenger = default(PromiseMessenger))
        {
            return _promise.Run(new PromiseOptions<ComputeTestRq, GenericUserIdentity>(request, user, messenger)).Response;
        }
    }
}