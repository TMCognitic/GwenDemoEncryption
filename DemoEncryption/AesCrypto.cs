using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DemoEncryption
{
    public class AesCrypto
    {
        private readonly byte[] _vector;
        private readonly byte[] _key;

        public byte[] IV
        {
            get
            {
                return _vector;
            }
        }

        public byte[] Key
        {
            get
            {
                return _key;
            }
        }

        public AesCrypto()
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 128;
                aes.GenerateIV();
                aes.GenerateKey();
                _vector = aes.IV;
                _key = aes.Key;
            }
        }

        public AesCrypto(byte[] vector, byte[] key)
        {
            _vector = vector;
            _key = key;
        }

        public byte[] Encrypt(string value)
        {
            using (Aes aes = Aes.Create())
            {
                aes.IV = IV;
                aes.Key = Key;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cryptoStream, Encoding.Default))
                        {
                            sw.Write(value);
                        }
                    }

                    return ms.ToArray();
                }
            }
        }

        public string Decrypt(byte[] cypher)
        {
            using (Aes aes = Aes.Create())
            {
                aes.IV = IV;
                aes.Key = Key;

                using (MemoryStream ms = new MemoryStream(cypher))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cryptoStream, Encoding.Default))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
