using System;
using Xamarin.Forms;
using mobilecenterpush.Pages;
using mobilecenterpush.ViewModels;
using Microsoft.Azure.Mobile.Push;
using mobilecenterpush.Interfaces;

namespace mobilecenterpush.Pages
{
    public class FirstPage : BaseContentPage<MainPageViewModel>
    {
        public FirstPage()
        {
            Title = "Main Page";
            var entry = new Entry();

            var button = new Button
            {
                Text = "Send Push Notification",
                Command = ViewModel.SendPushNotificationCommand,
                CommandParameter = new Binding(nameof(entry.Text))
            };

            button.SetBinding(Button.CommandParameterProperty, new Binding() { Source = entry, Path = "Text" });

            var layout = new StackLayout();

            layout.Children.Add(entry);
            layout.Children.Add(button);

            Content = layout;

        }

        public async override void OnPushNotificationReceived(PushNotificationReceivedEventArgs PushNotificationContent)
        {

			// Add the notification message and title to the message
			var summary = $"Push notification received:" +
								$"\n\tNotification title: {PushNotificationContent.Title}" +
								$"\n\tMessage: {PushNotificationContent.Message}";

			// If there is custom data associated with the notification,
			// print the entries
			if (PushNotificationContent.CustomData != null)
			{
				summary += "\n\tCustom data:\n";
				foreach (var key in PushNotificationContent.CustomData.Keys)
				{
					summary += $"\t\t{key} : {PushNotificationContent.CustomData[key]}\n";
				}
			}

			await DisplayAlert("Push Received", summary, "OK");
        }
    }
}
