using System.Runtime.Serialization;

namespace Termine.Promises.FluentValidation
{
    [DataContract]
    public class ValidationFailureModel
    {
        [DataMember(Name = "propertyName")]
        public string PropertyName { get; set; }

        [DataMember(Name = "errorMessage")]
        public string ErrorMessage { get; set; }

        [DataMember(Name = "attemptedValue")]
        public string AttemptedValue { get; set; }
    }
}
