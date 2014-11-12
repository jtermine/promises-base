namespace Termine.Promises.Interfaces
{
    public interface IAmAPromise <TT, TA, TW>
        where TT : IAmAPromiseWorkload<TA, TW>, new()
        where TA : IAmAPromiseRequest, new()
        where TW : IAmAPromiseResponse, new()
    {
    }
}
