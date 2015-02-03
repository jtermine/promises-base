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
        public static Promise<TW> WithRest<TW>(this Promise<TW> promise)
            where TW : class, IAmAPromiseWorkload, new()
        {
            promise.Context.XferActions.Add("sendRest", w =>
            {
                using (var client = new HttpClient())
                {
                    var request = promise.Workload.GetRequest();

                    var json = JsonConvert.SerializeObject(request);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = client.PostAsync("http://localhost.fiddler:2950/TestPromise", content);

                    if (response.Result.StatusCode != HttpStatusCode.NoContent)
                    {
                        promise.Block(new GenericEventMessage((int) response.Result.StatusCode, response.Result.Content.ReadAsStringAsync().Result));
                    }
                }
            });

            return promise;
        }
    }
}
