using GroceryStore.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace GroceryStore.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private BaseViewModel selectedViewModel;

        public  BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }

        public ICommand UpdateViewCommand { get; set; }

        // Constructor
        public MainViewModel()
        {
            UpdateViewCommand = new DelegateCommand(ViewSelector, canExecute);
        }

        bool canExecute(object obj)
        {
            return true;
        }

        void ViewSelector(object obj)
        {
            if ((obj as string).Equals("Admin"))
            {
                SelectedViewModel = new AdminViewModel();
            }
            else if ((obj as string).Equals("Customer"))
            {
                SelectedViewModel = new CustomerLoginViewModel();
            }
            else if ((obj as string).Equals("Exit"))
            {
                Application.Current.MainWindow.Close();
            }
        }
    }
}
