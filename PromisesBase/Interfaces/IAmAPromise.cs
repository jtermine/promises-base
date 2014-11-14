namespace Termine.Promises.Interfaces
{
    public interface IAmAPromise <TA, TW>
        where TA : IAmAPromiseRequest, new()
        where TW : IAmAPromiseResponse, new()
    {
        Promise<TA, TW>.PromiseContext Context { get; }

        PromiseWorkload<TA, TW> Workload { get; }

        int AuthChallengersCount { get; }
        int ValidatorsCount { get; }
        int ExecutorsCount { get; }
    }
}
