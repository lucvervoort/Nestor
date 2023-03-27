using Nestor.Shared;

namespace Nestor.Api.Controllers
{
    public partial class RegistrationController
    {
        private class RegistrationRequestAnswerWrapper
        {
            public RegistrationRequestResult? Answer { get; set; }
            public string? PrivateKey { get; set; }
        }
    }

    public partial class PaymentController
    {
        private class PaymentRequestAnswerWrapper
        {
            public PaymentRequestResult? Answer { get; set; }
            public string? PrivateKey { get; set; }
        }
    }
}