using Termine.Promises.Generics;

namespace Termine.Promises
{
    public static class PromiseMessages
    {
        public static readonly GenericEventMessage PromiseStarted = new GenericEventMessage(0,
            "The promise started processing.");

        public static readonly GenericEventMessage PromiseStartTransmitting = new GenericEventMessage(2,
            "A web socket transmission has started against > {0}.");

        public static readonly GenericEventMessage PromiseStopTransmitting = new GenericEventMessage(3,
            "A web socket transmission has stopped.");

        public static readonly GenericEventMessage PromiseErrorTransmitting = new GenericEventMessage(4,
            "{0} [Error {1}]");

        public static readonly GenericEventMessage PromiseDelayOccurred = new GenericEventMessage(5,
            "The operation is taking longer than expected to complete.");

        public static readonly GenericEventMessage PromiseTimeoutOccurred = new GenericEventMessage(6,
            "The operation timed out and was aborted.");

        public static readonly GenericEventMessage PromiseReady = new GenericEventMessage(7,
            "Promise told its listening object to be in READY state.");

        public static readonly GenericEventMessage PromiseNotReady = new GenericEventMessage(8,
            "Promise told its listening object to be in NOT READY state.");

        public static readonly GenericEventMessage PromiseSuccess = new GenericEventMessage(9,
            "The operation was successful.");

        public static readonly GenericEventMessage PromiseFail = new GenericEventMessage(10,
            "The operation did not complete successfully.");

        public static readonly GenericEventMessage PromiseAccessDenied = new GenericEventMessage(13,
            "Access denied : The operation could not be completed.");


        public static readonly GenericEventMessage PromiseReceivedHttp200 = new GenericEventMessage(11,
            "The promise received an http response with code 200 OK.");

        public static readonly GenericEventMessage PromiseReceivedHttpError = new GenericEventMessage(12,
            "The promise received an http response corresponding to an an error code.  See body for details.");

        public static GenericEventMessage AuthChallengerStarted(string actionId)
        {
            return new GenericEventMessage
            {
                EventId = 100,
                EventPublicMessage = string.Format("AuthChallenger started {0}", actionId)
            };
        }

        public static GenericEventMessage AuthChallengerStopped(string actionId)
        {
            return new GenericEventMessage
            {
                EventId = 101,
                EventPublicMessage = string.Format("AuthChallenger stopped {0}", actionId)
            };
        }

        public static GenericEventMessage ValidatorStarted(string actionId)
        {
            return new GenericEventMessage
            {
                EventId = 100,
                EventPublicMessage = string.Format("Validator started {0}", actionId)
            };
        }

        public static GenericEventMessage ValidatorStopped(string actionId)
        {
            return new GenericEventMessage
            {
                EventId = 101,
                EventPublicMessage = string.Format("Validator stopped {0}", actionId)
            };
        }

        public static GenericEventMessage ExecutorStarted(string actionId)
        {
            return new GenericEventMessage
            {
                EventId = 100,
                EventPublicMessage = string.Format("Executor started {0}", actionId)
            };
        }

        public static GenericEventMessage ExecutorStopped(string actionId)
        {
            return new GenericEventMessage
            {
                EventId = 101,
                EventPublicMessage = string.Format("Executor stopped {0}", actionId)
            };
        }

    }
}
