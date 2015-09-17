using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base
{
    public class PromiseOptions<TR, TU>
        where TR: IAmAPromiseRequest
        where TU: IAmAPromiseUser
    {
        public TR Request { get; set; }
        public TU GenericUser { get; set; }

        public PromiseOptions()
        {
            
        }

        public PromiseOptions(TR request, TU genericUser)
        {
            Request = request;
            GenericUser = genericUser;
        }

        public PromiseOptions(TR request)
        {
            Request = request;
        }

        public PromiseOptions(TU genericUser)
        {
            GenericUser = genericUser;
        }
    }
}