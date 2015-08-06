using System.Net;

namespace Termine.Promises.Base.Interfaces
{
    public interface IAmAPromiseFactory
    {
        HttpStatusCode ReturnHttpStatusCode { get; }
        string ReturnHttpMessage { get; }
        string Run(string json);
    }
}
