using ExpenseManagement.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ExpenseManagement.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }

        protected override bool OnBackButtonPressed()
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                base.OnBackButtonPressed();
                await Shell.Current.GoToAsync("///main/overview");
            });

            return true;
        }
    }
}