using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Newtonsoft.Json;
using Termine.Promises.Generics;

namespace Termine.Promises.FluentValidation.Base
{
    public class FailedValidationEventMessage : GenericEventMessage
    {
        public FailedValidationEventMessage(IEnumerable<ValidationFailure> failuresList)
        {
            if (failuresList == null) return;

            var validationFailures = failuresList as ValidationFailure[] ?? failuresList.ToArray();

            var validationErrors = validationFailures.Select(f => new ValidationFailureModel
            {
                AttemptedValue = f.AttemptedValue == null ? "null" : f.AttemptedValue.ToString(),
                ErrorMessage = f.ErrorMessage,
                PropertyName = f.PropertyName
            });

            EventPublicMessage = "Validation errors found.";
            EventPublicDetails = JsonConvert.SerializeObject(validationErrors);
        }
    }
}
