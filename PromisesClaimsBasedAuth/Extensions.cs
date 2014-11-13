using System;
using Termine.Promises.ClaimsBasedAuth.Interfaces;
using Termine.Promises.Interfaces;

namespace Termine.Promises.ClaimsBasedAuth
{
    public static class Extensions
    {
        public static TX WithClaimsBasedAuth<TX, TT, TA, TW>(this TX promise, IAmAPromiseAction<TT, TA, TW> authChallenger)
            where TX : IAmAPromise<TT, TA, TW>
            where TT : IAmAPromiseWorkload<TA, TW>, new()
            where TA : IAmAPromiseRequest, ISupportClaims, new()
            where TW : IAmAPromiseResponse, new()
        {
            if (string.IsNullOrEmpty(authChallenger.ActionId) || authChallenger.PromiseAction == default(Action<IHavePromiseMethods, TT>)) return promise;

            if (promise.Context.AuthChallengers.ContainsKey(authChallenger.ActionId)) return promise;
            promise.Context.AuthChallengers.Add(authChallenger.ActionId, authChallenger.PromiseAction);

            return promise;
        }

        public static IAmAPromise<TT, TA, TW> WithDefaultClaimsBasedAuthChallenger<TT, TA, TW>(this IAmAPromise<TT, TA, TW> promise)
            where TT : IAmAPromiseWorkload<TA, TW>, new()
            where TA : IAmAPromiseRequest, ISupportClaims, new()
            where TW : IAmAPromiseResponse, new()
        {
            var n = new PromiseActionInstance<TT, TA, TW>("8ce3a9ff472740dc87895c15694c9ff4", (methods, tt) =>
            {
                var claim = tt.Request.Claim;
                if (!string.IsNullOrEmpty(claim)) return;

                methods.Fatal();
                methods.AbortOnAccessDenied();
            });

            return promise.WithAuthChallenger(n);
        }

    }
}
