using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    public static class Extensions
    {
        public static IAmAPromise<TW> WithRequestId<TW>(this IAmAPromise<TW> promise, string requestId)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (string.IsNullOrEmpty(requestId)) return promise;
            if (promise.Workload == null) return promise;

            promise.Workload.RequestId = requestId;

            return promise;
        }

        public static IAmAPromise<TW> WithValidator<TW>(this IAmAPromise<TW> promise, IAmAPromiseAction<TW> validator)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (string.IsNullOrEmpty(validator.ActionId) || validator.PromiseAction == null) return promise;

            if (promise.Context.Validators.ContainsKey(validator.ActionId)) return promise;
            promise.Context.Validators.Add(validator.ActionId, validator.PromiseAction);

            return promise;
        }

        public static IAmAPromise<TW> WithAuthChallenger<TW>(this IAmAPromise<TW> promise, IAmAPromiseAction<TW> authChallenger)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (string.IsNullOrEmpty(authChallenger.ActionId) || authChallenger.PromiseAction == null) return promise;

            if (promise.Context.AuthChallengers.ContainsKey(authChallenger.ActionId)) return promise;
            promise.Context.AuthChallengers.Add(authChallenger.ActionId, authChallenger.PromiseAction);

            return promise;
        }

        public static IAmAPromise<TW> WithExecutor<TW>(this IAmAPromise<TW> promise, IAmAPromiseAction<TW> executor)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (string.IsNullOrEmpty(executor.ActionId) || executor.PromiseAction == null) return promise;

            if (promise.Context.Executors.ContainsKey(executor.ActionId)) return promise;
            promise.Context.Executors.Add(executor.ActionId, executor.PromiseAction);

            return promise;
        }

        public static IAmAPromise<TW> WithBlockHandler<TW>(this IAmAPromise<TW> promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.BlockHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.BlockHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static IAmAPromise<TW> WithTraceHandler<TW>(this IAmAPromise<TW> promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.TraceHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.TraceHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static IAmAPromise<TW> WithDebugHandler<TW>(this IAmAPromise<TW> promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.DebugHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.DebugHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static IAmAPromise<TW> WithInfoHandler<TW>(this IAmAPromise<TW> promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.InfoHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.InfoHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static IAmAPromise<TW> WithWarnHandler<TW>(this IAmAPromise<TW> promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.WarnHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.WarnHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static IAmAPromise<TW> WithErrorHandler<TW>(this IAmAPromise<TW> promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.ErrorHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.ErrorHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static IAmAPromise<TW> WithFatalHandler<TW>(this IAmAPromise<TW> promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.FatalHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.FatalHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static IAmAPromise<TW> WithAbortHandler<TW>(this IAmAPromise<TW> promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.AbortHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.AbortHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static IAmAPromise<TW> WithAbortOnAccessDeniedHandler<TW>(this IAmAPromise<TW> promise, string eventHandlerKey,
            Action<TW, IHandleEventMessage> eventHandler)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.AbortOnAccessDeniedHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.AbortOnAccessDeniedHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static void HandleInstrumentationError<TW>(this IAmAPromise<TW> promise, Exception ex)
            where TW : class, IAmAPromiseWorkload, new()
        {

        }
    }
}
