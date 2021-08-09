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
        public async void ListOfStore()   
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
            catch (Exception)
            {
                await DisplayAlert("", "Network Error!", "Ok");
            }
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SearchListView.IsVisible = true;
            SearchListView.BeginRefresh();

            try
            {
                var dataEmpty = data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                    SearchListView.IsVisible = false;
                else if (dataEmpty.Max().Length == 0)
                    SearchListView.IsVisible = false;
                else
                    SearchListView.ItemsSource = data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));
            }
            catch (Exception)
            {
                SearchListView.IsVisible = false;

            }
            SearchListView.EndRefresh();

        }


        private void ListView_OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            
            String listsd = e.Item as string;
            searchBar.Text = listsd;
            SearchListView.IsVisible = false;

            ((ListView)sender).SelectedItem = null;
        }



    }
}