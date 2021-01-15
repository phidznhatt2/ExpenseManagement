using ExpenseManagement.ViewModels;
using ExpenseManagement.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpenseManagement
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public string FullName { get; set; }
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));

            var FirstName = Application.Current.Properties["firstName"].ToString();
            var LastName = Application.Current.Properties["lastName"].ToString();

            if(!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName)){
                FullName = FirstName + " " + LastName;
            }
            else
            {
                FullName = "";
            }          

            BindingContext = this;
        }

        public ICommand ExecuteLogout => new Command(OnLogoutClicked);

        private void OnLogoutClicked()
        {
            Application.Current.Properties.Clear();
            Xamarin.Essentials.SecureStorage.RemoveAll();
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}
