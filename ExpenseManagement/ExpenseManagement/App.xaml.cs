using ExpenseManagement.Services;
using ExpenseManagement.Views;
using System;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExpenseManagement.Services.Routing;
using ExpenseManagement.Services.Identity;
using ExpenseManagement.ViewModels;

namespace ExpenseManagement
{
    public partial class App : Application
    {

        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzgwNTkzQDMxMzgyZTM0MmUzMG4zZ3FkWEh3dS9UNms5R2NxVld2L0ZIT1FXTlZuU09ZMmFqRERtVlkxbTA9");

            InitializeDi();
            InitializeComponent();

            MainPage = new LoadingPage();
        }

        private void InitializeDi()
        {
            // Services
            Locator.CurrentMutable.RegisterLazySingleton<IRoutingService>(() => new ExpenseRoutingService());
            Locator.CurrentMutable.RegisterLazySingleton<IIdentityService>(() => new IdentityServiceStub());

            // ViewModels
            Locator.CurrentMutable.Register(() => new LoadingViewModel());
            Locator.CurrentMutable.Register(() => new LoginViewModel());
            Locator.CurrentMutable.Register(() => new RegistrationViewModel());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
