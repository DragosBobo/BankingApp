

using BakingAppDataLayer;
using BankingAppApiModels.Models;
using BankingAppApiModels.Models.Requests;
using DataAcces;
using Microsoft.EntityFrameworkCore;
using CategoryTransaction = BakingAppDataLayer.CategoryTransaction;
using Currency = BakingAppDataLayer.Currency;

namespace BankingAppBusiness.TransactionRepo
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;
        public TransactionRepository(DataContext context)
        {
            _context = context;
        }
        private static Transaction ConvertToDbModel(CreateTransactionApiModel model)

        {
            return new Transaction
            {
                TransactionDate = model.TransactionDate,
                Amount =model.amount,
                CategoryTransation= (BakingAppDataLayer.CategoryTransaction)model.CategoryTransation,
                AccountId = model.AccountId,

            };
       

        }
        private bool IsExist(Guid id)
        {
            var dbAccount = _context.Accounts.FirstOrDefault(x => x.Id == id);

            return dbAccount != null ? true : false;
        }
        public async Task<bool> CreateTransaction(CreateTransactionApiModel model)
        {
            var transaction = ConvertToDbModel(model);
            if (IsExist(model.AccountId)){
                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Transaction>> GetTransactions()
        {
            var result = new List<Transaction>();
            var transactions = await _context.Transactions.ToListAsync();
            foreach (var transaction in transactions)
                result.Add(transaction);
            return result;
            
        }

        public async Task<Transaction> GetTransactionById(Guid id)
        {
            var account = await _context.Transactions.FindAsync(id);

            return account == null ? null : account;
        }
        private  TransactionToApiModel ConvertToApiModel(double amount , object t, Currency currency)
        {
            return new TransactionToApiModel
            {
                TotalAmount = amount,
                CategoryName = Enum.GetName(typeof(CategoryTransaction),t),
                Currency = Enum.GetName(typeof(Currency),currency),

            };
        }
        public async Task<List<TransactionToApiModel>> GetTransactioReport(Guid id, DateTimeOffset minDate, DateTimeOffset maxDate)
        {
            var result = new List<TransactionToApiModel>();
            List<Transaction> transactions = await _context.Transactions.Where(x => x.AccountId == id && x.TransactionDate >= minDate && x.TransactionDate<=maxDate).ToListAsync();
            var account = await _context.Accounts.FindAsync(id);
            Currency currency = account.Currency;

            foreach (CategoryTransaction t in Enum.GetValues(typeof(CategoryTransaction)))
            {
                double totalAmount = 0;
                foreach (Transaction transaction in transactions)
                {
                    if (transaction.CategoryTransation.CompareTo(t) == 0)
                    {
                        totalAmount += transaction.Amount;
                    }
                }
                result.Add(ConvertToApiModel(totalAmount, t, currency));
            }

            return result;
        }

   
    }
    
}
