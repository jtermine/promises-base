using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    public static class PromiseExtension
    {
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

        public static TX WithTraceHandler<TX, TW>(this TX promise, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.TraceHandlers.Contains(eventHandler)) return promise;

            promise.Context.TraceHandlers.Add(eventHandler);
            return promise;
        }

        public static TX WithDebugHandler<TX, TW>(this TX promise, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.DebugHandlers.Contains(eventHandler)) return promise;

            promise.Context.DebugHandlers.Add(eventHandler);
            return promise;
        }

        public static TX WithInfoHandler<TX, TW>(this TX promise, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.InfoHandlers.Contains(eventHandler)) return promise;

            promise.Context.InfoHandlers.Add(eventHandler);
            return promise;
        }

        public static TX WithWarnHandler<TX, TW>(this TX promise, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.WarnHandlers.Contains(eventHandler)) return promise;

            promise.Context.WarnHandlers.Add(eventHandler);
            return promise;
        }

        public static TX WithErrorHandler<TX, TW>(this TX promise, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.ErrorHandlers.Contains(eventHandler)) return promise;

            promise.Context.ErrorHandlers.Add(eventHandler);
            return promise;
        }

        public static TX WithFatalHandler<TX, TW>(this TX promise, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.FatalHandlers.Contains(eventHandler)) return promise;

            promise.Context.FatalHandlers.Add(eventHandler);
            return promise;
        }

        public static TX WithAbortHandler<TX, TW>(this TX promise, Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.AbortHandlers.Contains(eventHandler)) return promise;

            promise.Context.AbortHandlers.Add(eventHandler);
            return promise;
        }

        public static TX WithAbortOnAccessDeniedHandler<TX, TW>(this TX promise,
            Action<TW, IHandleEventMessage> eventHandler)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (eventHandler == null) return promise;
            if (promise.Context.AbortOnAccessDeniedHandlers.Contains(eventHandler)) return promise;

            promise.Context.AbortOnAccessDeniedHandlers.Add(eventHandler);
            return promise;
        }

        public static void HandleInstrumentationError<TX, TW>(this TX promise, Exception ex)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {

        }
    }
}
