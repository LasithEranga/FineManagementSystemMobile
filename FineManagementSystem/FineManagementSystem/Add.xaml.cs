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
    public partial class Add : ContentPage
    {
        private MySqlConnection conn;
        private string query;
        public Add()
        {
            InitializeComponent();

        }

        private async void AddDriver(object sender, EventArgs e)
        {
            MySqlDataReader dr = null;
            conn = Fine_Management_System.DBConnection.DB.GetConn();
            query = "SELECT nic FROM driver WHERE nic = '981145893V'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            dr = cmd.ExecuteReader();
            //verification
            try
            {
                dr.Read();
                if (dr.GetString("nic").Equals(nic.Text))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("NIC already exists!", new TimeSpan(3));
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Proceed", new TimeSpan(3));
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast(ex.Message, new TimeSpan(3));
            }
           
            //validation 

        }
    }
}