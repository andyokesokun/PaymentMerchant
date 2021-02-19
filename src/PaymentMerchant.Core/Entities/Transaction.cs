using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMerchant.Core.Entities
{
    public class Transaction : Dtos.Transaction
    {
        public  CreditCard  CreditCard{ get; set; }
        public  PaymentStatus PaymentStatus { get; set; }
    }
}
