using System;
using System.Windows.Forms;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public class PromiseHandler<TC, TU, TW, TR, TE>
        where TC : IHandlePromiseConfig
        where TU : IAmAPromiseUser
        where TW : IAmAPromiseWorkload
        where TR : IAmAPromiseRequest
        where TE : IAmAPromiseResponse
    {
        public Action<IHandleEventMessage, IHandlePromiseActions, TC, TU, TW, TR, TE> Action { get; set; }
        public string HandlerName { get; set; }
        public Control Control { get; set; }

    }
}