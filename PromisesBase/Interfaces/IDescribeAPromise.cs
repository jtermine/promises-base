namespace Termine.Promises.Interfaces
{
    public interface IDescribeAPromise
    {
        string PromiseId { get; }
        int AuthChallengersCount { get; }
        int ValidatorsCount { get; }
        int ExecutorsCount { get; }
    }
}
