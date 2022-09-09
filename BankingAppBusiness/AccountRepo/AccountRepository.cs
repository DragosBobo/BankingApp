using DataAcces;
using BakingAppDataLayer;
using Microsoft.EntityFrameworkCore;
using BankingAppApiModels.Models.Requests;
using BankingAppApiModels.Models;
namespace BankingAppBusiness.AccountRepo
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        private bool IsExist(Guid id)
        {
            var dbAccount = _context.Accounts.FirstOrDefault(x => x.Id == id);

            return dbAccount != null ? true : false;
        }

        private static Account ConvertToDbModel(CreateAccountApiModel model)
        {
            return new Account
            {
                Id = Guid.NewGuid(),
                Currency = (BakingAppDataLayer.Currency)model.Currency,
                AccountType = (BakingAppDataLayer.AccountType)model.AccountType,
                Iban = model.Iban,
                UserId = model.UserId,
            };
        }

        private static AccountApiModel ConvertToApiModel(Account model)
        {
            return new AccountApiModel
            {
                Currency = Enum.GetName(typeof(Currency), model.Currency),
                AccountType = Enum.GetName(typeof(AccountType), model.AccountType),
                Iban = model.Iban,
                AccountId = model.Id.ToString(),
            };
        }

        public async Task AddAccount(CreateAccountApiModel model)
        {
            var account = ConvertToDbModel(model);

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
        public async Task<List<AccountApiModel>> GetAccounts()
        {
            var accounts = await _context.Accounts.ToListAsync();
            List<AccountApiModel> result = new List<AccountApiModel>();

            foreach (var account in accounts)
                result.Add(ConvertToApiModel(account));

            return result;
        }
        public async Task<Account> GetAccountById(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);

            return account == null ? null : account;
        }
        public async Task<string> UpdateAccount(Guid id, CreateAccountApiModel model)
        {
            if (IsExist(id))
            {
                var account = _context.Accounts.Where(x => Guid.Equals(x.Id, id)).FirstOrDefault();
                var newAccount = ConvertToDbModel(model);
                account.Currency = newAccount.Currency;
                account.AccountType = newAccount.AccountType;
                account.UserId = newAccount.UserId;
                account.Iban = newAccount.Iban;
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();

                return id.ToString();
            }
            else
            {
                return null;
            }
        }
        public async Task<string> DeleteAccount(Guid id)
        {
            if (IsExist(id))
            {
                var account = _context.Accounts.Where(x => Guid.Equals(x.Id, id)).FirstOrDefault();
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();

                return id.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}

