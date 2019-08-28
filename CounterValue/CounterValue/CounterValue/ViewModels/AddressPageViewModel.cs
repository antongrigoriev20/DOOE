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
        private Dictionary<string, Dictionary<string, string>> odeskaOblData;
        private Dictionary<string, string> city;

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

        private ObservableCollection<string> cities;
        public ObservableCollection<string> Cities //{ get; set; }
        {
            get { return cities; }
            set
            {
                if (cities != value)
                {
                    cities = value;
                    OnPropertyChanged("Cities");
                }
            }
        }

        private bool cityVisible;
        public bool CityVisible
        {
            get { return cityVisible; }
            set
            {
                if (cityVisible != value)
                {
                    cityVisible = value;
                    OnPropertyChanged("CityVisible");
                }
            }
        }
        private string searchCity { get; set; }
        public string SearchCity
        {
            get
            {
                return searchCity;
            }
            set
            {
                if (searchCity != value)
                {
                    CityVisible = true;
                    searchCity = value;
                    SearchStreetVisible = false;
                    SearchStreet = "";
                }
                OnPropertyChanged("SearchCity");
            }
        }

        public ICommand ItemCityClickCommand
        {
            get
            {
                return new Command((item) => 
                {
                    if (searchCity != item as string)
                    {
                        SearchCity = item as string;

                        odeskaOblData.TryGetValue(SearchCity, out city);
                        CityVisible = false;
                        SearchStreetVisible = true;
                    }
                });
            }
        }


        private ICommand _searchCityCommand;

        public ICommand SearchCityCommand => _searchCityCommand ?? (_searchCityCommand = new Command<string>((text) =>
        {
            if (text.Length >= 3)
            {
                Cities.Clear();
                foreach (var city in odeskaOblData)
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

        //--------------------------------
       
        public string SelectedStreet
        {
            set
            {
                if (searchStreet != value)
                {
                    SearchStreet = value;

                    //city.TryGetValue(SearchStreet, out street);
                    //var t = city;
                    StreetVisible = false;
                }
            }
        }
        private string searchStreet { get; set; }
        public string SearchStreet
        {
            get { return searchStreet; }
            set
            {
                if (searchStreet != value)
                {
                    StreetVisible = true;
                    searchStreet = value;
                }
                OnPropertyChanged("SearchStreet");
            }
        }

        private bool searchStreetVisible;
        public bool SearchStreetVisible
        {
            get { return searchStreetVisible; }
            set
            {
                if (searchStreetVisible != value)
                {
                    searchStreetVisible = value;
                    OnPropertyChanged("SearchStreetVisible");
                }
            }
        }

        private bool streetVisible;
        public bool StreetVisible
        {
            get { return streetVisible; }
            set
            {
                if (streetVisible != value)
                {
                    streetVisible = value;
                    OnPropertyChanged("StreetVisible");
                }
            }
        }

        public ObservableCollection<string> Streets { get; set; }

        private ICommand searchStreetCommand;

        public ICommand SearchStreetCommand => searchStreetCommand ?? (searchStreetCommand = new Command<string>((text) =>
        {
            if (text.Length >= 3)
            {
                Streets.Clear();
                foreach (var street in city)
                {
                    if (street.Key.ToLower().Contains(text.ToLower()))
                        Streets.Add(street.Key);
                }
            }
            else
            {
                Streets.Clear();
            }

        }));


        private ICommand _nextPageButtonCommand;

        public ICommand NextPageButtonCommand => _nextPageButtonCommand ?? (_nextPageButtonCommand = new Command(async () =>
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CounterValuePageView());

        }));

        public AddressPageViewModel()
        {
            title = "Введите адрес";
            Cities = new ObservableCollection<string>();
            Streets= new ObservableCollection<string>(); 
            CityVisible = false;
            StreetVisible = false;
            SearchStreetVisible = false;

            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("CounterValue.OdeskaOblJson.txt");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
                odeskaOblData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(text);
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
