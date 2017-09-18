using System;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Push;

using Xamarin.Forms;
using mobilecenterpush.Constants;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Distribute;
using Xamarin.Forms.PlatformConfiguration;
using mobilecenterpush.Interfaces;
using Plugin.Connectivity;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using mobilecenterpush.Pages;

namespace mobilecenterpush
{
    public class App : Application
    {
        public EventHandler<PushNotificationReceivedEventArgs> NotificationReceived;

        public static App Instance
        {
            get
            {
                return Current as App;
            }
        }

        public App()
        {
            var title = "Test";
            // The root page of your application

            MainPage = new NavigationPage(new FirstPage());

            Push.PushNotificationReceived += (sender, e) =>
            {
                NotificationReceived?.Invoke(sender, e);
            };

        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            switch (Xamarin.Forms.Device.RuntimePlatform)
            {
                case Xamarin.Forms.Device.iOS:
                    await MobileCenterHelpers.Start(MobileCenterConstants.MobileCenterAPIKey_iOS);
                    break;
                case Xamarin.Forms.Device.Android:
                    await MobileCenterHelpers.Start(MobileCenterConstants.MobileCenterAPIKey_Droid);
                    break;
                default:
                    throw new Exception("Runtime Platform Not Supported");
            }

            var id = MobileCenter.GetInstallIdAsync().Result;
            MobileCenterHelpers.TrackEvent("App started", "INSTALL ID", id.ToString());
        }

        protected async override void OnSleep()
        {
            // Handle when your app sleeps

            //var task = Task.Delay(4000);

            ////var dosEomthingTask = doPundayTask();

            //CrossConnectivity.Current.ConnectivityTypeChanged+= Current_ConnectivityTypeChanged; 

            //var connectedTask = await CrossConnectivity.Current.IsRemoteReachable("www.google.com", 80, 5000);

            //if (connectedTask)
            //{
                
            //}

            ////var taskCompleted = await Task.WhenAny(task, connectedTask);
            ////if (task is timeout)
            ////{
            ////    //time it but the other task is still running. display popup saying that its slow
            ////}
        }

        void Current_ConnectivityTypeChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityTypeChangedEventArgs e)
        {
            if (e.ConnectionTypes.Contains(Plugin.Connectivity.Abstractions.ConnectionType.WiFi))
            {

            }
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
