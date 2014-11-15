using Termine.Promises.Base.Test.ClaimsBasePromiseObjects;
using Termine.Promises.ClaimsBasedAuth.Base;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class ClaimsBasedPromise: Promise<ClaimsBaseRequest, ClaimsBaseResponse>
    {
        public ClaimsBasedPromise()
        {
            var config = new ClaimsBasedConfig();
            this.WithDefaultClaimsBasedAuthChallenger(config);
        }
    }
}
