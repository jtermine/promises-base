using System;
using System.Net;
using RestSharp;
using Termine.Promises.Base.Generics;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Base.Handlers
{
    public class WorkloadXferHandler<TC, TU, TW, TR, TE>
        where TC : IHandlePromiseConfig
        where TU: IAmAPromiseUser
        where TW : IAmAPromiseWorkload
        where TR : IAmAPromiseRequest
        where TE : IAmAPromiseResponse, new()
    {
        public Action<WorkloadXferHandlerConfig, IHandlePromiseActions, TC, TU, TW, TR, TE> Action { get; set; }
        public string HandlerName { get; set; }
        public IHandleEventMessage StartMessage { get; set; }
        public IHandleEventMessage EndMessage { get; set; }
        public Action<WorkloadXferHandlerConfig, IHandlePromiseActions, TC, TU, TW, TR, TE> Configurator { get; set; }

        public WorkloadXferHandler()
        {
            ResetToDefaultAction();
        }

        public void ResetToDefaultAction()
        {
            Action = (gc, p, u, c, w, rq, rx) =>
            {
                var client = new RestClient(gc.BaseUri);

                var request = new RestRequest(gc.EndpointUri, Method.POST)
                {
                    JsonSerializer = new RestSharpJsonSerializer(),
                    Timeout = gc.TimeoutInMs
                };

                request.AddJsonBody(rq);

                var response = client.Execute(request);
                
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    p.Warn(new GenericEventMessage(response.ErrorMessage));
                }

                if (response.ErrorException != default(Exception))
                {
                    p.Abort(response.ErrorException);
                    return;
                }

                if (response.StatusCode != HttpStatusCode.OK) return;

                p.DeserializeResponse(response.Content);
            };
        }
    }
}
