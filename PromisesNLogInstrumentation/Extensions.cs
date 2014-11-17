using System;
using NLog;
using Termine.Promises.Interfaces;

// ReSharper disable once CheckNamespace
namespace Termine.Promises
{
    public static class Extensions
    {
        public static TX WithNlogInstrumentation<TX, TW>(this TX promise)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            var log = LogManager.GetCurrentClassLogger(typeof(TX));

            promise.WithTraceHandler("nlog.trace", new Action<TW, IHandleEventMessage>((w, m) =>
            {
                log.Trace(m.EventPrivateMessage);
                log.Trace(m.EventPrivateDetails);
            }));

            promise.WithDebugHandler("nlog.debug", new Action<TW, IHandleEventMessage>((w, m) =>
            {
                log.Debug(m.EventPrivateMessage);
                log.Debug(m.EventPrivateDetails);
            }));

            promise.WithInfoHandler("nlog.info", new Action<TW, IHandleEventMessage>((w, m) =>
            {
                log.Info(m.EventPrivateMessage);
                log.Info(m.EventPrivateDetails);
            }));

            promise.WithWarnHandler("nlog.warn", new Action<TW, IHandleEventMessage>((w, m) =>
            {
                log.Warn(m.EventPrivateMessage);
                log.Warn(m.EventPrivateDetails);
            }));

            promise.WithErrorHandler("nlog.error", new Action<TW, IHandleEventMessage>((w, m) =>
            {
                log.Error(m.EventPrivateMessage);
                log.Error(m.EventPrivateDetails);
            }));

            promise.WithFatalHandler("nlog.fatal", new Action<TW, IHandleEventMessage>((w, m) =>
            {
                log.Fatal(m.EventPrivateMessage);
                log.Fatal(m.EventPrivateDetails);
            }));

            promise.WithAbortHandler("nlog.abort", new Action<TW, IHandleEventMessage>((w, m) =>
            {
                log.Trace(m.EventPrivateMessage);
                log.Trace(m.EventPrivateDetails);
            }));

            promise.WithAbortOnAccessDeniedHandler("nlog.abortAccessDenied", new Action<TW, IHandleEventMessage>((w, m) =>
            {
                log.Trace(m.EventPrivateMessage);
                log.Trace(m.EventPrivateDetails);
            }));

            return promise;
        }
    }
}
