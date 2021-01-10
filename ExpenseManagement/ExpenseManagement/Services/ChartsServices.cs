using ExpenseManagement.Consts;
using ExpenseManagement.Models;
using ExpenseManagement.RestClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Services
{
    public class ChartsServices
    {
        TypeMethod typeConst = new TypeMethod();
        UrlApi urlConst = new UrlApi();
        RestClient<RootChart> restClient = new RestClient<RootChart>();

        public async Task<RootChart> GetDataChart(string idUser, int month, string type)
        {
            var url = "";
            if(type == typeConst.Cost)
            {
                url = urlConst.CostChart + "?idUser=" + idUser + "&month=" + month;
            }else if(type == typeConst.Income)
            {
                url = urlConst.IncomeChart + "?idUser=" + idUser + "&month=" + month;
            }
            else
            {
                url = urlConst.RegisterChart + "?idUser=" + idUser + "&month=" + month;
            }

            return await restClient.RefreshDataAsync(url);
        }
    }
}
