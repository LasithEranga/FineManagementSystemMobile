using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FineManagementSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Create : ContentPage
    {
        ObservableCollection<string> data = new ObservableCollection<string>();
        private List<List<Label>> labels = new List<List<Label>>();
        private List<Label> temp = new List<Label>();
        private List<Layout> views = new List<Layout>();
        private MySqlConnection conn;
        private MySqlDataReader dr = null;
        private MySqlCommand cmd;
        private string query = "SELECT tag FROM `rule`";
        private int rule= 0;
        private string id;
        private float amount = 0.00f;
        public Create(string id)
        {
            InitializeComponent();
            ListOfStore();
            AddLabel();
            GetId();
            this.id = id;
           
        }

        


        public void ListOfStore()
        {
            try
            {
                conn = Fine_Management_System.DBConnection.DB.GetConn();
                cmd = new MySqlCommand(query, conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(dr.GetString("tag"));
                }
                dr.Dispose();
            }
            catch (Exception)
            {
                
            }
        }

        async private void GetId()
        {
            conn = Fine_Management_System.DBConnection.DB.GetConn();
            while (true)
            {
                try
                {
                    driverNic.Text = await DisplayPromptAsync("Driver Nic", "Please enter driver NIC");
                    if (Validate_NIC(driverNic))
                    {
                        try
                        {
                            query = "SELECT `nic` FROM `driver` WHERE nic = '" + driverNic.Text + "';";
                            cmd = new MySqlCommand(query, conn);
                            dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                dr.Dispose();
                                break;
                            }
                            else
                            {
                                driverNic.Text = await DisplayPromptAsync("Driver Nic", "Please enter driver NIC");
                                dr.Dispose();
                            }

                        }
                        catch (Exception)
                        {

                        }

                    }
                }
                catch (Exception)
                {

                }
                
            }
            try
            {
                query = "SELECT MAX(Ref_No)+1 as id FROM `fine_receipt`;";
                cmd = new MySqlCommand(query, conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    stval.Text = dr.GetInt16("id").ToString();
                    dr.Dispose();
                }

            }
            catch (Exception)
            {

            }

            offcerID.Text = id;
            date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            time.Text = DateTime.Now.ToString("HHH:mm");
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SearchListView.IsVisible = true;
            viewlist.IsVisible = true;
            SearchListView.BeginRefresh();

            try
            {
                var dataEmpty = data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    SearchListView.IsVisible = false;
                    viewlist.IsVisible = false;


                }
                    
                else if (dataEmpty.Max().Length == 0)
                {
                    SearchListView.IsVisible = false;
                    viewlist.IsVisible = false;


                }
                    
                else
                    SearchListView.ItemsSource = data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));
            }
            catch (Exception)
            {
                SearchListView.IsVisible = false;
                viewlist.IsVisible = false;

            }
            SearchListView.EndRefresh();

        }

        private async void SaveDetails(object sender, EventArgs e)
        {
            try
            {
                string query2 = "";
                
                for (int i=0; i< rule; i++)
                {
                    //amount += Int32.Parse(labels[rule][4].Text);
                    query2 += QueryBuilder(stval.Text, labels[i][0].Text);
                }
                query = "INSERT INTO `fine_receipt`(`Ref_No`, `Date`, `Amount`, `vehicle`, `State`, `driver_nic`, `officer_id`) VALUES ("+ stval.Text + ",'"+ date.Text +"',"+amount+",'car',0,'"+driverNic.Text+"','"+offcerID.Text+"');" + query2;
                cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                Acr.UserDialogs.UserDialogs.Instance.Toast("Fine Receipt Saved!", new TimeSpan(3));
                await Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast(ex.Message, new TimeSpan(3));

            }
        }

        private string QueryBuilder(string id, string ruleid) {
            string query = "INSERT INTO `rules_broken`(`fine_receipt_id`, `rule_id`) VALUES ("+id+","+ruleid+");";
            return query;
        }


        private void ListView_OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            //EmployeeListView.IsVisible = false;  

            String listsd = e.Item as string;
            searchBar.Text = listsd;
            AddRule();
            SearchListView.IsVisible = false;
            searchBar.Text = "";
            viewlist.IsVisible = false;
            ((ListView)sender).SelectedItem = null;


        }

        private void AddRule() {
            query = "SELECT * FROM `rule` where tag = '" + searchBar.Text + "';";
            try
            {
                cmd = new MySqlCommand(query, conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    labels[rule][0].Text = dr.GetInt16("rule_id").ToString();
                    labels[rule][3].Text = dr.GetString("description");
                    amount += dr.GetFloat("penalty_amount");
                    labels[rule][4].Text = dr.GetFloat("penalty_amount").ToString();
                    labels[rule][1].Text = date.Text;
                    labels[rule][2].Text = time.Text;
                    labels[rule][5].Text = DateTime.Now.AddDays(14).ToString();

                }
                dr.Dispose();
                views[rule].IsVisible = true;
                rule++;
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast(ex.Message, new TimeSpan(3));

            }
        }

        private bool Validate_NIC(Label label)
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

        private void AddLabel() {
            temp.Add(id1);
            temp.Add(date1);
            temp.Add(time1);
            temp.Add(des1);
            temp.Add(pen1);
            temp.Add(due1);
            labels.Add(temp);
            temp =new List<Label>();

            temp.Add(id2);
            temp.Add(date2);
            temp.Add(time2);
            temp.Add(des2);
            temp.Add(pen2);
            temp.Add(due2);
            labels.Add(temp);
            temp = new List<Label>();

            temp.Add(id3);
            temp.Add(date3);
            temp.Add(time3);
            temp.Add(des3);
            temp.Add(pen3);
            temp.Add(due3);
            labels.Add(temp);
            temp = new List<Label>();

            temp.Add(id4);
            temp.Add(date4);
            temp.Add(time4);
            temp.Add(des4);
            temp.Add(pen4);
            temp.Add(due4);
            labels.Add(temp);
            temp = new List<Label>();

            temp.Add(id5);
            temp.Add(date5);
            temp.Add(time5);
            temp.Add(des5);
            temp.Add(pen5);
            temp.Add(due5);
            labels.Add(temp);
            temp = new List<Label>();

            views.Add(view1);
            views.Add(view2);
            views.Add(view3);
            views.Add(view4);
            views.Add(view5);
        }

    }
}