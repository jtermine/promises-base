using System.Collections.Generic;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public class WorkloadXferHandlerQueue<TC, TU, TW, TR, TE>
        where TW : class, IAmAPromiseWorkload, new()
        where TU: class, IAmAPromiseUser, new()
        where TC : class, IHandlePromiseConfig, new()
        where TR : class, IAmAPromiseRequest, new()
        where TE: class, IAmAPromiseResponse, new()
    {
        private readonly List<WorkloadXferHandler<TC, TU, TW, TR, TE>> _queue = new List<WorkloadXferHandler<TC, TU, TW, TR, TE>>();
        private readonly HashSet<string> _alreadyAdded = new HashSet<string>();

        public void Enqueue(WorkloadXferHandler<TC, TU, TW, TR, TE> item)
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
        /// <param name="u">user</param>
        /// <param name="w">workload</param>
        /// <param name="rq">request</param>
        /// <param name="rx">response</param>
        public void Invoke(IHandlePromiseActions p, TC c, TU u, TW w, TR rq, TE rx)
	    {
            _queue.ForEach(a =>
            {
                p.CancellationToken.ThrowIfCancellationRequested();

                if ((p.IsTerminated || p.IsBlocked)) return;

                p.Trace(a.StartMessage);

                var workloadXferHandlerConfig = new WorkloadXferHandlerConfig();
                var configurator = a.Configurator.Invoke(new PromiseXferFunc<TC, TU, TW, TR, TE>(workloadXferHandlerConfig, p, c, u, w, rq, rx));

                if (configurator.IsFailure)
                {
                    p.Abort(configurator);
                    return;
                }

                var response = a.Action.Invoke(new PromiseXferFunc<TC, TU, TW, TR, TE>(workloadXferHandlerConfig, p, c, u, w, rq, rx));

                if (response.IsFailure)
                {
                    p.Abort(response);
                    return;
                }

                p.Trace(a.EndMessage);
            });

	    }
    }
}