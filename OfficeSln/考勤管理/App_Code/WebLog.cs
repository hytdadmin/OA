using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;
using HYTD.CAPlatform.Common;
using HYTD.CAPlatform.Model;
/// <summary>
///Log 的摘要说明
/// </summary>


public static class WebLog
{

    public static void WriteLog(string strlog,string strFileName)
    {
        string dir = HttpContext.Current.Server.MapPath("\\") + "Activelog\\"+DateTime.Now.ToString("yyyyMMdd")+"\\";
        //创建父目录
        if (!System.IO.Directory.Exists(dir))
        {
            System.IO.Directory.CreateDirectory(dir);
        }
        //操作文件
        string file = dir + "/" +strFileName+"_"+ DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
        if (!System.IO.File.Exists(file))
        {
            FileStream fs1 = new FileStream(file, FileMode.Create, FileAccess.Write);//创建写入文件 
            StreamWriter sw = new StreamWriter(fs1, Encoding.GetEncoding("GB2312"));
            sw.WriteLine(strlog);//开始写入值
            sw.Close();
            fs1.Close();
        }
        else
        {
            StreamWriter sw = new StreamWriter(file, true, System.Text.Encoding.Default);
            sw.WriteLine(strlog);
            sw.Close();
        }

    }
    public static void WriteCallCenterMYDLog(string strlog)
    {
        string dir = HttpContext.Current.Server.MapPath("\\") + "CallCenter\\Mydlog\\";
        //创建父目录
        if (!System.IO.Directory.Exists(dir))
        {
            System.IO.Directory.CreateDirectory(dir);
        }
        //操作文件
        string file = dir + "" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        if (!System.IO.File.Exists(file))
        {
            FileStream fs1 = new FileStream(file, FileMode.Create, FileAccess.Write);//创建写入文件 
            StreamWriter sw = new StreamWriter(fs1, Encoding.GetEncoding("GB2312"));
            sw.WriteLine(strlog);//开始写入值
            sw.Close();
            fs1.Close();
        }
        else
        {
            StreamWriter sw = new StreamWriter(file, true, System.Text.Encoding.Default);
            sw.WriteLine(strlog);
            sw.Close();
        }

    }
    public static void WriteCallCenterLog(string strlog)
    {
        string dir = HttpContext.Current.Server.MapPath("\\") + "CallCenter\\log\\";
        //创建父目录
        if (!System.IO.Directory.Exists(dir))
        {
            System.IO.Directory.CreateDirectory(dir);
        }
        //操作文件
        string file = dir + "" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        if (!System.IO.File.Exists(file))
        {
            FileStream fs1 = new FileStream(file, FileMode.Create, FileAccess.Write);//创建写入文件 
            StreamWriter sw = new StreamWriter(fs1, Encoding.GetEncoding("GB2312"));
            sw.WriteLine(strlog);//开始写入值
            sw.Close();
            fs1.Close();
        }
        else
        {
            StreamWriter sw = new StreamWriter(file, true, System.Text.Encoding.Default);
            sw.WriteLine(strlog);
            sw.Close();
        }

    }
}


