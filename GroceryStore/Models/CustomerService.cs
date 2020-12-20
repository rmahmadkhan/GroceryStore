using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Models
{
    class CustomerService
    {
        public bool checkUsername(Customer c)
        {
            try
            {
                bool check = false;
                string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GroceryStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                string query = "Select * from Customers";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader col = cmd.ExecuteReader();
                while (col.Read())
                {
                    string temp = System.Convert.ToString(col[1]);
                    if (c.Username == temp)
                    {
                        check = true;
                        break;
                    }
                }
                con.Close();
                if (check == true)
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }
        public bool checkPassword(Customer c)
        {
            try
            {
                bool check = false;
                string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GroceryStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                string query = "Select * from Customers";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader col = cmd.ExecuteReader();
                while (col.Read())
                {
                    if (c.Username == System.Convert.ToString(col[1]) && c.Password == System.Convert.ToString(col[2]))
                    {
                        check = true;
                        break;
                    }
                }
                con.Close();
                if (check == true)
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }
        public bool signup(Customer c)
        {
            try
            {
                string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GroceryStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection connection = new SqlConnection(connString);

                string query = $"insert into Customers(Username, Password) values('{c.Username}','{c.Password}')";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                int insertedRows = cmd.ExecuteNonQuery();
                if (insertedRows >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }
    }
}
