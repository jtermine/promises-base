using Termine.Promises.Base.Test.ClaimsBasePromiseObjects;
using Termine.Promises.NLogInstrumentation;
using Termine.Promises.WithREST;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class ClaimsBasedPromise: Promise<ClaimsBasedWorkload>
    {
        public ClaimsBasedPromise()
        {
            this.WithRest();
            this.WithNLogInstrumentation();
        }
    }
}