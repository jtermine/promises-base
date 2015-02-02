using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Web
{

    public class PromiseAsyncHandler<TT, TW> : IHttpAsyncHandler
        where TT : IAmAPromise<TW>, new()
        where TW : class, IAmAPromiseWorkload, new()
    {
        public async virtual Task ProcessRequestAsync(HttpContext context)
        {
            await Task.Run(() =>
            {
                var webConstants = WebConstants.Other;

                var contentType = context.Request.ContentType.ToLowerInvariant();

                if (contentType.Contains("application/json") || contentType.Contains("text/javascript"))
                {
                    webConstants = WebConstants.Json;
                }

                var incoming = context.Request.GetBufferlessInputStream();

                if (incoming.Length < 1 || webConstants != WebConstants.Json)
                {
                    context.Response.StatusCode = 404;
                    context.Response.StatusDescription = "No content was provided to the POST request.";
                    return;
                }

                using (var stream = new MemoryStream())
                {
                    var buffer = new byte[2048]; // read in chunks of 2KB
                    int bytesRead;

                    while ((bytesRead = incoming.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        stream.Write(buffer, 0, bytesRead);
                    }

                    byte[] body = stream.ToArray();

                    var json = Encoding.UTF8.GetString(body);
                    var workload = JsonConvert.DeserializeObject<TW>(json);

                    var promise = new TT();

                    promise.WithWorkload(workload);

                    promise.Run();

                    HandlePromise(body, webConstants, context);
                }

                context.Response.StatusCode = 204;
                context.Response.StatusDescription = "Completed without exception";

            });

        }

        public virtual void HandlePromise(byte[] body, WebConstants webConstants, HttpContext context)
        {
            
        }

        private Task ProcessRequestAsync(HttpContext context, AsyncCallback cb)
        {
            return ProcessRequestAsync(context)
                .ContinueWith(task => cb(task));
        }

        public void ProcessRequest(HttpContext context)
        {
            ProcessRequestAsync(context).Wait();
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            
            return ProcessRequestAsync(context, cb);
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            ((Task)result).Dispose();
        }
    }

}