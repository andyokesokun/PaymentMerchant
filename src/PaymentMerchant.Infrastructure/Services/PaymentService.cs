using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaymentMerchant.Core.Dtos;
using PaymentMerchant.Core.Enums;
using PaymentMerchant.Core.Interfaces;
using PaymentMerchant.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMerchant.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<IPaymentService> _logger;
        private readonly ICheapPaymentGateway _cheapPaymentService;
        private readonly IExpensivePaymentGateway _expensivePaymentService;
        private readonly IPremiumPaymentGateway _premiumPaymentService;

        public PaymentService(ILogger<IPaymentService> logger, IServiceProvider serviceProvider) {

            _logger = logger;
            _cheapPaymentService = serviceProvider.GetRequiredService<ICheapPaymentGateway>();
            _expensivePaymentService = serviceProvider.GetRequiredService<IExpensivePaymentGateway>();
            _premiumPaymentService = serviceProvider.GetRequiredService<IPremiumPaymentGateway>();
     

        }

        public async Task<PaymentStatusType> Handle(Transaction transaction)
        {

            var amount = transaction.Amount;
            PaymentStatusType statusType;
          

            if (amount <= 20)
            {
                statusType = await _cheapPaymentService.Process(transaction);
            }
            else if (amount >= 21 && amount <= 500)
            {
                statusType = await ProcessExpensivePayment(transaction);
            }
            else {
                var retries = 3;
                statusType=await ProcessPremiumPayment(transaction, retries);
            }

            return statusType;

        }
        private async Task<PaymentStatusType> ProcessExpensivePayment(Transaction transaction) {

            //use cheapPaymentService once if expensivePaymentService is not available
            if (_expensivePaymentService.GetIsAvailable())
            {
                return await _expensivePaymentService.Process(transaction);
            }
            else
            {
               return await _cheapPaymentService.Process(transaction);
            }

        }

        private async Task<PaymentStatusType> ProcessPremiumPayment(Transaction transaction, int retries)
        {

            var status=await _premiumPaymentService.Process(transaction);

            if (status.Equals(PaymentStatusType.Failed)  && retries > 0)
            {
                return await ProcessPremiumPayment(transaction, retries - 1 );
            }

            return status;
          
  
            
        }

    
    }
}
