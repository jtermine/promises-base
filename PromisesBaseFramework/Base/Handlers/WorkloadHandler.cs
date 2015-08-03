using System;
using System.Windows.Forms;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public class WorkloadHandler<TC, TW, TR, TE>
        where TC: IHandlePromiseConfig
        where TW: IAmAPromiseWorkload
        where TR: IAmAPromiseRequest
        where TE: IAmAPromiseResponse
    {
        public Action<IHandlePromiseActions, TC, TW, TR, TE> Action { get; set; }
        public string HandlerName { get; set; }
        public IHandleEventMessage StartMessage { get; set; }
        public IHandleEventMessage EndMessage { get; set; }
        public Control Control { get; set; }
    }
}
