﻿using System.Collections.Generic;
using System.Linq;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public class PromiseHandlerQueue
    {
        private readonly List<PromiseHandler> _queue = new List<PromiseHandler>();
        private readonly HashSet<string> _alreadyAdded = new HashSet<string>();

        public void Enqueue(PromiseHandler item)
        {
            if (!_alreadyAdded.Add(item.HandlerName)) return;
            _queue.Add(item);
        }

        public int Count => _queue.Count;

	    public void Invoke(IHandlePromiseActions p, IHandleEventMessage m)
	    {
	        if (_queue.Select(promiseHandler => promiseHandler.Action.Invoke(
	            new PromiseMessageFunc(p, m))).Any(funcResponse => funcResponse.IsFailure))
	        {
	            p.Trace("Silently aborting on receipt of a failed promise handler.");
	        }
	    }
    }
}