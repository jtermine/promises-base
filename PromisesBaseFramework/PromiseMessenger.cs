using Termine.Promises.Base.Handlers;

namespace Termine.Promises
{
    public class PromiseMessenger
    {
        /// <summary>
        /// a collection of block handlers -- there are executed when a promise is blocked
        /// </summary>
        public PromiseHandlerQueue BlockHandlers { get; } = new PromiseHandlerQueue();

        /// <summary>
        /// a collection of trace handlers -- these are executed when a promise is tracing
        /// </summary>
        public PromiseHandlerQueue TraceHandlers { get; } = new PromiseHandlerQueue();

        /// <summary>
        /// a collection of debug handlers -- these are executed when a promise is reporting a debug event
        /// </summary>
        public PromiseHandlerQueue DebugHandlers { get; } = new PromiseHandlerQueue();

        /// <summary>
        /// a collection of info handlers -- these are executed when a promise is reporting an info event (e.g. system event log)
        /// </summary>
        public PromiseHandlerQueue InfoHandlers { get; } = new PromiseHandlerQueue();

        /// <summary>
        /// a collection of warn
        ///  handlers -- these are executed when a promise is tracing
        /// </summary>
        public PromiseHandlerQueue WarnHandlers { get; } = new PromiseHandlerQueue();

        /// <summary>
        /// a collection of error handlers -- these are executed when a promise is reporting an error event (e.g. system event log)
        /// </summary>
        public PromiseHandlerQueue ErrorHandlers { get; } = new PromiseHandlerQueue();

        /// <summary>
        /// a collection of error handlers -- these are executed when a promise is reporting a fatal event (e.g. system event log)
        /// </summary>
        public PromiseHandlerQueue FatalHandlers { get; } = new PromiseHandlerQueue();

        /// <summary>
        /// a collection of abort handlers -- these are executed when a promise is aborting for reasons other than AccessDenied
        /// </summary>
        public PromiseHandlerQueue AbortHandlers { get; } = new PromiseHandlerQueue();

        /// <summary>
        /// a collection of abort handlers -- these are executed when a promise is aborting because of AccessDenied condition
        /// </summary>
        public PromiseHandlerQueue AbortOnAccessDeniedHandlers { get; } = new PromiseHandlerQueue();

        /// <summary>
        /// a collection of handlers that fire when a promise executed successfully
        /// </summary>
        public PromiseHandlerQueue SuccessHandlers { get; } = new PromiseHandlerQueue();
    }
}
