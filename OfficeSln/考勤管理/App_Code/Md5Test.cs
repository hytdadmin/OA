using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
///Md5Test 的摘要说明
/// </summary>
public class Md5Test
{
    /// <summary>
    /// 把字符串变成一个MD5字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns>string</returns>
    public static string Md5str(string str)
    {
        //创建一个可变字符串
        StringBuilder sb = new StringBuilder();
        MD5 md5 = MD5.Create();//创建一个MD5
        //把字符串先变成一个任意长度字节数组，然后在把可变字节数组变成一个16位的字节数组
        byte[] bts = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
        //变量每一个字节然后追加组成一个新的32位字节的字符串
        for (int i = 0; i < bts.Length; i++)
        {
            sb.Append(bts[i].ToString("x2"));
        }
        return sb.ToString();
    }
    /// <summary>
    /// 传入一个文件路径，验证路径是否存在，然后计算文件的Md5值
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string Md5Path(string path)
    {
        if (!File.Exists(path))
        {
            throw new Exception("路径不存在！");
        }
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            MD5 md5 = MD5.Create();
            byte[] bt = md5.ComputeHash(fs);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bt.Length; i++)
            {
                sb.Append(bt[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}