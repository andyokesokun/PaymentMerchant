using PaymentMerchant.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMerchant.Core.Interfaces
{
    public interface ICreditCardService
    {
        Task<CreditCard> FindByCardNumber(string cardNumber);
        Task Save(CreditCard creditCard);
    }
}
