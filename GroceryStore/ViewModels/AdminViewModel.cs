using GroceryStore.Commands;
using GroceryStore.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace GroceryStore.ViewModels
{
    internal class AdminViewModel : BaseViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string delID { get; set; }

        // Commnads to add/delete
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        public ICommand UpdateViewCommand { get; set; }
        private BaseViewModel selectedViewModel;
        ProductService productService;

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
            if ((obj as string).Equals("Products"))
            {
                SelectedViewModel = new ProductsViewModel();
            }
            else if ((obj as string).Equals("Logout"))
            {
                SelectedViewModel = new MainViewModel();
            }
        }
        public AdminViewModel()
        {
            productService = new ProductService();
            UpdateViewCommand = new DelegateCommand(ViewSelector, canExecute);
            AddCommand = new DelegateCommand(addProduct, canAdd);
            DeleteCommand = new DelegateCommand(deleteProduct, canDelete);
        }
        public bool canAdd(object obj)
        {
            if (string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Price) || string.IsNullOrEmpty(Quantity))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void addProduct(object obj)
        {
            try
            {
                Product p = new Product();
                p.Id = System.Convert.ToInt32(this.ID);
                p.Name = this.Name;
                p.Price = System.Convert.ToDecimal(this.Price);
                p.Quantity = System.Convert.ToInt32(this.Quantity);
                if (productService.addProd(p))
                {
                    MessageBox.Show("Product added successfully.");
                }
                else
                {
                    MessageBox.Show("Product could not be added.");
                }
            }
            catch (FormatException e) { MessageBox.Show("Enter Input in correct format"); }
            catch { MessageBox.Show("Product already exists."); }
        }
        public bool canDelete(object obj)
        {
            if (string.IsNullOrEmpty(delID))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void deleteProduct(object obj)
        {
            try
            {
                if (productService.deleteProd(System.Convert.ToInt32(this.delID)))
                {
                    MessageBox.Show("Product deleted successfully!");
                }
                else
                {
                    MessageBox.Show("Specified product does not exist!");
                }
            }
            catch { MessageBox.Show("Error!\nEnter input in correct number"); }
        }

    }
}