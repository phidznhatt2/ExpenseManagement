using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExpenseManagement.RestClient
{
    public class RestClient<T> : IRestClient<T>
    {
        HttpClient client;

        public List<T> Items { get; private set; }

        public RestClient()
        {
#if DEBUG
            //client = new HttpClient(DependencyService.Get<IHttpClientHandlerService>().GetInsecureHandler());
            client = new HttpClient();
#else
            client = new HttpClient();
#endif
        }

        public async Task<T> RefreshDataAsync(string url)
        {

            Uri uri = new Uri(string.Format(Constants.RestUrl + url, string.Empty));

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return default(T);
        }

        public async Task<T> GetItemByIdAsync(string url)
        {

            Uri uri = new Uri(string.Format(Constants.RestUrl + url, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return default(T);
        }

        public async Task<bool> SaveItemAsync(T item, string url, bool isNewItem = false)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + url, string.Empty));

            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var oauthToken = await SecureStorage.GetAsync("oauthtoken");
                var accessToken = new AuthenticationHeaderValue("Bearer", oauthToken);
                client.DefaultRequestHeaders.Authorization = accessToken;

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Item successfully saved.");
                    return response.IsSuccessStatusCode;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);               
            }

            return false;
        }

        public async Task<bool> DeleteItemAsync(string id, string url)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + url, string.Empty));

            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauthtoken");
                var accessToken = new AuthenticationHeaderValue("Bearer", oauthToken);
                client.DefaultRequestHeaders.Authorization = accessToken;

                Debug.WriteLine(@"\Get access token successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tItem successfully deleted.");
                    return response.IsSuccessStatusCode;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return false;
        }

        public async Task<T> LoginAsync(object data)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "/User/Login", string.Empty));
            try
            {
                string json = JsonConvert.SerializeObject(data);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Login successfully.");
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return default(T);
        }

        public async Task<bool> RegisterAsync(object data)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl + "/User/register", string.Empty));

            try
            {
                string json = JsonConvert.SerializeObject(data);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Register successfully.");
                    return response.IsSuccessStatusCode;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return false;
        }
    }
}