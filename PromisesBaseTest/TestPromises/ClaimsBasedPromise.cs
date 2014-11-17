using Termine.Promises.Base.Test.ClaimsBasePromiseObjects;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class ClaimsBasedPromise: Promise<ClaimsBasedWorkload>
    {
        public ClaimsBasedPromise()
        {
            this.WithNlogInstrumentation<ClaimsBasedPromise, ClaimsBasedWorkload>();
            this.WithDefaultClaimsBasedAuth<ClaimsBasedPromise, ClaimsBasedWorkload>();
        }
    }
}