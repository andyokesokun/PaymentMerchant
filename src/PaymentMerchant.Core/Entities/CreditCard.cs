using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMerchant.Core.Entities
{
    public class CreditCard : Dtos.CreditCard
    {
        public ICollection<Transaction> Transactions { get; set; }
    }
}
