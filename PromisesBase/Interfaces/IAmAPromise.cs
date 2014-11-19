using System;

namespace Termine.Promises.Interfaces
{
    public interface IAmAPromise <TW>
        where TW : class, IAmAPromiseWorkload, new()
    {
        void Init();

        Promise<TW>.PromiseContext Context { get; }

        TW Workload { get; }

        int AuthChallengersCount { get; }
        int ValidatorsCount { get; }
        int ExecutorsCount { get; }

        void Block(IHandleEventMessage message);
        void Trace(IHandleEventMessage message);
        void Debug(IHandleEventMessage message);
        void Info(IHandleEventMessage message);
        void Warn(IHandleEventMessage message);
        void Error(IHandleEventMessage message);
        void Fatal(IHandleEventMessage message);
        void Abort(IHandleEventMessage message);
        void AbortOnAccessDenied(IHandleEventMessage message);

        void Block(Exception ex);
        void Trace(Exception ex);
        void Debug(Exception ex);
        void Info(Exception ex);
        void Warn(Exception ex);
        void Error(Exception ex);
        void Fatal(Exception ex);
        void Abort(Exception ex);
        void AbortOnAccessDenied(Exception ex);
        
    }
}
