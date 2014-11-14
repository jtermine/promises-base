using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    public static class PromiseExtension
    {
        public static TX WithValidator<TX, TA, TW>(this TX promise, IAmAPromiseAction<TA, TW> validator)
            where TX : IAmAPromise<TA, TW>
            where TA : IAmAPromiseRequest, new()
            where TW : IAmAPromiseResponse, new()
        {
            if (string.IsNullOrEmpty(validator.ActionId) || validator.PromiseAction == default(Action<IPromise, PromiseWorkload<TA, TW>>)) return promise;

            if (promise.Context.Validators.ContainsKey(validator.ActionId)) return promise;
            promise.Context.Validators.Add(validator.ActionId, validator.PromiseAction);

            return promise;
        }

        public static TX WithAuthChallenger<TX, TA, TW>(this TX promise, IAmAPromiseAction<TA, TW> authChallenger)
            where TX : IAmAPromise<TA, TW>
            where TA : IAmAPromiseRequest, new()
            where TW : IAmAPromiseResponse, new()
        {
            if (string.IsNullOrEmpty(authChallenger.ActionId) || authChallenger.PromiseAction == default(Action<IPromise, PromiseWorkload<TA, TW>>)) return promise;

            if (promise.Context.AuthChallengers.ContainsKey(authChallenger.ActionId)) return promise;
            promise.Context.AuthChallengers.Add(authChallenger.ActionId, authChallenger.PromiseAction);

            return promise;
        }

        public static TX WithExecutor<TX, TA, TW>(this TX promise, IAmAPromiseAction<TA, TW> executor)
            where TX : IAmAPromise<TA, TW>
            where TA : IAmAPromiseRequest, new()
            where TW : IAmAPromiseResponse, new()
        {
            if (string.IsNullOrEmpty(executor.ActionId) || executor.PromiseAction == default(Action<IPromise, PromiseWorkload<TA, TW>>)) return promise;

            if (promise.Context.Executors.ContainsKey(executor.ActionId)) return promise;
            promise.Context.Executors.Add(executor.ActionId, executor.PromiseAction);

            return promise;
        }
    }
}
