using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppApiModels.Models.Account
{
    public class CreateAccountModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public AccountType AccountType { get; set; }
        [Required]
        public Currency Currency { get; set; }
        [Required]
        public string Iban { get; set; }
    }
    public enum Currency { Ron, Euro, Dollar }
    public enum AccountType { Debit, Credit }
}
