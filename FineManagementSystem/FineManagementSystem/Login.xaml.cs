
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FineManagementSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void Go(object sender, EventArgs e)
        {
            MySqlDataReader dr = null;
            dr = Fine_Management_System.DBConnection.DB.Read("select fname, police_id from traffic_police_officer where police_id = "+password.Text+";");

            try
            {
                //check password with MD5 encryption 
                dr.Read();
                
                if (dr.GetString("fname").Equals(usrName.Text) && dr.GetString("police_id").Equals(password.Text))
                {
                    //textFields will be cleared after verification of the the password 
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Logged", new TimeSpan(3));
                    await Navigation.PushAsync(new Home());
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Incorrect username or password", new TimeSpan(3));
                    //new Error_messages.InputError("Login Failed", "Username or Password is incorrect").Show();
                }


            }
            catch (Exception ec)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast(ec.Message+"jksdnf", new TimeSpan(3));
                //new Error_messages.InputError("Login Failed", "Username or Password is incorrect").Show();
            }
            
        }
    }
}