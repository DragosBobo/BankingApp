using BakingAppDataLayer;
using BankingAppControllers.Models.Requests;
using DataAcces;
using Microsoft.AspNetCore.Identity;

namespace BankingAppBusiness.Auth
{
    public class AuthRequest:IAuthRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthRequest(DataContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        private bool IsExist(string userName)
        {
            var dbUser = _context.Users.FirstOrDefault(x=>x.UserName==userName);

            return dbUser != null ? true : false ;
        }
        private User MapperUser(RegisterApiModel model)
        {
            return new User { Email = model.Email, UserName = model.UserName, FirstName = model.FirstName, LastName = model.LastName };
        }
        private User MapperLogin(string email)
        {
            return new User { Email = email };
        }
    
        public async Task Register(RegisterApiModel model)
        {
            if (!IsExist(model.UserName))
            {
                var user = MapperUser(model);
               await _userManager.CreateAsync(user, model.Password);


            }
        }
        public async Task<SignInResult> Login(LoginApiModel model)
        {
            var user = MapperLogin(model.Email);
            return await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            
            
        }
    

    }
}
