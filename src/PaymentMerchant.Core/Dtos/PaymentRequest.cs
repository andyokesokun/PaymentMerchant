using PaymentMerchant.Core.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaymentMerchant.Core.Dtos
{
    public class PaymentRequest
    {
        [Required]
        [MaxLength(16)]
        [ValidateCreditCard]
        public string CreditCardNumber { get; set; }
        [Required]
        [MaxLength(500)]
        public string CardHolder { get; set; }
        [Required]
        [ValidateExpirationDate]
        public DateTime ExpirationDate { get; set; }
        [Required]
        [RegularExpression("^\\d{3}$", ErrorMessage ="Please Provide a valid Security Code") ]
        public int SecurityCode { get; set; }
        [Required]
        [RegularExpression("^\\+?(0|[1-9]\\d*)$|^\\+?(0|[1-9]\\d*)?(\\.[1-9]\\d*)$",ErrorMessage = "Please Provide a valid amount")]
        public Decimal Amount { get; set; }

    }
}
