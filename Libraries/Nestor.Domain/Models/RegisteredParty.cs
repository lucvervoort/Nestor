// https://paulstovell.com/x509certificate2/

namespace Nestor.Domain.Models
{
    public class RegisteredParty
    {
        public enum Type { Paid, Paying };
        public Guid Guid { get; set; }
        public string? UniqueName { get; set; }
        public string? Description { get; set; }
        public DateTime? ActivationTime { get; set; }
        public DateTime? DeactivationTime { get; set; }
    }
}