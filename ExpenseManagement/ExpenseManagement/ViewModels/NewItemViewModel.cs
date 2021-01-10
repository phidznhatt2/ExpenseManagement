using ExpenseManagement.Consts;
using ExpenseManagement.Models;
using ExpenseManagement.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpenseManagement.ViewModels
{
    [QueryProperty(nameof(Content), nameof(Content))]
    public class NewItemViewModel : BaseViewModel
    {
        public Command CancelCommand { get; }
        public Command SaveCommand { get; }

        private RegistersServices registersServices;
        TypesServices typesServices = new TypesServices();
        TypeMethod typeConst = new TypeMethod();

        public ObservableCollection<string> ListTypes { get; set; }

        public List<ItemCate> ListCostsPicker = new List<ItemCate>();
        public List<ItemCate> ListIncomesPicker = new List<ItemCate>();

        public NewItemViewModel()
        {
            Title = "Cập nhật";

            ListTypes = new ObservableCollection<string>();

            CancelCommand = new Command(OnCancel);

            SaveCommand = new Command(SaveItem);
        }

        private string content = "";
        public string Content
        {
            get => content;
            set
            {
                content = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();

                if (!string.IsNullOrEmpty(content))
                {
                    PerformOperation(content);
                }
                else
                {
                    OnInit();
                }
            }
        }

        private int _selectedKeyType { get; set; }
        public int SelectedKeyType
        {
            get { return _selectedKeyType; }
            set
            {
                _selectedKeyType = value;
                OnPropertyChanged("SelectedKeyType");
            }
        }
        private int _selectedKeyItem { get; set; }
        public int SelectedKeyItem
        {
            get { return _selectedKeyItem; }
            set
            {
                _selectedKeyItem = value;
                OnPropertyChanged("SelectedKeyItem");
            }
        }

        private string _imageSource { get; set; }
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        private double _money { get; set; }
        public double Money
        {
            get { return _money; }
            set
            {
                _money = value;
                OnPropertyChanged("Money");
            }
        }
        private string _description { get; set; }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private string _selectedType { get; set; }

        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged("SelectedType");

                HandleSelectedTypeChanged(_selectedType);
            }
        }

        private void HandleSelectedTypeChanged(string type)
        {
            if (!string.IsNullOrEmpty(type) && string.IsNullOrEmpty(Content))
            {
                if (type == "Chi tiêu")
                {
                    ListItemsPicker = ListCostsPicker;
                }
                else if (type == "Thu nhập")
                {
                    ListItemsPicker = ListIncomesPicker;
                    //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(ListItemsPicker));
                }

                ImageSource = ListItemsPicker[0].img;
                SelectedKeyItem = 0;
            }
        }

        private ItemCate _selectedItem { get; set; }

        public ItemCate SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");

                HandleSelectedItemChanged(_selectedItem);
            }
        }

        private void HandleSelectedItemChanged(ItemCate item)
        {
            if (item != null)
            {
                ImageSource = item.img;
            }
        }


        private ItemDetail _itemDetail;
        public ItemDetail ItemDetail
        {
            get { return _itemDetail; }
            set
            {
                _itemDetail = value;
                OnPropertyChanged("ItemDetail");
            }
        }

        private List<ItemCate> _listItemsPicker;
        public List<ItemCate> ListItemsPicker
        {
            get { return _listItemsPicker; }
            set
            {
                _listItemsPicker = value;
                OnPropertyChanged("ListItemsPicker");
            }
        }

        private async void PerformOperation(string getcont)
        {
            var content = JsonConvert.DeserializeObject<ItemDetail>(getcont);

            ItemDetail = content;

            ImageSource = ItemDetail.img;
            Money = ItemDetail.money;
            Description = ItemDetail.description;
            ListTypes.Add(ItemDetail.typeSub);

            var ListCates = await typesServices.GetAllCates(ItemDetail.type);
            ListItemsPicker = ListCates.data.items;
            SelectedKeyType = 0;
            SelectedKeyItem = ListItemsPicker.FindIndex(item => item.name == ItemDetail.name);
        }

        public async void OnInit()
        {
            try
            {
                ListTypes.Add("Chi tiêu");
                ListTypes.Add("Thu nhập");
                var CostManagement = await typesServices.GetAllCates(typeConst.Cost);
                var IncomeManagement = await typesServices.GetAllCates(typeConst.Income);
                ListCostsPicker = CostManagement.data.items;
                ListIncomesPicker = IncomeManagement.data.items;

                ListItemsPicker = ListCostsPicker;
                ImageSource = ListCostsPicker[0].img;
                SelectedKeyType = 0;
                SelectedKeyItem = 0;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.Navigation.PopModalAsync();
        }

        private bool checkValidate()
        {
            if(string.IsNullOrEmpty(Money.ToString()) || string.IsNullOrEmpty(Description))
            {
                return false;
            }

            return true;
        }

        private void SaveItem()
        {
            if (checkValidate())
            {
                if (ItemDetail == null)
                {
                    CreateItem();
                }
                else
                {
                    UpdateItem(ItemDetail.id, ItemDetail.type);
                }
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ thông tin!", "ok");
            }
        }

        public async void CreateItem()
        {
            registersServices = new RegistersServices();
            var idUser = Application.Current.Properties["userId"].ToString();
            PostRegister data = new PostRegister();

            data.money = Money;
            data.idUser = idUser;
            data.description = Description;
            data.idService = SelectedItem.id;
            if(SelectedType == "Chi tiêu")
            {
                data.type = typeConst.Cost;
            }
            else if(SelectedType == "Thu nhập")
            {
                data.type = typeConst.Income;
            }

            var status = await registersServices.PostRegisterAsync(data);

            if (status)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Thêm thành công!", "ok");
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Thêm không thành công!", "ok");
            }
        }

        public async void UpdateItem(int id, string type)
        {
            registersServices = new RegistersServices();
            PutRegister data = new PutRegister();

            data.money = Money;
            data.id = id;
            data.description = Description;
            data.idService = SelectedItem.id;
            data.type = type;

            var status = await registersServices.PutRegisterAsync(data);

            if (status)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Cập nhật thành công!", "ok");
                await Shell.Current.GoToAsync("///main/overview");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Cập nhật không thành công!", "ok");
            }
        }

    }
}
