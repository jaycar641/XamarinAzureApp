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
    public partial class AccountDetails : ContentPage
    {
        public Account myaccount { get; set; }
        private const string url = "https://myxamarinapi.azurewebsites.net/api/accounts";
        public AccountDetails(int myeditid, string myeditpassword, string myeditusername)
        {
            InitializeComponent();

            MyUserName.Text = myeditusername;
            MyPassword.Text = myeditpassword;
            Application.Current.Properties["id"] = myeditid;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var id = Application.Current.Properties["id"];
            int i = int.Parse(id.ToString());

            myaccount = new Account
            {
                Id = i,
                Username = MyUserName.Text,
                Password = MyPassword.Text
            };

            var serializedItem = JsonConvert.SerializeObject(myaccount);
            StringContent body = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var result = await client.PutAsync($"{url}/{i}", body);
            string data = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {

                var answer = await DisplayAlert("Sucess", "Do you want to continue", "Yes", "No");
                if (answer)
                {


                }
                else
                {

                }
            }
        }
    }
}