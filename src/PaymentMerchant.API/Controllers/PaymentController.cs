using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaymentMerchant.Core.Dtos;
using PaymentMerchant.Core.Enums;
using PaymentMerchant.Core.Interfaces;

namespace PaymentMerchant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ICreditCardService _creditCardService;
        private readonly IPaymentService _paymentService;
        private IMapper _mapper;

        public PaymentController(ILogger<PaymentController> logger, IServiceProvider serviceProvider)
        {
            _creditCardService = serviceProvider.GetService<ICreditCardService>();
            _paymentService = serviceProvider.GetService<IPaymentService>();
            _mapper = serviceProvider.GetService<IMapper>();
        }

        [HttpPost]
        [Route("processPayment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
    

        public  async Task <IActionResult> ProcessPayment([Bind]PaymentRequest paymentRequest) {

       
            var creditCard = await _creditCardService.FindByCardNumber(paymentRequest.CreditCardNumber);

            if (creditCard == null)
            {
                creditCard = _mapper.Map<CreditCard>(paymentRequest);
                await _creditCardService.Save(creditCard);
            }


            var transaction = _mapper.Map<Transaction>(paymentRequest);
            transaction.CreditCardId = creditCard.Id;
            var status = await _paymentService.Handle(transaction);


            return Ok(new ApiResponse { Message = "Payment Processed", Status=PaymentStatusType.Proccessed.ToString() });


        }
    }
}