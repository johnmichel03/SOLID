using JM.SOLID.Principles.Types;

namespace JM.SOLID.Principles.Services
{
    public interface IPaymentService
    {
        MakePaymentResult MakePayment(MakePaymentRequest request);
    }
}
