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
    }
}
