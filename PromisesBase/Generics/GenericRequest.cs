using Termine.Promises.Interfaces;

namespace Termine.Promises.Generics
{
    public class GenericRequest: IAmAPromiseRequest
    {
        public string RequestId { get; set; }
        public string PromiseName { get; set; }
        public string AppName { get; set; }
    }
}