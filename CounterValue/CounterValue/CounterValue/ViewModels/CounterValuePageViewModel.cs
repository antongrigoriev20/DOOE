using CounterValue.Models;
using Newtonsoft.Json;
using OdesaoblenergoLib.Models.Responses;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CounterValue.ViewModels
{
    class CounterValuePageViewModel
    {      
        public CounterValuePageViewModel()
        {
            string name = CrossSettings.Current.GetValueOrDefault("lic", null);
            if (!string.IsNullOrEmpty(name))
            {
                User = JsonConvert.DeserializeObject<User>(name);
                Lic = User.AbonentInfo.response.lsch;
            }
        }

        User User;
        public string Lic { get; set; }

        private ICommand _sendIndicators;

        public ICommand SendIndicators => _sendIndicators ?? (_sendIndicators = new Command(async () =>
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new AccountEntryPageView());
        }));
    }
}
