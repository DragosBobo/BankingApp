using BakingAppDataLayer;

using DataAcces;
using Microsoft.EntityFrameworkCore;

using BankingAppBusiness.Auth;

using BankingAppControllers.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NSubstitute;

namespace BankingAppBussinessTests
{
    [TestClass]
    public class AuthRepositoryTests
    {

        private readonly DataContext context;
        private readonly IAuthRepository auth = Substitute.For<IAuthRepository>();

        public AuthRepositoryTests()
        {
            DbContextOptionsBuilder<DataContext> dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(
                Guid.NewGuid().ToString());
            context = new DataContext(dbOptions.Options);
        }

        [TestMethod]
        public async Task TestRegister()
        {
            var sut = auth;
           
            var user = new RegisterApiModel
            {
                FirstName = "Popescu",
                LastName = "Marian",
                UserName = "PopescuMarian2000",
                Email = "Popescu.Marian@Gmail.com",
                Password = "Parola12345@",
                ConfirmedPassword = "Parola12345@"
            };

            // Act
            bool result = await sut.Register(user);

            //Assert
            Assert.IsTrue(true);
        }
        [TestMethod]
        public async Task TestLogin()
        {   //Arange
            var sut = auth;

            var user = new RegisterApiModel
            {
                FirstName = "Popescu",
                LastName = "Marian",
                UserName = "PopescuMarian2000",
                Email = "Popescu.Marian@Gmail.com",
                Password = "Parola12345@",
                ConfirmedPassword = "Parola12345@"
            };
            await sut.Register(user);
            LoginApiModel model = new LoginApiModel { 
            Password="Parola12345@",
            Email="Popescu.Marian@Gmail.com"
            
            };

            //Act 
            var result = await sut.Login(model);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
