using Xamarin.Forms;

namespace ExpenseManagement
{
    public static class Constants
    {
        // URL of REST service
        public static string RestUrl = Device.RuntimePlatform == Device.Android ? "https://samonvp.somee.com/api" : "https://samonvp.somee.com/api";
    }
}
