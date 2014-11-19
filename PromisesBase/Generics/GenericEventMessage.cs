using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Generics
{
    public class GenericEventMessage : IHandleEventMessage
    {
        public int EventId { get; set; }
        public string EventPublicMessage { get; set; }
        public string EventPublicDetails { get; set; }
        public bool IsPublicMessage { get; set; }

        public GenericEventMessage()
        {
            
        }

        public GenericEventMessage(int eventId, string publicMessage)
        {
            EventId = eventId;
            EventPublicMessage = publicMessage;
        }

        public GenericEventMessage(Exception ex)
        {
            EventId = -1;
            EventPublicMessage = ex.Message;
            EventPublicDetails = ex.ToString();
        }

        public GenericEventMessage(int eventId, string publicMessage, bool isPublicMessage =  false)
        {
            EventId = eventId;
            EventPublicMessage = publicMessage;
            IsPublicMessage = isPublicMessage;
        }
    }
}
