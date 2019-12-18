using CounterValue.Models;
using CounterValue.Views;
using Newtonsoft.Json;
using OdesaoblenergoLib.Models.Requests;
using OdesaoblenergoLib.Models.Responses;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace CounterValue.ViewModels
{
    class AccountEntryPageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region Interfaces
        private ICommand _accountEntryPageButtonCommand;
        #endregion

        #region Properties
        private double progress;
        public double Progress //{ get; set; }
        {
            get { return progress; }
            set
            {
                if (progress != value)
                {
                    progress = value;
                    OnPropertyChanged("Progress");
                }
            }
        }
        public string LicLeft { get; set; }
        //{
        //    get { return lic; }
        //    set
        //    {
        //        if (lic != value && !string.IsNullOrEmpty(value) && Regex.IsMatch(value, @"^\d+$"))
        //        {
        //            lic = value;
        //            OnPropertyChanged("Lic");
        //        }
        //        else
        //        {
        //            if (string.IsNullOrEmpty(value))
        //                lic = "";
        //            OnPropertyChanged("Lic");
        //        }
        //    }
        //}

        public string LicRight { get; set; }
        //{
        //    get { return lic; }
        //    set
        //    {
        //        if (lic != value && !string.IsNullOrEmpty(value) && Regex.IsMatch(value, @"^\d+$"))
        //        {
        //            lic = value;
        //            OnPropertyChanged("Lic");
        //        }
        //        else
        //        {
        //            if (string.IsNullOrEmpty(value))
        //                lic = "";
        //            OnPropertyChanged("Lic");
        //        }
        //    }
        //}

        public string Lic { get; set; }

        public ICommand AccountEntryPageButtonCommand => _accountEntryPageButtonCommand ?? (_accountEntryPageButtonCommand = new Command(async () =>
        {
            Progress = .1;
            //await Application.Current.MainPage.Navigation.PushModalAsync(new CounterValuePageView());
            //return;
            Lic = LicLeft + "/" + LicRight;

            //если строка содержит "/"
            //if (Lic != null && Lic.Contains("/"))
            //{
            //start progreessbar
            try
            {
                HttpResponseMessage httpResponse;
                AbonentInfoResponse AbonentInfo;
                string response;

                using (var client = new HttpClient())
                {
                    FormUrlEncodedContent content;
                    string jsonValue;
                    //client.BaseAddress = new Uri(AppSettings.uri);

                    #region получение токена
                    //jsonValue = JsonConvert.SerializeObject(new TokenRequestParams { Secret_key = AppSettings.secretKey, Name = AppSettings.name });
                    //content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>(AppSettings.tokenMethod, jsonValue) });
                    //httpResponse = await client.PostAsync("/api/", content);
                    //httpResponse.EnsureSuccessStatusCode();
                    //response = await httpResponse.Content.ReadAsStringAsync();
                    //TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(response);
                    #endregion

                    Progress = .3;
                    #region получение адреса
                    //jsonValue = JsonConvert.SerializeObject(new AbonentInfoRequestParams { token = token.Response.Access_token, res = Lic.Substring(0, 2), abonent = "", lsch = Lic, proctype = "3", phoneno = "" });
                    //content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>(AppSettings.infoMethod, jsonValue) });
                    //httpResponse = await client.PostAsync("/api/", content);
                    //httpResponse.EnsureSuccessStatusCode();
                    response = "{\"response\":{\"AbonentId\":72416,\"Abonentname\":\"\\u041b\\u043e\\u0431\\u0430\\u043d\\u044c \\u041e\\u043b\\u0435\\u043a\\u0441\\u0430\\u043d\\u0434\\u0440 \\u0424\\u0435\\u0434\\u043e\\u0440\\u043e\\u0432\\u0438\\u0447\",\"lsch\":\"02001\\/128039\",\"Email\":\"loanna@ukr.net\",\"sendflag\":1,\"abonaddr\":\"\\u041e\\u0434\\u0435\\u0441\\u0441\\u0430, \\u041f\\u0443\\u0442\\u044c\\u043e\\u0432\\u0430 \\u0432\\u0443\\u043b\\u0438\\u0446\\u044f, 25\",\"eladdr\":\" 2 \\u0422\\u041f-6142 - \\u0422\\u041f-2592 - 2592\\/13 \\u0412\\u041b \\u041f\\u0440\\u043e\\u0441\\u0435\\u043b.,25-31 \\u0421\\u0430\\u043c\\u043e\\u043b.19 \\u041f\\u0443\\u0442\\u0435\\u0432\\u0430\\u044f2-20,3-29\",\"catname\":\"\\u041e\\u0441\\u043d\\u043e\\u0432\\u043d\\u0438\\u0439 \\u0441\\u043f\\u043e\\u0436\\u0438\\u0432\\u0430\\u0447\",\"Passport\":\"\\u041a\\u0415841032\",\"AbonentTaxCode\":\"1771402317\"}}";
                    AbonentInfo = JsonConvert.DeserializeObject<AbonentInfoResponse>(response);
                    #endregion
                    Progress = .5;
                    //если ответ корректный
                    if (!string.IsNullOrEmpty(AbonentInfo.response.lsch))
                    {
                        #region получение токена
                        //jsonValue = JsonConvert.SerializeObject(new TokenRequestParams { Secret_key = AppSettings.secretKey, Name = AppSettings.name });
                        //content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>(AppSettings.tokenMethod, jsonValue) });
                        //httpResponse = await client.PostAsync("/api/", content);
                        //httpResponse.EnsureSuccessStatusCode();
                        //response = await httpResponse.Content.ReadAsStringAsync();
                        //token = JsonConvert.DeserializeObject<TokenResponse>(response);
                        #endregion

                        Progress = .7;
                        #region получение данных счетчика
                        //jsonValue = JsonConvert.SerializeObject(new SelectIndicatorsRequestParams { token = token.Response.Access_token, abonent = AbonentInfo.response.AbonentId, res = Lic.Substring(0, 2) });
                        //content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>(AppSettings.selectIndicatorsMethod, jsonValue) });
                        //httpResponse = await client.PostAsync("/api/", content);
                        //httpResponse.EnsureSuccessStatusCode();
                        response = "{\"response\":{\"digits\":6,\"Zones\":1,\"value1\":31982,\"value2\":null,\"value3\":null,\"CntName\":\"\\u0421\\u041e\\u0415-5020\\u041d**\",\"zn1\":\"\\u0437\\u0432\\u0438\\u0447\\u0430\\u0439\\u043d\\u0438\\u0439 \\u043e\\u0431\\u043b\\u0456\\u043a (\\u0448\\u043a\\u0430\\u043b\\u0430 1)\",\"zn2\":\"\",\"zn3\":\"\",\"number\":\"0030585\"}}";
                        SelectIndicatorsResponse SelectIndicatorsResponse = JsonConvert.DeserializeObject<SelectIndicatorsResponse>(response);
                        #endregion
                        Progress = .9;
                        User User = new User { AbonentInfo = AbonentInfo, IndicatorsResponse = SelectIndicatorsResponse };

                        if (Int32.Parse(SelectIndicatorsResponse.response.Zones) != 1)
                        {
                            await Application.Current.MainPage.DisplayAlert("Уведомление", $"Для этого лицевого счета используйте приложение Line Of Light", "ОK");
                            return;
                        }

                        //записываем данные по лицевому в памяти устройсва
                        CrossSettings.Current.AddOrUpdateValue("lic", Newtonsoft.Json.JsonConvert.SerializeObject(User));

                        await Application.Current.MainPage.DisplayAlert("Уведомление", $"Все ок: {AbonentInfo.response.lsch} {AbonentInfo.response.abonaddr}", "ОK");
                        await Application.Current.MainPage.Navigation.PushModalAsync(new CounterValuePageView());
                        Progress = 0;
                        return;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Уведомление", $"Cпробуйте знову", "ОK");
                        Progress = 0;
                    }
                }
            }

            catch
            {
                await Application.Current.MainPage.DisplayAlert("Уведомление", $"Помилка, спробуйте знову", "ОK");
                Progress = 0;
            }
            //}
        }));
        #endregion
    }
}
