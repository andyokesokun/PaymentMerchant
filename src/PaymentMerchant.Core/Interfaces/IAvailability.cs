using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMerchant.Core.Interfaces
{
    public interface  IAvailability
    {
        bool GetIsAvailable();
        void SetIsAvailable(bool value);
    }
}
