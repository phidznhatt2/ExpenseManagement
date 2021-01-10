using ExpenseManagement.Models;
using ExpenseManagement.Services;
using ExpenseManagement.ViewModels;
using ExpenseManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ExpenseManagement.Services.Routing;
using Newtonsoft.Json;

namespace ExpenseManagement.ViewModels
{
    public class OverviewViewModel : BaseViewModel
    {
        private ItemDetail _selectedItem;
        public ObservableCollection<ItemRegister> RegistersList { get; }
        public Command LoadItemsCommand { get; }
        public Command AddTransactionCommand { get; }
        public Command<ItemDetail> ItemTapped { get; }

        public UsersServices usersServices;

        public RegistersServices registersServices;

        private UserInfo _userinfo;
        public UserInfo UserInfo
        {
            get { return _userinfo; }
            set
            {
                _userinfo = value;
                OnPropertyChanged("UserInfo");
            }
        }

        public OverviewViewModel()
        {
            Title = "Overview";
            RegistersList = new ObservableCollection<ItemRegister>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddTransactionCommand = new Command(OnAddTransaction);
            ItemTapped = new Command<ItemDetail>(OnItemSelected);

            //AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            int HeightList = 0;
            double Cost = 0;
            double Income = 0;

            try
            {
                RegistersList.Clear();

                var idUser = Application.Current.Properties["userId"].ToString();

                usersServices = new UsersServices();
                var userInfo = await usersServices.GetUserInfo(idUser);
                UserInfo = userInfo.data;

                registersServices = new RegistersServices();
                var registersList = await registersServices.GetAllRegisters(idUser);

                foreach (ItemRegister itemRegister in registersList.data.items)
                {
                    HeightList = (itemRegister.data.Count * 40) + (10 * itemRegister.data.Count) + 30;
                    
                    foreach (ItemDetail itemDetail in itemRegister.data)
                    {
                        if (itemDetail.type == "chiphi")
                        {
                            Cost += itemDetail.money;
                        }

                        if (itemDetail.type == "thunhap")
                        {
                            Income += itemDetail.money;
                        }
                    }

                    itemRegister.AddProperty(HeightList, Cost, Income);
                    RegistersList.Add(itemRegister);

                    HeightList = 0; Cost = 0; Income = 0;
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
            SelectedItem = null;
        }

        public ItemDetail SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddTransaction(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewItemPage)}?Content={null}");
        }

        async void OnItemSelected(ItemDetail item)
        {
            if (item == null)
                return;

            var jsonStr = JsonConvert.SerializeObject(item);
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?Content={jsonStr}");
        }
    }
}
