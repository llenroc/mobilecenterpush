using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile;
using mobilecenterpush.Model;
using System.Net.Http;
using mobilecenterpush.Helpers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace mobilecenterpush.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public Command SendPushNotificationCommand { get; set; }

        public override void OnPushNotificationReceieved(Microsoft.Azure.Mobile.Push.PushNotificationReceivedEventArgs PushNotificationEventArgs)
        {
           
        }

		public MainPageViewModel()
        {
            SendPushNotificationCommand = new Command<string>(async (string tags) => await ExecuteSendPushNotificationCommand(tags));
        }

        async Task ExecuteSendPushNotificationCommand(string tag)
        {
            var id = await MobileCenter.GetInstallIdAsync();

            var content = new NotificationContent();
            var target = new NotificationTarget();

            content.Body = "This is a test!";
            content.Title = "push sent from mobile device";

            target.Devices = new string[] { id.ToString()};

            //if (!string.IsNullOrEmpty(tag))
                //target.Audiences = new List<string>() { tag };

            var push = new MobileCenterNotification();
            push.Content = content;
            push.Target = target;

            push.Content.CustomData = new Dictionary<string, string>()
            {
                {"hello","mahdi"}
            };

            var httpclient = new HttpClient();

            var jsonin = JsonConvert.SerializeObject(push);

            var res = await httpclient.PostAsync<string>("http://mobilecenterpush.azurewebsites.net/api/sendpush", push);


		}

    }
}
