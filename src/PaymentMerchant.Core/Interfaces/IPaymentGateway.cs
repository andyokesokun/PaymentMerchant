using PaymentMerchant.Core.Dtos;
using PaymentMerchant.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMerchant.Core.Interfaces
{
    public interface IPaymentGateway
    {
        Task <PaymentStatusType> Process(Transaction transaction);
    }
}
