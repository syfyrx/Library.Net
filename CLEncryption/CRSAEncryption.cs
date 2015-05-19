using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Library.Net.CLEncryption
{
    /// <summary>
    /// RSA类
    /// </summary>
    public class CRSAEncryption
    {
        //private static string publicKey = @"<RSAKeyValue><Modulus>ytp2VUoQ3nxCbunNEZBu8IzaF0B5iDNq0YciwM9l5O50LE74t3jjwd+jRlK7B+wysfTkjbb5PZ/mwmC0+WmA5J8egkTbcDLQvwfDZBbgnKVOj71O58lrq7Wmxih32DrsruI5o5XdxO2BLXHG1DsAtZrVDCOUgm7fKWnpPh9v9J8=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        //private static string privateKey = @"<RSAKeyValue><Modulus>ytp2VUoQ3nxCbunNEZBu8IzaF0B5iDNq0YciwM9l5O50LE74t3jjwd+jRlK7B+wysfTkjbb5PZ/mwmC0+WmA5J8egkTbcDLQvwfDZBbgnKVOj71O58lrq7Wmxih32DrsruI5o5XdxO2BLXHG1DsAtZrVDCOUgm7fKWnpPh9v9J8=</Modulus><Exponent>AQAB</Exponent><P>/3WKrQC/C2AEJzQCytDFImg1ObJiHxZcUJrWGV7QYKfuIuecLzX+Ye+5Zia+IvVqtF3sHGmc2yTCDezxx5Mv0w==</P><Q>y0hoh6LW06H5+4gNQOFMKuAD0R6KaEJEyM8mJdR0+7yY7OW6Q6VPeAui4n2rM1VBdEP919klepBVeSmupTH0hQ==</Q><DP>YscTeTPJq19UG8iLr9qr71L2CbpFIJon1e8ZDDRSRJ2KgiqmhMbO9xWXzbz7Vk8pFFcg3hekVERRWMse7jmmiw==</DP><DQ>Ph6rar91PIjj9lx2CFyQxQ1dvTrC+uc9U8wRkT/iW8cfSE1PZTqQFVQg+2uBtJcaAGiCmsJNtK2EoRj+uJaSbQ==</DQ><InverseQ>NDLH9k0DkNMR59x+RO+7cKB/u0mNkUqMiIkyKdsS4nhVlymBFoIh8kr46MXpCD4Te+cMaC6Yq7P5frc1WMdjrg==</InverseQ><D>LFO6wf5yWzvKBJSglDL8myIcUjJrnECoGesuw/VOLc2Ro1EKdoU7N9VXx3kyl5OcrFh4TSNrqXS0p0scoNfWKzr/cqO3oMTpjCtAA7SB0j9E+1L7xpj0sj/b5L9Nlc588D7CHcUFt+GTP1Z+x978jGnhFJpEgfa9w7TOE1nEi8k=</D></RSAKeyValue>";
        private static string publicKey = @"6,2,0,0,0,164,0,0,82,83,65,49,0,4,0,0,1,0,1,0,135,215,233,227,205,162,143,216,60,249,15,251,135,1,94,88,189,65,56,12,95,76,46,115,88,159,117,5,218,15,234,44,167,58,155,242,189,188,230,38,211,234,13,158,102,222,129,229,15,212,131,107,113,157,51,150,152,152,101,142,59,81,119,107,47,213,110,228,57,44,250,121,167,236,164,203,156,175,64,149,75,14,54,113,232,255,31,156,59,93,51,201,66,192,191,121,18,205,136,115,73,110,144,148,129,86,181,97,209,90,62,56,152,183,252,165,208,117,190,150,242,236,255,120,175,188,233,203";
        private static string privateKey = @"7,2,0,0,0,164,0,0,82,83,65,50,0,4,0,0,1,0,1,0,135,215,233,227,205,162,143,216,60,249,15,251,135,1,94,88,189,65,56,12,95,76,46,115,88,159,117,5,218,15,234,44,167,58,155,242,189,188,230,38,211,234,13,158,102,222,129,229,15,212,131,107,113,157,51,150,152,152,101,142,59,81,119,107,47,213,110,228,57,44,250,121,167,236,164,203,156,175,64,149,75,14,54,113,232,255,31,156,59,93,51,201,66,192,191,121,18,205,136,115,73,110,144,148,129,86,181,97,209,90,62,56,152,183,252,165,208,117,190,150,242,236,255,120,175,188,233,203,171,45,105,101,179,11,99,174,26,1,10,228,17,53,141,91,131,139,120,25,220,165,49,139,223,88,164,8,170,178,213,67,196,181,173,7,255,32,31,13,63,175,10,129,117,106,157,111,40,49,21,240,52,4,211,77,163,29,111,110,177,201,236,245,149,201,133,173,201,14,190,0,226,207,255,34,199,162,4,122,71,230,126,210,106,203,118,96,60,76,34,171,229,10,116,9,109,110,99,184,83,181,16,221,165,213,110,166,173,19,213,197,29,82,109,178,33,200,183,48,14,14,20,243,38,86,68,212,163,125,195,33,232,232,244,238,161,149,36,107,254,155,7,61,208,168,24,212,63,230,112,49,159,117,63,200,122,120,164,186,192,8,23,109,184,3,218,48,135,250,116,41,195,179,3,23,18,51,233,31,236,232,207,37,84,232,125,156,67,154,155,59,217,104,64,129,49,155,130,9,86,157,1,245,72,231,215,246,42,38,164,204,4,168,182,128,82,170,62,68,3,242,15,160,224,97,92,33,246,116,92,150,140,115,173,153,36,95,105,180,105,49,106,132,98,177,33,103,233,46,132,114,135,251,241,174,179,96,232,123,179,67,230,76,76,208,193,115,228,254,220,186,152,164,8,105,163,16,188,40,110,71,223,140,224,69,208,6,244,67,44,193,146,66,206,121,70,234,16,32,24,147,248,171,126,50,177,158,64,145,18,253,201,224,217,21,66,104,31,96,185,241,73,133,12,81,53,245,183,26,202,233,169,212,245,35,110,247,247,193,54,136,249,125,53,186,68,226,38,93,5,251,159,188,29,121,57,20,1,12,113,212,210,28,167,238,98,74,206,249,199,195,136,164,148,184,53,243,2,114,224,200,61,231,56,195,145,210,248,36,112,58,241,64,231,161,246,122,20,234,27,211,10,103,213,192,243,170,246,5,255,63,90,195,22,245,102,104,237,140,18,191,79,187,127,22,238,160,64,143,18,176,223,244,44,182,248,104,8,50,179,31,211,148,147,215,187,197";

        #region RSA的密钥产生
        /// <summary>
        /// RSA 的密钥产生（产生私钥 和公钥 ）
        /// </summary>
        public static void RSAKey()
        {
            try
            {
                byte[] bys;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                //privateKey = rsa.ToXmlString(true);
                //publicKey = rsa.ToXmlString(false);
                bys = rsa.ExportCspBlob(true);
                foreach (byte item in bys)
                {
                    if (privateKey != "")
                        privateKey += ",";
                    privateKey += item.ToString();
                }
                bys = rsa.ExportCspBlob(false);
                foreach (byte item in bys)
                {
                    if (publicKey != "")
                        publicKey += ",";
                    publicKey += item.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSAEncrypt(byte[] content)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                byte[] cipherbytes;
                //rsa.FromXmlString(publicKey);
                string[] strs = publicKey.Split(',');
                byte[] bys = new byte[strs.Length];
                for (int i = 0; i < strs.Length; i++)
                {
                    bys[i] = Convert.ToByte(strs[i]);
                }
                rsa.ImportCspBlob(bys);
                cipherbytes = rsa.Encrypt(content, false);
                return Convert.ToBase64String(cipherbytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privatekey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] RSADecrypt(string content)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                byte[] cipherbytes;
                //rsa.FromXmlString(privateKey);
                string[] strs = privateKey.Split(',');
                byte[] bys = new byte[strs.Length];
                for (int i = 0; i < strs.Length; i++)
                {
                    bys[i] = Convert.ToByte(strs[i]);
                }
                rsa.ImportCspBlob(bys);
                cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);
                return cipherbytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
