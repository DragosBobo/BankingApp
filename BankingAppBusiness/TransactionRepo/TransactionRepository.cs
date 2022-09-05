﻿

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
                CategoryTransaction= (BakingAppDataLayer.CategoryTransaction)model.CategoryTransaction,
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

        public async Task<List<TransactionToApiModel>> GetTransactions()
        {
          
            var transactions = await _context.Transactions.ToListAsync();
            List<TransactionToApiModel> result = new List<TransactionToApiModel>();

            foreach (var transaction in transactions)
                result.Add(ConvertToApiModel(transaction.Amount,transaction.CategoryTransaction));

            return result;

        }

        public async Task<TransactionToApiModel> GetTransactionById(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            var result = ConvertToApiModel(transaction.Amount, transaction.CategoryTransaction);

            return result == null ? null : result;
        }
        private  TransactionToApiModel ConvertToApiModel(double amount , object t)
        {
            return new TransactionToApiModel
            {
                TotalAmount = amount,
                CategoryName = Enum.GetName(typeof(CategoryTransaction),t),
                
            };
        }
        public async Task<List<TransactionToApiModel>> GetTransactionReport(Guid id, DateTimeOffset minDate, DateTimeOffset maxDate)
        {
            var result = new List<TransactionToApiModel>();
            List<Transaction> transactions = await _context.Transactions.Where(x => x.AccountId == id && x.TransactionDate >= minDate && x.TransactionDate<=maxDate).ToListAsync();
            
            

            foreach (CategoryTransaction t in Enum.GetValues(typeof(CategoryTransaction)))
            {
                double totalAmount = 0;
                foreach (Transaction transaction in transactions)
                {
                    if (transaction.CategoryTransaction.CompareTo(t) == 0)
                    {
                        totalAmount += transaction.Amount;
                    }
                }
                result.Add(ConvertToApiModel(totalAmount, t ));
            }

            return result;
        }
        public async Task<List<TransactionToApiModel>> GetAccountTransaction(Guid id)
        {
            var result = new List<TransactionToApiModel>();
            List<Transaction> transactions = await _context.Transactions.Where(x => x.AccountId == id ).ToListAsync();
            foreach(Transaction transaction in transactions)
            {
                result.Add(ConvertToApiModel(transaction.Amount, transaction.CategoryTransaction));
            }
            return result;
        }
   
    }
    
}