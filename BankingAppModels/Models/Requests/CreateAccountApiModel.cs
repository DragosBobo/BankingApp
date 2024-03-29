﻿using BakingAppDataLayer;
using System.ComponentModel.DataAnnotations;
namespace BankingAppApiModels.Models.Requests
{
    public class CreateAccountApiModel : IValidatableObject
    {
        [Required]
        public AccountType AccountType { get; set; }
        [Required]
        public Currency Currency { get; set; }
        [Required]
        public string Iban { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (Currency < Currency.Ron || Currency > Currency.Dollar)
            {
                validationResults.Add(new ValidationResult($"{nameof(Currency)} must be one of :  {string.Join(" , ", Enum.GetNames(typeof(Currency)))}"));
            }
            if (AccountType < AccountType.Debit || AccountType > AccountType.Credit)
            {
                validationResults.Add(new ValidationResult($"{nameof(AccountType)} must be one of : {string.Join(" , ", Enum.GetNames(typeof(AccountType)))}"));
            }
            return validationResults;
        }
    }
}
