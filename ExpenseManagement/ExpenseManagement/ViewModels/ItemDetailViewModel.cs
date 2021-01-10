using ExpenseManagement.Consts;
using ExpenseManagement.Models;
using ExpenseManagement.Services;
using ExpenseManagement.Views;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpenseManagement.ViewModels
{
    [QueryProperty(nameof(Content), nameof(Content))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private ItemDetail itemDetail;
        public ItemDetail ItemDetail
        {
            get { return itemDetail; }
            set
            {
                itemDetail = value;
                OnPropertyChanged("ItemDetail");
            }
        }

        public ItemDetailViewModel()
        {
            Title = "Chi tiết";
        }

        public ICommand EditItemCommand => new Command(() => EditItem(ItemDetail));

        public ICommand DeleteItemCommad => new Command(() => DeleteItem(ItemDetail.id, ItemDetail.type));

        private string content = "";
        public string Content
        {
            get => content;
            set
            {
                content = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
                PerformOperation(content);
            }
        }

        private void PerformOperation(string getcont)
        {
            var content = JsonConvert.DeserializeObject<ItemDetail>(getcont);

            if(content != null)
            {
                LoadItemId(content.type, content.id);
            }
        }

        public async void LoadItemId(string type, int itemId)
        {
            try
            {
                var registersServices = new RegistersServices();
                var typeConst = new TypeMethod();
                var urlApi = new UrlApi();

                var registerItem = await registersServices.GetRegisterById(type, itemId.ToString());

                if (type == typeConst.Income)
                {
                    registerItem.data.typeSub = "Thu nhập";
                }
                if (type == typeConst.Cost)
                {
                    registerItem.data.typeSub = "Chi phí";
                }

                ItemDetail = registerItem.data;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void EditItem(ItemDetail item)
        {
            if (item == null)
                return;

            var jsonStr = JsonConvert.SerializeObject(item);

            await Shell.Current.GoToAsync($"{nameof(NewItemPage)}?Content={jsonStr}");
        }

        private async void DeleteItem(int id, string type)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                var registersServices = new RegistersServices();
                var typeConst = new TypeMethod();
                var urlApi = new UrlApi();

                var status = await registersServices.DeleteRegisterAsync(id, type);

                if (status)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Xóa thành công!", "ok");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Xóa không thành công!", "ok");
                }
            }
        }
    }
}
