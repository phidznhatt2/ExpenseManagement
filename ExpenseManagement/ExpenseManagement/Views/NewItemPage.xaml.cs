using ExpenseManagement.Models;
using ExpenseManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseManagement.Views
{
    public partial class NewItemPage : ContentPage
    {

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }

        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            Recalculate();
        }

        void Recalculate()
        {

        }

        //protected override bool OnBackButtonPressed() => false;
    }

    public class CustomEntry : Entry
    {

    }

    public class CustomPicker : Picker
    {

    }
}