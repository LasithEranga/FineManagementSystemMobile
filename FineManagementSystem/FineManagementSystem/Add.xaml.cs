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
            query = "SELECT nic FROM driver WHERE nic ='"+ nic.Text +"';";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            /*dr = cmd.ExecuteReader();*/
            //verification
            try
            {

                /*dr.Read();
                if (dr.GetString("nic").Length>0)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("NIC already exists!", new TimeSpan(3));
                    dr.Close();
                }*/
                
                    //nic doesn't exit saving details after validation
                    query = "INSERT INTO `driver`(`nic`, `fname`, `lname`, `full_name`, `email`, `contact_no`, `address`) VALUES('"+nic.Text+"','"+fname.Text+"','"+lname.Text+"','"+full_name.Text+"','"+email.Text+"','"+phoneNo.Text+"','"+address.Text+"')";
                    cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Driver Details Saved!", new TimeSpan(3));
                    await Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast(ex.Message, new TimeSpan(3));
            }
           
            //validation 

        }
    }
}