using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Termine.Promises;
using Termine.Promises.Interfaces;

namespace PromisesWithFileTransactionLog
{
    public static class Extensions
    {
        public static Promise<TW> WithFileTransactionLog<TW>(this Promise<TW> promise, string transLogPath)
            where TW : class, IAmAPromiseWorkload, new()
        {
            var loggerName = string.Format("{0}.{1}", typeof(TW).FullName, promise.PromiseId);
            
            promise.WithPreStart("fileLog.init", w => promise.CreateObjectCache(transLogPath));

            promise.WithPostEnd("fileLog.save", w => promise.SaveFileLog());

            promise.WithBlockHandler("fileLog.block",
                (w, m) => m.LogEvent(w, m));

            promise.WithTraceHandler("fileLog.trace",
                (w, m) => m.LogEvent(w, m));

            promise.WithDebugHandler("fileLog.debug",
                (w, m) => m.LogEvent(w, m));

            promise.WithInfoHandler("fileLog.info",
                (w, m) => m.LogEvent(w, m));

            promise.WithWarnHandler("fileLog.warn",
                (w, m) => m.LogEvent(w, m));

            promise.WithErrorHandler("fileLog.error",
                (w, m) => m.LogEvent(w, m));

            promise.WithFatalHandler("fileLog.fatal",
                (w, m) => m.LogEvent(w, m));

            promise.WithAbortHandler("fileLog.abort",
                (w, m) => m.LogEvent(w, m));

            promise.WithAbortOnAccessDeniedHandler("fileLog.abortAccessDenied",
                (w, m) => m.LogEvent(w, m));

            return promise;
        }

        private static void LogEvent<TT, TW>(this TT message, IAmAPromise<TW> promise, params object[] options) where TT : IHandleEventMessage
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (!promise.Context.Objects.ContainsKey("objectCache") || !promise.Context.Objects.ContainsKey("objectCachePath")) return;
            
            var objectCache = promise.Context.Objects["objectCache"] as List<FileLogEntry>;

            if (objectCache == null) return;

            var fileLogEntry = new FileLogEntry
            {
                DateTimeStamp = DateTime.UtcNow,
                EventCode = message.EventId,
                EventId = message.EventId,
                EventPublicDetails = message.EventPublicDetails,
                EventPublicMessage = message.EventPublicMessage,
                IsPublicMessage = message.IsPublicMessage,
                Workload = JsonConvert.SerializeObject(promise.Workload)
            };

            objectCache.Add(fileLogEntry);
        }

        private static void CreateObjectCache<TW>(this IAmAPromise<TW> promise, string transLogPath )
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (promise.Context.Objects.ContainsKey("objectCache")) return;

            if (!Directory.Exists(transLogPath)) Directory.CreateDirectory(transLogPath);

            var path = Path.Combine(transLogPath, typeof (TW).FullName);

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            if (!promise.Context.Objects.ContainsKey("objectCachePath"))
                promise.Context.Objects.Add("objectCachePath", path);

            if (!string.Equals(promise.Context.Objects["objectCachePath"].ToString(), path))
                promise.Context.Objects["objectCachePath"] = path;
            
            promise.Context.Objects.Add("objectCache", new List<FileLogEntry>());
        }

        private static void SaveFileLog<TW>(this IAmAPromise<TW> promise)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (!promise.Context.Objects.ContainsKey("objectCache") || !promise.Context.Objects.ContainsKey("objectCachePath")) return;

            var path = promise.Context.Objects["objectCachePath"].ToString();

            var objectCache = promise.Context.Objects["objectCache"] as List<FileLogEntry>;

            if (objectCache == null || objectCache.Count < 1) return;

            //var file = Path.Combine(path, )

            //TODO: finish this.
        }
    }


}
