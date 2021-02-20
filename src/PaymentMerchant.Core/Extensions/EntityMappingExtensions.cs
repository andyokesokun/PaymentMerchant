using PaymentMerchant.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMerchant.Core.Extensions
{
    public static class EntityMappingExtensions
    {

        public static TransactionResponse MapTransactionResponse(this Core.Entities.Transaction transaction) {

            return new TransactionResponse
            {

                CardHolder = transaction.CreditCard.CardHolder,
                Amount = transaction.Amount,
                TransactionStatus = transaction.PaymentStatus.Status,
                TransactionDate = transaction.Date

            };

        }

    }
}
