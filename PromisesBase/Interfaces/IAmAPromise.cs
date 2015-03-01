using System.Threading.Tasks;

namespace Termine.Promises.Interfaces
{
    public interface IAmAPromise <TW>: IHandlePromiseActions
        where TW : class, IAmAPromiseWorkload, new()
    {
        TW Workload { get; }
        Task<IAmAPromise<TW>> RunAsync();
        IAmAPromise<TW> Run();
        void WithWorkload(TW workload);

    }
}