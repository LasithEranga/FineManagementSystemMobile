using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FineManagementSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
        }
        private async void SearchDriver(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Search());
        }

        private async void AddDriver(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Add());
        }

        private async void CreateFineReciept(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Create());
        }


        

    }
}