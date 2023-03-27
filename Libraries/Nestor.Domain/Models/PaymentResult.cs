// https://paulstovell.com/x509certificate2/

namespace Nestor.Domain
{
    public class PaymentResult // will be sent NestrixPublic encrypted
    {
        public Guid PaymentGuid { get; set; }
        public bool Succes { get; set; }
        public Version? ProtocolVersion { get; set; }
        public string? Description { get; set; }
    }
}