using ExpenseManagement.Models;
using ExpenseManagement.Services.Routing;
using ExpenseManagement.Views;
using Splat;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpenseManagement.ViewModels
{
    class RegistrationViewModel : BaseViewModel
    {
        private IRoutingService _navigationService;

        private UsersServices usersServices;

        public RegistrationViewModel(IRoutingService navigationService = null)
        {
            Title = "";
               _navigationService = navigationService ?? Locator.Current.GetService<IRoutingService>();
            ExecuteBack = new Command(() => Back());
            ExecuteRegistration = new Command(() => Register());
        }

        private string _firstname;
        public string Firstname {
            get { return _firstname; }
            set { SetProperty(ref _firstname, value); }
        }

        private string _lastname;
        public string Lastname {
            get { return _lastname; }
            set { SetProperty(ref _lastname, value); }
        }

        private string _username;
        public string Username {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

        public ICommand ExecuteBack { get; set; }
        public ICommand ExecuteRegistration { get; set; }

        private async void Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void Register()
        {
            if(string.IsNullOrEmpty(Firstname) || string.IsNullOrEmpty(Lastname) || string.IsNullOrEmpty(Username) || 
                string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng điền đầy đủ thông tin!", "ok");
            }else if(Password != ConfirmPassword)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Mật khẩu phải giống nhau!", "ok");
            }
            else
            {
                usersServices = new UsersServices();

                UserRegister data = new UserRegister(Firstname, Lastname, Username, Password, ConfirmPassword);

                var checkRegister = await usersServices.RegisterUserAsync(data);

                if (checkRegister)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Đăng ký tài khoản thành công", "ok");
                    await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Đăng ký tài khoản không thành công", "ok");
                }
            }

        }
    }
}