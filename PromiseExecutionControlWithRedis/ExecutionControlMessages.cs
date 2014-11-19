using Termine.Promises.Generics;

namespace Termine.Promises.ExectionControlWithRedis
{
    public static class ExecutionControlMessages
    {
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
