using System;
using System.IdentityModel.Tokens;
using System.Net.Mime;
using System.ServiceModel.Security.Tokens;
using System.Text;
using Termine.Promises.ClaimsBasedAuth.Interfaces;
using Termine.Promises.FluentValidation;
using Termine.Promises.Interfaces;

namespace Termine.Promises.ClaimsBasedAuth
{
    public static class Extensions
    {
        public static Promise<TC, TW, TR, TE> WithDefaultClaimsBasedAuth<TC, TW, TR, TE>(this Promise<TC, TW, TR, TE> promise)
            where TC : class, IHandlePromiseConfig, new()
            where TW : class, ISupportClaims, new()
            where TR : class, IAmAPromiseRequest, new()
            where TE: class, IAmAPromiseResponse, new()
        {
            var authValidator = new Action<IHandlePromiseActions, TC, TW, TR, TE>((promiseActions, config, workload, request, response) =>
            {
                var supportClaimsValidator = new SupportClaimsValidator<TW>();
                var validationResult = supportClaimsValidator.Validate(workload);

                if (!validationResult.IsValid)
                    promise.AbortOnAccessDenied(new FailedValidationEventMessage(validationResult.Errors));
            });

            promise.WithAuthChallenger("claims.validator", authValidator);

            var n = new Action<IHandlePromiseActions, TC, TW, TR, TE>((promiseActions, config, workload, request, response) =>
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
                    workload.ClaimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters,
                        out validatedToken);
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