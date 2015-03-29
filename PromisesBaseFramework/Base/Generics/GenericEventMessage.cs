using System;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Generics
{
    public class GenericEventMessage : IHandleEventMessage
    {
	    public int EventApplicationGroup { get; set; }
	    public EnumEventType EventType { get; set; }
	    public int MinorEventNumber { get; set; }
	    public int EventNumber { get; set; }
	    public string EventPublicMessage { get; set; }
        public string EventPublicDetails { get; set; }
        public bool IsPublicMessage { get; set; }

        public GenericEventMessage()
        {
            EventType = EnumEventType.Info;
        }

        public GenericEventMessage(int eventApplicationGroup, int eventId, string publicMessage)
        {
			EventType = EnumEventType.Info;
            EventNumber = eventId;
            EventPublicMessage = publicMessage;
	        EventApplicationGroup = eventApplicationGroup;
        }

        public GenericEventMessage(int eventApplicationGroup, Exception ex)
        {
			EventType = EnumEventType.Info;
			EventNumber = -1;
            EventPublicMessage = ex.Message;
            EventPublicDetails = ex.ToString();
			EventApplicationGroup = eventApplicationGroup;
		}

        public GenericEventMessage(int eventApplicationGroup, int eventId, string publicMessage, bool isPublicMessage =  false)
        {
			EventType = EnumEventType.Info;
			EventNumber = eventId;
            EventPublicMessage = publicMessage;
            IsPublicMessage = isPublicMessage;
			EventApplicationGroup = eventApplicationGroup;
		}

	    public static GenericEventMessage New(string message, EnumEventType eventType)
	    {
		    var genericEventMessage = new GenericEventMessage(0, -1, message, false) {EventType = eventType};
		    return genericEventMessage;
	    }
    }
}
