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
    public partial class TrendsCostPage : ContentPage
    {
        TrendsCostViewModel _viewModel;
        public TrendsCostPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new TrendsCostViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}