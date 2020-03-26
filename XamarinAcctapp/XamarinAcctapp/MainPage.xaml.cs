using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinAcctapp.Models;

namespace XamarinAcctapp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _client = new HttpClient();
        private const string Url = "https://myxamarinapi.azurewebsites.net/api/accounts";
        private ObservableCollection<Account> Account;
        public MainPage()
        {
            InitializeComponent();
        }

        async override protected void OnAppearing()
        {
            string responsecontent = await _client.GetStringAsync(Url);
            List<Account> mylist = JsonConvert.DeserializeObject<List<Account>>(responsecontent);
            Account = new ObservableCollection<Account>(mylist);
            ItemlistView.ItemsSource = Account;
            base.OnAppearing();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AccountPage()));
        }

        private async void ItemlistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var myselectedItem = e.Item as Account;
            await Navigation.PushAsync(new AccountDetails(myselectedItem.Id, myselectedItem.Password, myselectedItem.Username));
            ((ListView)sender).SelectedItem = null;
        }
    }
}
