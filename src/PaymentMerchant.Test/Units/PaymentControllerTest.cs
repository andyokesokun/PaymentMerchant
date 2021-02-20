using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentMerchant.API.Controllers;
using PaymentMerchant.Core.Dtos;
using PaymentMerchant.Core.Enums;
using PaymentMerchant.Core.Interfaces;
using PaymentMerchant.Core.map;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace PaymentMerchant.Test.Units
{
    public class PaymentControllerTest
    {

        private IMapper _mapper;
        private readonly Mock<ILogger<PaymentController>> _logger;
        private readonly Mock<IServiceProvider> _serviceProvider;


        public PaymentControllerTest()
        {

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _logger = new Mock<ILogger<PaymentController>>();
            _serviceProvider = new Mock<IServiceProvider>();

            ConfigService();


        }

        private void ConfigService() {
            _serviceProvider
                .Setup(s => s.GetService(typeof(IPaymentService)))
                .Returns(GetMockedPaymentService().Object);

            _serviceProvider
                .Setup(s => s.GetService(typeof(ICreditCardService)))
                .Returns(GetMockedCreditCardService().Object);

            _serviceProvider
                .Setup(s => s.GetService(typeof(IMapper)))
                .Returns(_mapper);
        }

   

        [Fact]
        public async Task When_PaymentIsProcess_ShouldReturnResultOk()
        {

            var paymentRequest = GetPaymentRequestTestData();

            var controller = new PaymentController(_logger.Object, _serviceProvider.Object);


            var result = await controller.ProcessPayment(paymentRequest);

            Assert.IsType<OkObjectResult>(result);

        }



        private Mock<IPaymentService> GetMockedPaymentService()
        {
            var paymentService = new Mock<IPaymentService>();
            paymentService.Setup(repo => repo.Handle(new Core.Dtos.Transaction()))
                          .Returns(Task.FromResult(PaymentStatusType.Proccessed));

            return paymentService;
        }

        private Mock<ICreditCardService> GetMockedCreditCardService()
        {

            var creditCardService = new Mock<ICreditCardService>();
            creditCardService.Setup(s => s.Save(GetCreditTestData()))
                             .Returns(Task.CompletedTask);
            return creditCardService;
        }

        private CreditCard GetCreditTestData()
        {

            return new CreditCard
            {
                Id = 1,
                CardHolder = "Andrew Okesokun",
                CreditCardNumber = "5261320221768612",
                ExpirationDate = DateTime.Now.AddDays(365),
                SecurityCode = 333,
            };
        }

        private PaymentRequest GetPaymentRequestTestData()
        {

            return new PaymentRequest
            {
                CardHolder = "Andrew Okesokun",
                CreditCardNumber = "5261320221768612",
                Amount = 300,
                ExpirationDate = DateTime.Now.AddDays(365),
                SecurityCode = 333,

            };

        }




    }
}
