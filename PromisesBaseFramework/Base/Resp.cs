using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base
{
    public class Resp: IHandleEventMessage
    {
        public bool IsFailure { get; set; }
        public int MinorEventNumber { get; set; }
        public int EventNumber { get; set; }
        public string EventPublicMessage { get; set; }
        public string EventPublicDetails { get; set; }
        public bool IsSensitiveMessage { get; set; }

        public static Resp Success() { return new Resp(); }

        public static Resp Failure()
        {
            return new Resp { IsFailure = true };
        }

        public static Resp Abort(IHandleEventMessage message)
        {
            return new Resp
            {
                IsFailure = true,
                EventPublicMessage = message.EventPublicMessage,
                EventNumber = message.EventNumber,
                EventPublicDetails = message.EventPublicDetails,
                IsSensitiveMessage = message.IsSensitiveMessage,
                MinorEventNumber = message.MinorEventNumber
            };
        }

        public static Resp Abort(string message)
        {
            return new Resp
            {
                IsFailure = true,
                EventPublicMessage = message
            };
        }

        public static Resp AbortOnAccessDenied(string message)
        {
            return new Resp
            {
                IsFailure = true,
                EventPublicMessage = message
            };
        }
    }
}