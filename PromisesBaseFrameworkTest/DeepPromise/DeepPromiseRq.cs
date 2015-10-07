using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest.DeepPromise
{
    public class DeepPromiseRq : GenericRequest
    {
        public int StartNum { get; set; }
        public int Multiplier { get; set; }
    }
}