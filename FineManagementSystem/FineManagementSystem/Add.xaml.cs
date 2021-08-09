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
            if ( ValidateText(fname) && ValidateText(lname) && ValidateText(full_name) && ValidateAddress(address) && ValidateEmail(email) && Validate_NIC(nic) && ValidatePhoneNo(phoneNo) && ValidateLicense(license) && ValidateNumberPlate(no_plate))
            {
                //proceed if all fields are filled 
                //then validate phoneno license no email nic and other text fields
                
                MySqlDataReader dr = null;
                conn = Fine_Management_System.DBConnection.DB.GetConn();

                MySqlCommand cmd;
                //verification
                try
                {
                        //nic doesn't exit saving details after validation
                        query = "INSERT INTO `driver`(`nic`, `fname`, `lname`, `full_name`, `email`, `contact_no`, `address`,`license_no`, `vehicle_no`) VALUES('" + nic.Text+"','"+fname.Text+"','"+lname.Text+"','"+full_name.Text+"','"+email.Text+"','"+phoneNo.Text+"','"+address.Text+"','"+license.Text+"','"+no_plate.Text+"')";
                        cmd = new MySqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        Acr.UserDialogs.UserDialogs.Instance.Toast("Driver Details Saved!", new TimeSpan(3));
                        await Navigation.PopAsync();

                }
                catch (MySqlException ex)
                {
                    if(ex.Message.Contains("Duplicate entry"))
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Toast("Driver already exist", new TimeSpan(3));
                    }

                }
                catch (Exception)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Something went wrong!", new TimeSpan(3));
                }
            }
            else
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Some fields are empty or check for invalid fields", new TimeSpan(3));
            }


        }

        private bool ValidatePhoneNo(Entry label) {
            Regex rgx = new Regex(@"^\d{10}$");

            if (rgx.IsMatch(label.Text))
            {
                label.TextColor = Color.LightGreen;
                return true;
            }
            else
            {
                label.TextColor = Color.LightPink;
                Acr.UserDialogs.UserDialogs.Instance.Toast("Invalid Contact Number", new TimeSpan(3));
                return false;
            }
            
        }

        private bool ValidateEmail(Entry email)
        {
            if (email.Text.Length > 0)
            {
                try
                {
                    var emailaddr = new MailAddress(email.Text);
                    email.TextColor = Color.LightGreen;
                    return true;
                }
                catch (FormatException b)
                {
                    email.TextColor = Color.LightPink;
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

        private bool ValidateText(Entry label)
        {
            if (label.Text.Length > 0)
            {
                Regex reg = new Regex(@"^[a-zA-Z\s]+$");
                if (reg.IsMatch(label.Text))
                {
                    label.TextColor = Color.LightGreen;
                    return true;
                }
                else
                {
                    label.TextColor = Color.LightPink;
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Check for invalid fields!", new TimeSpan(3));
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
        private bool ValidateAddress(Entry label)
        {
            if (label.Text.Length > 0)
            {
                string text = label.Text;
                Regex rgx = new Regex(@"^[a-zA-Z0-9\/\s,]+$");
                if (rgx.IsMatch(label.Text))
                {
                    label.TextColor = Color.LightGreen;
                    return true;
                }
                else
                {
                    label.TextColor = Color.LightPink;
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Check for invalid fields!", new TimeSpan(3));
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        private bool Validate_NIC(Entry label)
        {

            if (label.Text.Length == 10)
            {
                //pattern1
                Regex rgx = new Regex(@"^\d{9}[vxVX]{1}$");
                if (rgx.IsMatch(label.Text))
                {
                    label.TextColor = Color.LightGreen;
                    return true;
                }
                else
                {
                    label.TextColor = Color.LightPink;
                    return false;
                }

            }

            else if (label.Text.Length == 12)
            {
                //pattern 2
                Regex rgx = new Regex(@"^\d{12}$");
                if (rgx.IsMatch(label.Text))
                {
                    label.TextColor = Color.LightGreen;
                    return true;
                }
                else
                {
                    label.TextColor = Color.LightPink;
                    return false;
                }

            }
            else
            {
                //empty
                return false;
            }
        }

        private bool ValidateLicense(Entry label)
        {
            if (label.Text.Length > 0)
            {
                Regex reg = new Regex(@"^B\d{7}$");
                if (reg.IsMatch(label.Text))
                {
                    label.TextColor = Color.LightGreen;
                    return true;
                }
                else
                {
                    label.TextColor = Color.LightPink;
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Invalid Vehicle No!", new TimeSpan(3));
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        private bool ValidateNumberPlate(Entry label)
            {
            if (label.Text.Length > 0 && label.Text.Length<9)
            {
                Regex reg = new Regex(@"^[a-zA-Z0-9\s]+$");
                if (reg.IsMatch(label.Text))
                {
                    label.TextColor = Color.LightGreen;
                    return true;
                }
                else
                {
                    label.TextColor = Color.LightPink;
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Invalid Vehicle No!", new TimeSpan(3));
                    return false;
                }
            }
            else
            {
                return false;
            }

        }



    }
}