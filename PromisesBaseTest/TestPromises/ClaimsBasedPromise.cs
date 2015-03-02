using Termine.Promises.Base.Test.ClaimsBasePromiseObjects;
using Termine.Promises.Generics;
using Termine.Promises.NLogInstrumentation;
using Termine.Promises.WithREST;

namespace Termine.Promises.Base.Test.TestPromises
{
    public class ClaimsBasedPromise
    {
        public ClaimsBasedPromise()
        {
            var promise = new Promise<GenericConfig, ClaimsBasedWorkload, GenericRequest>();
            promise.WithRest().WithNLogInstrumentation();
        }
    }
}