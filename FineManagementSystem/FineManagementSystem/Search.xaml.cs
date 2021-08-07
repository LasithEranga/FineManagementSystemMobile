using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FineManagementSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        ObservableCollection<string> data = new ObservableCollection<string>();

        public Search()
        {
            InitializeComponent();
            ListOfStore();
        }
        MySqlConnection conn = Fine_Management_System.DBConnection.DB.GetConn();
        public async void ListOfStore() //List of Countries  
        {
            MySqlDataReader dr = null;
            MySqlCommand cmd;
            string query = "select police_id from traffic_police_officer";
            try
            {
                cmd = new MySqlCommand(query, conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(dr.GetString("police_id"));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("", "" + ex, "Ok");
            }
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            countryListView.IsVisible = true;
            countryListView.BeginRefresh();

            try
            {
                var dataEmpty = data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                    countryListView.IsVisible = false;
                else if (dataEmpty.Max().Length == 0)
                    countryListView.IsVisible = false;
                else
                    countryListView.ItemsSource = data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));
            }
            catch (Exception ex)
            {
                countryListView.IsVisible = false;

            }
            countryListView.EndRefresh();

        }


        private void ListView_OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            //EmployeeListView.IsVisible = false;  

            String listsd = e.Item as string;
            searchBar.Text = listsd;
            countryListView.IsVisible = false;

            ((ListView)sender).SelectedItem = null;
        }



    }
}