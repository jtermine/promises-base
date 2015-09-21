using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    public sealed class GenericValidationFailure: IAmAValidationFailure
    {
        public GenericValidationFailure()
        {
            
        }

        /// <summary>
        /// Creates a new ValidationFailure.
        /// </summary>
        public GenericValidationFailure(string propertyName, string error, object attemptedValue = null)
        {
            PropertyName = propertyName;
            ErrorMessage = error;
            AttemptedValue = attemptedValue;
        }

        /// <summary>
        /// The name of the property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The property value that caused the failure.
        /// </summary>
        public object AttemptedValue { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Creates a textual representation of the failure.
        /// </summary>
        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}
