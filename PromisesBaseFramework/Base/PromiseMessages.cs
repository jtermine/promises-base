using Termine.Promises.Base.Generics;

namespace Termine.Promises.Base
{
    /// <summary>
    /// A collection of standard messages representing the state of promises.
    /// </summary>
    public static class PromiseMessages
    {
        /// <summary>
        /// The promise started processing.
        /// </summary>
        public static readonly GenericEventMessage PromiseStarted = new GenericEventMessage("The promise started processing.");

        /// <summary>
        /// A web socket transmission has started against {someIp}
        /// </summary>
        public static readonly GenericEventMessage PromiseStartTransmitting = new GenericEventMessage("A web socket transmission has started against > {0}.", 2);

        /// <summary>
        /// A web socket transmission has stopped.
        /// </summary>
        public static readonly GenericEventMessage PromiseStopTransmitting = new GenericEventMessage("A web socket transmission has stopped.", 3);

        /// <summary>
        /// {Error Message} [Error {number}]
        /// </summary>
        public static readonly GenericEventMessage PromiseErrorTransmitting = new GenericEventMessage("{0} [Error {1}]", 4);

        /// <summary>
        /// The operation is taking longer than expected to complete.
        /// </summary>
        public static readonly GenericEventMessage PromiseDelayOccurred = new GenericEventMessage("The operation is taking longer than expected to complete.", 5);

        /// <summary>
        /// The operation timed out and was aborted.
        /// </summary>
        public static readonly GenericEventMessage PromiseTimeoutOccurred = new GenericEventMessage("The operation timed out and was aborted.", 6);

        /// <summary>
        /// Promise told its listening object to be in READY state.
        /// </summary>
        public static readonly GenericEventMessage PromiseReady = new GenericEventMessage("Promise told its listening object to be in READY state.", 7);

        /// <summary>
        /// Promise told its listening object to be in NOT READY state.
        /// </summary>
        public static readonly GenericEventMessage PromiseNotReady = new GenericEventMessage("Promise told its listening object to be in NOT READY state.", 8);

        /// <summary>
        /// The operation was successful.
        /// </summary>
        public static readonly GenericEventMessage PromiseSuccess = new GenericEventMessage("The operation was successful.", 9);

        /// <summary>
        /// The operation did not complete successfully.
        /// </summary>
        public static readonly GenericEventMessage PromiseFail = new GenericEventMessage("The operation did not complete successfully.", 10);

        /// <summary>
        /// Access denied : The operation could not be completed.
        /// </summary>
        public static readonly GenericEventMessage PromiseAccessDenied = new GenericEventMessage("Access denied : The operation could not be completed.", 13);

        /// <summary>
        /// The promise received an http response with code 200 OK.
        /// </summary>
        public static readonly GenericEventMessage PromiseReceivedHttp200 = new GenericEventMessage("The promise received an http response with code 200 OK.", 11);

        /// <summary>
        /// The promise received an http response corresponding to an an error code.  See body for details.
        /// </summary>
        public static readonly GenericEventMessage PromiseReceivedHttpError = new GenericEventMessage("The promise received an http response corresponding to an an error code.  See body for details.", 12);

        /// <summary>
        /// The promise was blocked from processing.
        /// </summary>
        public static readonly GenericEventMessage PromiseBlocked = new GenericEventMessage("The promise was blocked from processing.", 14);

        /// <summary>
        /// The promise was aborted.
        /// </summary>
        public static readonly GenericEventMessage PromiseAborted = new GenericEventMessage("The promise was aborted.", 14);

        /// <summary>
        /// A promise action to be invoked on a control thread was skipped because no control handle was created.
        /// </summary>
        public static readonly GenericEventMessage PromiseActionSkippedNoHandle = new GenericEventMessage("A promise action to be invoked on a control thread was skipped because no control handle was created.  It may have been asynchronously invoked on a control that does not exist or may have been disposed.", 80);

        /// <summary>
        /// AuthChallenger started {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the authChallenger that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage AuthChallengerStarted(string actionId)
        {
            return new GenericEventMessage
            {
                EventNumber = 100,
                EventPublicMessage = $"AuthChallenger started {actionId}"
            };
        }

        /// <summary>
        /// AuthChallenger stopped {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the authChallenger that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage AuthChallengerStopped(string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 101,
                EventPublicMessage = $"AuthChallenger stopped {actionId}"
            };
        }

        /// <summary>
        /// Validator started {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the validator that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage ValidatorStarted(string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 100,
                EventPublicMessage = $"Validator started {actionId}"
            };
        }

        /// <summary>
        /// Validator started {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the validator that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage ValidatorStopped(string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 101,
                EventPublicMessage = $"Validator stopped {actionId}"
            };
        }

        /// <summary>
        /// Executor started {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the executor that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage ExecutorStarted(string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 100,
                EventPublicMessage = $"Executor started {actionId}"
            };
        }

        /// <summary>
        /// Executor stopped {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the executor that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage ExecutorStopped(string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 101,
                EventPublicMessage = $"Executor stopped {actionId}"
            };
        }

        /// <summary>
        /// XferAction started {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the executor that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage XferActionStarted(string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 100,
                EventPublicMessage = $"XferAction started {actionId}"
            };
        }

        /// <summary>
        /// XferAction stopped {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the executor that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage XferActionStopped(string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 101,
                EventPublicMessage = $"XferAction stopped {actionId}"
            };
        }

        /// <summary>
        /// SqlAction started {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the executor that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage SqlActionStarted(string actionId)
        {
            return new GenericEventMessage
            {
                EventNumber = 100,
                EventPublicMessage = $"SqlAction started {actionId}"
            };
        }

        /// <summary>
        /// SqlAction stopped {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the executor that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage SqlActionStopped(string actionId)
        {
            return new GenericEventMessage
            {
                EventNumber = 101,
                EventPublicMessage = $"SqlAction stopped {actionId}"
            };
        }

        /// <summary>
        /// PreStartAction started {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the PreStartAction that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage PreStartActionStarted(string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 102,
                EventPublicMessage = $"PreStartAction started {actionId}"
            };
        }

        /// <summary>
        /// PreStartAction stopped {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the PreStartAction that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage PreStartActionStopped(string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 103,
                EventPublicMessage = $"PreStartAction stopped {actionId}"
            };
        }

        /// <summary>
        /// PostEndAction started {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the PostEndAction that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage PostEndActionStarted(string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 102,
                EventPublicMessage = $"PostEndAction started {actionId}"
            };
        }

        /// <summary>
        /// PostEndAction stopped {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the PostEndAction that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage PostEndActionStopped(string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 103,
                EventPublicMessage = $"PostEndAction stopped {actionId}"
            };
        }

        /// <summary>
        /// SuccessAction started {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the SuccessAction that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage SuccessActionStarted(string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 104,
                EventPublicMessage = $"SuccessAction started {actionId}"
            };
        }

        /// <summary>
        /// SuccessAction stopped {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the SuccessAction that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage SuccessActionStopped(string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 105,
                EventPublicMessage = $"SuccessAction stopped {actionId}"
            };
        }

    }
}
