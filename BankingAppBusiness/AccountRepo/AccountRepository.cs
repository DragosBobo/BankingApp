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
        public Task deleteAccount()
        {
            throw new NotImplementedException();
        }
        public Task getAccountById()
        {
            throw new NotImplementedException();
        }
        public async Task<List<Account>> getAccounts()
        {
           var accounts = await _context.Accounts.ToListAsync();

           return accounts;
        }
        public Task updateAccount()
        {
            throw new NotImplementedException();
        }
    }
        
    }

