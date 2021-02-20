using PaymentMerchant.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaymentMerchant.Core.Validators
{
    public class ValidateCreditCard : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var data = Convert.ToString(value);
            return UtilFuctions.IsCardNumberValid(data);

        }
    }
}
