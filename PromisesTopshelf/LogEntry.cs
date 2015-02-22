using System;

namespace PromisesTopshelf
{
    public class LogEntry
    {
        public DateTime DateTime { get; set; }
        public string PromiseId { get; set; }
        public int EventId { get; set; }
        public string EventMessage { get; set; }
        public string EventDetails { get; set; }
        public bool IsPublicMessage { get; set; }
        public string Body { get; set; }
    }
}