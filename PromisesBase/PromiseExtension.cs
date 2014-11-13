using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    public static class PromiseExtension
    {
        public static TX WithValidator<TX, TT, TA, TW>(this TX promise, IAmAPromiseAction<TT, TA, TW> validator)
            where TX : IAmAPromise<TT, TA, TW>
            where TT : IAmAPromiseWorkload<TA, TW>, new()
            where TA : IAmAPromiseRequest, new()
            where TW : IAmAPromiseResponse, new()
        {
            if (string.IsNullOrEmpty(validator.ActionId) || validator.PromiseAction == default(Action<IHavePromiseMethods, TT>)) return promise;

            if (promise.Context.Validators.ContainsKey(validator.ActionId)) return promise;
            promise.Context.Validators.Add(validator.ActionId, validator.PromiseAction);

            return promise;
        }

        public static TX WithAuthChallenger<TX, TT, TA, TW>(this TX promise, IAmAPromiseAction<TT, TA, TW> authChallenger)
            where TX : IAmAPromise<TT, TA, TW>
            where TT : IAmAPromiseWorkload<TA, TW>, new()
            where TA : IAmAPromiseRequest, new()
            where TW : IAmAPromiseResponse, new()
        {
            if (string.IsNullOrEmpty(authChallenger.ActionId) || authChallenger.PromiseAction == default(Action<IHavePromiseMethods, TT>)) return promise;

            if (promise.Context.AuthChallengers.ContainsKey(authChallenger.ActionId)) return promise;
            promise.Context.AuthChallengers.Add(authChallenger.ActionId, authChallenger.PromiseAction);

            return promise;
        }

        public static TX WithExecutor<TX, TT, TA, TW>(this TX promise, IAmAPromiseAction<TT, TA, TW> executor)
            where TX : IAmAPromise<TT, TA, TW>
            where TT : IAmAPromiseWorkload<TA, TW>, new()
            where TA : IAmAPromiseRequest, new()
            where TW : IAmAPromiseResponse, new()
        {
            if (string.IsNullOrEmpty(executor.ActionId) || executor.PromiseAction == default(Action<IHavePromiseMethods, TT>)) return promise;

            if (promise.Context.Executors.ContainsKey(executor.ActionId)) return promise;
            promise.Context.Executors.Add(executor.ActionId, executor.PromiseAction);

            return promise;
        }
    }
}
