using BakingAppDataLayer;
using BankingAppBusiness.Auth;
using BankingAppControllers.Models.Requests;
using DataAcces;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepository _auth;

        public AuthController(IAuthRepository auth)
        {
            _auth = auth;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
       
        public async Task<ActionResult> AddUser([FromBody] RegisterApiModel model)

        {

            await _auth.Register(model);
            return Ok("Succes Register");





        }
        [AllowAnonymous]
        [HttpPost("Login")]
        
        public ActionResult LoginUser(LoginApiModel model)
        {
             var login = _auth.Login(model);
            if(login == null)
            {
                return BadRequest("Can't login ");
             }
            else
            {
                return Ok(login);
            }

        }
    }

}