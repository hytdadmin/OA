using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Security;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace HYTD.Common
{
    /// <summary>
    /// 加密类
    /// </summary>
    public class Encrypt
    {
        //密钥
        private static string sKey = "EFD8524DF2174C17909F26A1AD18DCC1";
        private static string sKey1 = "A3F2569E6SJEAWBCJOTY45DYQWF88H1Y";
        //矢量，矢量可以为空
        private static string sIV = "qdCy6X+aKLw=";
        private static string iv = "023e5JK89UT2/450";

        #region ECB加密、解密
        /// <summary>
        /// 加密（可用在Cookie的加密）
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string EncryptString(string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
                ICryptoTransform ct;
                MemoryStream ms;
                CryptoStream cs;
                byte[] byt;
                mCSP.Key = Convert.FromBase64String(sKey1);
                mCSP.IV = Convert.FromBase64String(sIV);
                //指定加密的运算模式
                mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
                //获取或设置加密算法的填充模式
                mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);
                byt = Encoding.UTF8.GetBytes(value);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static string DecrypString(string source)
        {
            if (!String.IsNullOrEmpty(source))
            {
                try
                {
                    SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
                    byte[] bytIn = Convert.FromBase64String(source);
                    MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
                    mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
                    //获取或设置加密算法的填充模式
                    mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                    mCSP.Key = Convert.FromBase64String(sKey1);
                    mCSP.IV = Convert.FromBase64String(sIV);
                    ICryptoTransform encrypto = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);
                    CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                    StreamReader sr = new StreamReader(cs);
                    return sr.ReadToEnd();
                }
                catch
                {
                    return "";
                }
            }
            else
            {
                return "";

            }
        }
        #endregion


        #region AES加密、解密
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainText">原文</param>
        /// <returns>密文</returns>
        public static string ToAESEncrypt(string plainText)
        {
            return ToAESEncrypt(plainText, sKey, iv);
        }

        /// <summary>
        ///  AES加密
        /// </summary>
        /// <param name="plainText">原文</param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string ToAESEncrypt(string plainText, string key, string iv)
        {
            if (String.IsNullOrEmpty(plainText))
                return String.Empty;

            using (SymmetricAlgorithm aes = Rijndael.Create())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组      
                //设置密钥及密钥向量  
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(iv);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        var result = Convert.ToBase64String(ms.ToArray());
                        return result;
                    }
                }
            }
        }


        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="plainText">原文</param>
        /// <returns>密文</returns>
        public static string ToAESDecrypt(string plainText)
        {
            return ToAESDecrypt(plainText, sKey, iv);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">原文</param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns>密文</returns>
        public static string ToAESDecrypt(string cipherText, string key, string iv)
        {
            if (String.IsNullOrEmpty(cipherText))
                return String.Empty;
            if (cipherText.Length < 4 || (cipherText.Length % 4) != 0)
                return String.Empty;

            byte[] bytIn = Convert.FromBase64String(cipherText);

            using (SymmetricAlgorithm aes = Rijndael.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(iv);
                byte[] decryptBytes = new byte[cipherText.Length];

                try
                {
                    using (MemoryStream ms = new MemoryStream(bytIn))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                var result = sr.ReadToEnd();
                                return result;
                            }
                        }
                    }
                }
                catch (CryptographicException)
                {
                    return String.Empty;
                }
            }
        }
    }
        #endregion
}
