using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    public class PromiseActionInstance<TT, TA, TW> : IAmAPromiseAction<TT, TA, TW> where TT : IAmAPromiseWorkload<TA, TW>, new() where TA : IAmAPromiseRequest, new() where TW : IAmAPromiseResponse, new()
    {
        public string ActionId { get; private set; }
        public Action<TT> PromiseAction { get; private set; }

        public PromiseActionInstance(string actionId, Action<TT> promiseAction)
        {
            ActionId = actionId;
            PromiseAction = promiseAction;
        }
    }
}
