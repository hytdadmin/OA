<%@ WebHandler Language="C#" Class="DownloadCenterList" %>

using System;
using System.Web;
using System.Web.SessionState;
using BLL;
using System.Data;
using System.Text;
using Models;
using System.Collections.Generic;
using Model.TO;

public class DownloadCenterList : IHttpHandler, IRequiresSessionState
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
        PageBase.ClearClientPageCache();
        context.Response.Write(ResultStr(keyValue, context,PageBase.GetLoginCode())); ;
    }

    private string ResultStr(string keyValue, HttpContext context, string userCode)
    {
        StringBuilder sb = new StringBuilder();


        int numPerPage = 10;//每页显示数量
        string orderBy = " PublishTime desc";
        int totalCount = 0;
        int pageindex = Check.GetInt32(context.Request["p"]);
        if (pageindex == 0)
            pageindex = 1;
        DownloadCenterTO downloadCenterTO = new DownloadCenterTO();
        downloadCenterTO.Title = keyValue.Trim().Equals("输入关键字...") ? "" : keyValue.Trim();
        string fileds = " Id,Title,AffixNewName,AffixOldName,AffixUrl ";
        DataTable dt = new DownloadCenterBLL().GetDownloadCenterListByFileds(downloadCenterTO, pageindex, numPerPage, orderBy,fileds, out totalCount);

        sb.AppendFormat("<div class=\"right_content_title\">");
        sb.AppendFormat("全部内容<span id=\"lblCount\">{0}</span>项</div>", totalCount);
        sb.AppendFormat("<div class=\"right_conten_dd\" id=\"right_conten_dd\">");

        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendFormat("<dl>");
                sb.AppendFormat("<dt>{0}</dt>",dr["title"].ToString());
                sb.AppendFormat("<dd><span><a href=\"{0}\">下载</a></span>{1}</dd>", dr["AffixUrl"].ToString() + dr["AffixNewName"].ToString(), dr["AffixOldName"].ToString());
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