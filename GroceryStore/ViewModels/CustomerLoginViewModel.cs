using GroceryStore.Commands;
using GroceryStore.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GroceryStore.ViewModels
{
    internal class CustomerLoginViewModel : BaseViewModel
    {
        // Login/Signup Data Members
        public string Username { get; set; }
        public string Password { get; set; }

        // Commands
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand SignupCommand { get; set; }

        public ICommand UpdateViewCommand { get; set; }
        // ViewModel
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
        // To go back to main view
        void ViewSelector(object obj)
        {
            if ((obj as string).Equals("Back"))
            {
                SelectedViewModel = new MainViewModel();
            }
        }

        public CustomerLoginViewModel()
        {
            UpdateViewCommand = new DelegateCommand(ViewSelector, canExecute);
            LoginCommand = new DelegateCommand(SelectorForLogin, canExecuteLogin);
            SignupCommand = new DelegateCommand(SelectorForSignup, canExecuteSignup);
        }
        // Login execution methods
        bool canExecuteLogin(object obj)
        {
            var x = obj as PasswordBox;
            Password = x.Password;
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        void SelectorForLogin(object obj)
        {
            try
            {
                var x = obj as PasswordBox;
                Password = x.Password;
                Customer customer = new Customer();
                CustomerService customerService = new CustomerService();
                customer.Username = Username;
                customer.Password = Password;
                if (customerService.checkUsername(customer))
                {
                    if (customerService.checkPassword(customer))
                    {
                        SelectedViewModel = new CartViewModel();
                    }
                    else
                    {
                        MessageBox.Show("Warning: Password is incorrect!");
                    }
                }
                else
                {
                    MessageBox.Show("Warning: Invalid Username!");
                }
            }
            catch { }
        }
        bool canExecuteSignup(object obj)
        {
            var x = obj as PasswordBox;
            Password = x.Password;
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        void SelectorForSignup(object obj)
        {
            try
            {
                var x = obj as PasswordBox;
                Password = x.Password;
                Customer c = new Customer();
                CustomerService customerService = new CustomerService();
                c.Username = Username;
                c.Password = Password;
                if (!customerService.checkUsername(c))
                {
                    if (customerService.signup(c))
                    {
                        SelectedViewModel = new CustomerLoginViewModel();
                        MessageBox.Show("New User has been registered successfully.");
                    }
                    else
                    {
                        MessageBox.Show("ERROR: User could not be registered.");
                    }
                }
                else
                {
                    MessageBox.Show("ERROR: Username already exists.");
                }
            }
            catch { }
        }
    }
}