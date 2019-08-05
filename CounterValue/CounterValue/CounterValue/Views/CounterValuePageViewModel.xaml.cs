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
    public partial class CounterValuePageViewModel : ContentPage
    {
        public CounterValuePageViewModel()
        {
            InitializeComponent();
            BindingContext = new CounterValuePageViewModel();
        }
    }
}