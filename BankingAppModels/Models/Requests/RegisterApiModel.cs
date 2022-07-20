
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppControllers.Models.Requests
{
    public class RegisterApiModel:IValidatableObject
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
        
        public string Password { get; set; }
        [Required]
       
        public string ConfirmedPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (Password != ConfirmedPassword)
            {
                validationResults.Add(new ValidationResult($"{nameof(Password)} must match with {nameof(ConfirmedPassword)}"));
            }

            return validationResults;
        }
    }
}
