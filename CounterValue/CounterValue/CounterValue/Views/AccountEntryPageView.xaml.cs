using CounterValue.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CounterValue.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountEntryPageView : ContentPage
    {
        public AccountEntryPageView()
        {
            InitializeComponent();
            BindingContext = new AccountEntryPageViewModel();
        }
    }
}