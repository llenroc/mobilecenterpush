using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using mobilecenterpush.Model;
using mobilecenterpush.Tags;
using mobilecenterpush.Helpers;

namespace mobilecenterpush
{
    public class PushManager
    {
        static HttpClient _client;
        static PushManager _instance;

        public static HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                    _client.DefaultRequestHeaders.Add("X-API-Token", Keys.MobileCenter.AuthKey);
                }

                return _client;
            }
        }

        public static PushManager Instance
        {
            get
            {
                return _instance ?? (_instance = new PushManager());
            }

        }


        List<string> _platforms = new List<string> { Keys.MobileCenter.iOSUrl, Keys.MobileCenter.AndroidUrl };

        #region Audiences - NOT USED
        //async public Task<bool> CreateAudience(string key, string value)
        //{
        //    var audience = new Audience();
        //    audience.Description = $"Users whose {key} is {value}";
        //    audience.CustomProperties.Add(key, "string");
        //    audience.Definition = $"{key} eq '{value}'";

        //    foreach (var baseUrl in _platforms)
        //    {
        //        var url = $"{baseUrl}/analytics/audiences/{audience.Definition}";
        //        var result = Client.AddMobileCenterToken().Put<JObject>(url, audience);
        //    }

        //    return true;
        //}

        //async public Task<bool> SendNotification(string title, string message, string key, string value, Dictionary<string, string> payload = null)
        //{
        //    var audienceName = $"{key} eq '{value}'";
        //    var notification = new MobileCenterNotification();
        //    notification.Target.Type = TargetType.Audience;
        //    notification.Target.Audiences.Add(audienceName);
        //    notification.Content.Title = title;
        //    notification.Content.Body = message;
        //    notification.Content.CustomData = payload;
        //    notification.Content.Name = Guid.NewGuid().ToString();

        //    foreach (var baseUrl in _platforms)
        //    {
        //        var url = $"{baseUrl}/push/notifications/";
        //        var result = Client.Post<JObject>(url, notification);
        //    }

        //    return true;
        //}

        #endregion


        /// <summary>
        /// Create a new Tag - without adding installIds
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        async public Task<bool> CreateTagAsync(string title)
        {
            var result = await TagsService.Instance.CreateTagAsync(title);
            return result == null ? false : true;
       
        }

        /// <summary>
        /// Add an installationID to a Tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="installationId"></param>
        /// <returns></returns>
        async public Task<bool> AddInstallationIdToTagAsync(string tagName, List<string> installationId)
        {
            return await TagsService.Instance.AddSubscribersToTagAsync(tagName, installationId);
        }

        /// <summary>
        /// Remove an installationId from a Tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="installIds"></param>
        /// <returns></returns>
        async public Task<bool> RemoveInstallationIdsFromTagAsync(string tagName, List<string> installIds)
        {
            await TagsService.Instance.RemoveSubscribersFromTagAsync(tagName, installIds);
            return true;
        }

        /// <summary>
        /// Send Notification with a List of Tags
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        async public Task<bool> SendNotification(string title, string message, List<string> tags, Dictionary<string,string> payload = null)
        {
            foreach (var tag in tags)
            {
                var listOfUsers = await TagsService.Instance.GetUsersFromTagAsync(tag);
                if(listOfUsers.Count() != 0)
                    await SendNotification(title, message, listOfUsers.ToArray(), payload);
            }
            return true;
        }

        /// <summary>
        /// Send Notification with an array of Devices
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="devices"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        async public Task<bool> SendNotification(string title, string message, string[] devices = null, Dictionary<string, string> payload = null)
        {
            var notification = new MobileCenterNotification();
            
            if (devices == null) { 
                notification.Target = null;
            }
            else
            {
                notification.Target.Type = TargetType.Device;
                notification.Target.Devices = devices;
            }

            notification.Content.Title = title ?? "";
            notification.Content.Body = message ?? "";
            notification.Content.CustomData = payload;
            notification.Content.Name = devices == null ? $"Push to All devices - {title}" : devices.First();

            foreach (var baseUrl in _platforms)
            {
                var url = $"{baseUrl}/push/notifications/";
                var result = await Client.PostAsync<JObject>(url, notification);
            }

            return true;
        }
    }
}
