using System;
using System.Net;
using RestSharp;
using RestSharp.Authenticators;
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
        public Func<PromiseXferFunc<TC, TU, TW, TR, TE>, Resp> Action { get; set; }
        public string HandlerName { get; set; }
        public IHandleEventMessage StartMessage { get; set; }
        public IHandleEventMessage EndMessage { get; set; }
        public Func<PromiseXferFunc<TC, TU, TW, TR, TE>, Resp> Configurator { get; set; }

        public WorkloadXferHandler()
        {
            ResetToDefaultAction();
        }

        private void ResetToDefaultAction()
        {
            Action = func =>
            { 
                var client = new RestClient(func.XferConfig.BaseUri);

                if (func.XferConfig.UseNtlm) client.Authenticator = new NtlmAuthenticator();

                var request = new RestRequest(func.XferConfig.EndpointUri, Method.POST)
                {
                    JsonSerializer = new RestSharpJsonSerializer(),
                    Timeout = func.XferConfig.TimeoutInMs
                };

                request.AddJsonBody(func.Rq);

                var response = client.Execute(request);
                
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    return Resp.Success(new GenericEventMessage(response.ErrorMessage));
                }

                if (response.ErrorException != default(Exception))
                {
                    return Resp.Abort(new GenericEventMessage(response.ErrorException));
                }

                if (response.StatusCode != HttpStatusCode.OK) return Resp.Abort(response.StatusDescription);

                func.P.DeserializeResponse(response.Content);

                return Resp.Success();
            };
        }
    }
}
