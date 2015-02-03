using System;
using Termine.Promises.Interfaces;

namespace PromisesWithFileTransactionLog
{
    public class FileLogEntry: IHandleEventMessage
    {
        public DateTime DateTimeStamp { get; set; }
        public int EventCode { get; set; }
        public string Workload { get; set; }
        public int EventId { get; set; }
        public string EventPublicMessage { get; set; }
        public string EventPublicDetails { get; set; }
        public bool IsPublicMessage { get; set; }
    }
}
