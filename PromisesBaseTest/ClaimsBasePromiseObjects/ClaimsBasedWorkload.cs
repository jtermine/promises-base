using System.Security.Claims;
using Termine.Promises.ClaimsBasedAuth.Base.Interfaces;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Base.Test.ClaimsBasePromiseObjects
{
    public class ClaimsBasedWorkload: ISupportClaims
    {
        public bool TerminateProcessing { get; set; }
        public ClaimsBasedRequest Request { get; set; }
        public ClaimsBasedResponse Response { get; set; }
        public string Claim { get; private set; }
        public string HmacSigningKey { get; private set; }
        public string HmacAudienceUri { get; private set; }
        public string HmacIssuer { get; private set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
