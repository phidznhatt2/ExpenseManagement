using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Services.Identity
{
    interface IIdentityService
    {
        Task<bool> VerifyRegistration();
        Task Authenticate();
    }
}
