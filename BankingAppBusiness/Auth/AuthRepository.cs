using BakingAppDataLayer;
using BankingAppControllers.Models.Requests;
using DataAcces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingAppBusiness.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthRepository(DataContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        private bool IsExist(string userName)
        {
            var dbUser = _context.Users.FirstOrDefault(x => x.UserName == userName);

            return dbUser != null ? true : false ;
        }
        private User Mapper(RegisterApiModel model)
        {
            return new User 
            { 
                Email = model.Email,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName 
            };
        }
        public async Task Register(RegisterApiModel model)
        {
            if (!IsExist(model.UserName))
            {
                var user = Mapper(model);
                await _userManager.CreateAsync(user, model.Password);
            }
        }
        public async Task<string> Login(LoginApiModel model)
        {
            var user = await Authenticate(model);
            if (user != null)
            {
                var token = Generate(user);
                return token;
            }
            else
            {
                return null;
            }
        }
        private string Generate(User model)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AexAmsGRzrXbOcgK8lhB"));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier , model.UserName),
                new Claim(ClaimTypes.Email , model.Email),
                new Claim(ClaimTypes.GivenName , model.FirstName),
                new Claim(JwtRegisteredClaimNames.Nbf , new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp , new DateTimeOffset(DateTime.Now.AddHours(1)).ToUnixTimeSeconds().ToString()),
            };
            var token = new JwtSecurityToken(default,default,claims, signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<User> Authenticate(LoginApiModel model)
        {
            var user =  await _userManager.FindByEmailAsync(model.Email);
            var result =  await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if(result.Succeeded)
            {
                return user;
            }
            return null;  
        }
    }
}
