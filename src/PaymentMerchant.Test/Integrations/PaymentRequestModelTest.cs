using PaymentMerchant.Core.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace PaymentMerchant.Test.Integrations
{
    public class PaymentRequestModelTest
    {



        public PaymentRequestModelTest()
        {

        }


        [Fact]
        public void When_AllValuesProvided_ModelStateShouldBeValid() {

            var paymentRequest = new PaymentRequest
            {
                CardHolder = "Andrew Okesokun",
                CreditCardNumber = "5261320221768612",
                Amount = 300,
                ExpirationDate = DateTime.Now.AddDays(365),
                SecurityCode = 333,

            };

            var result = ModelValidation(paymentRequest);

            Assert.Equal(0, result.Count);


        }

        [Fact]
        public void When_SomeValuesProvided_ModelStateShouldBeInValid()
        {

            var paymentRequest = new PaymentRequest
            {
              //  CardHolder = "Andrew Okesokun",
              //  CreditCardNumber = "5261320221768612",
                Amount = 300,
                ExpirationDate = DateTime.Now.AddDays(365),
                SecurityCode = 333,

            };

            var result = ModelValidation(paymentRequest);

            Assert.Equal(2, result.Count);


        }

        [Fact]
        public void When_InvalidCreditCardValueProvided_ModelStateShouldBeInvalid()
        {

            var paymentRequest = new PaymentRequest
            {
                CardHolder = "Andrew Okesokun",
                CreditCardNumber = "5261454221768612",
                Amount = 300,
                ExpirationDate = DateTime.Now.AddDays(365),
                SecurityCode = 333,

            };

            var result = ModelValidation(paymentRequest);

            Assert.Equal(1, result.Count);

        }

        [Fact]
        public void When_InvalidSecurityCodeValueProvided_ModelStateShouldBeInvalid()
        {

            var paymentRequest = new PaymentRequest
            {
                CardHolder = "Andrew Okesokun",
                CreditCardNumber = "5261320221768612",
                Amount = 300,
                ExpirationDate = DateTime.Now.AddDays(365),
                SecurityCode = 33,

            };

            var result = ModelValidation(paymentRequest);

            Assert.Equal(1, result.Count);

        }


        [Fact]
        public void When_NegativeAmountValueProvided_ModelStateShouldBeInvalid()
        {

            var paymentRequest = new PaymentRequest
            {
                CardHolder = "Andrew Okesokun",
                CreditCardNumber = "5261320221768612",
                Amount = 0.0M,
                ExpirationDate = DateTime.Now.AddDays(365),
                SecurityCode = 333,

            };

            var result = ModelValidation(paymentRequest);

            Assert.Equal(1, result.Count);

        }


        [Fact]
        public void When_ExpirationDateValueProvidedIsLessThanCurrentDate_ModelStateShouldBeInvalid()
        {

            var paymentRequest = new PaymentRequest
            {
                CardHolder = "Andrew Okesokun",
                CreditCardNumber = "5261320221768612",
                Amount = 0.4545M,
                ExpirationDate = DateTime.Now.AddDays(-2),
                SecurityCode = 333,

            };

            var result = ModelValidation(paymentRequest);

            Assert.Equal(1, result.Count);

        }

        private IList<ValidationResult> ModelValidation(object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result,true);
            if (model is IValidatableObject) 
                (model as IValidatableObject).Validate(validationContext);

            return result;
        }

    }
}
