<%@ WebHandler Language="C#" Class="Others_JournalList" %>

using System;
using System.Web;
using System.Web.SessionState;
using BLL;
using System.Data;
using System.Text;
using Models;
using System.Collections.Generic;
using Model.TO;

/// <summary>
/// 其他人日志
/// </summary>
public class Others_JournalList : IHttpHandler, IRequiresSessionState
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
        string userCode = context.Request.Params.Get("uId");
        //参数
        if (string.IsNullOrEmpty(keyValue))
        {
            context.Response.Write("keyValue");
            return;
        }
        string searchDate = context.Request.Params.Get("searchDate");
        //int id = 0;
        //string ids = context.Request.Params.Get("uId");
        //int.TryParse(ids, out id);
        ////参数
        //if (id <= 0)
        //{
        //    context.Response.Write("parError");
        //    return;
        //}
        PageBase.ClearClientPageCache();
        context.Response.Write(ResultStr(keyValue, context, userCode, searchDate));
    }

    private string ResultStr(string keyValue, HttpContext context, string userCode, string searchDate)
    {
       // UserInfo userInfo = new UserInfoBLL().GetUserInfoEntity(uid);
        UserInfo userInfo = new UserInfoBLL().GetUserByUserCode(userCode);
        if (userCode == null || userInfo == null || userCode == "")
        {
            return "parError";
        }
        StringBuilder sb = new StringBuilder();


        int numPerPage = 10;//每页显示数量
        string orderBy = " PublishTime desc";
        int totalCount = 0;
        int pageindex = Check.GetInt32(context.Request["p"]);
        if (pageindex == 0)
            pageindex = 1;
        JournalTO journalTO = new JournalTO();
        journalTO.Contents = keyValue.Trim().Equals("输入关键字...") ? "" : keyValue.Trim();
        journalTO.PublishUserCode = userInfo.UserCode;
        if (searchDate != "undefined" && !string.IsNullOrEmpty(searchDate))
        {
            journalTO.SearchTime = searchDate;
        }
        DataTable dt = new JournalBLL().GetJournalList(journalTO, pageindex, numPerPage, orderBy, out totalCount);

        sb.AppendFormat("<div class=\"right_content_title\">");
        //sb.AppendFormat("<span style=\"color: #0A8CD2;\">{0}</span>的日志<span id=\"lblCount\">{1}</span>条</div>", userInfo.UserName, totalCount);
        //sb.AppendFormat("<div><input type=\"text\" id=\"search_date\" readonly=\"readonly\" style=\"border: 0px;\"/><div class=\"search_date\"><span  onclick=\"WdatePicker({{el:'search_date'}})\">时间</span></div></div>");

        sb.AppendFormat("<table style=\"width:100%\">");
        sb.AppendFormat("<tr>");
        sb.AppendFormat("<td><span style=\"color: #0A8CD2;\">{0}</span>的日志<span id=\"lblCount\">{1}</span>条</td>", userInfo.UserName, totalCount);
        sb.AppendFormat("<td></td>");
        sb.AppendFormat("<td style=\"text-align: right;\"><input type=\"text\" value=\"{0}\" id=\"search_date\" readonly=\"readonly\" style=\"border: 0px;width: 80px;text-align: right;\"/></td>", journalTO.SearchTime);
        sb.AppendFormat("<td style=\"width: 6%;\"><div class=\"search_date\"><span  onclick=\"WdatePicker({{el:'search_date',isShowOK:false,isShowToday :false,isShowClear:false,maxDate:'%y-%M-%d',onpicked:function(dp){{InitDataOl(0)}}}})\">时间</span></div></td>");
        sb.AppendFormat("<td style=\"width: 6%;\"><div class=\"search_date\" style=\"width: 60px;\"><span  onclick=\"$('#search_date').val('');InitDataOl(0);\">全部时间</span></div></td>");
        sb.AppendFormat("</tr>");
        sb.AppendFormat("</table>");
        sb.AppendFormat("</div>");
        
        sb.AppendFormat("<div class=\"right_conten_mr\" id=\"right_conten_mr\">");

        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendFormat("<dl id=\"{0}\">", "dl" + dr["id"]);
                //sb.AppendFormat("<dd class=\"del\"><a href=\"javascript:DelMyJournal('{0}')\">删除</a></dd>", dr["id"].ToString());
                sb.AppendFormat("<dd>");
                sb.AppendFormat("{0}</dd>", Check.GetString(dr["Contents"]));
                sb.AppendFormat("<dd style=\"text-indent: 0em;\">");
                sb.AppendFormat("<span>");
                sb.AppendFormat("{0}</span></dd>",PageBase.GetDateByPub(dr["PublishTime"],"yyyy-M-d HH:mm"));
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