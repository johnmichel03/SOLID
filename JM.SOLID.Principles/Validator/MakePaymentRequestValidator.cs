using JM.SOLID.Principles.Data;
using JM.SOLID.Principles.Types;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace JM.SOLID.Principles.Validator
{
    public class MakePaymentRequestValidator : AbstractValidator<MakePaymentRequest>, IPaymentRequestValidator
    {
        private readonly IUnitOfWork _accountRepo;

        public MakePaymentRequestValidator(IUnitOfWork accountRepo)
        {
            _accountRepo = accountRepo;

            RuleFor(r => r.Amount)
                .NotNull()
                .NotEmpty()
                ;

            RuleFor(r => r.CreditorAccountNumber)
                .NotEqual(x => x.DebtorAccountNumber)
                .WithMessage("The creditor account and debtor account should be differnt");

            RuleFor(r => r.CreditorAccountNumber)
                .NotNull()
                .NotEmpty()
                .Must(IsValidAccount)
                .WithMessage("Invalid account number: CreditorAccountNumber")
                ;

            RuleFor(r => r.DebtorAccountNumber)
                .NotNull()
                .NotEmpty()
                .Must(IsValidAccount)
                .WithMessage("Invalid account number: DebtorAccountNumber")
                ;

            RuleFor(r => r.PaymentDate)
                .NotNull()
                .NotEmpty()
                .Must(BeAValidDate)
                .WithMessage("Not a valid PaymentDate");

            RuleFor(r => r.PaymentScheme)
                 .IsInEnum()
                .WithMessage($"Not a valid {nameof(PaymentScheme)}");
        }

        private bool IsValidAccount(string accountNumber)
        {
            return _accountRepo.AccountDataStore.GetAccount(accountNumber) != null;
        }

        private bool BeAValidDate(DateTime date)
        {
            return date >= DateTime.MinValue && date <= DateTime.MaxValue;
        }

        public ValidationResult IsValid(MakePaymentRequest request)
        {
            return Validate(request);
        }
    }
}
