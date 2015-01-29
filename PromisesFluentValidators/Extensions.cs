using System.Text;
using NClone;
using Newtonsoft.Json;
using Termine.Promises.Interfaces;

namespace Termine.Promises.FluentValidation
{
    public static class Extensions
    {
        public static Promise<TW> WithWorkloadAsJson<TW>(this Promise<TW> promise, byte[] jsonBytes)
            where TW : class, IAmAPromiseWorkload, new()
        {
            var json = Encoding.UTF8.GetString(jsonBytes);

            return promise.WithWorkloadAsJson(json);
        }

        public static Promise<TW> WithWorkloadAsJson<TW>(this Promise<TW> promise, string jsonString)
            where TW : class, IAmAPromiseWorkload, new()
        {
            var workload = JsonConvert.DeserializeObject<TW>(jsonString);

            // Clone

            return null;

        }
    }
}