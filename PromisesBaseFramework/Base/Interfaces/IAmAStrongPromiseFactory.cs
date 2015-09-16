namespace Termine.Promises.Base.Interfaces
{
    public interface IAmAStrongPromiseFactory<TR, TX> : IAmAPromiseFactory
        where TR : IAmAPromiseRequest
        where TX : IAmAPromiseResponse
    {
        TX Run(TR request);
    }
}
