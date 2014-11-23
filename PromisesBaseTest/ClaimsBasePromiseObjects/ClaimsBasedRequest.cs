using ProtoBuf;
using Termine.Promises.Interfaces;
using Termine.Promises.WithProtobuf.Interfaces;

namespace Termine.Promises.Base.Test.ClaimsBasePromiseObjects
{

    [ProtoContract]
    public class ClaimsBasedRequest: IAmAPromiseRequest, ISupportProtobuf
    {
        [ProtoMember(1)]
        public string RequestName { get; set; }
        
        [ProtoMember(2)]
        public string RequestId { get; set; }

        [ProtoMember(3)]
        public string Claim { get; set; }

        public void Init(string requestId)
        {
            RequestId = requestId;
            RequestName = GetType().FullName;
        }
    }
}