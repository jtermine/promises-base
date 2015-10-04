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
        public static GenericEventMessage PromiseStarted(string promiseName)
        {
            return new GenericEventMessage
            {
                EventNumber = 100,
                EventPublicMessage = $"{promiseName} > The promise started processing."
            };
        }

        /// <summary>
        /// The operation was successful.
        /// </summary>
        public static GenericEventMessage PromiseSuccess(string promiseName)
        {
            return new GenericEventMessage
            {
                EventNumber = 9,
                EventPublicMessage = $"{promiseName} > The operation was successful."
            };
        }

        /// <summary>
        /// The operation did not complete successfully.
        /// </summary>
        public static GenericEventMessage PromiseFail(string promiseName)
        {
            return new GenericEventMessage
            {
                EventNumber = 10,
                EventPublicMessage = $"{promiseName} > The operation did not complete successfully."
            };
        }

        /// <summary>
        /// The promise was blocked from processing.
        /// </summary>
        public static GenericEventMessage PromiseBlocked(string promiseName)
        {
            return new GenericEventMessage
            {
                EventNumber = 14,
                EventPublicMessage = $"{promiseName} > The promise was blocked."
            };
        }

        /// <summary>
        /// The promise was aborted.
        /// </summary>
        /// 
        /// 
        public static GenericEventMessage PromiseAborted(string promiseName)
        {
            return new GenericEventMessage
            {
                EventNumber = 100,
                EventPublicMessage = $"{promiseName} > The promise was aborted."
            };
        }
        
        /// <summary>
        /// AuthChallenger started {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the authChallenger that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage AuthChallengerStarted(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
                EventNumber = 100,
                EventPublicMessage = $"{promiseName} > AuthChallenger started {actionId}"
            };
        }

        /// <summary>
        /// AuthChallenger stopped {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the authChallenger that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage AuthChallengerStopped(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 101,
                EventPublicMessage = $"{promiseName} > AuthChallenger stopped {actionId}"
            };
        }

        /// <summary>
        /// Validator started {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the validator that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage ValidatorStarted(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 100,
                EventPublicMessage = $"{promiseName} > Validator started {actionId}"
            };
        }

        /// <summary>
        /// Validator started {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the validator that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage ValidatorStopped(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 101,
                EventPublicMessage = $"{promiseName} > Validator stopped {actionId}"
            };
        }

        /// <summary>
        /// Executor started {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the executor that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage ExecutorStarted(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 100,
                EventPublicMessage = $"{promiseName} > Executor started {actionId}"
            };
        }

        /// <summary>
        /// Executor stopped {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the executor that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage ExecutorStopped(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 101,
                EventPublicMessage = $"{promiseName} > Executor stopped {actionId}"
            };
        }

        /// <summary>
        /// XferAction started {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the executor that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage XferActionStarted(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 100,
                EventPublicMessage = $"{promiseName} > XferAction started {actionId}"
            };
        }

        /// <summary>
        /// XferAction stopped {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the executor that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage XferActionStopped(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 101,
                EventPublicMessage = $"{promiseName} > XferAction stopped {actionId}"
            };
        }

        /// <summary>
        /// SqlAction started {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the executor that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage SqlActionStarted(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
                EventNumber = 100,
                EventPublicMessage = $"{promiseName} > SqlAction started {actionId}"
            };
        }

        /// <summary>
        /// SqlAction stopped {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the executor that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage SqlActionStopped(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
                EventNumber = 101,
                EventPublicMessage = $"{promiseName} > SqlAction stopped {actionId}"
            };
        }

        /// <summary>
        /// PreStartAction started {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the PreStartAction that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage PreStartActionStarted(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 102,
                EventPublicMessage = $"{promiseName} > PreStartAction started {actionId}"
            };
        }

        /// <summary>
        /// PreStartAction stopped {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the PreStartAction that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage PreStartActionStopped(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 103,
                EventPublicMessage = $"{promiseName} > PreStartAction stopped {actionId}"
            };
        }

        /// <summary>
        /// PostEndAction started {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the PostEndAction that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage PostEndActionStarted(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 102,
                EventPublicMessage = $"{promiseName} > PostEndAction started {actionId}"
            };
        }

        /// <summary>
        /// PostEndAction stopped {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the PostEndAction that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage PostEndActionStopped(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 103,
                EventPublicMessage = $"{promiseName} > PostEndAction stopped {actionId}"
            };
        }

        /// <summary>
        /// SuccessAction started {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the SuccessAction that started</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage SuccessActionStarted(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 104,
                EventPublicMessage = $"{promiseName} > SuccessAction started {actionId}"
            };
        }

        /// <summary>
        /// SuccessAction stopped {actionId}
        /// </summary>
        /// <param name="promiseName"></param>
        /// <param name="actionId">the actionId of the SuccessAction that stopped</param>
        /// <returns>an event message</returns>
        public static GenericEventMessage SuccessActionStopped(string promiseName, string actionId)
        {
            return new GenericEventMessage
            {
				EventNumber = 105,
                EventPublicMessage = $"{promiseName} > SuccessAction stopped {actionId}"
            };
        }

    }
}
