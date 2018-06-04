using JM.SOLID.Principles.Data;
using JM.SOLID.Principles.Types;
using JM.SOLID.Principles.Validator;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace JM.SOLID.Principles.Tests.Steps
{
    [Binding]
    public class MakePaymentRequestSteps
    {
        [Given(@"I have an payment request information (.*),(.*),(.*),(.*),(.*)")]
        public void GivenIHaveAnPaymentRequestInformation(string creditorAccountNumber, string debtorAccountNumber, Decimal amount, DateTime paymentDate, PaymentScheme paymentScheme)
        {
            var request = new MakePaymentRequest()
            {
                PaymentScheme = paymentScheme,
                CreditorAccountNumber = creditorAccountNumber,
                DebtorAccountNumber = debtorAccountNumber,
                Amount = amount,
                PaymentDate = paymentDate
            };
            ScenarioContext.Current["Request"] = request;
        }

        [When(@"I call the MakePaymentRequest validation")]
        public void WhenICallTheMakePaymentRequestValidation()
        {
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            var request = ScenarioContext.Current["Request"] as MakePaymentRequest;
            uow.When(() => request.DebtorAccountNumber != "").Setup(s => s.AccountDataStore.GetAccount(request.DebtorAccountNumber))
                .Returns(new Account() { AccountNumber = request.DebtorAccountNumber });
            uow.When(() => request.CreditorAccountNumber != "").Setup(s => s.AccountDataStore.GetAccount(request.CreditorAccountNumber))
              .Returns(new Account() {AccountNumber=request.CreditorAccountNumber });
            uow.When(() => request.CreditorAccountNumber == "").Setup(s => s.AccountDataStore.GetAccount(request.CreditorAccountNumber))
             .Returns(new Account());

            var validator = new MakePaymentRequestValidator(uow.Object);
            ScenarioContext.Current["Result"] = validator.IsValid(request);
        }

        [Then(@"The validation should be successful")]
        public void ThenTheValidationShouldBeSuccessful()
        {
            var result = ScenarioContext.Current["Result"] as ValidationResult;
            result.IsValid.Should().BeTrue();
        }

        [Then(@"The validation should be fail")]
        public void ThenTheValidationShouldBeFail()
        {
            var result = ScenarioContext.Current["Result"] as ValidationResult;
            Assert.IsFalse(result.IsValid, result.Errors?.FirstOrDefault()?.ErrorMessage);
        }
    }
}
