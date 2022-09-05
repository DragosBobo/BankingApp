using BankingAppBusiness.Auth;
using BankingAppControllers.Models.Requests;
using DataAcces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NSubstitute;

namespace BankingAppBussinessTests
{
    [TestClass]
    public class AuthRepositoryTests
    {
        private readonly IAuthRepository auth = Substitute.For<IAuthRepository>();
       
        [TestMethod]
        public async Task TestRegister()
        {
            var sut = auth;
            var Email = "Popescu.Marian@Gmail.com";
            var Password = "Parola12345@";
            bool isRegisterd;
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
            var foundUser = await sut.Login(new LoginApiModel() { Email=Email,Password=Password});
            if (foundUser != null)
            {
                isRegisterd = true;
            }
            else
            {
                isRegisterd = false;
            }

            //Assert
            Assert.IsTrue(isRegisterd);
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
