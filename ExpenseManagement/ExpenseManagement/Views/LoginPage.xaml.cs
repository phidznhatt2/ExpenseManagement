using ExpenseManagement.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseManagement.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        internal LoginViewModel ViewModel { get; set; } = Locator.Current.GetService<LoginViewModel>();

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}