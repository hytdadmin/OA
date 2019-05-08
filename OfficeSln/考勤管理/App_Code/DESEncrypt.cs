using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Active.Tools
{
    public class DESEncrypt
    {
        private static String strDesKey = "abcdefgh";//加密所需8位密匙  
        public static string DesKey {
            get{return strDesKey;}
            set{strDesKey=value;}
        }
        //加密的代码:  
        /// <summary>  
        /// DES加密  
        /// </summary>  
        /// <param name="str">要加密字符串</param>  
        /// <returns>返回加密后字符串</returns>  
        public static String Encrypt_DES(String str)
        {
            System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();
            Byte[] inputByteArray = System.Text.Encoding.Default.GetBytes(str);
            des.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strDesKey, "md5").Substring(0, 8));//加密对象的密钥      
            des.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strDesKey, "md5").Substring(0, 8));//加密对象的偏移量，此两个值重要不能修改      
            System.IO.MemoryStream ms = new System.IO.MemoryStream(); System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, des.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Byte b in ms.ToArray())
                sb.AppendFormat("{0:X2}", b);
            return sb.ToString();
        }
        /// <summary>  
        /// DES解密  
        /// </summary>  
        /// <param name="str">要解密字符串</param>  
        /// <returns>返回解密后字符串</returns>  
        public static String Decrypt_DES(String str)
        {
            System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();
            Int32 x;
            Byte[] inputByteArray = new Byte[str.Length / 2];
            for (x = 0; x < str.Length / 2; x++)
                inputByteArray[x] = (Byte)(Convert.ToInt32(str.Substring(x * 2, 2), 16));
            des.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strDesKey, "md5").Substring(0, 8));
            des.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strDesKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream(); System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, des.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        /// <summary>  
        /// DES解密  
        /// </summary>  
        /// <param name="str">要解密字符串</param>  
        /// <returns>返回解密后字符串</returns>  
        public static String Decrypt_DES_Old(String str)
        {
            System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();
            Int32 x;
            Byte[] inputByteArray = new Byte[str.Length / 2];
            for (x = 0; x < str.Length / 2; x++)
                inputByteArray[x] = (Byte)(Convert.ToInt32(str.Substring(x * 2, 2), 16));
            des.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(strDesKey);
            des.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(strDesKey);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(); System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, des.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }

    }
}
