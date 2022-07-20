using System.ComponentModel.DataAnnotations;
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
        [Required]
        public Guid UserId1 { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
    public enum Currency { Ron, Euro, Dollar }
    public enum AccountType { Debit, Credit }
}
