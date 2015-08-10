using System.Globalization;
using NLog;
using Termine.Promises.Base.Generics;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Logging
{
	public class NLogPromiseConfigurator : IConfigurePromise<GenericConfig, GenericWorkload, GenericRequest, GenericResponse>
    {
		public void Configure(IHandlePromiseEvents<GenericConfig, GenericWorkload, GenericRequest, GenericResponse> promise)
		{
			var log = LogManager.GetLogger(promise.LoggerName);

			promise.WithBlockHandler("nlog.block",
				(m, p, c, w, rq, rx) => LogEvent(m, log, LogLevel.Trace, p));

			promise.WithTraceHandler("nlog.trace",
				(m, p, c, w, rq, rx) => LogEvent(m, log, LogLevel.Trace, p));

			promise.WithDebugHandler("nlog.debug",
				(m, p, c, w, rq, rx) => LogEvent(m, log, LogLevel.Debug, p));

			promise.WithInfoHandler("nlog.info",
				(m, p, c, w, rq, rx) => LogEvent(m, log, LogLevel.Info, p));

			promise.WithWarnHandler("nlog.warn",
				(m, p, c, w, rq, rx) => LogEvent(m, log, LogLevel.Warn, p));

			promise.WithErrorHandler("nlog.error",
				(m, p, c, w, rq, rx) => LogEvent(m, log, LogLevel.Error, p));

			promise.WithFatalHandler("nlog.fatal",
				(m, p, c, w, rq, rx) => LogEvent(m, log, LogLevel.Fatal, p));

			promise.WithAbortHandler("nlog.abort",
				(m, p, c, w, rq, rx) => LogEvent(m, log, LogLevel.Info, p));

			promise.WithAbortOnAccessDeniedHandler("nlog.abortAccessDenied",
				(m, p, c, w, rq, rx) => LogEvent(m, log, LogLevel.Info, p));
		}

		private static void LogEvent<TT>(TT message, ILoggerBase logger, LogLevel logLevel, IHandlePromiseActions promise, params object[] options)
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
