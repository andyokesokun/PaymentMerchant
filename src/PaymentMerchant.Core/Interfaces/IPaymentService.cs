using PaymentMerchant.Core.Dtos;
using PaymentMerchant.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMerchant.Core.Interfaces
{
    public interface IPaymentService
    {    
       Task<PaymentStatusType> Handle(Transaction transaction);
    }
}
