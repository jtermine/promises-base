using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Handlers
{
    public class WorkloadHandler<TW>
        where TW: IAmAPromiseWorkload
    {
        public Action<IHandlePromiseActions, TW> Action { get; set; }
        public string HandlerName { get; set; }
        public IHandleEventMessage StartMessage { get; set; }
        public IHandleEventMessage EndMessage { get; set; }
    }
}
