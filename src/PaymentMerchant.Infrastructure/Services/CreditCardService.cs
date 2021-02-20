using AutoMapper;
using PaymentMerchant.Core.Dtos;
using PaymentMerchant.Core.Interfaces;
using PaymentMerchant.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMerchant.Infrastructure.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IMapper _mapper;

        public CreditCardService(ICreditCardRepository creditCardRepository, IMapper mapper)
        {
            _creditCardRepository = creditCardRepository;
            _mapper = mapper;
        }
        public async Task<CreditCard> FindByCardNumber(string cardNumber)
        {
            return await _creditCardRepository.FindByCardNumber(cardNumber);
        }


        public async Task Save(CreditCard creditCard)
        {
            var entity = _mapper.Map<Core.Entities.CreditCard>(creditCard);
            await _creditCardRepository.Save(entity);
            creditCard.Id = entity.Id;
        }
    }
}
