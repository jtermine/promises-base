using FluentValidation;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    public class GenericAuthenticator<TT>: AbstractValidator<TT> where TT: IAmAPromiseUser
    {
        
    }
}