using JM.SOLID.Principles.Types;

namespace JM.SOLID.Principles.Services
{
    public sealed class PaymentFactory : IPaymentFactory
    {
        public IPaymentScheme GetPaymentScheme(PaymentScheme paymentScheme)
        {
            switch (paymentScheme)
            {
                case PaymentScheme.FasterPayments:
                    return new FasterPaymentScheme();
                case PaymentScheme.Bacs:
                    return new BacsPaymentScheme();
                case PaymentScheme.Chaps:
                    return new ChapsPaymentScheme();
                default:
                    return null;
            }

        }
    }
}
