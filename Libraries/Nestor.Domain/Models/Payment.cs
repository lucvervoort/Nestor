// https://paulstovell.com/x509certificate2/

namespace Nestor.Domain
{
    public class Payment // will be sent NestrixPublic encrypted
    {
        public Guid PaymentGuid { get; set; }
        public string? Name { get; set; }
        public string? IBAN { get; set; }
        public string? BIC { get; set; }
        public bool StructuredDescription { get; set; }
        public string? Description { get; set; }
        public DateTime? Time { get; set; }
        public TimeSpan? ValidityPeriod { get; set; } // client requests to be deactivated automatically after this time lapse
        public Version? ProtocolVersion { get; set; } // for json payload: details Nestor stores concerning requestor - informational purposes only
        public string? Payload { get; set; } // json 
    }
}