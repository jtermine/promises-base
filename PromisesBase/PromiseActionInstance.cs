using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    public class PromiseActionInstance<TA, TW> : IAmAPromiseAction<TA, TW> where TA : IAmAPromiseRequest, new() where TW : IAmAPromiseResponse, new()
    {
        public string ActionId { get; private set; }
        public Action<IPromise, PromiseWorkload<TA, TW>> PromiseAction { get; private set; }

        public PromiseActionInstance(string actionId, Action<IPromise, PromiseWorkload<TA,TW>> promiseAction)
        {
            ActionId = actionId;
            PromiseAction = promiseAction;
        }
    }
}
