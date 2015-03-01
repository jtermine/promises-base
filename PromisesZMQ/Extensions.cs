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
        /// <typeparam name="TW">a type of promise workload (implemented IAmAWorkload)</typeparam>
        /// <param name="promise">a promise object</param>
        /// <returns>the instance of that promise with the RabbitMQ enabled</returns>
        public static Promise<TW> WithRabbitMQ<TW>(this Promise<TW> promise)
            where TW : class, IAmAPromiseWorkload, new()
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
                message.EventPublicMessage, message.EventPublicDetails, message.IsPublicMessage, promise.SerializeJson());

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