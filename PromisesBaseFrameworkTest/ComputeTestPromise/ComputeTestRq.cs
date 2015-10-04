using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest.ComputeTestPromise
{
    public class ComputeTestRq : GenericRequest
    {
        public int StartNum { get; set; }
        public int Multiplier { get; set; }
    }
}