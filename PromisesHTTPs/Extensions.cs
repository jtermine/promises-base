using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Termine.Promises.Generics;
using Termine.Promises.Interfaces;

namespace Termine.Promises.WithREST
{
    /// <summary>
    /// Extends the promise to add handers to transfer promise workloads to REST services
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TW"></typeparam>
        /// <param name="promise"></param>
        /// <returns></returns>
        public static Promise<TC, TW, TR> WithRest<TC, TW, TR>(this Promise<TC, TW, TR> promise)
            where TW : class, IAmAPromiseWorkload, new()
            where TC : class, IHandlePromiseConfig, new()
            where TR : class, IAmAPromiseRequest, new()
        {
            promise.WithXferAction("sendRest", (promiseActions, config, workload, request) =>
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(request);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = client.PostAsync("http://localhost.fiddler:2950/TestPromise", content);

                    if (response.Result.StatusCode != HttpStatusCode.NoContent)
                    {
                        promiseActions.Block(new GenericEventMessage((int) response.Result.StatusCode,
                            response.Result.Content.ReadAsStringAsync().Result));
                    }
                }
            });

            return promise;
        }
    }
}