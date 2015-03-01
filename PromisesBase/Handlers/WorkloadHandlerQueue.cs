using System.Collections.Generic;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Handlers
{
    public class WorkloadHandlerQueue<TW>
        where TW: IAmAPromiseWorkload
    {
        private readonly List<WorkloadHandler<TW>> _queue = new List<WorkloadHandler<TW>>();
        private readonly HashSet<string> _alreadyAdded = new HashSet<string>();

        public void Enqueue(WorkloadHandler<TW> item)
        {
            if (!_alreadyAdded.Add(item.HandlerName)) return;
            _queue.Add(item);
        }

        public int Count { get { return _queue.Count; } }

        public void Invoke(IHandlePromiseActions promise, TW workload, bool ignoreBlockOrTermination = false)
        {
            _queue.ForEach(a =>
            {
                if (ignoreBlockOrTermination || workload.IsTerminated || workload.IsBlocked) return;

                promise.Trace(a.StartMessage);
                a.Action.Invoke(promise, workload);
                promise.Trace(a.EndMessage);
            });
        }
    }
}