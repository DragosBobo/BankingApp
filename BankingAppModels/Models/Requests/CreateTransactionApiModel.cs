
using System.ComponentModel.DataAnnotations;

namespace BankingAppApiModels.Models.Requests
{
    public class CreateTransactionApiModel
    {
        [Required]
        public double amount { get; set; }
        [Required]
        public CategoryTransaction CategoryTransaction { get; set; }
        [Required]
        public DateTimeOffset TransactionDate { get; set; }
        [Required]
        public Guid AccountId { get; set; }
    }
    public enum CategoryTransaction { Food, Entertainment }


}
