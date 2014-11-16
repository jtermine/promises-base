namespace Termine.Promises.Interfaces
{
    public interface IHandleEventMessage
    {
        int EventId { get; set; }
        string EventPublicMessage { get; set; }
        string EventPublicDetails { get; set; }
        string EventPrivateMessage { get; set; }
        string EventPrivateDetails { get; set; }
    }
}
