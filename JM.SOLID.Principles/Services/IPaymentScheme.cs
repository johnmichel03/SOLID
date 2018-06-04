using JM.SOLID.Principles.Types;

namespace JM.SOLID.Principles.Services
{
    public interface IPaymentScheme
    {
        //NOTE: It can be retrun only balance but it will be helpful to do any other account related custom debit.
        /// <summary>
        /// To deduct the payment amount from the debtor account.
        /// </summary>
        /// <param name="debtorAccount"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        bool MakePayment(Account debtorAccount, MakePaymentRequest request);

    }
}
