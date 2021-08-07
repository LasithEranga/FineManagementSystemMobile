using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace FineManagementSystem
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

         void Loginke(object sender, EventArgs e)
        {
            App.Current.MainPage = new Login();
        }

        private async void Login(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }


    }
}
