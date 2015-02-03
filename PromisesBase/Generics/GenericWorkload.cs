using Termine.Promises.Interfaces;

namespace Termine.Promises.Generics
{
    public abstract class GenericWorkload : IAmAPromiseWorkload
    {
        public string RequestId { get; set; }
        public string RequestName { get; set; }
        public bool IsTerminated { get; set; }
        public bool IsBlocked { get; set; }
        public void WithRequestId(string requestId)
        {
            RequestId = requestId;
        }

        public virtual IAmAPromiseRequest GetRequest()
        {
            var genericRequest = new GenericRequest
            {
                RequestName = string.IsNullOrEmpty(RequestName) ? GetType().FullName : RequestName,
                RequestId = RequestId
            };
            
            return genericRequest;
        }
    }
}