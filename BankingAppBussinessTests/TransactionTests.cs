using BakingAppDataLayer;
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
    }
}