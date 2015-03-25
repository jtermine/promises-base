using System;

namespace Termine.Promises.Base.Interfaces
{
	public interface IHandlePromiseEvents
	{
		string PromiseId { get; }
		string LoggerName { get; }

		void WithBlockHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action);
        void WithTraceHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action);
        void WithDebugHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action);
        void WithInfoHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action);
        void WithWarnHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action);
        void WithErrorHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action);
        void WithFatalHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action);
		void WithAbortHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action);
		void WithAbortOnAccessDeniedHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action);
		void WithSuccessHandler(string actionId, Action<IHandlePromiseActions, IHandleEventMessage> action);
	}
}
