using Termine.Promises.Generics;

namespace Termine.Promises
{
    /// <summary>
    /// A collection of standard messages representing the state of promises.
    /// </summary>
    public static class PromiseMessages
    {
        /// <summary>
        /// The promise started processing.
        /// </summary>
        public static readonly GenericEventMessage PromiseStarted = new GenericEventMessage(0,
            "The promise started processing.");

        /// <summary>
        /// A web socket transmission has started against {someIp}
        /// </summary>
        public static readonly GenericEventMessage PromiseStartTransmitting = new GenericEventMessage(2,
            "A web socket transmission has started against > {0}.");

        /// <summary>
        /// A web socket transmission has stopped.
        /// </summary>
        public static readonly GenericEventMessage PromiseStopTransmitting = new GenericEventMessage(3,
            "A web socket transmission has stopped.");

        /// <summary>
        /// {Error Message} [Error {number}]
        /// </summary>
        public static readonly GenericEventMessage PromiseErrorTransmitting = new GenericEventMessage(4,
            "{0} [Error {1}]");

        /// <summary>
        /// The operation is taking longer than expected to complete.
        /// </summary>
        public static readonly GenericEventMessage PromiseDelayOccurred = new GenericEventMessage(5,
            "The operation is taking longer than expected to complete.");

        /// <summary>
        /// The operation timed out and was aborted.
        /// </summary>
        public static readonly GenericEventMessage PromiseTimeoutOccurred = new GenericEventMessage(6,
            "The operation timed out and was aborted.");

        /// <summary>
        /// Promise told its listening object to be in READY state.
        /// </summary>
        public static readonly GenericEventMessage PromiseReady = new GenericEventMessage(7,
            "Promise told its listening object to be in READY state.");

        /// <summary>
        /// Promise told its listening object to be in NOT READY state.
        /// </summary>
        public static readonly GenericEventMessage PromiseNotReady = new GenericEventMessage(8,
            "Promise told its listening object to be in NOT READY state.");

        /// <summary>
        /// The operation was successful.
        /// </summary>
        public static readonly GenericEventMessage PromiseSuccess = new GenericEventMessage(9,
            "The operation was successful.");

        /// <summary>
        /// The operation did not complete successfully.
        /// </summary>
        public static readonly GenericEventMessage PromiseFail = new GenericEventMessage(10,
            "The operation did not complete successfully.");

        /// <summary>
        /// Access denied : The operation could not be completed.
        /// </summary>
        public static readonly GenericEventMessage PromiseAccessDenied = new GenericEventMessage(13,
            "Access denied : The operation could not be completed.");

        /// <summary>
        /// The promise received an http response with code 200 OK.
        /// </summary>
        public static readonly GenericEventMessage PromiseReceivedHttp200 = new GenericEventMessage(11,
            "The promise received an http response with code 200 OK.");

        /// <summary>
        /// The promise received an http response corresponding to an an error code.  See body for details.
        /// </summary>
        public static readonly GenericEventMessage PromiseReceivedHttpError = new GenericEventMessage(12,
            "The promise received an http response corresponding to an an error code.  See body for details.");

        /// <summary>
        /// The promise was blocked from processing.
        /// </summary>
        public static readonly GenericEventMessage PromiseBlocked = new GenericEventMessage(14,
            "The promise was blocked from processing.");

        /// <summary>
        /// AuthChallenger started {actionId}
        /// </summary>
        /// <param name="actionId">the actionId of the authChallenger that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage AuthChallengerStarted(string actionId)
        {
            return new GenericEventMessage
            {
                EventId = 100,
                EventPublicMessage = string.Format("AuthChallenger started {0}", actionId)
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
                EventId = 101,
                EventPublicMessage = string.Format("AuthChallenger stopped {0}", actionId)
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
                EventId = 100,
                EventPublicMessage = string.Format("Validator started {0}", actionId)
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
                EventId = 101,
                EventPublicMessage = string.Format("Validator stopped {0}", actionId)
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
                EventId = 100,
                EventPublicMessage = string.Format("Executor started {0}", actionId)
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
                EventId = 101,
                EventPublicMessage = string.Format("Executor stopped {0}", actionId)
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
                EventId = 100,
                EventPublicMessage = string.Format("XferAction started {0}", actionId)
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
                EventId = 101,
                EventPublicMessage = string.Format("XferAction stopped {0}", actionId)
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
                EventId = 102,
                EventPublicMessage = string.Format("PreStartAction started {0}", actionId)
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
                EventId = 103,
                EventPublicMessage = string.Format("PreStartAction stopped {0}", actionId)
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
                EventId = 102,
                EventPublicMessage = string.Format("PostEndAction started {0}", actionId)
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
                EventId = 103,
                EventPublicMessage = string.Format("PostEndAction stopped {0}", actionId)
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
                EventId = 104,
                EventPublicMessage = string.Format("SuccessAction started {0}", actionId)
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
                EventId = 105,
                EventPublicMessage = string.Format("SuccessAction stopped {0}", actionId)
            };
        }

    }
}
