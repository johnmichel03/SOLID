using JM.SOLID.Principles.Data;
using JM.SOLID.Principles.Types;
using JM.SOLID.Principles.Validator;

namespace JM.SOLID.Principles.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _accountRepo;
        private readonly IPaymentFactory _paymentFact;
        private readonly IPaymentRequestValidator _paymentValidator;

        public PaymentService(
            IUnitOfWork accountRepo, 
            IPaymentFactory paymentFact,
            IPaymentRequestValidator paymentValidator
            )
        {
            _accountRepo = accountRepo;
            _paymentFact = paymentFact;
            _paymentValidator = paymentValidator;
        }
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var result = new MakePaymentResult() { Success = false };
            //NOTE : We could move this validation logic to PaymentController class 
            var validator= _paymentValidator.IsValid(request);
            if (!validator.IsValid) return result;

            // To check the payment scheme and validations
            var paymentScheme = _paymentFact.GetPaymentScheme(request.PaymentScheme);
            if (paymentScheme == null) return result;

            var debtorAccount = _accountRepo.AccountDataStore.GetAccount(request.DebtorAccountNumber);
            if (paymentScheme.MakePayment(debtorAccount, request))
            {
                _accountRepo.AccountDataStore.UpdateAccount(debtorAccount);
                result.Success = true;
            }
            return result;
        }
    }
}
