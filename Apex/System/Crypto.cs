namespace Apex.System
{
    public class Crypto
    {
        public Crypto()
        {
            throw new global::System.NotImplementedException("Crypto");
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("Crypto.Clone");
        }

        public static Blob Decrypt(string algorithmName, Blob secretKey, Blob initializationVector, Blob encryptedData)
        {
            throw new global::System.NotImplementedException("Crypto.Decrypt");
        }

        public static Blob DecryptWithManagedIV(string algorithmName, Blob secretKey, Blob encryptedData)
        {
            throw new global::System.NotImplementedException("Crypto.DecryptWithManagedIV");
        }

        public static Blob Encrypt(string algorithmName, Blob secretKey, Blob initializationVector, Blob clearData)
        {
            throw new global::System.NotImplementedException("Crypto.Encrypt");
        }

        public static Blob EncryptWithManagedIV(string algorithmName, Blob secretKey, Blob clearData)
        {
            throw new global::System.NotImplementedException("Crypto.EncryptWithManagedIV");
        }

        public static Blob GenerateAesKey(int size)
        {
            throw new global::System.NotImplementedException("Crypto.GenerateAesKey");
        }

        public static Blob GenerateDigest(string algorithmName, Blob input)
        {
            throw new global::System.NotImplementedException("Crypto.GenerateDigest");
        }

        public static Blob GenerateMac(string algorithmName, Blob input, Blob privateKey)
        {
            throw new global::System.NotImplementedException("Crypto.GenerateMac");
        }

        public static int GetRandomInteger()
        {
            throw new global::System.NotImplementedException("Crypto.GetRandomInteger");
        }

        public static long GetRandomLong()
        {
            throw new global::System.NotImplementedException("Crypto.GetRandomLong");
        }

        public static Blob Sign(string algorithmName, Blob input, Blob privateKey)
        {
            throw new global::System.NotImplementedException("Crypto.Sign");
        }

        public static Blob SignWithCertificate(string algorithmName, Blob input, string certDevName)
        {
            throw new global::System.NotImplementedException("Crypto.SignWithCertificate");
        }

        public static void SignXml(string algorithmName, Dom.XmlNode node, string idAttributeName, string certDevName)
        {
            throw new global::System.NotImplementedException("Crypto.SignXml");
        }

        public static void SignXml(string algorithmName, Dom.XmlNode node, string idAttributeName, string certDevName,
            Dom.XmlNode refChild)
        {
            throw new global::System.NotImplementedException("Crypto.SignXml");
        }
    }
}