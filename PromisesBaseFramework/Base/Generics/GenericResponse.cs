using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    public class GenericResponse: IAmAPromiseResponse
    {
        public int ResponseCode { get; set; }
    }
}
