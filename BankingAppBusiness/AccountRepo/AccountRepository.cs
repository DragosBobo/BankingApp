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
           return false ;
        }
        public  static Account Mapper(CreateAccountModel model)
        {
            return new Account
            {
                Id = model.Id,
                Currency = (BakingAppDataLayer.Currency)model.Currency,
                AccountType = (BakingAppDataLayer.AccountType)model.AccountType,
                Iban = model.Iban,
                UserId1 = model.UserId1,
                UserId = model.UserId
            };
        }
        public async Task AddAccount(CreateAccountModel model)
        {
            if (!IsExist(model.Id))
            {
               var account = Mapper(model);
               await _context.Accounts.AddAsync(account);
               _context.SaveChanges();
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
