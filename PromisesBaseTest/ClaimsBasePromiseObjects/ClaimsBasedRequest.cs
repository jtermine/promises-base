using Termine.Promises.Interfaces;

namespace Termine.Promises.Base.Test.ClaimsBasePromiseObjects
{
    public class ClaimsBasedRequest: IAmAPromiseRequest
    {
        public string Claim { get; set; }
    }
}