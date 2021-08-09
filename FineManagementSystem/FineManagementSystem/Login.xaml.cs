
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
            //check password and usename with MD5 encryption 
                try
                {
                    dr = Fine_Management_System.DBConnection.DB.Read("select fname, police_id from traffic_police_officer where password = '" + MD5Hashing.Encryption(password.Text) + "' AND fname='" + usrName.Text + "';");

                    dr.Read();
                    string id = "";
                    if (dr.GetString("fname").Equals(usrName.Text))
                    {
                        //textFields will be cleared after verification of the the password 
                        Acr.UserDialogs.UserDialogs.Instance.Toast("Login succeed!", new TimeSpan(3));
                        usrName.Text = "";
                        password.Text = "";
                        id = dr.GetString("police_id");
                        await Navigation.PushAsync(new Home(dr.GetString("fname"),id));
                        dr.Close();
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Toast("Incorrect username or password", new TimeSpan(3));
                        //new Error_messages.InputError("Login Failed", "Username or Password is incorrect").Show();
                    }


                }
                catch (Exception)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Login Failed!", new TimeSpan(3));
                    //new Error_messages.InputError("Login Failed", "Username or Password is incorrect").Show();
                }
            
            
        }
    }
}