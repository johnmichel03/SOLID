using JM.SOLID.Principles.Types;

namespace JM.SOLID.Principles.Services
{
    public interface IPaymentFactory
    {
        IPaymentScheme GetPaymentScheme(PaymentScheme paymentScheme);  
    }
}
