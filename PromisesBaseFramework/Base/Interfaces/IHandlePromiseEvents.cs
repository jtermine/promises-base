using System;

namespace Termine.Promises.Base.Interfaces
{
	public interface IHandlePromiseEvents
    {
		string PromiseId { get; }
		string LoggerName { get; }
		string PromiseName { get; }

        void WithUserMessageHandler(string actionId, Func<PromiseMessageFunc, Resp> action);
        void WithBlockHandler(string actionId, Func<PromiseMessageFunc, Resp> action);
        void WithTraceHandler(string actionId, Func<PromiseMessageFunc, Resp> action);
        void WithDebugHandler(string actionId, Func<PromiseMessageFunc, Resp> action);
        void WithInfoHandler(string actionId, Func<PromiseMessageFunc, Resp> action);
        void WithWarnHandler(string actionId, Func<PromiseMessageFunc, Resp> action);
        void WithErrorHandler(string actionId, Func<PromiseMessageFunc, Resp> action);
        void WithFatalHandler(string actionId, Func<PromiseMessageFunc, Resp> action);
		void WithAbortHandler(string actionId, Func<PromiseMessageFunc, Resp> action);
		void WithAbortOnAccessDeniedHandler(string actionId, Func<PromiseMessageFunc, Resp> action);
		void WithSuccessHandler(string actionId, Func<PromiseMessageFunc, Resp> action);
	}
}
