using CounterValue.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CounterValue.ViewModels
{
    class AccountEntryPageViewModel
    {
        private ICommand _accountEntryPageButtonCommand;

        public ICommand AccountEntryPageButtonCommand => _accountEntryPageButtonCommand ?? (_accountEntryPageButtonCommand = new Command(async () =>
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CounterValuePageView());

        }));
    }
}
