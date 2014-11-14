using Termine.Promises.Base.Test.ClaimsBasePromiseObjects;
using Termine.Promises.ClaimsBasedAuth.Base;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class ClaimsBasedPromise: Promise<ClaimsBaseWorkload, ClaimsBaseRequest, ClaimsBaseResponse>
    {
        public ClaimsBasedPromise()
        {
            this.WithDefaultClaimsBasedAuthChallenger();
        }
    }
}
