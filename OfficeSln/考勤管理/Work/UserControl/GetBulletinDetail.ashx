<%@ WebHandler Language="C#" Class="GetBulletinDetail" %>

using System;
using System.Web;
using System.Web.SessionState;
using BLL;
using System.Data;
using System.Text;
using Models;


public class GetBulletinDetail : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!PageBase.IsLogin())
        {
            context.Response.Write("sessionError");
            return;
        }
        int id = 0;
        string ids = context.Request.Params.Get("id");
        int.TryParse(ids,out id);
        //参数
        if (id<=0)
        {
            context.Response.Write("parError");
            return;
        }
        PageBase.ClearClientPageCache();
        context.Response.Write(ResultStr(PageBase.GetLoginCode(), context,id)); 
    }

    private string ResultStr(string userCode, HttpContext context,int id)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<div class=\"right_content_title\">");
        sb.AppendFormat("公告详情</div>");
        BulletinBLL bulletinBLL = new BulletinBLL();
        Models.Result.BulletinDetailResult result = bulletinBLL.GetBulletinDetailById(id);
        if (result != null && result.Id > 0)
        {
            sb.AppendFormat("<div class=\"bulletin_Title\">{0}</div>",result.Title);
            sb.AppendFormat("<div class=\"bulletin_Date\">{0}    {1}</div>", result.DepartmentName, Convert.ToDateTime(result.PublishTime).ToString("yyyy-M-d"));
            sb.AppendFormat("<div class=\"bulletin_Contents\">{0}</div>",result.Contents);
        }
        return sb.ToString();
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}