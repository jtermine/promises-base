using Termine.Promises.Interfaces;

namespace Termine.Promises.Generics
{
    public class GenericRequest: IAmAPromiseRequest
    {
        public string RequestId { get; set; }
        public string RequestName { get; set; }
    }
}
