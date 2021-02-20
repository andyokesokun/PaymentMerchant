//using System;
//using System.Collections.Generic;
//using System.Text;

using Microsoft.Extensions.Logging;
using Moq;
using PaymentMerchant.Core.Enums;
using PaymentMerchant.Core.Interfaces;
using PaymentMerchant.Infrastructure.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PaymentMerchant.Test.Integrations
{
    public class PaymentServiceTest
    {

        private readonly Mock<ILogger<PaymentService>> _logger;
        private readonly Mock<ICheapPaymentGateway> _cheapPaymentService;
        private readonly Mock<IExpensivePaymentGateway> _expensivePaymentService;
        private readonly Mock<IPremiumPaymentGateway> _premiumPaymentService;
        private readonly Mock<IServiceProvider> _serviceProvider;

        public PaymentServiceTest()
        {
            _serviceProvider = new Mock<IServiceProvider>();
            _logger = new Mock<ILogger<PaymentService>>();

            _cheapPaymentService = new Mock<ICheapPaymentGateway>();
            _expensivePaymentService = new Mock<IExpensivePaymentGateway>();
            _premiumPaymentService = new Mock<IPremiumPaymentGateway>();

            ConfigService();

        }

        private void ConfigService() {

            _serviceProvider.Setup(s => s.GetService(typeof(ICheapPaymentGateway)))
                            .Returns(_cheapPaymentService.Object);
          
            _serviceProvider.Setup(s => s.GetService(typeof(IExpensivePaymentGateway)))
                        .Returns(_expensivePaymentService.Object);

            _serviceProvider.Setup(s => s.GetService(typeof(IPremiumPaymentGateway)))
                        .Returns(_premiumPaymentService.Object);



        }

        [Fact]
        public async Task When_AmountIsLessThanOrEqualTo20EuroUseCheapPaymentGateway_ShouldReturnPaymentStatusProccessed() {

            _cheapPaymentService.Setup(s => s.Process(It.IsAny<Core.Dtos.Transaction>() ))
                                .Returns(Task.FromResult(PaymentStatusType.Proccessed));

            var paymentService = new PaymentService(_logger.Object, _serviceProvider.Object);

            var data = await paymentService.Handle(GetTransactionTestData(20));

            Assert.Equal(PaymentStatusType.Proccessed, data);

        }


        [Fact]
        public async Task When_AmountIsGreaterThan21AndLessThan500Euro_UseExpensivePaymentGatewayIfAvailable_ShouldReturnPaymentStatusProccessed()
        {


            _expensivePaymentService.Setup(s => s.GetIsAvailable())
                              .Returns(true);

            _expensivePaymentService.Setup(s => s.Process(It.IsAny<Core.Dtos.Transaction>()))
                                .Returns(Task.FromResult(PaymentStatusType.Proccessed));

            var paymentService = new PaymentService(_logger.Object, _serviceProvider.Object);

            var data = await paymentService.Handle(GetTransactionTestData(300));

            Assert.Equal(PaymentStatusType.Proccessed, data);

        }

        [Fact]
        public async Task When_AmountIsGreaterThan21AndLessThan500Euro_IfExpensiveServiceUnavailableUseCheapServiceGatway_ShouldReturnPaymentStatusProccessed()
        {

            _expensivePaymentService.Setup(s => s.GetIsAvailable())
                              .Returns(false);

            _cheapPaymentService.Setup(s => s.Process(It.IsAny<Core.Dtos.Transaction>()))
                    .Returns(Task.FromResult(PaymentStatusType.Proccessed));

            var paymentService = new PaymentService(_logger.Object, _serviceProvider.Object);

            var data = await paymentService.Handle(GetTransactionTestData(300));

            Assert.Equal(PaymentStatusType.Proccessed, data);

        }

        [Fact]
        public async Task When_AmountIsGreaterThan500Euro_UsePremuimPaymentGatewayIfAvailable_ShouldReturnPaymentStatusProccessed()
        {


            _premiumPaymentService.Setup(s => s.GetIsAvailable())
                              .Returns(true);

            _premiumPaymentService.Setup(s => s.Process(It.IsAny<Core.Dtos.Transaction>()))
                                .Returns(Task.FromResult(PaymentStatusType.Proccessed));

            var paymentService = new PaymentService(_logger.Object, _serviceProvider.Object);

            var data = await paymentService.Handle(GetTransactionTestData(700));

            Assert.Equal(PaymentStatusType.Proccessed, data);

        }

        [Fact]
        public async Task When_AmountIsGreaterThan500Euro_AnndPremuimPaymentGatewayNotAvailableRetryThrice_ShouldReturnPaymentStatusFailed()
        {


            _premiumPaymentService.Setup(s => s.GetIsAvailable())
                              .Returns(false);

            _premiumPaymentService.Setup(s => s.Process(It.IsAny<Core.Dtos.Transaction>()))
                                .Returns(Task.FromResult(PaymentStatusType.Failed));

            var paymentService = new PaymentService(_logger.Object, _serviceProvider.Object);

            var data = await paymentService.Handle(GetTransactionTestData(700));

            Assert.Equal(PaymentStatusType.Failed, data);

        }



        private Core.Dtos.Transaction GetTransactionTestData(Decimal amount)
        {
            return new Core.Dtos.Transaction
            {
                Amount =amount,
                CreditCardId = 1

            };
        }


    }
}
