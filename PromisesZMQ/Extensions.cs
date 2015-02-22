using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        /// <param name="bus">an instance of the connected message bus</param>
        /// <returns>the instance of that promise with the RabbitMQ enabled</returns>
        public static Promise<TW> WithRabbitMQ<TW>(this Promise<TW> promise)
            where TW : class, IAmAPromiseWorkload, new()
        {

            promise.WithBlockHandler("rabbitMq.block",
                (w, m) => m.LogEvent(RabbitLogLevel.Trace, w));

            promise.WithTraceHandler("rabbitMq.trace",
                (w, m) => m.LogEvent(RabbitLogLevel.Trace, w));

            promise.WithDebugHandler("rabbitMq.debug",
                (w, m) => m.LogEvent(RabbitLogLevel.Debug, w));

            promise.WithInfoHandler("rabbitMq.info",
                (w, m) => m.LogEvent(RabbitLogLevel.Info, w));

            promise.WithWarnHandler("rabbitMq.warn",
                (w, m) => m.LogEvent(RabbitLogLevel.Warn, w));

            promise.WithErrorHandler("rabbitMq.error",
                (w, m) => m.LogEvent(RabbitLogLevel.Error, w));

            promise.WithFatalHandler("rabbitMq.fatal",
                (w, m) => m.LogEvent(RabbitLogLevel.Fatal, w));

            promise.WithAbortHandler("rabbitMq.abort",
                (w, m) => m.LogEvent(RabbitLogLevel.Info, w));

            promise.WithAbortOnAccessDeniedHandler("rabbitMq.abortAccessDenied",
                (w, m) => m.LogEvent(RabbitLogLevel.Info, w));

            promise.WithSuccessHandler("rabbitMq.success",
                aPromise => LogEvent(new GenericEventMessage(6, "Success"), RabbitLogLevel.Success, aPromise));

            return promise;
        }

        private static void LogEvent<TT, TW>(this TT message, RabbitLogLevel rabbitLogLevel, IAmAPromise<TW> promise)
            where TT : IHandleEventMessage
            where TW : class, IAmAPromiseWorkload, new()
        {

            var body = JsonConvert.SerializeObject(promise.Workload, new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
            
            var fs = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", DateTime.Now.ToString("O"), promise.PromiseId, message.EventId,
                message.EventPublicMessage, message.EventPublicDetails, message.IsPublicMessage, body);

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