using System.Globalization;
using NLog;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Logging
{
	public class NLogPromiseConfigurator<TC, TW, TR, TE> : IConfigurePromise<TC, TW, TR, TE>
        where TC : IHandlePromiseConfig
        where TW : IAmAPromiseWorkload
        where TR : IAmAPromiseRequest
        where TE : IAmAPromiseResponse
    {
		public void Configure(IHandlePromiseEvents<TC, TW, TR, TE> promise)
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
