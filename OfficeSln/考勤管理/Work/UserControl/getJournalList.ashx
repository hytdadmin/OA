<%@ WebHandler Language="C#" Class="getJournalList" %>

using System;
using System.Web;
using System.Web.SessionState;
using BLL;
using System.Data;
using System.Text;
using Models;
using System.Collections.Generic;
using Model.TO;

public class getJournalList : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!PageBase.IsLogin())
        {
            context.Response.Write("sessionError");
            return;
        }
        string keyValue = context.Request.Params.Get("keyValue");
        //参数
        if (string.IsNullOrEmpty(keyValue))
        {
            context.Response.Write("keyValue");
            return;
        }
        string searchDate = context.Request.Params.Get("searchDate");
        //if (string.IsNullOrEmpty(searchDate))
        //{
        //    context.Response.Write("keyValue");
        //    return;
        //}
        PageBase.ClearClientPageCache();
        context.Response.Write(ResultStr(keyValue, context,PageBase.GetLoginCode(), searchDate)); 
    }

    private string ResultStr(string keyValue,HttpContext context,string userCode,string searchDate)
    {
        StringBuilder sb = new StringBuilder();


        int numPerPage = 10;//每页显示数量
        string orderBy = " PublishTime desc";
        int totalCount = 0;
        int pageindex = Check.GetInt32(context.Request["p"]);
        if (pageindex == 0)
            pageindex = 1;
        JournalTO journalTO = new JournalTO();
        journalTO.Contents = keyValue.Trim().Equals("输入关键字...") ? "" : keyValue.Trim();
        journalTO.PublishUserCode = userCode;
        if (searchDate != "undefined" && !string.IsNullOrEmpty(searchDate)) {
            journalTO.SearchTime = searchDate;
        }
        //journalTO.SearchTime = searchDate.ToString("yyyy-MM-dd");
        DataTable dt = new JournalBLL().GetJournalList(journalTO, pageindex, numPerPage, orderBy, out totalCount);

        sb.AppendFormat("<div class=\"right_content_title\">");
        //sb.AppendFormat("全部日志<span id=\"lblCount\">{0}</span>条</div>", totalCount);
        //sb.AppendFormat("<div><input type=\"text\" value=\"{0}\" id=\"search_date\" readonly=\"readonly\" style=\"border: 0px;\"/><div class=\"search_date\"><span  onclick=\"WdatePicker({{el:'search_date',maxDate:'%y-%M-%d',onpicked:function(dp){{InitData()}}}})\">时间</span></div></div>", journalTO.SearchTime);

        sb.AppendFormat("<table style=\"width:100%\">");
        sb.AppendFormat("<tr>");
        sb.AppendFormat("<td>全部日志<span id=\"lblCount\">{0}</span>条</td>", totalCount);
        sb.AppendFormat("<td></td>");
        sb.AppendFormat("<td style=\"text-align: right;\"><input type=\"text\" value=\"{0}\" id=\"search_date\" readonly=\"readonly\" style=\"border: 0px;width: 80px;text-align: right;\"/></td>", journalTO.SearchTime);
        sb.AppendFormat("<td style=\"width: 6%;\"><div class=\"search_date\"><span  onclick=\"WdatePicker({{el:'search_date',isShowOK:false,isShowToday :false,isShowClear:false,maxDate:'%y-%M-%d',onpicked:function(dp){{InitData()}}}})\">时间</span></div></td>");
        sb.AppendFormat("<td style=\"width: 6%;\"><div class=\"search_date\" style=\"width: 60px;\"><span  onclick=\"$('#search_date').val('');InitData();\">全部时间</span></div></td>");
        sb.AppendFormat("</tr>");
        sb.AppendFormat("</table>");
        sb.AppendFormat("</div>");
        
        
        
        sb.AppendFormat("<div class=\"right_conten_mr\" id=\"right_conten_mr\">");
        
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                //sb.AppendFormat("<dl id=\"{0}\">","dl"+dr["id"]);
                //sb.AppendFormat("<dd class=\"del\"><a href=\"javascript:DelMyJournal('{0}')\">删除</a></dd>",dr["id"].ToString());
                //sb.AppendFormat("<dd>");
                //sb.AppendFormat("{0}</dd>",Check.GetString(dr["Contents"]));
                //sb.AppendFormat("<dd style=\"text-indent: 0em;\">");
                //sb.AppendFormat("<span>");
                //sb.AppendFormat("{0}</span></dd>",PageBase.GetDateByPub(dr["PublishTime"],"yyyy-M-d HH:mm"));
                //sb.AppendFormat("</dl>");
                sb.AppendFormat("<dl id=\"{0}\">", "dl" + dr["id"]);
                sb.AppendFormat("<table>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td rowspan=\"3\" class=\"tdimg\"><img src=\"{0}\" class=\"img\"/></td>", "/Image/headImg/default.jpg");
                sb.AppendFormat("<td class=\"tdtitle\"><span name=\"spanNameAll\"></span></td>");
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdcontent\">{0}</td>", Check.GetString(dr["Contents"]).Replace("\n", "<br/>"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tddate\" style=\"width: 100%;\">{0}<a href=\"javascript:DelMyJournal('{1}')\" style=\"float:right;color: #0A8CD2;\">删除</a></td>", PageBase.GetDateByPub(dr["PublishTime"], "yyyy-M-d HH:mm"), dr["id"].ToString());
                sb.AppendFormat("</tr>");
                sb.AppendFormat("</table>");
                sb.AppendFormat("</dl>");
            }
        }

        sb.AppendFormat("</div>");
        //总页数
        sb.AppendFormat("<input type=\"hidden\" id=\"pagecount\" value=\"{0}\"/>", totalCount);
        //每页数量
        sb.AppendFormat("<input type=\"hidden\" id=\"pageSize\" value=\"{0}\"/>", numPerPage);

        return sb.ToString();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}