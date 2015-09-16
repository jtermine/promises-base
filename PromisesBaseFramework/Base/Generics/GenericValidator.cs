using FluentValidation;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    public class GenericValidator<TT>: AbstractValidator<TT> where TT: IAmAPromiseRequest
    {
        
    }
}
