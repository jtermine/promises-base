using FluentValidation;

namespace PromisesBaseFrameworkTest.TestPromiseComponents
{
    public class TestPromiseV: AbstractValidator<TestPromiseRq>
    {
        public TestPromiseV()
        {
            RuleFor(f => f.Name).NotEmpty();
        }
    }
}
