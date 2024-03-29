using BakingAppDataLayer;
using BankingAppApiModels.Models.Requests;
using BankingAppBusiness.AccountRepo;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
namespace BankingAppBussinessTests
{
    [TestClass]
    public class AccountRepositoryTests : BaseTest
    {
        [TestMethod]
        public async Task TestAddAccountShouldCreateOneAccount()
        {
            //Arrange 
            var authRepo = new AccountRepository(context);
            var guid = new Guid();
            var account = new CreateAccountApiModel
            {
                AccountType =AccountType.Credit,
                Currency = Currency.Euro,
                Iban = "RO0736183718293",
                UserId = guid,
            };
            //Act 
            await authRepo.AddAccount(account);
            var foundAccount = await context.Accounts.FirstOrDefaultAsync(x=>x.Iban == account.Iban);
            
            //Assert 
            context.Accounts.Should().HaveCount(1);
            foundAccount.Should().BeEquivalentTo(foundAccount, opt => opt.Excluding(si => si.Id));
        }
        [TestMethod]
        public async Task TestGetAccounts()
        {
            //Arrange 
            var authRepo = new AccountRepository(context);
            var accounts = new List<Account>()
            {
                new()
                {
                    AccountType =AccountType.Credit,
                    Currency = Currency.Ron,
                    Iban = "RO033373618371231238293",
                    UserId=new Guid("cff9d17f-bdfc-450d-a6c7-6aa8467383c8"),
                },
                new()
                {
                    AccountType = AccountType.Debit,
                    Currency = Currency.Dollar,
                    Iban = "RO07361123138373318293",
                    UserId=new Guid("fc231452-7805-40a9-ae8c-9ac4743d4250"),
                }
            };
            context.Accounts.AddRange(accounts);
            await context.SaveChangesAsync();

            // Act 
            var response = await authRepo.GetAccounts();

            // Assert 
            response.Should().HaveCount(accounts.Count);
        }
        [TestMethod]
        public async Task TestGetAcountByIdShouldReturnOneAccount()
        {
            //Arrange 
            var authRepo = new AccountRepository(context);
            var id = new Guid("2df99e1f-ca5c-4c62-a444-c379b900cb96");
            var account = new Account
            {
                Id = new Guid("2df99e1f-ca5c-4c62-a444-c379b900cb96"),
                AccountType = AccountType.Credit,
                Currency = Currency.Euro,
                Iban = "RO033373618371231238293",
                UserId = id,
            };
            context.Accounts.Add(account);
            await context.SaveChangesAsync();

            //Act 
            var result = await authRepo.GetAccountById(id);

            // Assert 
            result.Should().Be(account);
        }
        [TestMethod]
        public async Task TestUpdateAccountShouldUpdateOneAccount()
        {
            //Arrange
            var authRepo = new AccountRepository(context);
            var id = new Guid("2df99e1f-ca5c-4c62-a444-c379b900cb86");
            var account = new Account
            {
                Id = id,
                AccountType = AccountType.Credit,
                Currency = Currency.Euro,
                Iban = "RO033373618371231238293",
                UserId = new Guid("cff9d17f-bdfc-450d-a6c7-6aa8467383c8"),
            };
            context.Accounts.Add(account);
            await context.SaveChangesAsync();
            var updateModel = new CreateAccountApiModel
            {
                AccountType = AccountType.Credit,
                Currency = Currency.Euro,
                UserId = new Guid("3386e917-4437-4342-94c1-f2693117d638"),
                Iban = "RO012315751234523",
            };
           
            //Act 
            var result = await authRepo.UpdateAccount(id, updateModel);
            var accountUpdated = await context.Accounts.FirstOrDefaultAsync(x => x.Id == id);

            // Assert
            accountUpdated.Should().BeEquivalentTo(updateModel);
        }
        [TestMethod]
        public async Task TestDeleteAccountShouldDeleteAccount()
        {
            //Arrange
            var sut = new AccountRepository(context);
            var id =new Guid("2df99e1f-ca5c-4c62-a444-c379b900cb86");
            var account = new Account
            {
                Id = id,
                AccountType = AccountType.Credit,
                Currency = Currency.Euro,
                Iban = "RO033373618371231238293",
                UserId = id,
            };
            context.Accounts.Add(account);
            await context.SaveChangesAsync();
            
            // Act
            var result = await sut.DeleteAccount(id);
            var foundAccount = await context.Accounts.FirstOrDefaultAsync(x => x.Id == id); 

            //Assert
            context.Accounts.Should().BeEmpty();
            foundAccount.Should().BeNull();
        }
    }
}
