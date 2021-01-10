using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseManagement.Services.Routing
{
    public class ExpenseRoutingService : IRoutingService
    {
        public ExpenseRoutingService()
        {
        }

        public Task NavigateTo(string route)
        {
            return Shell.Current.GoToAsync(route);
        }

        public Task GoBack()
        {
            return Shell.Current.Navigation.PopAsync();
        }

        public Task GoBackModal()
        {
            return Shell.Current.Navigation.PopModalAsync();
        }
    }
}