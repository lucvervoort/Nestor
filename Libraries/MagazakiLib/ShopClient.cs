using Flurl;
using Flurl.Http;
using Nestor.Shared;
using System.Security.Cryptography;

namespace ShopLib
{

    public class ShopClient
    {
        public static string BasePath { get; set; } = "https://localhost:7237/";
        public async Task<Guid> EnregisterRegistrationInterest()
        {
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider(4096); // set generally, not in config file
            var publicKey = rsaCryptoServiceProvider.ToXmlString(false);
            var dto = new RegistrationInterestDTO { RegistrationRequestId = Guid.NewGuid(), PublicKey = publicKey };

            var p = BasePath.AppendPathSegment("/Registration/AnnounceRegistrationInterest");
            var result = await p.PostJsonAsync(dto).ReceiveJson<RegistrationRequestResult>();

            // Check GUID
            // Check arrival time, based on dictionary and returned DateTime
            return result.RegistrationGuid;
        }

        // Versie
        // Class
        public bool PerformRegistration(Guid registrationId, string data)
        {
            return true;
        }
    }
}