using BankingAppControllers.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace BankingAppBusiness.Auth
{
    public interface IAuthRepository
    {
     
        Task Register(RegisterApiModel model);

        string Login(LoginApiModel model);
    }
}
