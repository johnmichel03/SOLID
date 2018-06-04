using JM.SOLID.Principles.Types;
using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using JM.SOLID.Principles.Config;
using JM.SOLID.Principles.Data;
using JM.SOLID.Principles.Services;
using JM.SOLID.Principles.Validator;

namespace JM.SOLID.Principles.Tests.Steps
{
    [Binding]
    public class PaymentServiceFeatureSteps
    {
        Mock<IConfigSettings> config;
        Mock<IUnitOfWork> uow;
        [BeforeScenario]
        public void BeforeExecution()
        {
            config = new Mock<IConfigSettings>();
            config.Setup(s => s.StoreType).Returns("BACKUP");
            uow = new Mock<IUnitOfWork>();
        }

        [Given(@"I have an account (.*),(.*),(.*),(.*)")]
        public void GivenIHaveAnAccount(string accountNumber, Decimal balance, AccountStatus accountStatus, AllowedPaymentSchemes allowedPaymentScheme)
        {
            var account = new Account()
            {
                AccountNumber = accountNumber,
                Balance = balance,
                Status = accountStatus,
                AllowedPaymentSchemes = allowedPaymentScheme
            };
            ScenarioContext.Current["DebtorAccount"] = account;
        }

        [Given(@"I have Payment Request details (.*),(.*),(.*),(.*),(.*)")]
        public void GivenIHavePaymentRequestDetails(PaymentScheme paymentScheme, string creditorAccountNumber, string debtorAccountNumber, decimal amount, DateTime paymentDate)
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

        [When(@"I make the payment")]
        public void WhenIMakeThePayment()
        {
            var request = ScenarioContext.Current["Request"] as MakePaymentRequest;
            var accountDebtor = ScenarioContext.Current["DebtorAccount"] as Account;
            var accountCreditor = ScenarioContext.Current["DebtorAccount"] as Account;
            accountCreditor.AccountNumber = request.CreditorAccountNumber;

            uow.When(() => request.DebtorAccountNumber != null).Setup(s => s.AccountDataStore.GetAccount(request.DebtorAccountNumber))
               .Returns(accountDebtor);
            uow.When(() => request.CreditorAccountNumber != null).Setup(s => s.AccountDataStore.GetAccount(request.CreditorAccountNumber))
              .Returns(accountCreditor);

            var paymentScheme = new PaymentFactory();
            var paymentRequestValidator = new MakePaymentRequestValidator(uow.Object);
            var paymentService = new PaymentService(uow.Object, paymentScheme, paymentRequestValidator);
            var result = paymentService.MakePayment(request);
            ScenarioContext.Current["Result"] = result;
        }

        [Then(@"The payment should be successful")]
        public void ThenThePaymentShouldBeSuccessful()
        {
            var result = ScenarioContext.Current["Result"] as MakePaymentResult;
            result.Success.Should().BeTrue();
        }

        [Then(@"The payment should be fail")]
        public void ThenThePaymentShouldBeFail()
        {
            var result = ScenarioContext.Current["Result"] as MakePaymentResult;
            result.Success.Should().BeFalse();
        }
    }
}
