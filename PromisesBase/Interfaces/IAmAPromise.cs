using System.Threading.Tasks;

namespace Termine.Promises.Interfaces
{
    public interface IAmAPromise <TC, TW, TR>: IHandlePromiseActions
        where TW : class, IAmAPromiseWorkload, new()
        where TC : class, IHandlePromiseConfig, new()
        where TR : class, IAmAPromiseRequest, new()
    {
        TC Config { get; }
        TW Workload { get; }
        TR Request { get; }
        Task<IAmAPromise<TC, TW, TR>> RunAsync();
        IAmAPromise<TC, TW, TR> Run();
        void WithWorkload(TW workload);
        void WithConfig(TC config);
        void WithRequest(TR request);
    }
}