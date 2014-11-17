using Termine.Promises.Interfaces;

namespace Termine.Promises.Base.Test.FalseLockPromiseObjects
{
    public class FalseLockWorkload: IAmAPromiseWorkload
    {
        public string PromiseId { get; set; }
        public bool TerminateProcessing { get; set; }
    }
}
