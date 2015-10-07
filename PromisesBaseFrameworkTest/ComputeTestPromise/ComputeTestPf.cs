using System;
using PromisesBaseFrameworkTest.DeepPromise;
using Termine.Promises;
using Termine.Promises.Base;
using Termine.Promises.Base.Generics;
using Termine.Promises.Base.Handlers;
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

            _promise.WithPromiseExecutor("px", func =>
            {
                return new PromiseExecutorConfig<DeepPromiseRq, DeepPromiseRx, GenericUserIdentity>
                {
                    PromiseFactory = new DeepPromisePf(),
                    Rq = new DeepPromiseRq {Multiplier = 5, StartNum = 1},
                    OnResponse = rx =>
                    {
                        func.W.DeepPromiseResult = rx.Result;
                        func.W.StandingValue = func.W.StandingValue + func.W.DeepPromiseResult;
                    }
                };
            });
            
            _promise.WithExecutor("e4", func =>
            {
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