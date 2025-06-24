using System.Security.Cryptography;
using System.Text;

namespace DemoEncryption
{
    public class RsaCrypto : IDisposable
    {
        private readonly RSACryptoServiceProvider _rsaCrypto;

        public RsaCrypto(int keySize = 2048)
        {
            _rsaCrypto = new RSACryptoServiceProvider(keySize);
        }

        public RsaCrypto(byte[] keys)
        {
            _rsaCrypto = new RSACryptoServiceProvider();
            _rsaCrypto.ImportCspBlob(keys);
        }

        public byte[] ExportKeys(bool includePrivateKey)
        {
            return _rsaCrypto.ExportCspBlob(includePrivateKey);
        }

        public byte[] Encrypt(string dataToEncrypt)
        {
            byte[] stringInByteArray = Encoding.Default.GetBytes(dataToEncrypt);
            return _rsaCrypto.Encrypt(stringInByteArray, true);
        }

        public string Decrypt(byte[] cypher)
        {
            if(_rsaCrypto.PublicOnly)
            {
                throw new InvalidOperationException("The private key is needed for decrypt");
            }

            byte[] decryptedValue = _rsaCrypto.Decrypt(cypher, true);
            return Encoding.Default.GetString(decryptedValue);
        }

        public void Dispose()
        {
            _rsaCrypto.Dispose();
        }
    }
}
