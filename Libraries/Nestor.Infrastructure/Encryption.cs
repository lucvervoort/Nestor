using System.Security.Cryptography;
using System.Text;

namespace Nestor.Infrastructure
{
    public class CryptoManager
    {
        #region Symmetric
        public static string EncryptSym(string data, out string key)
        {
            byte[] initializationVector = Encoding.ASCII.GetBytes("abcede0123456789");
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.GenerateKey();
                key = Convert.ToBase64String(aes.Key);

                //aes.Key = Encoding.UTF8.GetBytes(key);

                aes.IV = initializationVector;
                var symmetricEncryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream as Stream,
symmetricEncryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(cryptoStream as Stream))
                        {
                            streamWriter.Write(data);
                        }
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }
        public static string DecryptSym(string cipherText, string key)
        {
            byte[] initializationVector = Encoding.ASCII.GetBytes("abcede0123456789");
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = initializationVector;
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var memoryStream = new MemoryStream(buffer))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream as Stream,
decryptor, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(cryptoStream as Stream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        #endregion

        #region Asymmetric
        public static string Encrypt(string data, RSAParameters rsaParameters)
        {
            using (var rsaCryptoServiceProvider = new RSACryptoServiceProvider())
            {
                rsaCryptoServiceProvider.ImportParameters(rsaParameters);
                var byteData = Encoding.UTF8.GetBytes(data);
                var encryptedData = rsaCryptoServiceProvider.Encrypt(byteData, false);
                return Convert.ToBase64String(encryptedData);
            }
        }

        public static string EncryptS(string data, RSAParameters rsaParameters)
        {
            using (var rsaCryptoServiceProvider = new RSACryptoServiceProvider())
            {
                rsaCryptoServiceProvider.ImportParameters(rsaParameters);
                string localKey;
                var encryptedContent = EncryptSym(data, out localKey);
                var byteData = Encoding.UTF8.GetBytes(localKey);
                var encryptedData = rsaCryptoServiceProvider.Encrypt(byteData, false);
                // First 44 bytes are the encrypted pwd
                return Convert.ToBase64String(encryptedData) + encryptedContent;
            }
        }

        public static string Encrypt(string data, string xml)
        {
            using (var rsaCryptoServiceProvider = new RSACryptoServiceProvider())
            {
                string pwd;
                rsaCryptoServiceProvider.FromXmlString(xml);
                var encryptedContent = EncryptSym(data, out pwd);
                var byteData = Encoding.UTF8.GetBytes(pwd);
                var encryptedData = rsaCryptoServiceProvider.Encrypt(byteData, false);
                return Convert.ToBase64String(encryptedData) + "/" + encryptedContent;
            }
        }

        public static string Decrypt(string cipherText, RSAParameters rsaParameters)
        {
            using (var rsaCryptoServiceProvider = new RSACryptoServiceProvider())
            {
                var cipherDataAsByte = Convert.FromBase64String(cipherText);
                rsaCryptoServiceProvider.ImportParameters(rsaParameters);
                var encryptedData = rsaCryptoServiceProvider.Decrypt(cipherDataAsByte, false);
                return Encoding.UTF8.GetString(encryptedData);
            }
        }

        public static string Decrypt(string cipherText, string xml)
        {
            using (var rsaCryptoServiceProvider = new RSACryptoServiceProvider())
            {
                rsaCryptoServiceProvider.FromXmlString(xml);
                var cipherDataAsByte = Convert.FromBase64String(cipherText);
                var decryptedData = rsaCryptoServiceProvider.Decrypt(cipherDataAsByte, false);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }

        public static string Decrypt(string cipherText, Guid guid, string xml)
        {
            using (var rsaCryptoServiceProvider = new RSACryptoServiceProvider())
            {
                rsaCryptoServiceProvider.FromXmlString(xml);
                var cipherDataAsByte = Convert.FromBase64String(cipherText);
                var decryptedData = rsaCryptoServiceProvider.Decrypt(cipherDataAsByte, false);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }
        #endregion
    }
}
