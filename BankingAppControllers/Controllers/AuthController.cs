using BakingAppDataLayer;
using BankingAppBusiness.Auth;
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
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepository _auth;

        public AuthController(IAuthRepository auth)
        {
            _auth = auth;
        }


        [HttpPost("Register")]
        public async Task<ActionResult> AddUser([FromBody] RegisterApiModel model)

        {

            await _auth.Register(model);
            return Ok("Succes Register");





        }
        [HttpPost("Login")]

        public async Task<ActionResult> LoginUser(LoginApiModel model)
        {
             await _auth.Login(model);
            return Ok("Succes login");



        }
    }

}