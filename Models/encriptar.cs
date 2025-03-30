using System.Security.Cryptography;
using System.Text;


namespace BRIAMSHOP.Models
{
    public class encriptar
    {
        public string Encrypt (string message)
        {
            string hash = "coding con c";
            byte[]data=UTF8Encoding.UTF8.GetBytes (message);
            MD5 md5 = MD5.Create();
            TripleDES tripldes = TripleDES.Create();
            tripldes.Key = md5.ComputeHash (UTF8Encoding.UTF8.GetBytes(hash));
            tripldes.Mode = CipherMode.ECB;
            ICryptoTransform transform = tripldes.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String (result);
        }
    }
}
  