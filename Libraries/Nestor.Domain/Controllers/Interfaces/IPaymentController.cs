using Nestor.Shared;

namespace Nestor.Domain.Controllers.Interfaces
{
    public interface IPaymentController
    {
        PaymentRequestResult AnnouncePaymentInterest(PaymentRequest request);
        PaymentResult ExecutePayment(Payment payment);
    }
}