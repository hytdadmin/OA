<%@ WebHandler Language="C#" Class="getJournalListOthers" %>

using System;
using System.Web;
using System.Web.SessionState;
using BLL;
using System.Data;
using System.Text;
using Models;
using System.Collections.Generic;
using Model.TO;

public class getJournalListOthers : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
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
        int depId = 0;
        int.TryParse(context.Request.Params.Get("depId"), out depId);
        PageBase.ClearClientPageCache();
        context.Response.Write(ResultStr(keyValue, context, PageBase.GetLoginCode(), depId, searchDate)); 
    }

    private string ResultStr(string keyValue, HttpContext context, string userCode, int depId, string searchDate)
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
        journalTO.DepId = depId;
        if (searchDate != "undefined" && !string.IsNullOrEmpty(searchDate))
        {
            journalTO.SearchTime = searchDate;
        }
        DataTable dt = new JournalBLL().GetJournalListByTB(journalTO, pageindex, numPerPage, orderBy, out totalCount);

        sb.AppendFormat("<div class=\"right_content_title\">");
        sb.AppendFormat("全部日志<span id=\"lblCount\">{0}</span>条</div>", totalCount);

        sb.AppendFormat("<table style=\"width:100%\">");
	  sb.AppendFormat("<tr>");
      sb.AppendFormat("<td><a id=\"0\" class=\"{0}\" href=\"javascript:InitDataO(0,{1})\">全部</a>", depId == 0 ? "depB" : "depA",0);
          List<Department> list = new DepartmentBLL().GetDepListByIsdel();
          if (list != null && list.Count > 0)
          {
              foreach (Department dep in list)
              {
                  sb.AppendFormat("丨<a id=\"{1}\" class=\"{2}\" href=\"javascript:InitDataO(0,{1})\">{0}</a>", dep.RoleName, dep.Id, dep.Id == depId ? "depB" : "depA",dep.Id);
              }
          }
      sb.AppendFormat("</td>");
	  sb.AppendFormat("<td></td>");
      sb.AppendFormat("<td style=\"text-align: right;\"><input type=\"text\" value=\"{0}\" id=\"search_date\" readonly=\"readonly\" style=\"border: 0px;width: 80px;text-align: right;\"/></td>", journalTO.SearchTime);
      sb.AppendFormat("<td style=\"width: 6%;\"><div class=\"search_date\"><span  onclick=\"WdatePicker({{el:'search_date',maxDate:'%y-%M-%d',onpicked:function(dp){{InitDataO(0,{0})}}}})\">时间</span></div></td>",depId);
      sb.AppendFormat("<td style=\"width: 6%;\"><div class=\"search_date\" style=\"width: 60px;\"><span  onclick=\"$('#search_date').val('');InitDataO(0,{0});\">全部时间</span></div></td>",depId);
	  sb.AppendFormat("</tr>");
	  sb.AppendFormat("</table>");
        
        sb.AppendFormat("<div class=\"right_conten_mr\" id=\"right_conten_mr\">");

        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendFormat("<dl>");
                string imgsrc = "Image/headImg/" + dr["HeadImg"].ToString();
                string img = HttpContext.Current.Server.MapPath("~/Image/headImg/" + Check.GetString(dr["HeadImg"]));
                if (string.IsNullOrEmpty(dr["HeadImg"].ToString())||!System.IO.File.Exists(img))
                    imgsrc = "Image/headImg/default.jpg";
                sb.AppendFormat("<table>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td rowspan=\"3\" class=\"tdimg\"><a href=\"Others_Journal.aspx?uid={0}\"><img src=\"{1}\" class=\"img\"/></a></td>", dr["PublishUserCode"].ToString(), context.Request.ApplicationPath + imgsrc);
                sb.AppendFormat("<td class=\"tdtitle\"><a style=\"color: #343434;\" href=\"Others_Journal.aspx?uid={0}\">{1}</a></td>", dr["PublishUserCode"].ToString(), Check.GetString(dr["UserName"]));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tdcontent\">{0}</td>", Check.GetString(dr["Contents"]).Replace("\n", "<br/>"));
                sb.AppendFormat("</tr>");
                sb.AppendFormat("<tr>");
                sb.AppendFormat("<td class=\"tddate\">{0}</td>",PageBase.GetDateByPub(dr["PublishTime"],"yyyy-M-d HH:mm"));
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