using Termine.Promises.Interfaces;

namespace Termine.Promises.Generics
{
    public class GenericWorkload : IAmAPromiseWorkload
    {
        public string RequestId { get; set; }
        public bool IsTerminated { get; set; }
        public bool IsBlocked { get; set; }
    }
}
