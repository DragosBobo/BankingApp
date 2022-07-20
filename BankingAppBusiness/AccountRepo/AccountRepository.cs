using BankingAppApiModels.Models.Account;
using DataAcces;
using BakingAppDataLayer;
namespace BankingAppBusiness.AccountRepo
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        public AccountRepository(DataContext context)
        {
            _context = context;
        }
        private bool IsExist(Guid id )
        {
           var dbAccount =  _context.Accounts.FirstOrDefault(x => x.Id == id);
           return dbAccount != null ? true : false ;
        }
        public  Account Mapper(CreateAccountModel model)
        {
            return new Account
            {
                Id = model.Id,
                Currency = (BakingAppDataLayer.Currency)model.Currency,
                AccountType = (BakingAppDataLayer.AccountType)model.AccountType,
                Iban = model.Iban
            };
        }
        public async Task AddAccount(CreateAccountModel model)
        {
            if (IsExist(model.Id))
            {
               var account = Mapper(model);
               await _context.Accounts.AddAsync(account);
               await _context.SaveChangesAsync();
            }
        }
        public Task deleteAccount()
        {
            throw new NotImplementedException();
        }
        public Task getAccountById()
        {
            throw new NotImplementedException();
        }
        public Task getAccounts()
        {
            throw new NotImplementedException();
        }
        public Task updateAccount()
        {
            throw new NotImplementedException();
        }
    }
}
