using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Threading.Tasks;
using Newtonsoft.Json;
using mobilecenterpush.Model;
using mobilecenterpush.Tags;
using System;

namespace mobilecenterpush
{
    public static class SendPushFunction
    {
        [FunctionName("sendpush")]

        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sendpush")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            
            try
            {
                var pushData = JsonConvert.DeserializeObject<MobileCenterNotification>(req.Content.ReadAsStringAsync().Result);

                if (pushData?.Target?.Audiences?.Count != 0)
                {
                    await PushManager.Instance.SendNotification(pushData.Content.Title, pushData.Content.Body, pushData.Target.Audiences, pushData.Content.CustomData);

                }
                else
                {
                    await PushManager.Instance.SendNotification(pushData.Content.Title, pushData.Content.Body, pushData.Target.Devices, pushData.Content.CustomData);
                }

                return req.CreateResponse(HttpStatusCode.OK, "Push Notification Sent");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return req.CreateResponse(HttpStatusCode.BadRequest, "Bad Request - please check the http body");
            }
        }
    }
}