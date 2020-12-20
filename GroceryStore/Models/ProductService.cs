using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Data.SqlClient;

namespace GroceryStore.Models
{
    class ProductService
    {
        // Addd a product to the database
        public bool addProduct(Product p)
        {
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GroceryStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open(); 
            string query = $"insert into Products(ID, Name, Price, Quantity) values('{p.Id}','{p.Name}','{p.Price}','{p.Quantity}')";
            SqlCommand cmd = new SqlCommand(query, connection);
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

        // Delete a product of a certain ID
        // Used in AdminView
        public bool deleteProduct(int ID)
        {
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GroceryStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = $"delete from Products where Id = '{ID}'";
            SqlCommand cmd = new SqlCommand(query, connection);
            int rows = cmd.ExecuteNonQuery();
            if (rows >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Returns the data from database in a list
        public ObservableCollection<Product> getProducts()
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GroceryStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            string query = "Select * from Products";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Product p = new Product();
                p.Id = System.Convert.ToInt32(dr[0]);
                p.Name = System.Convert.ToString(dr[1]);
                p.Price = System.Convert.ToDecimal(dr[2]);
                p.Quantity = System.Convert.ToInt32(dr[3]);
                products.Add(p);
            }
            con.Close();
            return products;
        }

        // Checks if Product is available in database
        public bool isProductAvailable(Product p)
        {
            bool check = false;
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GroceryStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string query = "Select * from Products";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (p.Id == System.Convert.ToInt32(dr[0]) && p.Quantity <= System.Convert.ToInt32(dr[3]))
                {
                    check = true;
                    break;
                }
            }
            connection.Close();
            if (check) { return true; }
            else { return false; }
        }

        // Takes a list and update the database
        public void updateDatabase(ObservableCollection<Product> products)
        {
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GroceryStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            foreach (Product p in products)
            {
                // If quantity of a product is zero, it deletes it from the database
                if(p.Quantity == 0)
                {
                    string query = $"delete from Products where Id = '{p.Id}'";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                // else it updates the quantity
                else
                {
                    string query = $"Update Products set Quantity ='{p.Quantity}' where Id = '{p.Id}'";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            connection.Close();
        }
    }
}
