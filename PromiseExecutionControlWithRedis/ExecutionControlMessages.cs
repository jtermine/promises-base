using Termine.Promises.Generics;

namespace Termine.Promises.ExectionControlWithRedis
{
    /// <summary>
    /// Contains messages raised by the REDIS execution control mechanism
    /// </summary>
    public static class ExecutionControlMessages
    {
        /// <summary>
        /// Raises when a promise with a given requestId has already been submitted
        /// </summary>
        /// <param name="requestId">the requestId of the promise</param>
        /// <returns>the message object</returns>
        public static GenericEventMessage PromiseIsADuplicate(string requestId)
        {
            return new GenericEventMessage
            {
                EventId = 600,
                EventPublicMessage =
                    string.Format(
                        "A promise with the requestId [{0}] has been blocked because it is a duplicate of a promise already submitted.",
                        requestId)
            };
        } 
    }
}
