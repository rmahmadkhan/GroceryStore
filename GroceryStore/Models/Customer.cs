using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Models
{
    class Customer
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
