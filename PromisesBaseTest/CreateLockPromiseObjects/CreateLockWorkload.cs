using System.Security.Claims;
using Termine.Promises.ClaimsBasedAuth.Base.Interfaces;

namespace Termine.Promises.Base.Test.CreateLockPromiseObjects
{
    public class CreateLockWorkload: ISupportClaims
    {
        public string PromiseId { get; set; }
        public bool IsTerminated { get; set; }
        public bool IsBlocked { get; set; }
        public string Claim { get; private set; }
        public string HmacSigningKey { get; private set; }
        public string HmacAudienceUri { get; private set; }
        public string HmacIssuer { get; private set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
