using ExpenseManagement.Consts;
using ExpenseManagement.Models;
using ExpenseManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseManagement.ViewModels
{
    public class TransactionIncomeViewModel : BaseViewModel
    {
        public TypesServices typesServices;
        public TypeMethod typeConst;
        public Command LoadItemsCommand { get; }
        public ObservableCollection<DataTransaction> TransactionList { get; }

        public TransactionIncomeViewModel()
        {
            Title = "Transaction";

            TransactionList = new ObservableCollection<DataTransaction>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                TransactionList.Clear();

                var idUser = Application.Current.Properties["userId"].ToString();

                typeConst = new TypeMethod();

                typesServices = new TypesServices();

                int HeightList = 0;

                var transactionList = await typesServices.GetAllTransaction(typeConst.Income, idUser);
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(transactionList));
                foreach (DataTransaction item in transactionList.data.items)
                {
                    HeightList = (item.data.Count * 40) + (10 * item.data.Count) + 30;

                    item.AddProperty(HeightList);

                    TransactionList.Add(item);

                    HeightList = 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
