using GroceryStore.Commands;
using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace GroceryStore.ViewModels
{
    class ReceiptViewModel : BaseViewModel
    {
        public decimal totalAmount { get; set; }
        public ObservableCollection<Product> receiptProducts { get; set; }
        public ICommand UpdateViewCommand { get; set; }
        private BaseViewModel selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }
        void calculateTotal(ObservableCollection<Product> prodList)
        {
            foreach (Product p in prodList)
            {
                totalAmount += (p.Quantity * p.Price);
            }
        }
        public ReceiptViewModel(ObservableCollection<Product> prodList)
        {
            UpdateViewCommand = new DelegateCommand(ViewSelector, canExecute);
            receiptProducts = new ObservableCollection<Product>();
            receiptProducts = prodList;
            calculateTotal(prodList);
        }
        bool canExecute(object o)
        {
            return true;
        }
        void ViewSelector(object o)
        {
            SelectedViewModel = new MainViewModel();
        }
    }
}
