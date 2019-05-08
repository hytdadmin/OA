<%@ WebHandler Language="C#" Class="GetBulletinList" %>

using System;
using System.Web;
using System.Web.SessionState;
using BLL;
using System.Data;
using System.Text;
using Models;
using System.Collections.Generic;
using Model.TO;

public class GetBulletinList : IHttpHandler, IRequiresSessionState
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
        context.Response.Write(ResultStr(keyValue, context, PageBase.GetLoginCode()));
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
        BulletinTO bulletinTO = new BulletinTO();
        bulletinTO.Title = keyValue.Trim().Equals("输入关键字...") ? "" : keyValue.Trim();
        bulletinTO.PublishUserCode = userCode;
        DataTable dt = new BulletinBLL().GetBulletinList(bulletinTO, pageindex, numPerPage, orderBy, out totalCount);

        sb.AppendFormat("<div class=\"right_content_title\">");
        sb.AppendFormat("全部公告<span id=\"lblCount\">{0}</span>条</div>", totalCount);
        sb.AppendFormat("<div class=\"right_conten_mr\" id=\"right_conten_mr\">");

        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendFormat("<dl>");
                sb.AppendFormat("<dt><span>[{0}]</span><a href=\"{1}\">{2} </a></dt>", Convert.ToDateTime(dr["PublishTime"]).ToString("yyyy-M-d"), "BulletinDetail.aspx?id="+dr["id"], PageBase.GetString(Check.GetString(dr["title"]), 60, true));
                sb.AppendFormat("<dd>{0}</dd>", PageBase.GetString(PageBase.NoHTML(Check.GetString(dr["Contents"])), 400, true));
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