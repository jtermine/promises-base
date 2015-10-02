using FluentValidation;

namespace PromisesBaseFrameworkTest.GetResvPromise
{
    public class GetResvByIdV: AbstractValidator<GetResvByIdRq>
    {
        public GetResvByIdV()
        {
            RuleFor(rq => rq.ResvId).GreaterThan(0);
        }
    }
}
