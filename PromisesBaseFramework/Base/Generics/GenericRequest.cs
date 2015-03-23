using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    public class GenericRequest: IAmAPromiseRequest
    {
        public string RequestId { get; set; }
        public string PromiseName { get; set; }
        public string AppName { get; set; }
    }
}