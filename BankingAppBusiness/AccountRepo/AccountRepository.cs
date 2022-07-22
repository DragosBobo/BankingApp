using BankingAppApiModels.Models.Account;
using DataAcces;
using BakingAppDataLayer;
using Microsoft.EntityFrameworkCore;
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
        private static Account Mapper(CreateAccountModel model)
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
        public async Task AddAccount(CreateAccountModel model)
        {
            var account = Mapper(model);
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Account>> getAccounts()
        {
           var accounts = await _context.Accounts.ToListAsync();

           return accounts;
        }
        public async Task<Account> getAccountById(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);

            return account == null ? null : account ;
        }
        public async Task<string> updateAccount(Guid id , CreateAccountModel model)
        {
            if (IsExist(id))
            {
                var account = _context.Accounts.Where(x => Guid.Equals(x.Id, id)).FirstOrDefault();
                var newAccount = Mapper(model);
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
        public async Task<string> deleteAccount(Guid id)
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

