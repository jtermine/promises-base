using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Tabular.TabModels
{
    public class EventSink
    {
        private readonly SynchronizationContext _syncContext;
        private readonly Timer _timer;

        public readonly ConcurrentQueue<Action> Queue = new ConcurrentQueue<Action>();

        public EventSink()
        {
            _syncContext = SynchronizationContext.Current;
            _timer = new Timer(OnTick, null, 0, 100);
        }

        public event EventHandler<Action> Tick;

        private void OnTick(object sender)
        {
            Action result;

            while (Queue.TryDequeue(out result))
            {
                if (result == null) continue;
                
                _syncContext.Send(state =>
                {
                    var dr = state as Action;
	                Tick?.Invoke(this, dr);
                }, result);
            }
        }

        public void Stop()
        {
            _timer.Change(0, int.MaxValue);
        }

        public void Start()
        {
            _timer.Change(0, 100);
        }
    }
}