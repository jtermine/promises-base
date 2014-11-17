namespace Termine.Promises.Interfaces
{
    public interface IAmAPromiseWorkload
    {
        string PromiseId { get; set; }
        bool TerminateProcessing { get; set; }
    }
}
