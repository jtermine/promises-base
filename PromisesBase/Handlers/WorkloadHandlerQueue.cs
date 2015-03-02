using System.Collections.Generic;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Handlers
{
    public class WorkloadHandlerQueue<TC, TW, TR>
        where TW : class, IAmAPromiseWorkload, new()
        where TC : class, IHandlePromiseConfig, new()
        where TR : class, IAmAPromiseRequest, new()
    {
        private readonly List<WorkloadHandler<TC, TW, TR>> _queue = new List<WorkloadHandler<TC, TW, TR>>();
        private readonly HashSet<string> _alreadyAdded = new HashSet<string>();

        public void Enqueue(WorkloadHandler<TC, TW, TR> item)
        {
            if (!_alreadyAdded.Add(item.HandlerName)) return;
            _queue.Add(item);
        }

        public int Count { get { return _queue.Count; } }

        public void Invoke(IHandlePromiseActions promise, TC config, TW workload, TR request, bool ignoreBlockOrTermination = false)
        {
            _queue.ForEach(a =>
            {
                if (ignoreBlockOrTermination || workload.IsTerminated || workload.IsBlocked) return;

                promise.Trace(a.StartMessage);
                a.Action.Invoke(promise, config, workload, request);
                promise.Trace(a.EndMessage);
            });
        }
    }
}