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

            }
        }

        User User;
        public string Lic { get; set; }
        public string CounterValue { get; set; }

        //количество ячеек счeтчикa
        private int digits;
        private int visibleDigits;

        public bool IsVisibleDigit5 { get; set; }
        public bool IsVisibleDigit6 { get; set; }
        public bool IsVisibleDigit7 { get; set; }
        public bool IsVisibleDigit8 { get; set; }
        public bool IsVisibleDigit9 { get; set; }

        private ICommand _sendIndicators;

        public ICommand SendIndicators => _sendIndicators ?? (_sendIndicators = new Command(async () =>
        {
            string s = CounterValue;
            //await Application.Current.MainPage.Navigation.PushAsync(new AccountEntryPageView());
        }));
    }
}
