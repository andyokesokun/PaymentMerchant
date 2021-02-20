using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaymentMerchant.Core.Dtos
{
    public class CreditCard
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(16)]
        public string CreditCardNumber { get; set; }
        [Required]
        [MaxLength(500)]
        public string CardHolder { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set;} 
        [MaxLength(3)]
        [Required]
        public int SecurityCode { get; set; }
    }
}
