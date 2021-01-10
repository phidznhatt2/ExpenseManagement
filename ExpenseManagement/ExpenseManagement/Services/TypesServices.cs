using ExpenseManagement.Consts;
using ExpenseManagement.Models;
using ExpenseManagement.RestClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Services
{
    public class TypesServices
    {
        TypeMethod typeConst = new TypeMethod();
        UrlApi urlConst = new UrlApi();
  
        public async Task<TypesManagement> GetAllCates(string type)
        {
            RestClient<TypesManagement> restClient = new RestClient<TypesManagement>();

            if (type == typeConst.Cost)
            {
                return await restClient.RefreshDataAsync(urlConst.CostAll);
            }
            else if (type == typeConst.Income)
            {
                return await restClient.RefreshDataAsync(urlConst.IncomeAll);
            }
            else return null;
        }

        public async Task<TypeManagement> GetCateById(string type, string id)
        {
            RestClient<TypeManagement> restClient = new RestClient<TypeManagement>();

            if (type == typeConst.Cost)
            {
                var url = urlConst.Cost + "/id?idMakeMoney=" + id;
                return await restClient.GetItemByIdAsync(url);
            }
            else if (type == typeConst.Income)
            {
                var url = urlConst.Income + "/id?idSpend=" + id;
                return await restClient.GetItemByIdAsync(url);
            }
            else return null;
        }

        public async Task<bool> SaveCateAsync(string type, ItemCate data, bool isNewCate = false)
        {
            RestClient<ItemCate> restClient = new RestClient<ItemCate>();

            if (type == typeConst.Cost)
            {
                return await restClient.SaveItemAsync(data, urlConst.Cost, isNewCate);
            }
            else if (type == typeConst.Income)
            {
                return await restClient.SaveItemAsync(data, urlConst.Income, isNewCate);
            }
            else return false;
        }

        public async Task<bool> DeleteCate(string type, string id)
        {
            RestClient<TypesManagement> restClient = new RestClient<TypesManagement>();

            if (type == typeConst.Cost)
            {
                var url = urlConst.Cost + "?idMakeMoney=" + id;
                return await restClient.DeleteItemAsync(id, url);
            }
            else if (type == typeConst.Income)
            {
                var url = urlConst.Income + "?idSpend=" + id;
                return await restClient.DeleteItemAsync(id, url);
            }
            else return false;
        }

        public async Task<RootTransaction> GetAllTransaction(string type, string id)
        {
            RestClient<RootTransaction> restClient = new RestClient<RootTransaction>();

            if (type == typeConst.Cost)
            {
                var url = urlConst.Cost + "/idUser?IdUSer=" + id;
                return await restClient.GetItemByIdAsync(url);
            }
            else if (type == typeConst.Income)
            {
                var url = urlConst.Income + "/idUser?IdUSer=" + id;
                return await restClient.GetItemByIdAsync(url);
            }
            else return null;
        }
    }
}
