using FluentValidation;

namespace PromisesBaseFrameworkTest.TestPromiseComponents
{
    public class TestPromiseV: AbstractValidator<TestPromiseRequest>
    {
        public TestPromiseV()
        {
            RuleFor(f => f.Name).NotEmpty();
        }
    }
}
