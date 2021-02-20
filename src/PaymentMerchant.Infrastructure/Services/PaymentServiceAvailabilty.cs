using PaymentMerchant.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMerchant.Infrastructure.Services
{
    public abstract class PaymentServiceAvailabilty : IAvailability
    {
        public bool IsAvailable { get; set; } = true;

        public bool GetIsAvailable()
        {
            return IsAvailable;
        }

        public void SetIsAvailable(bool value)
        {
            IsAvailable = value;
        }
    }
}
