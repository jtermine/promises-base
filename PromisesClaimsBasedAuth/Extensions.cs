using System;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;
using System.Text;
using Termine.Promises.ClaimsBasedAuth.Base;
using Termine.Promises.ClaimsBasedAuth.Base.Interfaces;
using Termine.Promises.FluentValidation.Base;
using Termine.Promises.Interfaces;

// ReSharper disable once CheckNamespace
namespace Termine.Promises
{
    public static class Extensions
    {
        public static TX WithDefaultClaimsBasedAuth<TX, TW>(this TX promise)
            where TX : IAmAPromise<TW>
            where TW : class, ISupportClaims, new()
        {
            var authValidator = new PromiseActionInstance<TW>("claims.validator", workload =>
            {
                var supportClaimsValidator = new SupportClaimsValidator<TW>();
                var validationResult = supportClaimsValidator.Validate(workload);
                
                if (!validationResult.IsValid)
                promise.AbortOnAccessDenied(new FailedValidationEventMessage(validationResult.Errors));
            });

            promise.WithAuthChallenger(authValidator);

            var n = new PromiseActionInstance<TW>("claims.authenticator", workload =>
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
                        promise.AbortOnAccessDenied(ClaimsBasedMessages.JwtTokenIsNull);
                        return;
                    }

                    SecurityToken validatedToken;
                    workload.ClaimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, out validatedToken);
                }
                catch (Exception ex)
                {
                    promise.AbortOnAccessDenied(ex);
                }
            });

            return promise.WithAuthChallenger(n);
        }
    }
}