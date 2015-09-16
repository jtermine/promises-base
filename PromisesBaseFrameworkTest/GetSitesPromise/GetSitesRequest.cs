using FluentValidation;
using Termine.Promises.Base.Generics;

namespace PromisesBaseFrameworkTest.GetSitesPromise
{
    public class GetSitesRequest: GenericRequest
    {
        public int TestInt { get; set; }

        public override IValidator GetValidator()
        {
            return new GetSitesRequestValidator();
        }
    }

    public class GetSitesRequestValidator : AbstractValidator<GetSitesRequest>
    {
        public GetSitesRequestValidator()
        {
            RuleFor(rq => rq.TestInt).GreaterThan(0).WithMessage("Does not have a testInt set.");
        }
    }
}
