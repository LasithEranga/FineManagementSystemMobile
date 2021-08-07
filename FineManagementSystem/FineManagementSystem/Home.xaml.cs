using MySqlConnector;
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

        public Home(string user_name, string id)
        {
            MySqlDataReader dr=null;
            
            InitializeComponent();
            name.Text = user_name;
            /*try
            {
                dr = Fine_Management_System.DBConnection.DB.Read("SELECT COUNT(Ref_No) as count FROM fine_receipt WHERE officer_id = "+id+" AND Date= CURDATE()");
                while (dr.Read())
                {
                    DisplayAlert(dr.GetString("count"), "", "Ok");
                    num.Text = dr.GetString("count");
                }
                
            }
            catch (Exception ex){
                DisplayAlert(ex.Message,"","Ok");
            }*/

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