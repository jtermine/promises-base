using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    public class PromiseActionInstance<TW> : IAmAPromiseAction<TW> where TW : class, IAmAPromiseWorkload, new()
    {
        public string ActionId { get; private set; }
        public Action<TW> PromiseAction { get; private set; }

        public PromiseActionInstance(string actionId, Action<TW> promiseAction)
        {
            ActionId = actionId;
            PromiseAction = promiseAction;
        }
    }
}
