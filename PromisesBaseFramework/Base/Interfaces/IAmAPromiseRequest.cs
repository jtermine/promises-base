using FluentValidation;

namespace Termine.Promises.Base.Interfaces
{
    public interface IAmAPromiseRequest
    {
        string RequestId { get; set; }
        string RequestName { get; set; }
        bool ReturnLog { get; set; }
        IValidator GetValidator();        
    }
}