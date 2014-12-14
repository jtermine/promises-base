using System;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;
using System.Text;
using Termine.Promises.ClaimsBasedAuth.Base;
using Termine.Promises.ClaimsBasedAuth.Base.Interfaces;
using Termine.Promises.FluentValidation.Base;

// ReSharper disable once CheckNamespace
namespace Termine.Promises
{
    public static class Extensions
    {
        public static Promise<TW> WithDefaultClaimsBasedAuth<TW>(this Promise<TW> promise) where TW : class, ISupportClaims, new()
        {
            var authValidator = new Action<TW>(workload =>
            {
                var supportClaimsValidator = new SupportClaimsValidator<TW>();
                var validationResult = supportClaimsValidator.Validate(workload);
                
                if (!validationResult.IsValid)
                promise.AbortOnAccessDenied(new FailedValidationEventMessage(validationResult.Errors));
            });

            promise.WithAuthChallenger("claims.validator", authValidator);

            var n = new Action<TW>( workload =>
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

            return promise.WithAuthChallenger("claims.authenticator", n);
        }
    }
}