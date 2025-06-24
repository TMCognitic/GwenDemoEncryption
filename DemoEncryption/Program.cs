// See https://aka.ms/new-console-template for more information
using DemoEncryption;
using System.Security.Cryptography;
using System.Text;

string dataToEncrypt = "Le contenu à crypter";

byte[] publicKey;
byte[] keys;
byte[] cypher;

using (RsaCrypto crypto = new RsaCrypto())
{
    publicKey = crypto.ExportKeys(false);
    keys = crypto.ExportKeys(true);
}


using (RsaCrypto crypto = new RsaCrypto(publicKey))
{
    cypher = crypto.Encrypt(dataToEncrypt);
}

using (RsaCrypto crypto = new RsaCrypto(keys))
{
    Console.WriteLine(crypto.Decrypt(cypher));
}