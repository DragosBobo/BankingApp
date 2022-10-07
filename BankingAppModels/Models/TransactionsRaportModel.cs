using System.ComponentModel.DataAnnotations;


namespace BankingAppApiModels.Models
{
    public class TransactionRaportModel
    {
        [Required]
        public double TotalAmount { get; set; }
        [Required]
        public string CategoryName { get; set; }

    }

}
