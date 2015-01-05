using FluentValidation;
using Termine.Promises.ClaimsBasedAuth.Interfaces;

namespace Termine.Promises.ClaimsBasedAuth
{
    public class SupportClaimsValidator<TT>: AbstractValidator<TT>
        where TT: class, ISupportClaims
    {
        public SupportClaimsValidator()
        {
            RuleFor(f => f.HmacAudienceUri).NotEmpty().WithMessage("HmacAudienceUri cannot be empty in the promise workload.");
            RuleFor(f => f.Claim).NotEmpty().WithMessage("Claim cannot be empty in the promise workload.");
            RuleFor(f => f.HmacIssuer).NotEmpty().WithMessage("HmacIssuer cannot be empty in the promise workload.");
            RuleFor(f => f.HmacSigningKey).NotEmpty().WithMessage("HmacSigningKey cannot be empty in the promise workload.");
        }
    }
}
