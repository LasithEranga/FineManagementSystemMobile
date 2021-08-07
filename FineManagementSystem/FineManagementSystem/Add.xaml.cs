using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
            if (nic.Text.Length > 0 && fname.Text.Length > 0 && lname.Text.Length>0 && address.Text.Length>0 && full_name.Text.Length>0 && phoneNo.Text.Length >0 && no_plate.Text.Length>0)
            {
                //proceed if all fields are filled 
                //then validate phoneno license no email nic and other text fields
                //ValidatePhoneNo();
                ValidateTextOnly();
            }
            else
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Not All Fields are Filled!", new TimeSpan(3));
            }

            /*MySqlDataReader dr = null;
            conn = Fine_Management_System.DBConnection.DB.GetConn();
            query = "SELECT nic FROM driver WHERE nic ='"+ nic.Text +"';";
            MySqlCommand cmd = new MySqlCommand(query, conn);*/

            /*dr = cmd.ExecuteReader();*/
            //verification
            /*try
            {

                *//*dr.Read();
                if (dr.GetString("nic").Length>0)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("NIC already exists!", new TimeSpan(3));
                    dr.Close();
                }*//*
                
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
            }*/

            //validation 

        }

        private bool ValidatePhoneNo() {
            Regex rgx = new Regex(@"^\d{10}$");

            if (rgx.IsMatch(phoneNo.Text))
            {
                return true;
            }
            else
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Invalid Contact Number", new TimeSpan(3));
                return false;
            }
            
        }

        private bool Validate_Email()
        {
            if (email.Text.Length > 0)
            {
                try
                {
                    var emailaddr = new MailAddress(email.Text);
                    return true;
                }
                catch (FormatException b)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Invalid Email Address", new TimeSpan(3));
                    return false;
                }

            }
            else
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Email Address Required", new TimeSpan(3));
                return false;
            }
        }

        private bool ValidateTextOnly()
        {
            Regex reg = new Regex("^[a-z]+$");
            if (reg.IsMatch(fname.Text))
            {
                return true;
            }
            else
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Invalid Name!", new TimeSpan(3));
                return false;
            }
        }

        

    }
}