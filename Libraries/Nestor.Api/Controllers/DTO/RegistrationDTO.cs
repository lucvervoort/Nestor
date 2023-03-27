using System.ComponentModel.DataAnnotations;

namespace Nestor.Api.Controllers.DTO
{
    public class RegistrationDTO
    {
        [Required]
        public Guid RegistrationId { get; set; }

        [Required]
        public string Encrypted { get; set; }
    }
}