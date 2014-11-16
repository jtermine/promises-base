using System;

namespace Termine.Promises.Interfaces
{
    public interface IAmAPromiseAction<in TW>
        where TW : IAmAPromiseWorkload, new()
    {
        string ActionId { get; }
        Action<TW> PromiseAction { get; }
    }
}
