using System;
using Termine.Promises.ClaimsBasedAuth.Base.Interfaces;
using Termine.Promises.Interfaces;

namespace Termine.Promises.ClaimsBasedAuth.Base
{
    public static class Extensions
    {
        public static TX WithClaimsBasedAuth<TX, TA, TW>(this TX promise, IAmAPromiseAction<TA, TW> authChallenger)
            where TX : IAmAPromise<TA, TW>
            where TA : IAmAPromiseRequest, ISupportClaims, new()
            where TW : IAmAPromiseResponse, new()
        {
            if (string.IsNullOrEmpty(authChallenger.ActionId) || authChallenger.PromiseAction == default(Action<IPromise, IAmAPromise<TA, TW>>)) return promise;

            if (promise.Context.AuthChallengers.ContainsKey(authChallenger.ActionId)) return promise;
            promise.Context.AuthChallengers.Add(authChallenger.ActionId, authChallenger.PromiseAction);

            return promise;
        }

        public static IAmAPromise<TA,TW> WithDefaultClaimsBasedAuthChallenger<TA, TW>(this IAmAPromise<TA, TW> promise)
            where TA : IAmAPromiseRequest, ISupportClaims, new()
            where TW : IAmAPromiseResponse, new()
        {
            var n = new PromiseActionInstance<TA, TW>("8ce3a9ff472740dc87895c15694c9ff4", (methods, tt) =>
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
