using System;
using Termine.Promises.Base.Generics;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Logging
{
    public class ChaosMonkeyPromiseConfigurator :
        IConfigurePromise<GenericConfig, GenericUserIdentity, GenericWorkload, GenericRequest, GenericResponse>
    {
        public void Configure(
            IHandlePromiseEvents<GenericConfig, GenericUserIdentity, GenericWorkload, GenericRequest, GenericResponse> promise)
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

        private static void ChaosMonkey(IHandleEventMessage m, IHandlePromiseActions p, GenericConfig c, GenericUserIdentity u, GenericWorkload w,
            GenericRequest rq, GenericResponse rx)
        {
            var random = new Random();

            var randomNumber = random.Next(0, 100);

            var throwChaos = randomNumber < 35;

            if (!throwChaos) return;

            var hasConfigChaos = new Random().Next(1, 10) > 7;
            var hasWorkloadChaos = new Random().Next(1, 10) > 7;
            var hasRequestChaos = new Random().Next(1, 10) > 7;
            var hasResponseChaos = new Random().Next(1, 10) >7;

            if (hasConfigChaos) p.ThrowChaosOnConfig();
            if (hasWorkloadChaos) p.ThrowChaosOnWorkload();
            if (hasRequestChaos) p.ThrowChaosOnRequest();
            if (hasResponseChaos) p.ThrowChaosOnResponse();
        }

    }
}