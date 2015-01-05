using System;
using System.Globalization;
using NLog;
using Termine.Promises.Interfaces;

namespace Termine.Promises.NLogInstrumentation
{
    public static class Extensions
    {
        public static Promise<TW> WithNlogInstrumentation<TW>(this Promise<TW> promise)
            where TW : class, IAmAPromiseWorkload, new()
        {
            var log = LogManager.GetLogger(typeof (TW).FullName);
            
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

        private static void LogEvent<TT>(this TT message, Logger logger, LogLevel logLevel, string requestId,
            params object[] options)
            where TT : IHandleEventMessage
        {
            var theEvent = new LogEventInfo(logLevel, logger.Name, CultureInfo.DefaultThreadCurrentCulture,
                message.EventPublicMessage, options);

            if (string.IsNullOrEmpty(requestId)) requestId = Guid.NewGuid().ToString("N");

            theEvent.Properties.Add("RequestId", requestId);
            theEvent.Properties.Add("EventTypeId", message.EventId);
            theEvent.Properties.Add("EventPublicDetails", message.EventPublicDetails);

            logger.Log(theEvent);
        }
    }
}