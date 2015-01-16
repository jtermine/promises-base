﻿using System;
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
        /// <typeparam name="TW">any workload that implements IAmAPromiseWorkload which is a class that can be initialized with new()</typeparam>
        /// <param name="promise">the promise object</param>
        /// <returns>the promise that NLog has been added to</returns>
        public static Promise<TW> WithNLogInstrumentation<TW>(this Promise<TW> promise)
            where TW : class, IAmAPromiseWorkload, new()
        {
            var loggerName = string.Format("{0}.{1}", typeof (TW).FullName, promise.PromiseId);

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