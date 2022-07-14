using BakingAppDataLayer;
using BankingAppControllers.Models.Requests;
using DataAcces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppControllers.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class UserController   : ControllerBase 
    {
        private readonly DataContext _context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public UserController(DataContext context , UserManager<User> userManager, SignInManager<IdentityUser> signInManager*/)
        { 
            
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> AddUsers(DboRegisterApiModel model)
        {
            if (model.Password == model.ConfirmedPassword)
            {
                var user = new User { Email = model.Email, UserName = model.UserName,FirstName=model.FirstName,LastName=model.LastName};
                var result = await this.userManager.CreateAsync(user, model.Password);
                return Ok("Succes Register");
                //implement some auto login after register 
            }
            else return BadRequest("Passwords must mach ");
        }
        
        


    }
}
