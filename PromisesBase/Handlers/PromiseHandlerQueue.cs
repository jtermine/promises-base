using System.Collections.Generic;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Handlers
{
    public class PromiseHandlerQueue<TW>
        where TW : class, IHandlePromiseActions
    {
        private readonly List<PromiseHandler<TW>> _queue = new List<PromiseHandler<TW>>();
        private readonly HashSet<string> _alreadyAdded = new HashSet<string>();

        public void Enqueue(PromiseHandler<TW> item)
        {
            if (!_alreadyAdded.Add(item.HandlerName)) return;
            _queue.Add(item);
        }

        public int Count { get { return _queue.Count; } }
        
        public void Invoke(TW promise, IHandleEventMessage eventMessage)
        {
            _queue.ForEach(a=> a.Action.Invoke(promise, eventMessage));
        }

    }
}