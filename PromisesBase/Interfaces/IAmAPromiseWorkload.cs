namespace Termine.Promises.Interfaces
{
    public interface IAmAPromiseWorkload
    {
        string PromiseId { get; set; }
        bool IsTerminated { get; set; }
        bool IsBlocked { get; set; }
    }
}
