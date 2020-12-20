using GroceryStore.Commands;
using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace GroceryStore.ViewModels
{
    class ProductsViewModel : BaseViewModel
    {
        public ICommand UpdateViewCommand { get; set; }
        private BaseViewModel selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }
        bool canExecute(object obj)
        {
            return true;
        }
        void ViewSelector(object obj)
        {
            if ((obj as string).Equals("Return"))
            {
                SelectedViewModel = new AdminViewModel();
            }
        }
        public ObservableCollection<Product> Products { get; set; }
        public ProductsViewModel()
        {
            UpdateViewCommand = new DelegateCommand(ViewSelector, canExecute);
            ShowProd();
        }
        ObservableCollection<Product> ShowProd()
        {
            ProductService p = new ProductService();
            Products = p.getProducts();
            if (Products.Count == 0)
            {
                MessageBox.Show("No products exists.");
            }
            return Products;
        }
    }
}
