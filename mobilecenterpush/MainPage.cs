using System;
using Xamarin.Forms;
namespace mobilecenterpush
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {

            Title = "Main Page";


            var button = new Button
            {
                Text = "Click me"
            };

            Content = button;
        }

    }
}
