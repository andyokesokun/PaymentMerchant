﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMerchant.Core.Interfaces
{
    public interface IPremiumPaymentGateway : IPaymentGateway, IAvailability
    {
    }
}