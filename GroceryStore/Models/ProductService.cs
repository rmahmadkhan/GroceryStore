using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Data.SqlClient;

namespace GroceryStore.Models
{
    class ProductService
    {
        public bool addProd(Product p)
        {
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjectDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
        public bool deleteProd(int ID)
        {
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjectDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            string query = $"delete from Products where ID = '{ID}'";
            SqlCommand cmd = new SqlCommand(query, con);
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

        public ObservableCollection<Product> showProd()
        {
            ObservableCollection<Product> prodList = new ObservableCollection<Product>();
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjectDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
                prodList.Add(p);
            }
            con.Close();
            return prodList;
        }
        public bool isItemExist(int pID)
        {
            bool check = false;
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjectDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            string query = "Select * from Products";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (pID == System.Convert.ToInt32(dr[0]))
                {
                    check = true;
                    break;
                }
            }
            con.Close();
            if (check) { return true; }
            else { return false; }
        }
        public bool isQuantityExist(Product p)
        {
            bool check = false;
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProjectDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            string query = "Select * from Products";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (p.Id == System.Convert.ToInt32(dr[0]) && p.Quantity <= System.Convert.ToInt32(dr[3]))
                {
                    check = true;
                    break;
                }
            }
            con.Close();
            if (check) { return true; }
            else { return false; }
        }
        public void updateData(ObservableCollection<Product> prodList)
        {
            string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(connString);
            foreach (Product p in prodList)
            {
                string query = $"Update Products set Quantity ='{p.Quantity}' where ID = '{p.Id}'";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                int insertedRows = cmd.ExecuteNonQuery();
            }
        }
    }
}
