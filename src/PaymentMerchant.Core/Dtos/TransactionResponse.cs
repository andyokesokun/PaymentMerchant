using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMerchant.Core.Dtos
{
    public class TransactionResponse
    {
        public string CardHolder { get; set; }
        public Decimal Amount { get; set; }
        public string  TransactionStatus { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
