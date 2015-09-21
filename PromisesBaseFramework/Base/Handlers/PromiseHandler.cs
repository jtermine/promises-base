using System;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public class PromiseHandler
    {
        public Func<PromiseMessageFunc, Resp> Action { get; set; }
        public string HandlerName { get; set; }
    }
}