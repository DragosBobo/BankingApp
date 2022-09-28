using BakingAppDataLayer;
using BankingAppApiModels.Models;
using BankingAppApiModels.Models.Requests;

namespace BankingAppBusiness.AccountRepo
{
    public interface IAccountRepository
    {
        Task<List<AccountApiModel>> GetAccounts();
        Task<List<AccountApiModel>> GetAccountById(Guid id);
        Task<CreateAccountApiModel> AddAccount(CreateAccountApiModel account);
        Task<string> UpdateAccount(Guid id, CreateAccountApiModel model);
        Task<string> DeleteAccount(Guid id);
    }
}
