// https://paulstovell.com/x509certificate2/

namespace Nestor.Domain
{
    public class Registration // will be sent NestorPublic encrypted
    {
        public Guid RegistrationGuid { get; set; }
        public enum Type { Paid, Paying };
        public string? UniqueName { get; set; }
        public string? Description { get; set; }
        public DateTime? ActivationTime { get; set; }
        public TimeSpan? ValidityPeriod { get; set; } // client requests to be deactivated automatically after this time lapse
        public Version? ProtocolVersion { get; set; } // for json payload: details Nestor stores concerning requestor - informational purposes only
        public string? Payload { get; set; } // json 
    }
}