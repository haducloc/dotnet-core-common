using Microsoft.AspNetCore.Http;
using NetCore.Common.Utils;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Common.AspNet
{
    public static class AspNetUtils
    {
        public static bool IsGetOrHead(this HttpRequest request)
        {
            return request.Method == "GET" || request.Method == "HEAD";
        }

        public static async Task WriteJsonAsync(this HttpResponse response, object model, int status)
        {
            response.ContentType = "application/json; charset=utf-8";
            response.StatusCode = status;

            var output = new JsonTextWriter(new StreamWriter(response.Body, Encoding.UTF8));
            JsonSerializer.CreateDefault().Serialize(output, model);
            await output.FlushAsync();
        }

        public static void SetContentDisposition(this HttpResponse response, string fileName, bool inline)
        {
            string type = inline ? "inline" : "attachment";
            response.Headers["Content-Disposition"] = $"{type}; filename=\"{URLEncoding.EncodePath(fileName)}\"";
        }
    }
}
