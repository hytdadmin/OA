<%@ WebHandler Language="C#" Class="VisitManage" %>

using System;
using System.Web;
using Models;
using BLL;

public class VisitManage : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (!PageBase.IsLogin())
        {
            context.Response.Write("sessionError");
            return;
        }
        
        PageBase.ClearClientPageCache();

        string type = context.Request["type"];
        //参数
        if (string.IsNullOrEmpty(type) || (type != "add" && type != "show"))
        {
            context.Response.Write("");
            return;
        }
        if (type.Equals("add"))
        {
            string visitPage = context.Request["pageName"];
            visitPage = visitPage.IndexOf("?") > 0 ? visitPage.Substring(0, visitPage.IndexOf("?")) : visitPage;
            new VisitTbBLL().AddVisitTb(new VisitTb() { IsDel = 1, UserCode = PageBase.GetLoginCode(), VisitPage = visitPage, VisitTime = DateTime.Now });
        }
        else if (type.Equals("show"))
        {
            context.Response.Write(new VisitTbBLL().GetVisitCount(Check.GetInt32(Config.visitMin))); 
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}