namespace Nestor.Shared
{
    public class RegistrationRequestResult // will be sent RequestorPublic encrypted
    {
        public Guid RequestorGuid { get; set; }
        public Guid RegistrationGuid { get; set; }
        public string? NestorPublic { get; set; } // To send to Nestor
        public string? NestorSecret { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
