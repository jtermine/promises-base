using Termine.Promises.ClaimsBasedAuth.Base.Interfaces;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Base.Test.ClaimsBasePromiseObjects
{
    public class ClaimsBaseRequest: IAmAPromiseRequest, ISupportClaims
    {
        public string Claim { get; set; }
    }
}
