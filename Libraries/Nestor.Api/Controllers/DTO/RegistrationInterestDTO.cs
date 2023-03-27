using System.ComponentModel.DataAnnotations;

namespace Nestor.Api.Controllers.DTO
{
    public class RegistrationInterestDTO
    {
        [Required]
        public Guid RegistrationRequestId { get; set; }

        [Required]
        public string PublicKey { get; set; }
    }
}