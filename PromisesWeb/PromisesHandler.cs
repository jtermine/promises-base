using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Termine.Promises.Web
{
    public class PromisesHandler: IHttpHandler
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

            byte[] buffer = {};
            int offset = 0;
            int count = 4096;

            var streamReader = new BinaryReader(incoming, Encoding.UTF8);

            byte[] result = {};

            while (streamReader.Read(buffer, offset, count) > 0)
            {   
                buffer.CopyTo(result, offset);
                offset = offset + count;
            }

            context.Response.StatusCode = 204;
            context.Response.StatusDescription = "Completed without exception";

        }

        public bool IsReusable { get { return true; } }
    }
}