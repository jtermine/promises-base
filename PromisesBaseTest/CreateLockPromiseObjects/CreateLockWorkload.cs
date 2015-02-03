using System.Security.Claims;
using Termine.Promises.ClaimsBasedAuth.Interfaces;
using Termine.Promises.Generics;

namespace Termine.Promises.Base.Test.CreateLockPromiseObjects
{
    public class CreateLockWorkload: GenericWorkload, ISupportClaims
    {
        public string Claim { get; private set; }
        public string HmacSigningKey { get; private set; }
        public string HmacAudienceUri { get; private set; }
        public string HmacIssuer { get; private set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
