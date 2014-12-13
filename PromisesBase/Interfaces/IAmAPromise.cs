using System;
using System.Threading.Tasks;

namespace Termine.Promises.Interfaces
{
    public interface IAmAPromise <TW>: IDescribeAPromise, IHandlePromiseActions
        where TW : class, IAmAPromiseWorkload, new()
    {
        Promise<TW>.PromiseContext Context { get; }

        TW Workload { get; }

        Task<IAmAPromise<TW>> RunAsync();
        IAmAPromise<TW> Run();

    }
}

