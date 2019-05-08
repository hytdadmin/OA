using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYTD.Common
{
    public class ResponseMessage
    {

        public static string MsgStructCallBackFunc(string statusCode, string msg, string navTabId, string callbackType, string callbackFunc)
        {
            StringBuilder sbd = new StringBuilder();
            sbd.Append("{");
            sbd.Append("\"statusCode\":\"" + statusCode + "\",");
            sbd.Append("\"message\":\"" + msg + "\",");
            sbd.Append("\"navTabId\":\"" + navTabId + "\",");//要刷新的tabid,不能为当前页
            sbd.Append("\"rel\":\"\",");
            sbd.Append("\"callbackType\":\"" + callbackType + "\",");//closeCurrent关闭当前tab页，不关闭不需要写
            sbd.Append("\"callbackFunc\":\"" + callbackFunc + "\",");    //回调js函数
            sbd.Append("\"forwardUrl\":\"\",");
            sbd.Append("\"confirmMsg\":\"\"");
            sbd.Append("}");
            return sbd.ToString();

        }


        /// <summary>
        /// 返回消息结构
        /// </summary>
        /// <param name="statusCode">消息状态码 200正常 300错误</param>
        /// <param name="msg">消息正文</param>
        /// <param name="navTabId">要刷新的tabid,不能为当前页</param>
        /// <param name="callbackType">closeCurrent关闭当前tab页，不关闭不需要写</param>
        public static string MsgStruct(string statusCode,string msg,string navTabId,string callbackType)
        {
            StringBuilder sbd=new StringBuilder();
            sbd.Append("{");
            sbd.Append("\"statusCode\":\"" + statusCode + "\",");
            sbd.Append("\"message\":\"" + msg + "\",");
            sbd.Append("\"navTabId\":\"" + navTabId + "\",");//要刷新的tabid,不能为当前页
            sbd.Append("\"rel\":\"\",");
            sbd.Append("\"callbackType\":\"" + callbackType + "\",");//closeCurrent关闭当前tab页，不关闭不需要写
            sbd.Append("\"forwardUrl\":\"\",");
            sbd.Append("\"confirmMsg\":\"\"");
            sbd.Append("}");
            return sbd.ToString();
        }

        public static string MsgStruct(string statusCode, string msg, string navTabId, string callbackType, string httpUrl)
        {
            StringBuilder sbd = new StringBuilder();
            sbd.Append("{");
            sbd.Append("\"statusCode\":\"" + statusCode + "\",");
            sbd.Append("\"message\":\"" + msg + "\",");
            sbd.Append("\"navTabId\":\"" + navTabId + "\",");//要刷新的tabid,不能为当前页
            sbd.Append("\"rel\":\"\",");
            sbd.Append("\"callbackType\":\"" + callbackType + "\",");//closeCurrent关闭当前tab页，不关闭不需要写
            sbd.Append("\"httpUrl\":\"" + httpUrl + "\",");
            sbd.Append("\"confirmMsg\":\"\"");
            sbd.Append("}");
            return sbd.ToString();
        }

        public static string MsgStruct(string statusCode, string msg, string navTabId, string callbackType, string httpUrl,string fileName)
        {
            StringBuilder sbd = new StringBuilder();
            sbd.Append("{");
            sbd.Append("\"statusCode\":\"" + statusCode + "\",");
            sbd.Append("\"message\":\"" + msg + "\",");
            sbd.Append("\"navTabId\":\"" + navTabId + "\",");//要刷新的tabid,不能为当前页
            sbd.Append("\"rel\":\"\",");
            sbd.Append("\"callbackType\":\"" + callbackType + "\",");//closeCurrent关闭当前tab页，不关闭不需要写
            sbd.Append("\"httpUrl\":\"" + httpUrl + "\",");
            sbd.Append("\"fileName\":\"" + fileName + "\",");
            sbd.Append("\"confirmMsg\":\"\"");
            sbd.Append("}");
            return sbd.ToString();
        }
        public static string MsgStruct(string statusCode, string msg, string navTabId, string callbackType, string httpUrl, string strUrl, string fileName)
        {
            StringBuilder sbd = new StringBuilder();
            sbd.Append("{");
            sbd.Append("\"statusCode\":\"" + statusCode + "\",");
            sbd.Append("\"message\":\"" + msg + "\",");
            sbd.Append("\"navTabId\":\"" + navTabId + "\",");//要刷新的tabid,不能为当前页
            sbd.Append("\"rel\":\"\",");
            sbd.Append("\"callbackType\":\"" + callbackType + "\",");//closeCurrent关闭当前tab页，不关闭不需要写
            sbd.Append("\"httpUrl\":\"" + httpUrl + "\",");
            sbd.Append("\"strUrl\":\"" + strUrl + "\",");
            sbd.Append("\"fileName\":\"" + fileName + "\",");
            sbd.Append("\"confirmMsg\":\"\"");
            sbd.Append("}");
            return sbd.ToString();
        }
    }
}
