using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base
{
    public class PromiseOptions<TR, TU>
        where TR: IAmAPromiseRequest
        where TU: IAmAPromiseUser
    {
        public TR Request { get; set; }
        public TU GenericUser { get; set; }
        public PromiseMessenger Messenger { get; set; }

        public PromiseOptions()
        {
        }

        public PromiseOptions(TR request, TU genericUser, PromiseMessenger messenger = default(PromiseMessenger))
        {
            Request = request;
            GenericUser = genericUser;
            Messenger = messenger;
        }

        public PromiseOptions(TR request, PromiseMessenger messenger = default(PromiseMessenger))
        {
            Messenger = messenger;
            Request = request;
        }

        public PromiseOptions( TU genericUser, PromiseMessenger messenger = default(PromiseMessenger))
        {
            Messenger = messenger;
            GenericUser = genericUser;
        }

        public PromiseOptions(PromiseMessenger messenger = default(PromiseMessenger))
        {
            Messenger = messenger;
        }
    }
}