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
        public async Task deleteAccount(Guid id)
        {
            var account =  _context.Accounts.Where( x=>Guid.Equals(x.Id,id)).FirstOrDefault();
            _context.Accounts.Remove(account);
           await _context.SaveChangesAsync();
        }
        public async Task<Account> getAccountById(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);

            return account;
        }
        public async Task<List<Account>> getAccounts()
        {
           var accounts = await _context.Accounts.ToListAsync();

           return accounts;
        }
        public async Task updateAccount(Guid id , CreateAccountModel model)
        {
            var account = _context.Accounts.Where(x => Guid.Equals(x.Id, id)).FirstOrDefault();
            var newAccount = Mapper(model);
            account.Currency = newAccount.Currency;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();


        }
    }
        
    }

