<%@ WebHandler Language="C#" Class="SayUC" %>

using System;
using System.Web;
using System.Web.SessionState;
using BLL;
using System.Data;
using System.Text;
using Models;
using System.Collections.Generic;
using Models.TO;
using Model.TO;

public class SayUC : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!PageBase.IsLogin())
        {
            context.Response.Write("sessionError");
            return;
        }
        PageBase.ClearClientPageCache();
        context.Response.Write(ResultStr(context)); 
    }

    private string ResultStr(HttpContext context)
    {
        StringBuilder sb = new StringBuilder();
        List<SayList> list = new BLL.SayBLL().GetSayListByTop(10);
        sb.AppendFormat("<div class=\"divleft_say_title\">");
        sb.AppendFormat("<span class=\"divleft_say_title_more\"><a href=\"javascript:divSayAlertDisplay('block');\">说说</a></span> <span class=\"divleft_say_title_name\">");
        sb.AppendFormat("大家正在说</span>");
        sb.AppendFormat("</div>");
        if (list != null && list.Count > 0)
        {
            sb.AppendFormat("<div class=\"divleft_say_list\">");
            sb.AppendFormat("<ul id=\"divleft_say_ul\">");
            foreach (SayList sayList in list)
            {
                sb.AppendFormat("<li class=\"divleft_say_list_li\"><a href=\"/Work/Others_Journal.aspx?uid={0}\"><span class=\"divleft_say_list_title\">", sayList.UserCode);
                sb.AppendFormat("{0}</span></a>： <span class=\"divleft_say_list_content\">", PageBase.GetString(sayList.UserName, 10, true));
                //sb.AppendFormat("{0}</span> </li>",PageBase.GetString(sayList.Contents,100,true));
                sb.AppendFormat("{0}</span> </li>", PageBase.ShowExpressionHtml(PageBase.GetstrByName(sayList.Contents).Replace("\n", "<br/>")));
            }
            sb.AppendFormat("</ul>");
            sb.AppendFormat("<ul><a href=\"/Work/SayList.aspx\" class=\"divleft_say_list_more\">更多>></a></ul>");
            sb.AppendFormat("</div>");
        }
        return sb.ToString();

        //StringBuilder sb = new StringBuilder();


        //int numPerPage = 10;//每页显示数量
        //string orderBy = " PublishTime desc";
        //int totalCount = 0;
        //int pageindex = Check.GetInt32(context.Request["p"]);
        //if (pageindex == 0)
        //    pageindex = 1;
        //SayTO sayTO = new SayTO();
        //DataTable dt = new SayBLL().GetSayListByLeft(sayTO, pageindex, numPerPage, orderBy, out totalCount);

        //sb.AppendFormat("<div class=\"divleft_say_title\">");
        //sb.AppendFormat("<span class=\"divleft_say_title_more\"><a href=\"javascript:divSayAlertDisplay('block');\">说说</a></span> <span class=\"divleft_say_title_name\">");
        //sb.AppendFormat("大家正在说</span>");
        //sb.AppendFormat("</div>");


        //sb.AppendFormat("<div class=\"divleft_say_list\">");
        //sb.AppendFormat("<ul id=\"divleft_say_ul\">");
        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        sb.AppendFormat("<li class=\"divleft_say_list_li\"><a href=\"/Work/Others_Journal.aspx?uid={0}\"><span class=\"divleft_say_list_title\">", dr["UserCode"].ToString());
        //        sb.AppendFormat("{0}</span></a>： <span class=\"divleft_say_list_content\">", PageBase.GetString(dr["UserName"].ToString(), 10, true));
        //        //sb.AppendFormat("{0}</span> </li>",PageBase.GetString(sayList.Contents,100,true));
        //        sb.AppendFormat("{0}</span> </li>", PageBase.GetstrByName(dr["Contents"].ToString()));
        //    }
        //}
        //sb.AppendFormat("</ul>");
        //sb.AppendFormat("</div>");

        //sb.AppendFormat("</div>");
        //////总页数
        ////sb.AppendFormat("<input type=\"hidden\" id=\"pagecountSay\" value=\"{0}\"/>", totalCount);
        //////每页数量
        ////sb.AppendFormat("<input type=\"hidden\" id=\"pageSizeSay\" value=\"{0}\"/>", numPerPage);

        //return sb.ToString();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}