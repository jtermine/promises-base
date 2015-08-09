using System;
using System.Windows.Forms;

namespace Termine.Promises.Base.Interfaces
{
	public interface IHandlePromiseEvents<out TC, out TW, out TR, out TE>
        where TC : IHandlePromiseConfig
        where TW : IAmAPromiseWorkload
        where TR : IAmAPromiseRequest
        where TE : IAmAPromiseResponse
    {
		string PromiseId { get; }
		string LoggerName { get; }
		string PromiseName { get; }

		void WithBlockHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control));
        void WithTraceHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control));
        void WithDebugHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control));
        void WithInfoHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control));
        void WithWarnHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control));
        void WithErrorHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control));
        void WithFatalHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control));
		void WithAbortHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control));
		void WithAbortOnAccessDeniedHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control));
		void WithSuccessHandler(string actionId, Action<IHandleEventMessage, IHandlePromiseActions, TC, TW, TR, TE> action, Control control = default(Control));
	}
}
