using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Page_Navigation_App.Utilities;
using System.Windows.Input;
using Page_Navigation_App.Configs;

namespace Page_Navigation_App.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand CustomersCommand { get; set; }
        public ICommand ProductsCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand TransactionsCommand { get; set; }
        public ICommand ShipmentsCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        private void Home(object obj)
        {
            if (Userhandling.GrantPermission(0, true))
            {
                CurrentView = new HomeVM();
            }
        }

        private void Customer(object obj)
        {
            if (Userhandling.GrantPermission(0, true))
            {
                CurrentView = new CustomerVM();
            }
        }

        private void Product(object obj)
        {
            if (Userhandling.GrantPermission(0, true))
            {
                CurrentView = new ProductVM();
            }
        }

        private void Order(object obj)
        {
            if (Userhandling.GrantPermission(0, true))
            {
                CurrentView = new OrderVM();
            }
        }

        private void Transaction(object obj)
        {
            if (Userhandling.GrantPermission(0, true))
            {
                CurrentView = new Bills();
            }
        }

        private void Shipment(object obj)
        {
            if (Userhandling.GrantPermission(0, true))
            {
                CurrentView = new RessourcesVM();
            }
        }

        private void Setting(object obj)
        {
            if (Userhandling.GrantPermission(0, true))
            {
                CurrentView = new SettingVM();
            }
        }

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            CustomersCommand = new RelayCommand(Customer);
            ProductsCommand = new RelayCommand(Product);
            OrdersCommand = new RelayCommand(Order);
            TransactionsCommand = new RelayCommand(Transaction);
            ShipmentsCommand = new RelayCommand(Shipment);
            SettingsCommand = new RelayCommand(Setting);

            if (Userhandling.GrantPermission(0, false))
            {
                // Startup Page
                CurrentView = new HomeVM();
            }
        }
    }
}