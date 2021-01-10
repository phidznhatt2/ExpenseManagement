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
    public partial class TrendsItemPage : ContentPage
    {
        public TrendsItemPage()
        {
            InitializeComponent();

            BindingContext = new TrendsItemViewModel();
        }
    }
}