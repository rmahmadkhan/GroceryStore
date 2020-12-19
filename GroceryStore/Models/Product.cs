using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GroceryStore.Models
{
    class Product : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        // Product Data Members
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("ProductID"); }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("ProductName"); }
        }
        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged("ProductPrice"); }
        }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged("ProductQuantity"); }
        }
    }
}
