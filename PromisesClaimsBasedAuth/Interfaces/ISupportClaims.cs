using System.Security.Claims;
using Termine.Promises.Interfaces;

namespace Termine.Promises.ClaimsBasedAuth.Interfaces
{
    public interface ISupportClaims: IAmAPromiseWorkload
    {
        string Claim { get; }
        string HmacSigningKey { get; }
        string HmacAudienceUri { get; }
        string HmacIssuer { get; }

        ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
