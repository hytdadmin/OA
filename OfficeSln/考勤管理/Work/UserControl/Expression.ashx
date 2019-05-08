<%@ WebHandler Language="C#" Class="Expression" %>

using System;
using System.Web;

/// <summary>
/// 返回表情框、文本替换为表情
/// </summary>
public class Expression : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        string type = context.Request["PicType"];
        //参数
        if (string.IsNullOrEmpty(type)||(type!="getPic"&&type!="showPic"))
        {
            context.Response.Write("");
            return;
        }
        string str = string.Empty;
        if (type == "getPic")
            str=PageBase.GetExpressionHtml();
        else if (type == "showPic")
            str=PageBase.ShowExpressionHtml(context.Request["picStr"]);
        context.Response.Write(str);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}