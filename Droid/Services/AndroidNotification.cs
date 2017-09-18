using System;
using Android.App;
using mobilecenterpush.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(mobilecenterpush.Droid.Services.AndroidNotification))]

namespace mobilecenterpush.Droid.Services
{
    public class AndroidNotification : INotification
    {
        public AndroidNotification()
        {
        }

        public void HandleNotification(string title, string msg)
        {
			var n = new Notification.Builder(Forms.Context);
			n.SetSmallIcon(Resource.Drawable.notification_template_icon_bg);
			n.SetLights(Android.Graphics.Color.Blue, 300, 1000);
			//n.SetContentIntent(pendingIntent);
			n.SetContentTitle(title);
			n.SetTicker(msg);
			//n.SetLargeIcon(global::Android.Graphics.BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.icon));
			n.SetAutoCancel(true);
			n.SetContentText(msg);
			n.SetVibrate(new long[] {
						200,
						200,
						100,
					});

			var nm = NotificationManager.FromContext(Forms.Context);
			nm.Notify(0, n.Build());
        }
    }
}
