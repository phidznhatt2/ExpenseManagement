using System;

namespace ExpenseManagement.Models
{
    public class UserRegister
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }

        public UserRegister() { }
        public UserRegister(string firstName, string lastName, string userName, string phone, string password, string confirmPassword)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.userName = userName;
            this.phone = phone;
            this.password = password;
            this.confirmPassword = confirmPassword;
        }
    }

    public class UserInfo
    {
        public string accessToken { get; set; }
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public object phone { get; set; }
        public double totalSpend { get; set; }
        public double totalMakeMoney { get; set; }
        public double limitMoney { get; set; }
        public double accountBalance { get; set; }
    }

    public class UserLogin
    {
        public bool isSuccessed { get; set; }
        public string message { get; set; }
        public UserInfo data { get; set; }
    }
}
