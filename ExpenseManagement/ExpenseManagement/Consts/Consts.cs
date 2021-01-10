using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManagement.Consts
{
    public class TypeMethod
    {
        public string Cost = "chiphi";
        public string Income = "thunhap";
        public string Register = "dangky";
    }

    public class UrlApi
    {
        //Register
        public string Register = "/Register";
        public string RegisterById = "/Register/Id";
        public string RegisterAll = "/Register/All";
        public string RegisterChart = "/Register/Chart";

        //Types Management
        public string Cost = "/Spend";
        public string CostAll = "/Spend/All";
        public string CostChart = "/Spend/Chart";
        public string IncomeAll = "/MakeMoney/All";
        public string Income = "/MakeMoney";
        public string IncomeChart = "/MakeMoney/Chart";

        //User
        public string User = "/User";
        public string UserRegister = "/User/register";
        public string UserLogin = "/User/Login";
    }
}