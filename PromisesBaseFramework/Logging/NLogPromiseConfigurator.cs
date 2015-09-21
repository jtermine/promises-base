using System.Globalization;
using NLog;
using Termine.Promises.Base;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Logging
{
    public class NLogPromiseConfigurator : IConfigurePromise
    {
        public void Configure(IHandlePromiseEvents promise)
        {
            var logger = LogManager.GetLogger(promise.LoggerName);

            promise.WithBlockHandler("nlog.block", func => LogEvent(func, logger, LogLevel.Trace));

            promise.WithTraceHandler("nlog.trace", func => LogEvent(func, logger, LogLevel.Trace));

            promise.WithDebugHandler("nlog.debug", func => LogEvent(func, logger, LogLevel.Debug));

            promise.WithInfoHandler("nlog.info", func => LogEvent(func, logger, LogLevel.Info));

            promise.WithWarnHandler("nlog.warn", func => LogEvent(func, logger, LogLevel.Warn));

            promise.WithErrorHandler("nlog.error", func => LogEvent(func, logger, LogLevel.Error));

            promise.WithFatalHandler("nlog.fatal", func => LogEvent(func, logger, LogLevel.Fatal));

            promise.WithAbortHandler("nlog.abort", func => LogEvent(func, logger, LogLevel.Trace));

            promise.WithAbortOnAccessDeniedHandler("nlog.abortAccessDenied",
                func => LogEvent(func, logger, LogLevel.Trace));

        }

        private static Resp LogEvent(PromiseMessageFunc promiseMessageFunc, Logger logger, LogLevel logLevel, params object[] options)
        {
            var theEvent = new LogEventInfo(logLevel, logger.Name, CultureInfo.DefaultThreadCurrentCulture,
                promiseMessageFunc.M.EventPublicMessage, options);

            theEvent.Properties.Add("RequestId", promiseMessageFunc.P.PromiseId);

            var messageNumber = $"{promiseMessageFunc.M.EventNumber}.{promiseMessageFunc.M.MinorEventNumber}";

            theEvent.Properties.Add("EventTypeId", messageNumber);
            theEvent.Properties.Add("EventPublicDetails", promiseMessageFunc.M.EventPublicDetails);

            logger.Log(theEvent);

            return Resp.Success();
        }
    }
}