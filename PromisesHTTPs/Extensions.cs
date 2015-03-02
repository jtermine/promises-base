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
        public static Promise<TC, TW, TR, TE> WithRest<TC, TW, TR, TE>(this Promise<TC, TW, TR,TE> promise)
            where TW : class, IAmAPromiseWorkload, new()
            where TC : class, IHandlePromiseConfig, new()
            where TR : class, IAmAPromiseRequest, new()
            where TE : class, IAmAPromiseResponse, new()
        {
            promise.WithXferAction("sendRest", (promiseActions, config, workload, request, response) =>
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(request);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var webResponse = client.PostAsync("http://localhost.fiddler:2950/TestPromise", content);

                    if (webResponse.Result.StatusCode != HttpStatusCode.NoContent)
                    {
                        promiseActions.Block(new GenericEventMessage((int) webResponse.Result.StatusCode,
                            webResponse.Result.Content.ReadAsStringAsync().Result));
                    }
                }
            });

            return promise;
        }
    }
}