using System;

namespace Termine.Promises.Interfaces
{
    public interface IAmAPromiseAction<in TT, TA, TW>
        where TT : IAmAPromiseWorkload<TA, TW>, new()
        where TA : IAmAPromiseRequest, new()
        where TW : IAmAPromiseResponse, new()
    {
        string ActionId { get; }
        Action<IHavePromiseMethods, TT> PromiseAction { get; }
    }
}
