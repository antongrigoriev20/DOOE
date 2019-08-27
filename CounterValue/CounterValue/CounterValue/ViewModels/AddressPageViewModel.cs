using CounterValue.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CounterValue.ViewModels
{
    class AddressPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Cities { get; set; }
        private Dictionary<string, Dictionary<string, string>> OdeskaOblData;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private string _searchText { get; set; }
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                }
                OnPropertyChanged("SearchText");
            }
        }

        private ICommand _nextPageButtonCommand;

        public ICommand NextPageButtonCommand => _nextPageButtonCommand ?? (_nextPageButtonCommand = new Command(async () =>
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CounterValuePageView());

        }));


        private ICommand _searchCommand;

        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new Command<string>((text) =>
        {
            if (text.Length >= 3)
            {
                Cities.Clear();
                foreach (var city in OdeskaOblData)
                {
                    if (city.Key.ToLower().Contains(text.ToLower()))
                        Cities.Add(city.Key);
                }
                //Recipes.Clear();
                //Init();
                //var suggestions = Recipes.Where(c => c.RecipeName.ToLower().StartsWith(text.ToLower())).ToList();
                //Recipes.Clear();
                //foreach (var recipe in suggestions)
                //    Recipes.Add(recipe);

            }
            else
            {
                Cities.Clear();
                //Recipes.Clear();
                //Init();
                //ListViewVisible = true;
                //SuggestionsListViewVisible = false;
            }

        }));

        public AddressPageViewModel()
        {
            title = "Введите адрес";
            Cities = new ObservableCollection<string>();
            //Cities.Add("111");
            //Cities.Add("222");
            //Cities.Add("333");


            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("CounterValue.OdeskaOblJson.txt");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
                OdeskaOblData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(text);
            }

        }

        //public ICommand SearchCommand
        //{
        //    get
        //    {
        //        return _searchCommand ?? (_searchCommand = new Command<string>((text) =>
        //        {
        //            // THIS IS WHAT I DON'T KNOW WHAT TO DO**
        //    }));
        //    }
        //}
    }
}
