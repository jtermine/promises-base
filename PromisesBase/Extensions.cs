using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    public static class Extensions
    {
        public static TX WithRequestId<TX, TW>(this TX promise, string requestId)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (string.IsNullOrEmpty(requestId)) return promise;

            promise.Workload.RequestId = requestId;

            return promise;
        }

        public static TX WithValidator<TX, TW>(this TX promise, IAmAPromiseAction<TW> validator)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (string.IsNullOrEmpty(validator.ActionId) || validator.PromiseAction == null) return promise;

            if (promise.Context.Validators.ContainsKey(validator.ActionId)) return promise;
            promise.Context.Validators.Add(validator.ActionId, validator.PromiseAction);

            return promise;
        }

        public static TX WithAuthChallenger<TX, TW>(this TX promise, IAmAPromiseAction<TW> authChallenger)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (string.IsNullOrEmpty(authChallenger.ActionId) || authChallenger.PromiseAction == null) return promise;

            if (promise.Context.AuthChallengers.ContainsKey(authChallenger.ActionId)) return promise;
            promise.Context.AuthChallengers.Add(authChallenger.ActionId, authChallenger.PromiseAction);

            return promise;
        }

        public static TX WithExecutor<TX, TW>(this TX promise, IAmAPromiseAction<TW> executor)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (string.IsNullOrEmpty(executor.ActionId) || executor.PromiseAction == null) return promise;

            if (promise.Context.Executors.ContainsKey(executor.ActionId)) return promise;
            promise.Context.Executors.Add(executor.ActionId, executor.PromiseAction);

            return promise;
        }

        public static TX WithBlockHandler<TX, TW>(this TX promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.BlockHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.BlockHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static TX WithTraceHandler<TX, TW>(this TX promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.TraceHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.TraceHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static TX WithDebugHandler<TX, TW>(this TX promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.DebugHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.DebugHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static TX WithInfoHandler<TX, TW>(this TX promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.InfoHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.InfoHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static TX WithWarnHandler<TX, TW>(this TX promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.WarnHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.WarnHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static TX WithErrorHandler<TX, TW>(this TX promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.ErrorHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.ErrorHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static TX WithFatalHandler<TX, TW>(this TX promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.FatalHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.FatalHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static TX WithAbortHandler<TX, TW>(this TX promise, string eventHandlerKey, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.AbortHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.AbortHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static TX WithAbortOnAccessDeniedHandler<TX, TW>(this TX promise, string eventHandlerKey,
            Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.AbortOnAccessDeniedHandlers.ContainsKey(eventHandlerKey)) return promise;

            promise.Context.AbortOnAccessDeniedHandlers.Add(eventHandlerKey, eventHandler);
            return promise;
        }

        public static void HandleInstrumentationError<TX, TW>(this TX promise, Exception ex)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {

        }
    }
}
