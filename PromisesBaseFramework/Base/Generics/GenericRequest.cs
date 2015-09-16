using FluentValidation;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    public class GenericRequest: IAmAPromiseRequest
    {
	    public GenericRequest()
	    {
		    var typeName = GetType().Name;
		    RequestName = $"{typeName.Substring(0, 1).ToLowerInvariant()}{typeName.Substring(1, typeName.Length-1)}" ;
	    }

	    public string RequestId { get; set; }
	    public string RequestName { get; set; }

        public virtual IValidator GetValidator()
        {
            return new GenericValidator<GenericRequest>();
        }
    }
}