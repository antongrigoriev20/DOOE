using CounterValue.Models;
using Newtonsoft.Json;
using OdesaoblenergoLib.Models.Responses;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CounterValue.ViewModels
{
    class CounterValuePageViewModel:INotifyPropertyChanged
    {
        #region Implement INotyfyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public CounterValuePageViewModel()
        {
            //получаем из памяти устройства строку с данными пользователя
            string name = CrossSettings.Current.GetValueOrDefault("lic", null);
            if (!string.IsNullOrEmpty(name))
            {
                //десериализуем из строки данные пользователя в экземпляр объекта User
                User = JsonConvert.DeserializeObject<User>(name);
                //получаем лицевой счет
                Lic = User.AbonentInfo.response.lsch;
                //получаем значение счетчика
                CounterValue = User.IndicatorsResponse.response.value1;
                //получаем количество цифр счетчика
                Int32.TryParse( User.IndicatorsResponse.response.digits,out digits);
                //получаем отображаемое количество цифр
                visibleDigits = CounterValue.Length + 1;
                //если отображаемое количество цифр меньше 4 делаем его равным 4(минимальное количество значений счетчика)
                if (visibleDigits < 4)
                    visibleDigits = 4;
                //если отображаемое количество больше чем физическое количество цифр на счетчике делаем его равным количеству цифр счетчика
                if (visibleDigits > digits)
                    visibleDigits = digits;
                //делаем видимыми ячейки
                switch (visibleDigits)
                {
                    case 5:
                        IsVisibleDigit5 = true;
                        break;
                    case 6:
                        IsVisibleDigit5 = true;
                        IsVisibleDigit6 = true;
                        break;
                    case 7:
                        IsVisibleDigit5 = true;
                        IsVisibleDigit6 = true;
                        IsVisibleDigit7 = true;
                        break;
                    case 8:
                        IsVisibleDigit5 = true;
                        IsVisibleDigit6 = true;
                        IsVisibleDigit7 = true;
                        IsVisibleDigit8 = true;
                        break;
                    case 9:
                        IsVisibleDigit5 = true;
                        IsVisibleDigit6 = true;
                        IsVisibleDigit7 = true;
                        IsVisibleDigit8 = true;
                        IsVisibleDigit9 = true;
                        break;
                }
                Digit1 = 1;
                Digit2 = 0;
                Digit3 = 0;
                Digit4 = 0;
                Digit5 = 0;
                Digit6 = 0;
                Digit7 = 0;
                Digit8 = 0;
                Digit9 = 0;

                string tempValue = CounterValue;
                if (tempValue != null)
                {
                    for (int i = 0; tempValue.Length < 9; i++)
                        tempValue = "0" + tempValue;
                    if (tempValue.Length == 9)
                    {
                        Digit1 = int.Parse(tempValue[8].ToString());
                        Digit2 = int.Parse(tempValue[7].ToString());
                        Digit3 = int.Parse(tempValue[6].ToString());
                        Digit4 = int.Parse(tempValue[5].ToString());
                        Digit5 = int.Parse(tempValue[4].ToString());
                        Digit6 = int.Parse(tempValue[3].ToString());
                        Digit7 = int.Parse(tempValue[2].ToString());
                        Digit8 = int.Parse(tempValue[1].ToString());
                        Digit9 = int.Parse(tempValue[0].ToString());
                    }
                }
            }
        }

        User User;
        public string Lic { get; set; }
        public string CounterValue { get; set; }

        //количество ячеек счeтчикa
        private int digits;
        private int visibleDigits;

        private int digit1;
        private int digit2;
        private int digit3;
        private int digit4;
        private int digit5;
        private int digit6;
        private int digit7;
        private int digit8;
        private int digit9;

        public bool IsVisibleDigit5 { get; set; }
        public bool IsVisibleDigit6 { get; set; }
        public bool IsVisibleDigit7 { get; set; }
        public bool IsVisibleDigit8 { get; set; }
        public bool IsVisibleDigit9 { get; set; }

        public int Digit1
        {
            get { return digit1; }
            set
            {
                if (digit1 != value)
                {
                    digit1 = value;
                    OnPropertyChanged("Digit1");
                }
            }
        }

        public int Digit2
        {
            get { return digit2; }
            set
            {
                if (digit2 != value)
                {
                    digit2 = value;
                    OnPropertyChanged("Digit2");
                }
            }
        }
        public int Digit3
        {
            get { return digit3; }
            set
            {
                if (digit3 != value)
                {
                    digit3 = value;
                    OnPropertyChanged("Digit3");
                }
            }
        }
        public int Digit4
        {
            get { return digit4; }
            set
            {
                if (digit4 != value)
                {
                    digit4 = value;
                    OnPropertyChanged("Digit4");
                }
            }
        }
        public int Digit5
        {
            get { return digit5; }
            set
            {
                if (digit5 != value)
                {
                    digit5 = value;
                    OnPropertyChanged("Digit5");
                }
            }
        }
        public int Digit6
        {
            get { return digit6; }
            set
            {
                if (digit6 != value)
                {
                    digit6 = value;
                    OnPropertyChanged("Digit6");
                }
            }
        }

        public int Digit7
        {
            get { return digit7; }
            set
            {
                if (digit7 != value)
                {
                    digit7 = value;
                    OnPropertyChanged("Digit7");
                }
            }
        }
        public int Digit8
        {
            get { return digit8; }
            set
            {
                if (digit8 != value)
                {
                    digit8 = value;
                    OnPropertyChanged("Digit8");
                }
            }
        }
        public int Digit9
        {
            get { return digit9; }
            set
            {
                if (digit9 != value)
                {
                    digit9 = value;
                    OnPropertyChanged("Digit9");
                }
            }
        }

        private ICommand _sendIndicators;

        public ICommand SendIndicators => _sendIndicators ?? (_sendIndicators = new Command(async () =>
        {
            string s = CounterValue;
            //await Application.Current.MainPage.Navigation.PushAsync(new AccountEntryPageView());
        }));
    }
}
