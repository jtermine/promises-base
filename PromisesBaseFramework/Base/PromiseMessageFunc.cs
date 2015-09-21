using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base
{
    public class PromiseMessageFunc
    {
        public PromiseMessageFunc() { }

        public PromiseMessageFunc(IHandlePromiseActions p, IHandleEventMessage m)
        {
            M = m;
            P = p;
        }

        public IHandleEventMessage M { get; set; }
        public IHandlePromiseActions P { get; set; }
    }
}
