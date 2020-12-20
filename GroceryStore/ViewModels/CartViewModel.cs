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
    class CartViewModel : BaseViewModel
    {
        public string ID { get; set; }
        public string Quantity { get; set; }
        
        // Commands
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand Checkout { get; set; }
        public ICommand UpdateViewCommand { get; set; }

        // Lists to have products
        private ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Product> products
        {
            get { return Products; }
            set
            {
                Products = value;
                OnPropertyChanged("products");
            }
        } 
        public ObservableCollection<Product> cartProducts { get; set; }
        public ObservableCollection<Product> updatedProducts { get; set; }

        ProductService productService;
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
            if ((obj as string).Equals("Logout"))
            {
                SelectedViewModel = new MainViewModel();
            }
        }
        public CartViewModel()
        {
            // List to show items in cart
            cartProducts = new ObservableCollection<Product>();
            // Logout
            UpdateViewCommand = new DelegateCommand(ViewSelector, canExecute);
            // Add product to cart
            AddCommand = new DelegateCommand(addProduct, canAdd);
            // Checkout
            Checkout = new DelegateCommand(checkout, canCheckout);

            productService = new ProductService();
            // Get Products from Database
            Products = productService.getProducts();
            if (Products.Count == 0)
            {
                MessageBox.Show("No products are available");
            }
        }

        public bool canAdd(object o)
        {
            if (string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(Quantity))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Add item to the cart
        public void addProduct(object o)
        {
            try
            {
                Product pInput = new Product();
                pInput.Id = System.Convert.ToInt32(ID);
                pInput.Quantity = System.Convert.ToInt32(Quantity);

                // Checks if product is available in database
                if (productService.isProductAvailable(pInput))
                {
                    if (pInput.Quantity != 0)
                    {
                        for(int i=0; i<Products.Count; i++)
                        {
                            if (pInput.Id == Products[i].Id)
                            {
                                if (pInput.Quantity <= Products[i].Quantity)
                                {
                                    pInput.Name = Products[i].Name;
                                    pInput.Price = Products[i].Price;
                                    // Decrement the quantity of Product
                                    Products[i].Quantity -= pInput.Quantity;
                                    cartProducts.Add(pInput);
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show("Selected quantity not available!");
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select sufficient quantity!");
                    }
                }
                else
                {
                    MessageBox.Show("Product is not available");
                }
            }
            catch (FormatException e) { MessageBox.Show("Enter input in correct format"); }
        }
        void checkout(object obj)
        {
            // Updates the Products in Database
            productService.updateDatabase(Products);
            // Take cart items to the receipt view
            SelectedViewModel = new ReceiptViewModel(cartProducts);
        }
        bool canCheckout(object obj)
        {
            if (cartProducts.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
