using System;
using EasyNetQ;
using Newtonsoft.Json;
using Termine.Promises.Generics;
using Termine.Promises.Interfaces;

namespace Termine.Promises.ZMQ
{
    /// <summary>
    /// Implements extensions to promises related to NLog
    /// </summary>
    public static class Extensions
    {
        public static Promise<TW> WithRabbitMQ<TW>(this Promise<TW> promise)
            where TW : class, IAmAPromiseWorkload, new()
        {
            var bus = RabbitHutch.CreateBus("host=localhost");
            return promise.WithRabbitMQ(bus);
        }


        /// <summary>
        /// Adds support for broadcasting event states to RabbitMQ
        /// </summary>
        /// <typeparam name="TW">a type of promise workload (implemented IAmAWorkload)</typeparam>
        /// <param name="promise">a promise object</param>
        /// <param name="bus">an instance of the connected message bus</param>
        /// <returns>the instance of that promise with the RabbitMQ enabled</returns>
        public static Promise<TW> WithRabbitMQ<TW>(this Promise<TW> promise, IBus bus)
            where TW : class, IAmAPromiseWorkload, new()
        {

            promise.WithBlockHandler("rabbitMq.block",
                (w, m) => m.LogEvent(RabbitLogLevel.Trace, w, bus));

            promise.WithTraceHandler("rabbitMq.trace",
                (w, m) => m.LogEvent(RabbitLogLevel.Trace, w, bus));

            promise.WithDebugHandler("rabbitMq.debug",
                (w, m) => m.LogEvent(RabbitLogLevel.Debug, w, bus));

            promise.WithInfoHandler("rabbitMq.info",
                (w, m) => m.LogEvent(RabbitLogLevel.Info, w, bus));

            promise.WithWarnHandler("rabbitMq.warn",
                (w, m) => m.LogEvent(RabbitLogLevel.Warn, w, bus));

            promise.WithErrorHandler("rabbitMq.error",
                (w, m) => m.LogEvent(RabbitLogLevel.Error, w, bus));

            promise.WithFatalHandler("rabbitMq.fatal",
                (w, m) => m.LogEvent(RabbitLogLevel.Fatal, w, bus));

            promise.WithAbortHandler("rabbitMq.abort",
                (w, m) => m.LogEvent(RabbitLogLevel.Info, w, bus));

            promise.WithAbortOnAccessDeniedHandler("rabbitMq.abortAccessDenied",
                (w, m) => m.LogEvent(RabbitLogLevel.Info, w, bus));

            promise.WithSuccessHandler("rabbitMq.success",
                aPromise => LogEvent(new GenericEventMessage(6, "Success"), RabbitLogLevel.Success, aPromise, bus));

            return promise;
        }

        private static void LogEvent<TT, TW>(this TT message, RabbitLogLevel rabbitLogLevel, IAmAPromise<TW> promise, IBus bus)
            where TT : IHandleEventMessage
            where TW : class, IAmAPromiseWorkload, new()
        {
            var payload = new {rabbitId = Convert.ChangeType(rabbitLogLevel, rabbitLogLevel.GetTypeCode()), header = message, body = promise.Workload};
            bus.Send("my.queue", JsonConvert.SerializeObject(payload));
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