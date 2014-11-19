namespace Termine.Promises.Interfaces
{
    public interface IHandleEventMessage
    {
        int EventId { get; set; }
        string EventPublicMessage { get; set; }
        string EventPublicDetails { get; set; }
        bool IsPublicMessage { get; set; }
    }
}
