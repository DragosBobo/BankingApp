using BakingAppDataLayer;
using BankingAppApiModels.Models.Account;
namespace BankingAppBusiness.AccountRepo
{
    public interface IAccountRepository
    {
        Task<List<Account>> getAccounts();
        Task<Account>  getAccountById(Guid id);
        Task AddAccount(CreateAccountModel account);
        Task<string> updateAccount(Guid id, CreateAccountModel model);
        Task<string> deleteAccount(Guid id);
    }
}
