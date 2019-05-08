using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace HYTD.CAPlatform.Common
{
   public class caLog
    {
        /// <summary>
        /// 保存服务器跟踪错误日志
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="Operator"></param>
        /// <param name="Message"></param>
        /// <param name="TargetSite"></param>
        /// <param name="Source"></param>
        /// <param name="StackTrace"></param>
        public static void WriteTraceLog(string strModel, string strMessage)
        {
            string strPath = HttpContext.Current.Server.MapPath("\\") + "log\\";
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            string fileName = strPath + "Log" + DateTime.Now.ToString("yyyyMMddHH") + ".txt";
            StringBuilder sbErrorMsg = new StringBuilder();
            sbErrorMsg.Append(Environment.NewLine);
            sbErrorMsg.Append(DateTime.Now.ToString() + "--" + strModel + "--" + strMessage + Environment.NewLine);
            try
            {
                StreamWriter sw = File.AppendText(fileName);
                sw.WriteLine(sbErrorMsg.ToString());
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            { }
        }
    }
}
