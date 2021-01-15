using ExpenseManagement.Services.Identity;
using ExpenseManagement.Services.Routing;
using ExpenseManagement.Views;
using Splat;
using Xamarin.Forms;

namespace ExpenseManagement.ViewModels
{
    class LoadingViewModel : BaseViewModel
    {
        private readonly IRoutingService routingService;
        private readonly IIdentityService identityService;

        public LoadingViewModel(IRoutingService routingService = null, IIdentityService identityService = null)
        {
            this.routingService = routingService ?? Locator.Current.GetService<IRoutingService>();
            this.identityService = identityService ?? Locator.Current.GetService<IIdentityService>();
        }

        // Called by the views OnAppearing method
        public void Init()
        {
            /*var isAuthenticated = await this.identityService.VerifyRegistration();
            if (isAuthenticated)
            {
                await this.routingService.NavigateTo("///main");
            }
            else
            {
                await this.routingService.NavigateTo("///login");
            }*/

            var isLogged = Xamarin.Essentials.SecureStorage.GetAsync("isLogged").Result;
            if (isLogged == "1")
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}