using PaymentMerchant.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaymentMerchant.Core.Validators
{
    public class ValidateExpirationDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var expirationDate = Convert.ToDateTime(value);
            return UtilFuctions.IsEqualOrGreater(expirationDate, DateTime.Now);

        }
    }
}
