using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JM.SOLID.Principles.Types;

namespace JM.SOLID.Principles.Services
{
    internal class FasterPaymentScheme : IPaymentScheme
    {
        private bool IsAllowed(Account debtorAccount, MakePaymentRequest request)
        {
            return
                debtorAccount != null
                && request.PaymentScheme == PaymentScheme.FasterPayments
                && debtorAccount.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments)
                && debtorAccount.Balance > request.Amount;
        }

        public bool MakePayment(Account debtorAccount, MakePaymentRequest request)
        {
            if (IsAllowed(debtorAccount, request))
            {
                debtorAccount.Balance = debtorAccount.Balance - request.Amount;
                return true;
            }
            return false;
        }
    }
}



