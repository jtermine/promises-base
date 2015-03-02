using Termine.Promises.Interfaces;

namespace Termine.Promises.Generics
{
    public abstract class GenericWorkload : IAmAPromiseWorkload
    {
        public string RequestId { get; set; }
        public string PromiseName { get; set; }
        public string AppName { get; set; }
        public bool IsTerminated { get; set; }
        public bool IsBlocked { get; set; }
        
    }
}