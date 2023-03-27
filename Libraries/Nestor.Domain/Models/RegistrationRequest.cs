// https://paulstovell.com/x509certificate2/

namespace Nestor.Domain
{
    public class RegistrationRequest // on registration url - registration controller
    {
        public Guid Guid { get; set; }
        public DateTime? DateTime { get; set; }
        public string? RequestorPublic { get; set; } // To send to Requestor
    }
}