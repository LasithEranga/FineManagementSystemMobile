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
        string id;
        private MySqlDataReader dr = null;
        
        public Home(string user_name, string id)
        {

            this.id = id;
            InitializeComponent();
            name.Text = user_name;
            Acr.UserDialogs.UserDialogs.Instance.Toast(id, new TimeSpan(3));
            try
            {
                dr = Fine_Management_System.DBConnection.DB.Read("SELECT COUNT(Ref_No) as count FROM fine_receipt WHERE officer_id = '"+id+"' AND Date= CURDATE()");
                while (dr.Read())
                {
                    num.Text = dr.GetInt16("count").ToString() + "  Receipts";
                }
                dr.Close();
            }
            catch (Exception)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast(id, new TimeSpan(3));

            }

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
            await Navigation.PushAsync(new Create(id));
        }


        

    }
}