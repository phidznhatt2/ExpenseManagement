using ExpenseManagement.ViewModels;
using ExpenseManagement.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpenseManagement
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute("registration", typeof(RegistrationPage));
            Routing.RegisterRoute("main/login", typeof(LoginPage));

            BindingContext = this;
        }

        public ICommand ExecuteLogout => new Command(async () => await Shell.Current.GoToAsync("//login"));

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
