using BakingAppDataLayer;
using BankingAppApiModels.Models;
using BankingAppApiModels.Models.Requests;
using BankingAppBusiness.TransactionRepo;
using FluentAssertions;
using CategoryTransaction = BankingAppApiModels.Models.Requests.CategoryTransaction;

namespace BankingAppBussinessTests
{
    [TestClass]
    public class TransactionTests : BaseTest
    {
        [TestMethod]
        public async Task TestCreateTransactionShouldCreateTransaction()
        {   
            //Arrange
            var transRepo = new TransactionRepository(context);
            var date = DateTime.Now;
            var id = new Guid("a0214d14-5570-4970-9834-c86f321e594b");
            var transaction = new CreateTransactionApiModel
            {  
                Amount = 200,
                TransactionDate = date,
                AccountId = id,
                CategoryTransaction = CategoryTransaction.Food
            };
            
            //Act
            await transRepo.CreateTransaction(transaction);
            var result = context.Transactions.FirstOrDefault();

            //Assert
            transaction.Should().BeEquivalentTo(result, opt => opt.Excluding(x => x.Id).Excluding(x => x.Account).Excluding(x => x.AccountId).Excluding(x => x.Amount));
        }

        [TestMethod]
        public async Task TestGetTransactionsShouldReturnTransactions()
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
        public async Task TestGetTransactionByIdShouldReturnTransactionsForOneAccount()
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
                    AccountId = accountId,
                    CategoryTransaction = (BakingAppDataLayer.CategoryTransaction)CategoryTransaction.Entertainment
                },
                new Transaction()
                {
                    Amount = 350,
                    TransactionDate = DateTime.Now,
                    AccountId = accountId,
                    CategoryTransaction = (BakingAppDataLayer.CategoryTransaction)CategoryTransaction.Food
                }
            };
            context.Transactions.AddRange(transactions);
            context.SaveChanges();

            //Act
            var result = await transRepo.GetAccountTransaction(accountId);

            //Assert
            result.Should().HaveCount(transactions.Count);
        }

        [TestMethod]
        public async Task TestGetTransactionReportShouldReturnReportForOneAccount()
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
                }
            };
            var expectedResult = new List<TransactionToApiModel>() 
            {
                new TransactionToApiModel
                {
                    CategoryName = CategoryTransaction.Entertainment.ToString(),
                    TotalAmount = 200,
                },
                new TransactionToApiModel
                {
                    CategoryName = CategoryTransaction.Food.ToString(),
                    TotalAmount = 350,
                }
            };
            context.Transactions.AddRange(transactions);
            context.SaveChanges();

            //Act
            var result =  await transRepo.GetTransactionReport(accountId, startDate, endDate);

            //Assert
            expectedResult.Should().BeEquivalentTo(result); 
        }
    }
}