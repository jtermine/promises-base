using Termine.Promises;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;
using Termine.Promises.Base.Interfaces;

namespace PromisesBaseFrameworkTest.DeepPromise
{
    public class DeepPromisePf: IAmAStrongPromiseFactory<DeepPromiseRq, DeepPromiseRx, GenericUserIdentity>
    {
        private readonly Promise<GenericConfig, GenericUserIdentity, DeepPromiseW, DeepPromiseRq, DeepPromiseRx>
            _promise = new Promise<GenericConfig, GenericUserIdentity, DeepPromiseW, DeepPromiseRq, DeepPromiseRx>();

        public DeepPromisePf()
        {
            _promise.WithExecutor("deep1", func =>
            {
                func.W.StandingValue = func.Rq.StartNum + func.Rq.Multiplier;
                return Resp.Success();
            });

            _promise.WithExecutor("deep2", func =>
            {
                func.W.StandingValue = func.W.StandingValue + func.Rq.Multiplier;
                return Resp.Success();
            });

            _promise.WithExecutor("deep3", func =>
            {
                func.W.StandingValue = func.W.StandingValue + func.Rq.Multiplier;
                return Resp.Success();
            });

            _promise.WithExecutor("deep4", func =>
            {
                return Resp.Abort("Something botched.");
                //func.W.StandingValue = func.W.StandingValue * func.Rq.Multiplier;
                //return Resp.Success();
            });

            _promise.WithExecutor("deep5", func =>
            {
                func.Rx.Result = func.W.StandingValue;
                return Resp.Success();
            });
        }

        public DeepPromiseRx Run(DeepPromiseRq request, GenericUserIdentity user, PromiseMessenger messenger = default(PromiseMessenger))
        {
            return _promise.Run(new PromiseOptions<DeepPromiseRq, GenericUserIdentity>(request, user, messenger)).Response;
        }
    }
}