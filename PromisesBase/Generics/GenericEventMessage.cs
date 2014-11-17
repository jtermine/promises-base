using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Generics
{
    public class GenericEventMessage : IHandleEventMessage
    {
        public int EventId { get; set; }
        public string EventPublicMessage { get; set; }
        public string EventPublicDetails { get; set; }
        public string EventPrivateMessage { get; set; }
        public string EventPrivateDetails { get; set; }

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
            EventPublicMessage = "An unhandled exception occurred.";
            EventPublicDetails =
                "Exception details are hidden from public view.  Please contact support to assistance with this problem.";
            EventPrivateMessage = ex.Message;
            EventPrivateDetails = ex.ToString();
        }

        public GenericEventMessage(int eventId, string publicMessage, string privateMessage)
        {
            EventId = eventId;
            EventPublicMessage = publicMessage;
            EventPrivateMessage = privateMessage;
        }
    }
}
