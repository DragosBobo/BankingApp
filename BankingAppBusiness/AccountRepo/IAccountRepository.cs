using BakingAppDataLayer;
using BankingAppApiModels.Models;
using BankingAppApiModels.Models.Requests;

namespace BankingAppBusiness.AccountRepo
{
    public interface IAccountRepository
    {
        Task<List<AccountApiModel>> getAccounts();
        Task<Account>  getAccountById(Guid id);
        Task AddAccount(CreateAccountApiModel account);
        Task<string> updateAccount(Guid id, CreateAccountApiModel model);
        Task<string> deleteAccount(Guid id);
    }
}
