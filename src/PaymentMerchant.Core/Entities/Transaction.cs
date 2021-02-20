using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMerchant.Core.Entities
{
    public class Transaction : Dtos.Transaction
    {
        public virtual CreditCard  CreditCard{ get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }
    }
}
