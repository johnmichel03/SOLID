using JM.SOLID.Principles.Data;
using JM.SOLID.Principles.Types;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.SOLID.Principles.Validator
{
    public class AccountValidator : AbstractValidator<Account>
    {
        private readonly IUnitOfWork _accountRepo;
        public AccountValidator(IUnitOfWork accountRepo)
        {
            _accountRepo = accountRepo;

            RuleFor(r => r.AccountNumber)
                .NotNull()
                .NotEmpty()
                .Must(IsValidAccount)
                .WithMessage("Not a valid account number")
                ;

            RuleFor(r => r.Balance)
                .NotNull()
                .NotEmpty()
                ;

            RuleFor(r => r.Status)
                .IsInEnum()
               .WithMessage($"Not a valid {nameof(AccountStatus)} enum")
               ;

            RuleFor(r => r.AllowedPaymentSchemes)
                .IsInEnum()
                .WithMessage($"Not a valid {nameof(AllowedPaymentSchemes)} enum")
                ;
        }

        bool IsValidAccount(string accountNumber)
        {
            return _accountRepo.AccountDataStore.GetAccount(accountNumber) != null;
        }
    }
}
