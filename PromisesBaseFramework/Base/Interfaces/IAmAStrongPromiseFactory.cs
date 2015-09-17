using System.Net;

namespace Termine.Promises.Base.Interfaces
{
    public interface IAmAStrongPromiseFactory<in TR, TX, in TU>
        where TR : IAmAPromiseRequest
        where TX : IAmAPromiseResponse
        where TU : IAmAPromiseUser
    {
        TX Run(TR request, TU user);
        HttpStatusCode ReturnHttpStatusCode { get; }
        string ReturnHttpMessage { get; }
    }
}
