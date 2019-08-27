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
    class CounterValuePageViewModel : INotifyPropertyChanged
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

        #region Fields
        //количество ячеек счeтчикa
        private int digits;
        //количество отображаемых ячеек
        private int visibleDigits;

        private DateTime date;
        private string lastData;
        private string penultimateData;
        private bool lastDataVisible;

        private int digit1;
        private int digit2;
        private int digit3;
        private int digit4;
        private int digit5;
        private int digit6;
        private int digit7;
        private int digit8;
        private int digit9;
        #endregion

        #region Properties
        User User { get; set; }
        public string Lic { get; set; }
        public string CounterValue { get; set; }

        public DateTime Date { get; set; }
        //{
        //    get { return date; }
        //    set
        //    {
        //        if (date != value)
        //        {
        //            date = value;
        //            OnPropertyChanged("Date");
        //        }
        //    }
        //}

        public bool LastDataVisible
        {
            get { return lastDataVisible; }
            set
            {
                if (lastDataVisible != value)
                {
                    lastDataVisible = value;
                    OnPropertyChanged("LastDataVisible");
                }
            }
        }
        public string LastData
        {
            get { return lastData; }
            set
            {
                if (lastData != value)
                {
                    lastData = value;
                    OnPropertyChanged("LastData");
                }
            }
        }

        public string PenultimateData
        {
            get { return penultimateData; }
            set
            {
                if (penultimateData != value)
                {
                    penultimateData = value;
                    OnPropertyChanged("PenultimateData");
                }
            }
        }
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

        public ICommand BtnTappedCommand { get; set; }

        public ICommand SendIndicators => _sendIndicators ?? (_sendIndicators = new Command(async () =>
        {
            string first = Digit9.ToString() + Digit8.ToString() + Digit7.ToString() + Digit6.ToString() + Digit5.ToString() + Digit4.ToString() + Digit3.ToString() + Digit2.ToString() + Digit1.ToString();
            if (long.Parse(first) <= long.Parse(User.IndicatorsResponse.response.value1))
            {
                //останавливаем прогрессбар и активируем кнопку
                //StopProgressBarEnableButton();
                await Application.Current.MainPage.DisplayAlert("Уведомление", $"Попередні показники вище або рівні", "ОK");
                return;
            }
            PenultimateData = LastData;
            LastData =$"{Date.Day}.{Date.Month}.{Date.Year} / {long.Parse(first)}";
            LastDataVisible = true;

            User.IndicatorsResponse.response.value1 = long.Parse(first).ToString();
            User.LastData = LastData;
            User.PenultimateData = PenultimateData;
            //записываем данные по лицевому в памяти устройсва
            CrossSettings.Current.AddOrUpdateValue("lic", Newtonsoft.Json.JsonConvert.SerializeObject(User));
        }));
        #endregion

        private ICommand _sendIndicators;

        #region Constructor
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
                Int32.TryParse(User.IndicatorsResponse.response.digits, out digits);
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
                Digit1 = 0;
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

                BtnTappedCommand = new Command<string>(BtnTapped);

                if (!string.IsNullOrEmpty(User.LastData))
                    LastDataVisible = true;
                LastData = User.LastData;
                PenultimateData = User.PenultimateData;

                Date = DateTime.Now;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Увеличивет значение цифры на кнопке на единицу(если 9 то сбрасывает на 0)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private int DigitIncrement(int param)
        {
            if (param == 9)
                param = 0;
            else param++;
            return param;
        }

        /// <summary>
        /// Уменьшает значение цифры на кнопке на единицу(если 0 то сбрасывает на 9)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private int DigitDecrement(int param)
        {
            if (param == 0)
                param = 9;
            else param--;
            return param;
        }

        /// <summary>
        /// Оработчик нажатия кнопок
        /// </summary>
        /// <param name="param"></param>
        private void BtnTapped(string param)
        {
            switch (param)
            {
                case "1Up":
                    Digit1 = DigitIncrement(Digit1);
                    break;
                case "2Up":
                    Digit2 = DigitIncrement(Digit2);
                    break;
                case "3Up":
                    Digit3 = DigitIncrement(Digit3);
                    break;
                case "4Up":
                    Digit4 = DigitIncrement(Digit4);
                    break;
                case "5Up":
                    Digit5 = DigitIncrement(Digit5);
                    break;
                case "6Up":
                    Digit6 = DigitIncrement(Digit6);
                    break;
                case "7Up":
                    Digit7 = DigitIncrement(Digit7);
                    break;
                case "8Up":
                    Digit8 = DigitIncrement(Digit8);
                    break;
                case "9Up":
                    Digit9 = DigitIncrement(Digit9);
                    break;
                case "1Down":
                    Digit1 = DigitDecrement(Digit1);
                    break;
                case "2Down":
                    Digit2 = DigitDecrement(Digit2);
                    break;
                case "3Down":
                    Digit3 = DigitDecrement(Digit3);
                    break;
                case "4Down":
                    Digit4 = DigitDecrement(Digit4);
                    break;
                case "5Down":
                    Digit5 = DigitDecrement(Digit5);
                    break;
                case "6Down":
                    Digit6 = DigitDecrement(Digit6);
                    break;
                case "7Down":
                    Digit7 = DigitDecrement(Digit7);
                    break;
                case "8Down":
                    Digit8 = DigitDecrement(Digit8);
                    break;
                case "9Down":
                    Digit9 = DigitDecrement(Digit9);
                    break;
            }
        }
        #endregion
    }
}
