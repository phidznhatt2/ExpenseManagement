using System.Net.Http;

namespace ExpenseManagement
{
    public interface IHttpClientHandlerService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
