using System;
using System.Globalization;
using Termine.Promises.Generics;
using Termine.Promises.Interfaces;

namespace Termine.Promises.ZMQ
{
    /// <summary>
    /// Implements extensions to promises related to NLog
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds support for broadcasting event states to RabbitMQ
        /// </summary>
        /// <typeparam name="TW">any workload that implements IAmAPromiseWorkload which can be initialized with generic constructor -- new()</typeparam>
        /// <typeparam name="TC">any configuration that implements IHandlePromiseConfig which can be initialized with a generic constructor -- new()</typeparam>
        /// <typeparam name="TR">any request class the implments a IAmAPromiseRequest which can be initialized with a generic constructor -- new()</typeparam>
        /// <typeparam name="TE">any response class the implments a IAmAPromiseResponse which can be initialized with a generic constructor -- new()</typeparam>
        /// <param name="promise">a promise object</param>
        /// <returns>the instance of that promise with the RabbitMQ enabled</returns>
        public static Promise<TC, TW, TR, TE> WithRabbitMQ<TC, TW, TR, TE>(this Promise<TC, TW, TR, TE> promise)
            where TC : class, IHandlePromiseConfig, new()
            where TW : class, IAmAPromiseWorkload, new()
            where TR : class, IAmAPromiseRequest, new()
            where TE : class, IAmAPromiseResponse, new()
        {

            promise.WithBlockHandler("rabbitMq.block",
                (p, m) => m.LogEvent(RabbitLogLevel.Trace, p));

            promise.WithTraceHandler("rabbitMq.trace",
                (p, m) => m.LogEvent(RabbitLogLevel.Trace, p));

            promise.WithDebugHandler("rabbitMq.debug",
                (p, m) => m.LogEvent(RabbitLogLevel.Debug, p));

            promise.WithInfoHandler("rabbitMq.info",
                (p, m) => m.LogEvent(RabbitLogLevel.Info, p));

            promise.WithWarnHandler("rabbitMq.warn",
                (p, m) => m.LogEvent(RabbitLogLevel.Warn, p));

            promise.WithErrorHandler("rabbitMq.error",
                (p, m) => m.LogEvent(RabbitLogLevel.Error, p));

            promise.WithFatalHandler("rabbitMq.fatal",
                (p, m) => m.LogEvent(RabbitLogLevel.Fatal, p));

            promise.WithAbortHandler("rabbitMq.abort",
                (p, m) => m.LogEvent(RabbitLogLevel.Info, p));

            promise.WithAbortOnAccessDeniedHandler("rabbitMq.abortAccessDenied",
                (p, m) => m.LogEvent(RabbitLogLevel.Info, p));

            promise.WithSuccessHandler("rabbitMq.success",
                (p, m) => LogEvent(new GenericEventMessage(6, "Success"), RabbitLogLevel.Success, p));

            return promise;
        }

        private static void LogEvent<TT, TW>(this TT message, RabbitLogLevel rabbitLogLevel, TW promise)
            where TT : IHandleEventMessage
            where TW : class, IHandlePromiseActions
        {

            var fs = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", DateTime.Now.ToString("O"), promise.PromiseId, message.EventId,
                message.EventPublicMessage, message.EventPublicDetails, message.IsPublicMessage, promise.SerializeWorkload());

            RabbitMqServiceBus.Instance.Publish(fs, message.EventId.ToString(CultureInfo.InvariantCulture));
        }

        private enum RabbitLogLevel
        {
            Trace = 0,
            Debug = 1,
            Info = 2,
            Warn = 3,
            Error = 4,
            Fatal = 5,
            Success = 6
        }
    }
}