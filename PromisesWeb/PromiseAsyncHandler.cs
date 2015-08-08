using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Termine.Promises.Base.Interfaces;

namespace Termine.Promises.Web
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class PromiseAsyncHandler<TT> : IHttpAsyncHandler
        where TT : IAmAPromiseFactory, new()
    {
	    private static async Task ProcessRequestAsync(HttpContext context)
        {
            await Task.Run(() =>
            {
                var contentType = context.Request.ContentType.ToLowerInvariant();

                if (!contentType.Contains("application/json") & !contentType.Contains("text/javascript"))
                {
					context.Response.StatusCode = 401;
					context.Response.StatusDescription = "The request header 'application/json' was not provided with the request.";
					return;
				}

                var incoming = context.Request.GetBufferlessInputStream();

                if (incoming.Length < 1)
                {
                    context.Response.StatusCode = 404;
                    context.Response.StatusDescription = "No content was provided to the POST request.";
                    return;
                }

	            string json;

                using (var stream = new MemoryStream())
                {
                    var buffer = new byte[2048]; // read in chunks of 2KB
                    int bytesRead;

                    while ((bytesRead = incoming.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        stream.Write(buffer, 0, bytesRead);
                    }


                    var body = stream.ToArray();
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

        private static Task ProcessRequestAsync(HttpContext context, AsyncCallback cb)
        {
            return ProcessRequestAsync(context)
                .ContinueWith(task => cb(task));
        }

        public void ProcessRequest(HttpContext context)
        {
            ProcessRequestAsync(context).Wait();
        }

        public bool IsReusable => true;

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