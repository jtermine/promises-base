using System;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    public class GenericEventMessage : IHandleEventMessage
    {
	    public int MinorEventNumber { get; set; }
	    public int EventNumber { get; set; }
	    public string EventPublicMessage { get; set; }
        public string EventPublicDetails { get; set; }
        public bool IsSensitiveMessage { get; set; }
        public bool IsFailure { get; set; }

        public GenericEventMessage()
        {
        }

        public GenericEventMessage(Exception ex, int eventId = -1)
        {
			EventNumber = eventId;
            EventPublicMessage = ex.Message;
            EventPublicDetails = ex.ToString();
		}

        public GenericEventMessage(string publicMessage, int eventId = 0, bool isSensitiveMessage = false )
        {
			EventNumber = eventId;
            EventPublicMessage = publicMessage;
            IsSensitiveMessage = isSensitiveMessage;
		}
    }
}