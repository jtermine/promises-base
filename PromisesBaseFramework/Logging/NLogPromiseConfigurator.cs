using System.Globalization;
using NLog;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Logging
{
	public class NLogPromiseConfigurator : IConfigurePromise
	{
		public void Configure(IHandlePromiseEvents promise)
		{
			var log = LogManager.GetLogger(promise.LoggerName);

			promise.WithBlockHandler("nlog.block",
				(w, m) => LogEvent(m, log, LogLevel.Trace, w));

			promise.WithTraceHandler("nlog.trace",
				(w, m) => LogEvent(m, log, LogLevel.Trace, w));

			promise.WithDebugHandler("nlog.debug",
				(w, m) => LogEvent(m, log, LogLevel.Debug, w));

			promise.WithInfoHandler("nlog.info",
				(w, m) => LogEvent(m, log, LogLevel.Info, w));

			promise.WithWarnHandler("nlog.warn",
				(w, m) => LogEvent(m, log, LogLevel.Warn, w));

			promise.WithErrorHandler("nlog.error",
				(w, m) => LogEvent(m, log, LogLevel.Error, w));

			promise.WithFatalHandler("nlog.fatal",
				(w, m) => LogEvent(m, log, LogLevel.Fatal, w));

			promise.WithAbortHandler("nlog.abort",
				(w, m) => LogEvent(m, log, LogLevel.Info, w));

			promise.WithAbortOnAccessDeniedHandler("nlog.abortAccessDenied",
				(w, m) => LogEvent(m, log, LogLevel.Info, w));
		}

		private static void LogEvent<TT>(TT message, Logger logger, LogLevel logLevel, IHandlePromiseActions promise, params object[] options)
			where TT : IHandleEventMessage
		{
			var theEvent = new LogEventInfo(logLevel, logger.Name, CultureInfo.DefaultThreadCurrentCulture,
				message.EventPublicMessage, options);

			theEvent.Properties.Add("RequestId", promise.PromiseId);

			var messageNumber = $"{message.EventNumber}.{message.MinorEventNumber}";

			theEvent.Properties.Add("EventTypeId", messageNumber);
			theEvent.Properties.Add("EventPublicDetails", message.EventPublicDetails);

			logger.Log(theEvent);
		}
	}
}
