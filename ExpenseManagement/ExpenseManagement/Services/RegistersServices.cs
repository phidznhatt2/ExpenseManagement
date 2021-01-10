using ExpenseManagement.Consts;
using ExpenseManagement.Models;
using ExpenseManagement.RestClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Services
{
    public class RegistersServices
    {
        TypeMethod typeConst = new TypeMethod();
        UrlApi urlConst = new UrlApi();

        public async Task<Registers> GetAllRegisters(string id)
        {
            var url = urlConst.RegisterAll + "?idUser=" + id;

            RestClient<Registers> restClient = new RestClient<Registers>();

            return await restClient.RefreshDataAsync(url);
        }

        public async Task<Register> GetRegisterById(string type, string id)
        {
            var url = urlConst.RegisterById + "?idService=" + id + "&type=" + type;

            RestClient<Register> restClient = new RestClient<Register>();

            return await restClient.GetItemByIdAsync(url);
        }

        public async Task<bool> PostRegisterAsync(PostRegister data)
        {
            var url = urlConst.Register;

            RestClient<PostRegister> restClient = new RestClient<PostRegister>();

            return await restClient.SaveItemAsync(data, url, true);
        }

        public async Task<bool> PutRegisterAsync(PutRegister data)
        {
            var url = urlConst.Register;

            RestClient<PutRegister> restClient = new RestClient<PutRegister>();

            return await restClient.SaveItemAsync(data, url, false);
        }

        public async Task<bool> DeleteRegisterAsync(int id, string type)
        {
            var url = urlConst.Register + "?id=" + id + "&type=" + type;

            RestClient<Register> restClient = new RestClient<Register>();

            return await restClient.DeleteItemAsync(id.ToString(), url);
        }
    }
}
