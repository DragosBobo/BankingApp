using BakingAppDataLayer;
using BankingAppApiModels.Models;
using BankingAppApiModels.Models.Requests;

namespace BankingAppBusiness.AccountRepo
{
    public interface IAccountRepository
    {
        Task<List<AccountApiModel>> GetAccounts();
        Task<Account>  GetAccountById(Guid id);
        Task AddAccount(CreateAccountApiModel account);
        Task<string> UpdateAccount(Guid id, CreateAccountApiModel model);
        Task<string> DeleteAccount(Guid id);
    }
}
