using System.Collections.Generic;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public class PromiseHandlerQueue<TC, TU, TW, TR, TE>
        where TC : IHandlePromiseConfig
        where TU: IAmAPromiseUser
        where TW : IAmAPromiseWorkload
        where TR : IAmAPromiseRequest
        where TE : IAmAPromiseResponse
    {
        private readonly List<PromiseHandler<TC, TU, TW, TR, TE>> _queue = new List<PromiseHandler<TC, TU, TW, TR, TE>>();
        private readonly HashSet<string> _alreadyAdded = new HashSet<string>();

        public void Enqueue(PromiseHandler<TC, TU, TW, TR, TE> item)
        {
            if (!_alreadyAdded.Add(item.HandlerName)) return;
            _queue.Add(item);
        }

        public int Count => _queue.Count;

	    public void Invoke(IHandleEventMessage eventMessage, IHandlePromiseActions p, TC c, TU u, TW w, TR rq, TE rx)
        {
            _queue.ForEach(a=> a.Action.Invoke(eventMessage, p, c, u, w, rq, rx));
        }

    }
}