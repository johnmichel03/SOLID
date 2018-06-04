using JM.SOLID.Principles.Data;
using JM.SOLID.Principles.Services;
using JM.SOLID.Principles.Types;
using JM.SOLID.Principles.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.SOLID.Principles.RequestHandler
{
    public class PaymentController
    {
        private readonly IPaymentService _paymentService;
        private readonly IUnitOfWork _accountRepo;

        public PaymentController(IPaymentService paymentService, IUnitOfWork accountRepo)
        {
            _paymentService = paymentService;
            _accountRepo = accountRepo;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var result = _paymentService.MakePayment(request);
            if (result.Success)
            {
                _accountRepo.Commit();
                return result;
            }
            return new MakePaymentResult() { Success = false };
        }

    }
}
