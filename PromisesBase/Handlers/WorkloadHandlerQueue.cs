using System.Collections.Generic;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Handlers
{
    public class WorkloadHandlerQueue<TC, TW, TR, TE>
        where TW : class, IAmAPromiseWorkload, new()
        where TC : class, IHandlePromiseConfig, new()
        where TR : class, IAmAPromiseRequest, new()
        where TE: class, IAmAPromiseResponse, new()
    {
        private readonly List<WorkloadHandler<TC, TW, TR, TE>> _queue = new List<WorkloadHandler<TC, TW, TR, TE>>();
        private readonly HashSet<string> _alreadyAdded = new HashSet<string>();

        public void Enqueue(WorkloadHandler<TC, TW, TR, TE> item)
        {
            if (!_alreadyAdded.Add(item.HandlerName)) return;
            _queue.Add(item);
        }

        public int Count { get { return _queue.Count; } }

        public void Invoke(IHandlePromiseActions promise, TC config, TW workload, TR request, TE response, bool ignoreBlockOrTermination = false)
        {
            _queue.ForEach(a =>
            {
                if (ignoreBlockOrTermination || promise.IsTerminated || promise.IsBlocked) return;
               
                promise.Trace(a.StartMessage);
                a.Action.Invoke(promise, config, workload, request, response);
                promise.Trace(a.EndMessage);
            });
        }
    }
}