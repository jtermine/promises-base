using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    public class GenericRequest: IAmAPromiseRequest
    {
        public string RequestId { get; set; }
        public string RequestName { get; set; }

	    public GenericRequest()
	    {
		    RequestName = "_genericRequest";
	    }
    }
}