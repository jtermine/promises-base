using System;
using Termine.Promises.Base;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Logging
{
    public class ChaosMonkeyPromiseConfigurator : IConfigurePromise
    {
        public void Configure(IHandlePromiseEvents promise)
        {
            promise.WithBlockHandler("chaos.block", ChaosMonkey);

            promise.WithTraceHandler("chaos.trace", ChaosMonkey);

            promise.WithDebugHandler("chaos.debug", ChaosMonkey);

            promise.WithInfoHandler("chaos.info", ChaosMonkey);

            promise.WithWarnHandler("chaos.warn", ChaosMonkey);

            promise.WithErrorHandler("chaos.error", ChaosMonkey);

            promise.WithFatalHandler("chaos.fatal", ChaosMonkey);

            promise.WithAbortHandler("chaos.abort", ChaosMonkey);

            promise.WithAbortOnAccessDeniedHandler("chaos.abortAccessDenied", ChaosMonkey);
        }

        private static Resp ChaosMonkey(PromiseMessageFunc messageFunc)
        {
            var random = new Random();

            var randomNumber = random.Next(0, 100);

            var throwChaos = randomNumber < 35;

            if (!throwChaos) return new Resp();

            var hasConfigChaos = new Random().Next(1, 10) > 7;
            var hasWorkloadChaos = new Random().Next(1, 10) > 7;
            var hasRequestChaos = new Random().Next(1, 10) > 7;
            var hasResponseChaos = new Random().Next(1, 10) >7;

            if (hasConfigChaos) messageFunc.P.ThrowChaosOnConfig();
            if (hasWorkloadChaos) messageFunc.P.ThrowChaosOnWorkload();
            if (hasRequestChaos) messageFunc.P.ThrowChaosOnRequest();
            if (hasResponseChaos) messageFunc.P.ThrowChaosOnResponse();

            return new Resp();
        }
        
    }
}