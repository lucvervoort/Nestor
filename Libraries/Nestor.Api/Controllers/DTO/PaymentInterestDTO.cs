using System.ComponentModel.DataAnnotations;

namespace Nestor.Api.Controllers.DTO
{
    public class PaymentInterestDTO
    {
        [Required]
        public Guid PaymentRequestId { get; set; }

        [Required]
        public string PublicKey { get; set; }
    }
}