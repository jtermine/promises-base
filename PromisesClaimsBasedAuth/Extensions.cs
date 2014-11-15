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
        public static TX WithClaimsBasedAuth<TX, TA, TW>(this TX promise, IAmAPromiseAction<TA, TW> authChallenger)
            where TX : IAmAPromise<TA, TW>
            where TA : IAmAPromiseRequest, ISupportClaims, new()
            where TW : IAmAPromiseResponse, new()
        {
            if (string.IsNullOrEmpty(authChallenger.ActionId) || authChallenger.PromiseAction == null) return promise;

            if (promise.Context.AuthChallengers.ContainsKey(authChallenger.ActionId)) return promise;
            promise.Context.AuthChallengers.Add(authChallenger.ActionId, authChallenger.PromiseAction);

            return promise;
        }

        public static IAmAPromise<TA, TW> WithDefaultClaimsBasedAuthChallenger<TA, TW>(this IAmAPromise<TA, TW> promise, IConfigureClaims config)
            where TA : IAmAPromiseRequest, ISupportClaims, new()
            where TW : IAmAPromiseResponse, new()
        {
            var n = new PromiseActionInstance<TA, TW>("8ce3a9ff472740dc87895c15694c9ff4", (promiseMethods, tt) =>
            {
                var jwtToken = tt.Request.Claim;

                var tokenHandler = new JwtSecurityTokenHandler();
                byte[] hmacKeyBytes = Encoding.UTF8.GetBytes(config.HmacSigningKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidAudience = config.HmacAudienceUri,
                    IssuerSigningToken = new BinarySecretSecurityToken(hmacKeyBytes),
                    ValidIssuer = config.HmacIssuer,
                };

                if (string.IsNullOrEmpty(jwtToken))
                {
                    promiseMethods.Error();
                    promiseMethods.AbortOnAccessDenied();
                    
                    return;
                }
                    
                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, out validatedToken);

                // TODO: DO something with the claims principal.
                
            });

            return promise.WithAuthChallenger(n);
        }
    }
}