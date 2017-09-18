using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using mobilecenterpush.Tags;

namespace mobilecenterpush
{
    public static class CreateTagFunction
    {
        [FunctionName("CreateTagFunction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "CreateTag/{name}")]HttpRequestMessage req, string name, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var result = await PushManager.Instance.CreateTagAsync(name);

            if (result)
                return req.CreateResponse(HttpStatusCode.OK, $"Tag {name} created");
            // Fetching the name from the path parameter in the request URL
            return req.CreateResponse(HttpStatusCode.InternalServerError, $"Request to create Tag {name} failed");
        }
    }
}
