using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaymentMerchant.Core.Dtos
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        public int CreditCardId { get; set; }
        [Required]
        public int PaymentStatusId { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal Amount { get; set; }
        public DateTime Date { get; set; } = new DateTime();
    }

}