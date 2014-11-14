using System;

namespace Termine.Promises.Interfaces
{
    public interface IAmAPromiseAction<TA, TW>
        where TA : IAmAPromiseRequest, new()
        where TW : IAmAPromiseResponse, new()
    {
        string ActionId { get; }
        Action<IPromise, PromiseWorkload<TA, TW>> PromiseAction { get; }
    }
}
