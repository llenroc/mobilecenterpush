using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using mobilecenterpush.Tags;
using Newtonsoft.Json;
using mobilecenterpush.Model;
using System;

namespace mobilecenterpush
{
    public static class UpdateUsersInTagFunction
    {
        [FunctionName("UpdateUsersInTag")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", "put", Route = "UpdateUsersInTag")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            bool result = false;

            try
            {
                var tagInfo = JsonConvert.DeserializeObject<Tag>(req.Content.ReadAsStringAsync().Result);

                if (!string.IsNullOrEmpty(tagInfo.TagName)) { 
                    if (req.Method == HttpMethod.Put)
                    {
                        result = await PushManager.Instance.RemoveInstallationIdsFromTagAsync(tagInfo.TagName, tagInfo.Subscribers);

                        if (result)
                            return req.CreateResponse(HttpStatusCode.OK, $"Users removed from Tag {tagInfo.TagName}");
                    }
                    else
                    {
                        result = await PushManager.Instance.AddInstallationIdToTagAsync(tagInfo.TagName, tagInfo.Subscribers);

                        if (result)
                            return req.CreateResponse(HttpStatusCode.OK, $"Users added to Tag {tagInfo.TagName}");
                    }
                }
                return req.CreateResponse(HttpStatusCode.InternalServerError, $"Request to create Tag failed - Invalid Tag Name");
            }
            catch(Exception e)
            {
                return req.CreateResponse(HttpStatusCode.InternalServerError, $"Request to create Tag failed - make sure the body is correct");
            }

            return req.CreateResponse(HttpStatusCode.InternalServerError, $"Request to create Tag failed");
        }
    }
}
