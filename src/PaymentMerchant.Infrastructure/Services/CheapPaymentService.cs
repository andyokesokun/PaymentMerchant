using AutoMapper;
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
    public class CheapPaymentService : ICheapPaymentGateway
    {
        private ILogger<CheapPaymentService> _logger;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public CheapPaymentService(ILogger<CheapPaymentService> logger, IServiceProvider serviceProvider)
        {

            _logger = logger;
            _transactionRepository = serviceProvider.GetRequiredService<ITransactionRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<PaymentStatusType> Process(Transaction transaction)
        {

            _logger.LogInformation("Transaction Processing with CheapPaymentService");
  
            var entity = _mapper.Map<Core.Entities.Transaction>(transaction);

            entity.PaymentStatusId = (int)PaymentStatusType.Proccessed;
            await _transactionRepository.Save(entity);

            return PaymentStatusType.Proccessed;
   

        }
    }
}
