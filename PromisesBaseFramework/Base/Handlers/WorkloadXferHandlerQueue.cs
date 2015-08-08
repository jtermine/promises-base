using System.Collections.Generic;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public class WorkloadXferHandlerQueue<TC, TW, TR, TE>
        where TW : class, IAmAPromiseWorkload, new()
        where TC : class, IHandlePromiseConfig, new()
        where TR : class, IAmAPromiseRequest, new()
        where TE: class, IAmAPromiseResponse, new()
    {
        private readonly List<WorkloadXferHandler<TC, TW, TR, TE>> _queue = new List<WorkloadXferHandler<TC, TW, TR, TE>>();
        private readonly HashSet<string> _alreadyAdded = new HashSet<string>();

        public void Enqueue(WorkloadXferHandler<TC, TW, TR, TE> item)
        {
            if (!_alreadyAdded.Add(item.HandlerName)) return;
            _queue.Add(item);
        }

        public int Count => _queue.Count;

        /// <summary>
        /// Invoke a transfer handler
        /// </summary>
        /// <param name="p">promise</param>
        /// <param name="c">configuration</param>
        /// <param name="w">workload</param>
        /// <param name="rq">request</param>
        /// <param name="rx">response</param>
	    public void Invoke(IHandlePromiseActions p, TC c, TW w, TR rq, TE rx)
	    {
	       _queue.ForEach(a =>
            {
                p.CancellationToken.ThrowIfCancellationRequested();

                if ((p.IsTerminated || p.IsBlocked)) return;

                if (a.Configurator == null)
                {
                    p.Abort("No configurator was provided to the xfer handler");
                    return;
                }

                var workloadXferHandlerConfig = new WorkloadXferHandlerConfig();

                a.Configurator.Invoke(workloadXferHandlerConfig, p, c, w, rq, rx);

                p.Trace(a.StartMessage);

                a.Action.Invoke(workloadXferHandlerConfig, p, c, w, rq, rx);

                p.Trace(a.EndMessage);
            });
	    }
    }
}