using CounterValue.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CounterValue.ViewModels
{
    class MainPageViewModel
    {

        private ICommand _addressPageButtonCommand;

        public ICommand AddressPageButtonCommand => _addressPageButtonCommand ?? (_addressPageButtonCommand = new Command(async () =>
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddressPageView());

        }));

        private ICommand _accountEntryPageButtonCommand;

        public ICommand AccountEntryPageButtonCommand => _accountEntryPageButtonCommand ?? (_accountEntryPageButtonCommand = new Command(async () =>
       {
           await Application.Current.MainPage.Navigation.PushAsync(new AccountEntryPageView());
       }));
    }
}
