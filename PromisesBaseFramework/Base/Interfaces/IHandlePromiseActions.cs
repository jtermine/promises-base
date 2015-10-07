using System;
using System.Threading;

namespace Termine.Promises.Base.Interfaces
{
	public interface IHandlePromiseActions
	{
        PromiseMessenger PromiseMessenger { get; }
        CancellationToken CancellationToken { get; }
		bool IsBlocked { get; }
		bool IsTerminated { get; }

		string SerializeWorkload();
		string SerializeRequest();
		string SerializeConfig();
		string SerializeResponse();

	    void DeserializeResponse(string json);

		string PromiseId { get; }
		string LoggerName { get; }
		string PromiseName { get; }
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
	    void Stop();

		void Block(Exception ex);
		void Trace(Exception ex);
		void Debug(Exception ex);
		void Info(Exception ex);
		void Warn(Exception ex);
		void Error(Exception ex);
		void Fatal(Exception ex);
		void Abort(Exception ex);
		void AbortOnAccessDenied(Exception ex);

        void Block(string message);
        void Trace(string message);
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);
        void Abort(string message);
        void AbortOnAccessDenied(string message);
        void Stop(string message);

	    void ThrowChaos();
	    void ThrowChaosOnConfig();
	    void ThrowChaosOnWorkload();
	    void ThrowChaosOnRequest();
	    void ThrowChaosOnResponse();

	}
}
