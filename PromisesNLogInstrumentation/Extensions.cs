using System.Globalization;
using NLog;
using Termine.Promises.Interfaces;

namespace Termine.Promises.NLogInstrumentation
{
    /// <summary>
    /// Implements extensions to promises related to NLog
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Enables NLog instrumentation on a promise
        /// </summary>
        /// <typeparam name="TW">any workload that implements IAmAPromiseWorkload which can be initialized with generic constructor -- new()</typeparam>
        /// <typeparam name="TC">any configuration that implements IHandlePromiseConfig which can be initialized with a generic constructor -- new()</typeparam>
        /// <typeparam name="TR">any request class the implments a IAmAPromiseRequest which can be initialized with a generic constructor -- new()</typeparam>
        /// <param name="promise">the promise object</param>
        /// <returns>the promise that NLog has been added to</returns>
        public static Promise<TC,TW, TR> WithNLogInstrumentation<TC, TW, TR>(this Promise<TC,TW, TR> promise)
            where TC : class, IHandlePromiseConfig, new()
            where TW : class, IAmAPromiseWorkload, new()
            where TR : class, IAmAPromiseRequest, new()
        {
            var loggerName = string.Format("{0}.{1}", typeof (TW).FullName, promise.PromiseId);

            var log = LogManager.GetLogger(loggerName);
            
            promise.WithBlockHandler("nlog.block",
                (w, m) => m.LogEvent(log, LogLevel.Trace, w));

            promise.WithTraceHandler("nlog.trace",
                (w, m) => m.LogEvent(log, LogLevel.Trace, w));

            promise.WithDebugHandler("nlog.debug",
                (w, m) => m.LogEvent(log, LogLevel.Debug, w));

            promise.WithInfoHandler("nlog.info",
                (w, m) => m.LogEvent(log, LogLevel.Info, w));

            promise.WithWarnHandler("nlog.warn",
                (w, m) => m.LogEvent(log, LogLevel.Warn, w));

            promise.WithErrorHandler("nlog.error",
                (w, m) => m.LogEvent(log, LogLevel.Error, w));

            promise.WithFatalHandler("nlog.fatal",
                (w, m) => m.LogEvent(log, LogLevel.Fatal, w));

            promise.WithAbortHandler("nlog.abort",
                (w, m) => m.LogEvent(log, LogLevel.Info, w));

            promise.WithAbortOnAccessDeniedHandler("nlog.abortAccessDenied",
                (w, m) => m.LogEvent(log, LogLevel.Info, w));

            return promise;
        }

        private static void LogEvent<TT>(this TT message, Logger logger, LogLevel logLevel, IHandlePromiseActions promise, params object[] options)
            where TT : IHandleEventMessage
        {
            var theEvent = new LogEventInfo(logLevel, logger.Name, CultureInfo.DefaultThreadCurrentCulture,
                message.EventPublicMessage, options);

            theEvent.Properties.Add("RequestId", promise.PromiseId);
            theEvent.Properties.Add("EventTypeId", message.EventId);
            theEvent.Properties.Add("EventPublicDetails", message.EventPublicDetails);

            logger.Log(theEvent);
        }
    }
}