using System.IO;
using System.Web;

namespace Termine.Promises.Web
{
    public abstract class PromisesHandler : IHttpHandler
    {
        public abstract void HandlePromise(byte[] body, WebConstants webConstants, HttpContext context);

        public void ProcessRequest(HttpContext context)
        {
            var webConstants = WebConstants.Other;

            var contentType = context.Request.ContentType.ToLowerInvariant();

            if (contentType.Contains("application/json") || contentType.Contains("text/javascript"))
            {
                webConstants = WebConstants.Json;
            }

            var incoming = context.Request.GetBufferlessInputStream();

            if (incoming.Length < 1)
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

                byte[] result = stream.ToArray();

                HandlePromise(result, webConstants, context);
            }
            
            context.Response.StatusCode = 204;
            context.Response.StatusDescription = "Completed without exception";
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}