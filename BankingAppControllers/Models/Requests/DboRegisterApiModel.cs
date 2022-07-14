using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppControllers.Models.Requests
{
    public class DboRegisterApiModel
    {
       
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringValidator(MinLength = 8)]
        public string Password { get; set; }
        [Required]
        [StringValidator(MinLength = 8)]
        public string ConfirmedPassword { get; set; }



    }
}
