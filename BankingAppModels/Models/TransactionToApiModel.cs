﻿
using System.ComponentModel.DataAnnotations;


namespace BankingAppApiModels.Models
{
    public class TransactionToApiModel
    {
        [Required]
        public double TotalAmount { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Currency { get; set; }
      
    }
    public enum CategoryTransaction { Food, Entertainment }
    
}
