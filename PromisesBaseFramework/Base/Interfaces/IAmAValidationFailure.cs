namespace Termine.Promises.Base.Interfaces
{
    public interface IAmAValidationFailure
    {
        string PropertyName { get; set; }

        string ErrorMessage { get; set; }

        object AttemptedValue { get; set; }

        string ErrorCode { get; set; }


    }
}