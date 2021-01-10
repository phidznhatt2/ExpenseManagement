using ExpenseManagement.Models;
using ExpenseManagement.Services.Routing;
using ExpenseManagement.Views;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExpenseManagement.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private IRoutingService _navigationService;
        UserLogin DataLogin = new UserLogin();
        UsersServices usersServices = new UsersServices();

        public LoginViewModel(IRoutingService navigationService = null)
        {
            _navigationService = navigationService ?? Locator.Current.GetService<IRoutingService>();
            ExecuteLogin = new Command(() => Login());
            ExecuteRegistration = new Command(() => Register());
        }
        public string _username;

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public ICommand ExecuteLogin { get; set; }
        public ICommand ExecuteRegistration { get; set; }

        private async void Login()
        {
            // This is where you would probably check the login and only if valid do the navigation...
            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Tên đăng nhập và mật khẩu không được bỏ trống!", "ok");
            }
            else
            {
                var account = new { username = Username, password = Password };
                DataLogin = await usersServices.CheckLoginAsync(account);

                if (DataLogin is null)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Tên đăng nhập hoặc mật khẩu không chính xác!", "ok");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Đăng nhập thành công", "ok");
                    Application.Current.Properties["userId"] = DataLogin.data.id;
                    await SecureStorage.SetAsync("oauthtoken", DataLogin.data.accessToken);
                    Username = "";
                    Password = "";
                    await _navigationService.NavigateTo("///main/overview");
                }
            }
        }

        private void Register()
        {
            Shell.Current.GoToAsync("//login/registration");
        }
    }
}
