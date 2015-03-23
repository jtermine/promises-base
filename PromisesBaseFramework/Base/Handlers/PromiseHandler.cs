using System;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public class PromiseHandler<TW>
        where TW : IHandlePromiseActions
    {
        public Action<TW, IHandleEventMessage> Action { get; set; }
        public string HandlerName { get; set; }
        
    }
}
