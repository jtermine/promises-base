using System;
using System.Globalization;
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
            var log = LogManager.GetLogger(typeof(TX).FullName);

            promise.WithTraceHandler("nlog.trace",
                new Action<TW, IHandleEventMessage>(
                    (w, m) => m.LogEvent(log, LogLevel.Trace, w.PromiseId, m)));

            promise.WithDebugHandler("nlog.debug",
                new Action<TW, IHandleEventMessage>(
                    (w, m) => m.LogEvent(log, LogLevel.Debug, w.PromiseId, m)));

            promise.WithInfoHandler("nlog.info",
                new Action<TW, IHandleEventMessage>(
                    (w, m) => m.LogEvent(log, LogLevel.Info, w.PromiseId, m)));

            promise.WithWarnHandler("nlog.warn",
                new Action<TW, IHandleEventMessage>(
                    (w, m) => m.LogEvent(log, LogLevel.Warn, w.PromiseId, m)));

            promise.WithErrorHandler("nlog.error",
                new Action<TW, IHandleEventMessage>(
                    (w, m) => m.LogEvent(log, LogLevel.Error, w.PromiseId, m)));

            promise.WithFatalHandler("nlog.fatal",
                new Action<TW, IHandleEventMessage>(
                    (w, m) => m.LogEvent(log, LogLevel.Fatal, w.PromiseId, m)));

            promise.WithAbortHandler("nlog.abort",
                new Action<TW, IHandleEventMessage>(
                    (w, m) => m.LogEvent(log, LogLevel.Info, w.PromiseId, m)));

            promise.WithAbortOnAccessDeniedHandler("nlog.abortAccessDenied",
                new Action<TW, IHandleEventMessage>(
                    (w, m) => m.LogEvent(log, LogLevel.Info, w.PromiseId, m)));
            
            return promise;
        }

        private static void LogEvent<TT>(this TT message, Logger logger, LogLevel logLevel, string promiseId, params object[] options)
            where TT : IHandleEventMessage
        {
            var theEvent = new LogEventInfo(logLevel, logger.Name, CultureInfo.DefaultThreadCurrentCulture, message.EventPublicMessage, options);

            if (string.IsNullOrEmpty(promiseId)) promiseId = Guid.NewGuid().ToString("N");

            theEvent.Properties.Add("RequestId", promiseId);
            theEvent.Properties.Add("EventTypeId", message.EventId);
            theEvent.Properties.Add("EventPublicDetails", message.EventPublicDetails);

            logger.Log(theEvent);
        }
    }
}