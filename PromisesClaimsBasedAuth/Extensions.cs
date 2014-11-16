using System;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;
using System.Text;
using Termine.Promises.ClaimsBasedAuth.Base.Interfaces;
using Termine.Promises.Interfaces;

namespace Termine.Promises.ClaimsBasedAuth.Base
{
    public static class Extensions
    {
        public static TX WithClaimsBasedAuth<TX, TW>(this TX promise, IAmAPromiseAction<TW> authChallenger)
            where TX : IAmAPromise<TW>
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (string.IsNullOrEmpty(authChallenger.ActionId) || authChallenger.PromiseAction == null) return promise;

            if (promise.Context.AuthChallengers.ContainsKey(authChallenger.ActionId)) return promise;
            promise.Context.AuthChallengers.Add(authChallenger.ActionId, authChallenger.PromiseAction);

            return promise;
        }

        public static TX WithDefaultClaimsBasedAuthChallenger<TX, TW>(this TX promise)
            where TX : IAmAPromise<TW>
            where TW : class, ISupportClaims, new()
        {
            var n = new PromiseActionInstance<TW>("8ce3a9ff472740dc87895c15694c9ff4", workload =>
            {
                try
                {
                    var jwtToken = workload.Claim;

                    var tokenHandler = new JwtSecurityTokenHandler();
                    byte[] hmacKeyBytes = Encoding.UTF8.GetBytes(workload.HmacSigningKey);

                    var validationParameters = new TokenValidationParameters
                    {
                        ValidAudience = workload.HmacAudienceUri,
                        IssuerSigningToken = new BinarySecretSecurityToken(hmacKeyBytes),
                        ValidIssuer = workload.HmacIssuer,
                    };

                    if (string.IsNullOrEmpty(jwtToken))
                    {
                        promise.Error();
                        promise.AbortOnAccessDenied();

                        return;
                    }

                    SecurityToken validatedToken;
                    workload.ClaimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, out validatedToken);
                }
                catch (Exception ex)
                {
                    promise.AbortOnAccessDenied();
                }
            });

            return promise.WithAuthChallenger(n);
        }
    }
}