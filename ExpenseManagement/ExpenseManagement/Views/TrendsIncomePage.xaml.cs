using ExpenseManagement.ViewModels;
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
    public partial class TrendsIncomePage : ContentPage
    {
        TrendsIncomeViewModel _viewModel;
        public TrendsIncomePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new TrendsIncomeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}