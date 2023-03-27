//using System.Security.Cryptography;
//using System.Text;

using Nestor.Infrastructure;
using System.Security.Cryptography;

namespace EncryptionTest.Console.App
{
    internal class Program
    {
        // Regard en Ella Henderson
        // Golding miracle

        static void Main(string[] args)
        {
            // https://github.com/Dentrax/Z00bfuscator
            // Encrypt public key xml using symmetric key: https://learn.microsoft.com/en-us/dotnet/standard/security/how-to-encrypt-xml-elements-with-symmetric-keys
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider(4096);
            string publicKeyXML = rsaCryptoServiceProvider.ToXmlString(false);
            string privateAndPublicKeyXML = rsaCryptoServiceProvider.ToXmlString(true);
            //var publicKey = rsaCryptoServiceProvider.ExportParameters(false);
            //var publicAndPrivateKeys = rsaCryptoServiceProvider.ExportParameters(true);
            var reconstructedPublicKey = new RSACryptoServiceProvider();
            reconstructedPublicKey.FromXmlString(publicKeyXML);
            //reconstructedPublicKey.ImportParameters(publicKey);
            var reconstructedPrivateAndPublicKey = new RSACryptoServiceProvider();
            reconstructedPrivateAndPublicKey.FromXmlString(privateAndPublicKeyXML);
            //reconstructedPrivateAndPublicKey.ImportParameters(publicAndPrivateKeys);

            var cipherText = CryptoManager.EncryptS(@"{""RequestorGuid"":""bc90fff1-1e9c-4581-89c9-b1272a263bc4"",""RegistrationGuid"":""08388406-0586-4461-a1a5-143d0e44c868"",""NestorPublic"":""\u003CRSAKeyValue\u003E\u003CModulus\u003E3Fn3MW78fCcDkdxvopFSOD12WAXSlbyFRMDABQpalkOXhorA0UVYbnKmFRxXgTWumXfkFZFARRY6gnELg\u002BcsGyyXa2sR8QE2Ho5XzmQFwv\u002BfUjOWmWJzmMxSciqfQ87prRZ5I5ovzOgPu4SdrJpLCUAi9c17a2\u002BVh5QwEzimtG6zf5ek9b2L\u002BOUopTWf7OI1Ys4yiP71eOh03rghmv9TMTCs1Zon4LbHLMY7BBxAYkq4pdhKtaV8Z3qff7BJM7qfR4fnYhP9rsrJBXguToPm7hFbYZ3KxH5NyrXwRaPhGsB401GfZlPK/95\u002BV5MCgmIF9ReBhs7b5ZBYXTGRXNYblrdqpfGjCtCVL4aHIpyu8OhirT2plRialCwOaORWvbyecsluc2obKMKWMfzje/4Sj8Fg9PyxmVeG3ANYV4lwvp4n/FoVuiF329JKj3uI89lCHUMJkIWgm9u\u002BCKbCXKxGgRKFmCw1dSJxYrxiWPWMd9AN4Z/JI\u002BJXFR1TA\u002BfTZX3a8V5sYzGw4m/noY3nS5b7Wk7el\u002BrrB/R7PbqtabzLCg\u002BqVDtbBnUfCUM\u002BpRKw7ftTzBzH1XjHiJ59qa78USa30afVfUYbUXl8pn89IhI\u002BRxVtwQxo/C8bR6tDory2e6IIEktnquJtDFhyxmCCMu8jpd5x8CP3tWGVL/kHZN6RfdE=\u003C/Modulus\u003E\u003CExponent\u003EAQAB\u003C/Exponent\u003E\u003C/RSAKeyValue\u003E"",""NestorSecret"":null,""DateTime"":""2023-03-26T19:39:36.6645102+02:00""}", reconstructedPublicKey.ExportParameters(false)); // public only
            System.Console.WriteLine(cipherText);
            //var plainText = CryptoManager.Decrypt(cipherText, guid, reconstructedPrivateAndPublicKey.ExportParameters(true)); // public and private
            //System.Console.WriteLine(plainText);            
        }
    }
}