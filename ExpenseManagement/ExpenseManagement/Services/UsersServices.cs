using ExpenseManagement.Models;
using ExpenseManagement.RestClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagement
{
    public class UsersServices
    {
		RestClient<UserLogin> restClient = new RestClient<UserLogin>();

		string baseUrl = "/User";

		public UsersServices() { }
	
		public async Task<UserLogin> CheckLoginAsync(object user)
        {
			return await restClient.LoginAsync(user);
        }

		public async Task<UserLogin> GetUserInfo(string id)
        {
			var url = baseUrl + "?id=" + id;

			return await restClient.RefreshDataAsync(url);
		}

		public async Task<bool> RegisterUserAsync(UserRegister user)
		{
			Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(user));
			return await restClient.RegisterAsync(user);
		}
	}
}
