using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace BankingAppApiModels.Models
{
    public class AccountApiModel
    {

        [Required]
        public string AccountType { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public string Iban { get; set; }
        [Required]
        public string AccountId { get; set; }

    }
    
}
