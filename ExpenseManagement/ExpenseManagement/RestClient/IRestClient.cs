using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagement.RestClient
{
	public interface IRestClient<T>
	{
		Task<T> RefreshDataAsync(string url);

		Task<T> GetItemByIdAsync(string url);

		Task<bool> SaveItemAsync(T item, string url, bool isNewItem);

		Task<bool> DeleteItemAsync(string id, string url);

		Task<T> LoginAsync(object data);

		Task<bool> RegisterAsync(object data);
	}
}

