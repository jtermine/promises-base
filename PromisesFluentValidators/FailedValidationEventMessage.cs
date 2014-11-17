using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;
using Termine.Promises.Generics;

namespace Termine.Promises.FluentValidation.Base
{
    public class FailedValidationEventMessage : GenericEventMessage
    {
        public FailedValidationEventMessage(IEnumerable<ValidationFailure> failuresList)
        {
            var sb = new StringBuilder();

            sb.Append("Validation Errors : [");

            foreach (var validationFailure in failuresList)
            {
                sb.Append(string.Format("{ {0}: prop: {1}, attempted value: {2} } ", validationFailure.ErrorMessage,
                    validationFailure.PropertyName, validationFailure.AttemptedValue == null ? "null": validationFailure.AttemptedValue.ToString()));
            }

            sb.Append("]");

            EventPublicMessage = "Validation errors found.";
            EventPublicDetails = sb.ToString();
        }
    }
}
