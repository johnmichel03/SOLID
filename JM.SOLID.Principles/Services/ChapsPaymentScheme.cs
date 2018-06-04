using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JM.SOLID.Principles.Types;

namespace JM.SOLID.Principles.Services
{
    internal class ChapsPaymentScheme : IPaymentScheme
    {
        private bool IsAllowed(Account debtorAccount, MakePaymentRequest request)
        {
            return
                   debtorAccount != null
                && request.PaymentScheme == PaymentScheme.Chaps
                && debtorAccount.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps)
                && debtorAccount.Status == AccountStatus.Live;
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



