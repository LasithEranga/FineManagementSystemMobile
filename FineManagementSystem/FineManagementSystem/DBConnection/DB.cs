
using System;
using System.Data.Common;
using MySqlConnector; 

namespace Fine_Management_System.DBConnection
{
    class DB
    {
        public static MySqlDataReader Read(string query)
        {
            MySqlDataReader dr = null;
            try
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Enter", new TimeSpan(3));
                string connStr = "server=mysql-42457-0.cloudclusters.net;user=admin;database=fmsdb;port=19451;password=jaOuzvbF;";
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                dr = cmd.ExecuteReader();
                //dr = db.GetReader(query);
                Acr.UserDialogs.UserDialogs.Instance.Toast("Connected", new TimeSpan(3));
            }

            
            catch(Exception ec)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast(ec.Message+"sdnfj", new TimeSpan(3));
            }
            
            return dr;
        }

        /* public static void Write(string query)
         {
             string connStr = "server=localhost;user=root;database=fmsdb;port=3306;password=;SSL Mode=None;";
             MySqlConnection conn = new MySqlConnection(connStr);
             try
             {
                 conn.Open();
                 MySqlCommand cmd = new MySqlCommand(query, conn);
                 cmd.ExecuteNonQuery();
             }
             catch (Exception)
             {

             }
             conn.Close();
         }

         public static void Delete(string query)
         {
             string connStr = "server=localhost;user=root;database=fmsdb;port=3306;password=;SSL Mode=None;";
             MySqlConnection conn = new MySqlConnection(connStr);
             try
             {
                 conn.Open();
                 MySqlCommand cmd = new MySqlCommand(query, conn);
                 cmd.ExecuteNonQuery();
             }
             catch (Exception)
             {
             }
             conn.Close();
         }

         public static void Update(string query)
         {
             string connStr = "server=localhost;user=root;database=fmsdb;port=3306;password=;SSL Mode=None;";
             MySqlConnection conn = new MySqlConnection(connStr);
             try
             {
                 conn.Open();
                 MySqlCommand cmd = new MySqlCommand(query, conn);
                 cmd.ExecuteNonQuery();
             }
             catch (Exception)
             {
             }
             conn.Close();
         }
     }*/
    }
}
