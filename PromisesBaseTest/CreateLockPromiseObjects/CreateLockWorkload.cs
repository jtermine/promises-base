using Termine.Promises.Interfaces;

namespace Termine.Promises.Base.Test.CreateLockPromiseObjects
{
    public class CreateLockWorkload: IAmAPromiseWorkload
    {
        public bool TerminateProcessing { get; set; }
    }
}
