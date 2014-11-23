namespace Termine.Promises.Interfaces
{
    public interface IAmAPromiseRequest
    {
        string RequestId { get; set; }
        string RequestName { get; set; }
        void Init(string requestId);
    }
}
