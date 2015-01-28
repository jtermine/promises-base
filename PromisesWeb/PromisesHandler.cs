using System.IO;
using System.Linq;
using System.Web;
using Termine.Promises.WithProtobuf;

namespace Termine.Promises.Web
{
    public class PromisesHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var headers = context.Request.Headers;

            if (!headers.AllKeys.Contains("X-OLR-PromiseName"))
            {
                context.Response.StatusCode = 500;
                context.Response.StatusDescription = "The header X-OLR-PromiseName does not exist in this POST request.";
                return;
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

                var genericWorkload = result.FromByteArray<ProtobufWorkload>();

                context.Response.Write(genericWorkload.RequestId);
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