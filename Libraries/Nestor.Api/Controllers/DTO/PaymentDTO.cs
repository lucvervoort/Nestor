using System.ComponentModel.DataAnnotations;

namespace Nestor.Api.Controllers.DTO
{
    public class PaymentDTO
    {
        [Required]
        public Guid PaymentId { get; set; }

        [Required]
        public string Encrypted { get; set; }
    }
}