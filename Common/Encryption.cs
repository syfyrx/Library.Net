using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Library.Net.Common
{
    public class Encryption
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的MD5码</returns>
        public static string MD5Encryption(string str)
        {
            MD5 md5 = MD5.Create();//创建一个MD5实例
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);//将字符串转换成byte[]
            byte[] encryptionBytes = md5.ComputeHash(bytes);//对byte[]进行加密
            string result = "";
            for (int i = 0; i < encryptionBytes.Length; i++)
            {
                result += encryptionBytes[i].ToString("X").Length == 1 ? "0" + encryptionBytes[i].ToString("X") : encryptionBytes[i].ToString("X");//X的作用：变成16进制；如果长度是1，在前面加0
            }
            return result;
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="key">增大加密强度</param>
        /// <returns>加密后的MD5码</returns>
        public static string MD5Encryption(string str, string key)
        {
            string result = "";
            if (key.Length > 0)
            {
                result = key + MD5Encryption(key + MD5Encryption(str));
            }
            else
            {
                result = MD5Encryption(str);
            }
            return result;
        }
    }
}
