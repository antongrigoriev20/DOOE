using CounterValue.Views;
using Plugin.Settings;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CounterValue
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CrossSettings.Current.Clear();
            string name = CrossSettings.Current.GetValueOrDefault("lic", null);

            if (string.IsNullOrEmpty(name))
                MainPage = new NavigationPage(new MainPage());
            else
                MainPage = new CounterValuePageView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
