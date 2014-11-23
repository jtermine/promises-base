namespace Termine.Promises.Interfaces
{
    public interface IAmAPromiseWorkload
    {
        string RequestId { get; set; }
        bool IsTerminated { get; set; }
        bool IsBlocked { get; set; }
    }
}
