namespace Termine.Promises.Base.Interfaces
{
    public interface IAmAPromiseRequest
    {
        string RequestId { get; set; }
        string PromiseName { get; set; }
        string AppName { get; set; }
    }
}
