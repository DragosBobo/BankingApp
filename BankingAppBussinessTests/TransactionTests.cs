using BakingAppDataLayer;
using BankingAppApiModels.Models.Requests;
using BankingAppBusiness.TransactionRepo;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using CategoryTransaction = BankingAppApiModels.Models.Requests.CategoryTransaction;

namespace BankingAppBussinessTests
{
    [TestClass]
    public class TransactionTests : BaseTest
    {
        [TestMethod]
        public async Task TestCreateTransaction()
        {   
            //Arrange
            var transRepo = new TransactionRepository(context);
            var date = DateTime.Now;
            var id = new Guid("a0214d14-5570-4970-9834-c86f321e594b");
            var transaction = new CreateTransactionApiModel
            {  
                amount = 200,
                TransactionDate = date,
                AccountId = id,
                CategoryTransaction = CategoryTransaction.Food
            };
            
            //Act
            await transRepo.CreateTransaction(transaction);
            var foundTransaction = context.Transactions.FirstOrDefault(x => x.TransactionDate == transaction.TransactionDate && x.Amount == transaction.amount && x.AccountId == transaction.AccountId);

            //Assert
            context.Transactions.Should().HaveCount(1);
            transaction.Should().BeEquivalentTo(foundTransaction, opt => opt.Excluding(x => x.Id).Excluding(x => x.Account).Excluding(x => x.AccountId).Excluding(x => x.Amount));
        }
        [TestMethod]
        public async Task TestGetTransactions()
        {
            //Arrange 
            var transRepo = new TransactionRepository(context);
            var transactions = new List<Transaction>()
            {
                new Transaction()
                {
                    Amount = 200, 
                    TransactionDate = DateTime.Now,
                    AccountId = new Guid("a0214d14-5570-4970-9834-c86f321e594b"),
                    CategoryTransaction = (BakingAppDataLayer.CategoryTransaction)CategoryTransaction.Entertainment
                },
                new Transaction()
                {   
                    Amount = 350,
                    TransactionDate = DateTime.Now,
                    AccountId = new Guid("cb254d14-5570-4323-9834-c86f3321594b"),
                    CategoryTransaction = (BakingAppDataLayer.CategoryTransaction)CategoryTransaction.Food
                }
            };
            context.Transactions.AddRange(transactions);
            context.SaveChanges();

            //Act
            var result = await transRepo.GetTransactions();

            //Assert
            result.Should().HaveCount(transactions.Count);
        }
        [TestMethod]
        public async Task TestGetTransactionById()
        {
            //Arrange
            var transRepo = new TransactionRepository(context);
            var accountId = new Guid("a0214d14-5570-4970-9834-c86f321e594b");
            var transactions = new List<Transaction>()
            {
                new Transaction()
                {
                    Amount = 200,
                    TransactionDate = DateTime.Now,
                    AccountId = new Guid("a0214d14-5570-4970-9834-c86f321e594b"),
                    CategoryTransaction = (BakingAppDataLayer.CategoryTransaction)CategoryTransaction.Entertainment
                },
                new Transaction()
                {
                    Amount = 350,
                    TransactionDate = DateTime.Now,
                    AccountId = new Guid("cb254d14-5570-4323-9834-c86f3321594b"),
                    CategoryTransaction = (BakingAppDataLayer.CategoryTransaction)CategoryTransaction.Food
                }
            };
            context.Transactions.AddRange(transactions);
            context.SaveChanges();

            //Act
            var result = await transRepo.GetAccountTransaction(accountId);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }
        [TestMethod]
        public async Task TestGetTransactionReport()
        {
            //Arrange
            var transRepo = new TransactionRepository(context);
            var accountId = new Guid("a0214d14-5570-4970-9834-c86f321e594b");
            var startDate = DateTime.UtcNow.AddDays(-1);
            var endDate = DateTime.UtcNow.AddDays(1);
            var transactions = new List<Transaction>()
            {
                new Transaction()
                {
                    Amount = 200,
                    TransactionDate = DateTime.Now,
                    AccountId = accountId,
                    CategoryTransaction = (BakingAppDataLayer.CategoryTransaction)CategoryTransaction.Entertainment
                },
                new Transaction()
                {
                    Amount = 350,
                    TransactionDate = DateTime.Now,
                    AccountId = accountId,
                    CategoryTransaction = (BakingAppDataLayer.CategoryTransaction)CategoryTransaction.Food
                },
                new Transaction()
                {
                    Amount = 750,
                    TransactionDate = DateTime.Now,
                    AccountId = new Guid("52971ead-df4b-4a87-84a1-f7a710f164ba"),
                    CategoryTransaction = (BakingAppDataLayer.CategoryTransaction)CategoryTransaction.Food
                }
            };
            context.Transactions.AddRange(transactions);
            context.SaveChanges();

            //Act
            var result =  await transRepo.GetTransactionReport(accountId, startDate, endDate);

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
    }
}