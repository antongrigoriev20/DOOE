using CounterValue.Views;
using Newtonsoft.Json;
using OdesaoblenergoLib.Models.Requests;
using OdesaoblenergoLib.Models.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CounterValue.ViewModels
{
    class AccountEntryPageViewModel
    {
        #region Interfaces
        private ICommand _accountEntryPageButtonCommand;
        #endregion

        #region Properties
        public string Lic { get; set; }

        public ICommand AccountEntryPageButtonCommand => _accountEntryPageButtonCommand ?? (_accountEntryPageButtonCommand = new Command(async () =>
        {
            //если строка содержит "/"
            if (Lic != null && Lic.Contains("/"))
            {
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
                        client.BaseAddress = new Uri(AppSettings.uri);

                        #region получение токена
                        jsonValue = JsonConvert.SerializeObject(new TokenRequestParams { Secret_key = AppSettings.secretKey, Name = AppSettings.name });
                        content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>(AppSettings.tokenMethod, jsonValue) });
                        httpResponse = await client.PostAsync("/api/", content);
                        httpResponse.EnsureSuccessStatusCode();
                        response = await httpResponse.Content.ReadAsStringAsync();
                        TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(response);
                        #endregion

                        #region получение адреса
                        jsonValue = JsonConvert.SerializeObject(new AbonentInfoRequestParams { token = token.Response.Access_token, res = Lic.Substring(0, 2), abonent = "", lsch = Lic, proctype = "3", phoneno = "" });
                        content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>(AppSettings.infoMethod, jsonValue) });
                        httpResponse = await client.PostAsync("/api/", content);
                        httpResponse.EnsureSuccessStatusCode();
                        response = await httpResponse.Content.ReadAsStringAsync();
                        AbonentInfo = JsonConvert.DeserializeObject<AbonentInfoResponse>(response);
                        #endregion

                        //если ответ корректный
                        if (!string.IsNullOrEmpty(AbonentInfo.response.lsch))
                        {
                            await Application.Current.MainPage.DisplayAlert("Уведомление", $"Все ок: {AbonentInfo.response.lsch} {AbonentInfo.response.abonaddr}" , "ОK");
                            await Application.Current.MainPage.Navigation.PushModalAsync(new CounterValuePageView());
                            return;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Уведомление", $"Cпробуйте знову", "ОK");
                        }
                    }
                }

                catch
                {
                    await Application.Current.MainPage.DisplayAlert("Уведомление", $"Помилка, спробуйте знову", "ОK");
                }
            }           
        }));
        #endregion
    }
}
