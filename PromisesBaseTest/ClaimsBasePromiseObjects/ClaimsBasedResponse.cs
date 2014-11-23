using ProtoBuf;
using Termine.Promises.Interfaces;
using Termine.Promises.WithProtobuf.Interfaces;

namespace Termine.Promises.Base.Test.ClaimsBasePromiseObjects
{
    [ProtoContract]
    public class ClaimsBasedResponse: IAmAPromiseResponse, ISupportProtobuf
    {
        [ProtoMember(1)]
        public string ResponseMessage { get; set; }
    }
}
