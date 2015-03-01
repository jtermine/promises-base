using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Handlers
{
    public class PromiseHandler<TW>
        where TW : IHandlePromiseActions
    {
        public Action<TW, IHandleEventMessage> Action { get; set; }
        public string HandlerName { get; set; }
        
    }
}
