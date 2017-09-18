using System;
using System.Threading.Tasks;
using mobilecenterpush.ViewModels;
using Plugin.Connectivity;
using Xamarin.Forms;
using Microsoft.Azure.Mobile.Push;

namespace mobilecenterpush.Pages
{
	public abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel, new()
	{
		T viewModel;

		protected BaseContentPage()
		{
			this.ViewModel = Activator.CreateInstance<T>();
		}

		/// <summary>
		/// Gets or sets the view model.
		/// </summary>
		/// <value>The view model.</value>
		public T ViewModel
		{
			get { return this.viewModel; }
			set
			{
				viewModel = value;
				BindingContext = viewModel;
				this.SetBinding(TitleProperty, nameof(ViewModel.Title));
				Task.Run(async () =>
				{
					await this.Init();
				});
			}
		}

        void App_NotificationReceived(object sender, PushNotificationReceivedEventArgs e)
        {
            OnPushNotificationReceived(e);
        }

        /// <summary>
        /// Push Notification Received
        /// </summary>
        /// <param name="PushNotificationContent">Push Notification Content.</param>
        public virtual void OnPushNotificationReceived(PushNotificationReceivedEventArgs PushNotificationContent)
        {
            viewModel.OnPushNotificationReceieved(PushNotificationContent);
        }

		//public abstract void SubscribeEventHandlers();
		public virtual void SubscribeEventHandlers()
		{
			if (!CrossConnectivity.Current.IsConnected)
			{
				//TODO: handle network lost
			}

			CrossConnectivity.Current.ConnectivityChanged += OnConnectivityChanged;
		}

		//public abstract void UnsubscribeEventHandlers();
		public virtual void UnsubscribeEventHandlers()
		{

			CrossConnectivity.Current.ConnectivityChanged -= OnConnectivityChanged;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
            App.Instance.NotificationReceived += App_NotificationReceived;;
			SubscribeEventHandlers();
		}

		void OnConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
		{

			if (!e.IsConnected)
			{
				//todo handle this
			}
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			App.Instance.NotificationReceived -= App_NotificationReceived;
			UnsubscribeEventHandlers();
		}

		private async Task Init()
		{
			await this.ViewModel.InitAsync();
		}
	}
}