<%@ WebHandler Language="C#" Class="BulletinUC" %>

using System;
using System.Web;
using System.Web.SessionState;
using BLL;
using System.Data;
using System.Text;
using Models;
using System.Collections.Generic;

public class BulletinUC : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!PageBase.IsLogin())
        {
            context.Response.Write("sessionError");
            return;
        }
        PageBase.ClearClientPageCache();
        context.Response.Write(ResultStr()); ;
    }

    private string ResultStr()
    {
        StringBuilder sb = new StringBuilder();
        List<Bulletin> list = new BLL.BulletinBLL().GetBulletinListTop(5);
        sb.AppendFormat("<div class=\"div_Bulletins_title\">");
        sb.AppendFormat("<span class=\"div_Bulletins_title_more\"><a href=\"{0}\">更多>></a></span> <span class=\"div_Bulletins_title_name\">", "/Work/Bulletins.aspx");
        sb.AppendFormat("通知公告</span>");
        sb.AppendFormat("</div>");
        if (list != null && list.Count > 0)
        {
            sb.AppendFormat("<div class=\"div_Bulletins_list\">");
            sb.AppendFormat("<ul>");
            foreach (Bulletin bulletin in list)
            {
                sb.AppendFormat("<li class=\"div_Bulletins_list_li\"><span class=\"div_Bulletins_list_title\">▲ ");
                sb.AppendFormat("<a href=\"/Work/BulletinDetail.aspx?id={2}\">{0}</a></span> <span class=\"div_Bulletins_list_date\">({1})</span>", PageBase.GetString(bulletin.Title, 80, true), Convert.ToDateTime(bulletin.PublishTime).ToString("yyyy-M-d"),bulletin.Id);
                sb.AppendFormat("</li>");
            }
            sb.AppendFormat("</ul>");
            sb.AppendFormat("</div>");
        }
        return sb.ToString();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}