using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Termine.Promises.Interfaces;

namespace Termine.Promises.Web
{

    public sealed class PromiseAsyncHandler<TT> : IHttpAsyncHandler
        where TT : IAmAPromiseFactory, new()
    {
        public async Task ProcessRequestAsync(HttpContext context)
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

                byte[] body;
                string json;

                using (var stream = new MemoryStream())
                {
                    var buffer = new byte[2048]; // read in chunks of 2KB
                    int bytesRead;

                    while ((bytesRead = incoming.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        stream.Write(buffer, 0, bytesRead);
                    }


                    body = stream.ToArray();
                    json = Encoding.UTF8.GetString(body);
                }
                
                var promiseFactory = new TT();

                var response = promiseFactory.Run(json);

                context.Response.AddHeader("Content-Type", "application/json");
                context.Response.Write(response);
                context.Response.StatusCode = 200;
                context.Response.StatusDescription = "Completed without exception";

            });

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