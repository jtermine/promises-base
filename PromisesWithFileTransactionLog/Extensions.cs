using System;
using Termine.Promises;
using Termine.Promises.Interfaces;

namespace PromisesWithFileTransactionLog
{
    public static class Extensions
    {
        public static Promise<TW> WithNLogInstrumentation<TW>(this Promise<TW> promise)
            where TW : class, IAmAPromiseWorkload, new()
        {
            var loggerName = string.Format("{0}.{1}", typeof(TW).FullName, promise.PromiseId);

            var log = LogManager.GetLogger(loggerName);

            promise.WithBlockHandler("nlog.block",
                (w, m) => m.LogEvent(log, LogLevel.Trace, w.PromiseId, m));

            promise.WithTraceHandler("nlog.trace",
                (w, m) => m.LogEvent(log, LogLevel.Trace, w.PromiseId, m));

            promise.WithDebugHandler("nlog.debug",
                (w, m) => m.LogEvent(log, LogLevel.Debug, w.PromiseId, m));

            promise.WithInfoHandler("nlog.info",
                (w, m) => m.LogEvent(log, LogLevel.Info, w.PromiseId, m));

            promise.WithWarnHandler("nlog.warn",
                (w, m) => m.LogEvent(log, LogLevel.Warn, w.PromiseId, m));

            promise.WithErrorHandler("nlog.error",
                (w, m) => m.LogEvent(log, LogLevel.Error, w.PromiseId, m));

            promise.WithFatalHandler("nlog.fatal",
                (w, m) => m.LogEvent(log, LogLevel.Fatal, w.PromiseId, m));

            promise.WithAbortHandler("nlog.abort",
                (w, m) => m.LogEvent(log, LogLevel.Info, w.PromiseId, m));

            promise.WithAbortOnAccessDeniedHandler("nlog.abortAccessDenied",
                (w, m) => m.LogEvent(log, LogLevel.Info, w.PromiseId, m));

            return promise;
        }
    }
}
