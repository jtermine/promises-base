using Termine.Promises.Interfaces;

namespace Termine.Promises.Base.Test.FalseLockPromiseObjects
{
    public class FalseLockWorkload: IAmAPromiseWorkload
    {
        public bool TerminateProcessing { get; set; }
    }
}
