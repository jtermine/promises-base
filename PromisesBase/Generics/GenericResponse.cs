using Termine.Promises.Interfaces;

namespace Termine.Promises.Generics
{
    public class GenericResponse: IAmAPromiseResponse
    {
        public int ResponseCode { get; set; }
    }
}
