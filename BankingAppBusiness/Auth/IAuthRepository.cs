using BankingAppControllers.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace BankingAppBusiness.Auth
{
    public interface IAuthRepository
    {   
        Task Register(RegisterApiModel model);
        Task<string> Login(LoginApiModel model);
        Task<bool> ValidateEmail(string email);
    }
}
