namespace Termine.Promises.Base.Interfaces
{
    public interface IAmAPromiseResponse
    {
        string ResponseCode { get; set; }
        string ResponseDescription { get; set; }
        bool IsSuccess { get; set; }
    }
}
