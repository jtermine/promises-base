using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    public static class Extensions
    {
        public static IAmAPromise<TW> WithRequestId<TW>(this IAmAPromise<TW> promise, string requestId)
            where TW : class, IAmAPromiseWorkload, new()
        {
            if (string.IsNullOrEmpty(requestId)) return promise;
            if (promise.Workload == null) return promise;

            promise.Workload.RequestId = requestId;

            return promise;
        }

        public static void HandleInstrumentationError<TW>(this IAmAPromise<TW> promise, Exception ex)
            where TW : class, IAmAPromiseWorkload, new()
        {

        }
    }
}
