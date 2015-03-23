using System;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base
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
        private static void HandleInstrumentationError<TW>(this TW promise, Exception ex)
            where TW : class, IHandlePromiseActions
        {
            // do nothing and supress error
        }

        public static void SafeInvoke<TW>(this Action<TW> action, TW promise) where TW : class, IHandlePromiseActions
        {
            try
            {
                action.Invoke(promise);
            }
            catch (Exception ex)
            {
                promise.HandleInstrumentationError(ex);
            }
        }

        public static bool SafeInvoke<TW>(this Action<TW, IHandleEventMessage> action, TW promise, IHandleEventMessage eventMessage) where TW : class, IHandlePromiseActions
        {
            try
            {
                action.Invoke(promise, eventMessage);
                return true;
            }
            catch (Exception ex)
            {
                promise.HandleInstrumentationError(ex);
            }

            return false;
        }
    }
}
