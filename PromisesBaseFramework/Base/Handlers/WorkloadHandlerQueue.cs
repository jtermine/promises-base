using System.Collections.Generic;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public class WorkloadHandlerQueue<TC, TU, TW, TR, TE>
        where TW : class, IAmAPromiseWorkload, new()
        where TU : class, IAmAPromiseUser, new()
        where TC : class, IHandlePromiseConfig, new()
        where TR : class, IAmAPromiseRequest, new()
        where TE : class, IAmAPromiseResponse, new()
    {
        private readonly List<WorkloadHandler<TC, TU, TW, TR, TE>> _queue =
            new List<WorkloadHandler<TC, TU, TW, TR, TE>>();

        private readonly HashSet<string> _alreadyAdded = new HashSet<string>();

        public void Enqueue(WorkloadHandler<TC, TU, TW, TR, TE> item)
        {
            if (!_alreadyAdded.Add(item.HandlerName)) return;
            _queue.Add(item);
        }

        public int Count => _queue.Count;

        public void Invoke(IHandlePromiseActions p, TC c, TU u, TW w, TR rq, TE rx,
            bool ignoreBlockOrTermination = false)
        {
            _queue.ForEach(a =>
            {
                if (!ignoreBlockOrTermination) p.CancellationToken.ThrowIfCancellationRequested();

                if (!ignoreBlockOrTermination & (p.IsTerminated || p.IsBlocked)) return;

                p.Trace(a.StartMessage);

                var response = a.Action.Invoke(new PromiseFunc<TC, TU, TW, TR, TE>(p, c, u, w, rq, rx));

                if (response.IsFailure)
                {
                    p.Trace("Silently aborting on receipt of a failed promise workload handler.");
                    return;
                }

                p.Trace(a.EndMessage);
            });
        }
    }
}