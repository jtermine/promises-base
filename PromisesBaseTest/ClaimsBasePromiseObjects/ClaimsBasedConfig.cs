using Termine.Promises.ClaimsBasedAuth.Base.Interfaces;

namespace Termine.Promises.Base.Test.ClaimsBasePromiseObjects
{
    public class ClaimsBasedConfig: IConfigureClaims
    {
        public string HmacSigningKey { get; private set; }
        public string HmacAudienceUri { get; private set; }
        public string HmacIssuer { get; private set; }

        public ClaimsBasedConfig()
        {
            HmacSigningKey = "1";
            HmacAudienceUri = "audience";
            HmacIssuer = "issuer";
        }
    }
}
