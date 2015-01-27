using System;
using Termine.Promises.Interfaces;

namespace Termine.Promises
{
    /// <summary>
    /// Extends the promise object to add functional
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TW"></typeparam>
        /// <param name="promise"></param>
        /// <param name="ex"></param>
        public static void HandleInstrumentationError<TW>(this IAmAPromise<TW> promise, Exception ex)
            where TW : class, IAmAPromiseWorkload, new()
        {

        }
    }
}
