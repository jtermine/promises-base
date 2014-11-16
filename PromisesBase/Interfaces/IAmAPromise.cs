namespace Termine.Promises.Interfaces
{
    public interface IAmAPromise <TW>
        where TW : class, IAmAPromiseWorkload, new()
    {
        Promise<TW>.PromiseContext Context { get; }

        TW Workload { get; }

        int AuthChallengersCount { get; }
        int ValidatorsCount { get; }
        int ExecutorsCount { get; }

        void Trace();
        void Debug();
        void Info();
        void Warn();
        void Error();
        void Fatal();
        void Abort();
        void AbortOnAccessDenied();
    }
}
