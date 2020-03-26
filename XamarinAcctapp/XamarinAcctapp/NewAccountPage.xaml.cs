using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinAcctapp.Models;

namespace XamarinAcctapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        private const string url = "https://myxamarinapi.azurewebsites.net/api/accounts";
       public Account account { get; set; }
       public string UserName { get; set; }
        public string Password { get; set; }
        
        public AccountPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {

            account = new Account
            {
                Username = this.UserName,
                Password = this.Password
            };
            var serializedItem = JsonConvert.SerializeObject(account);
            StringContent body = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var result = await client.PostAsync(url, body);
            string data = await result.Content.ReadAsStringAsync();
            if(result.IsSuccessStatusCode)
            {

                var answer = await DisplayAlert("Sucess", "Do you want to continue", "Yes", "No");
                if(answer)
                {


                }
                else
                {

                }
            }
        }
    }
}