using System.Threading.Tasks;

namespace Termine.Promises.Interfaces
{
    public interface IAmAPromise <TC, TW, TR, TE>: IHandlePromiseActions
        where TW : class, IAmAPromiseWorkload, new()
        where TC : class, IHandlePromiseConfig, new()
        where TR : class, IAmAPromiseRequest, new()
        where TE: class, IAmAPromiseResponse, new()
    {

        #pragma warning disable 108,114
        bool IsBlocked { set; }
        bool IsTerminated { set; }
        #pragma warning restore 108,114

        TC Config { get; }
        TW Workload { get; }
        TR Request { get; }
        TE Response { get; }
        Task<IAmAPromise<TC, TW, TR, TE>> RunAsync();
        IAmAPromise<TC, TW, TR, TE> Run();
        void WithWorkload(TW workload);
        void WithConfig(TC config);
        void WithRequest(TR request);
        void WithResponse(TE response);
        IAmAPromise<TC, TW, TR, TE> DeserializeWorkload(string json);
        IAmAPromise<TC, TW, TR, TE> DeserializeRequest(string json);
        IAmAPromise<TC, TW, TR, TE> DeserializeConfig(string json);
        IAmAPromise<TC, TW, TR, TE> DeserializeResponse(string json);
    }
}