<%@ WebHandler Language="C#" Class="DelJournal" %>

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
/// 删除自己的日志
/// </summary>
public class DelJournal : IHttpHandler, IRequiresSessionState
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
        int.TryParse(ids, out id);
        //参数
        if (id <= 0)
        {
            context.Response.Write("parError");
            return;
        }
        PageBase.ClearClientPageCache();
        JournalBLL journalBLL = new JournalBLL();
        if (journalBLL.DeleteJournalByUserCode(id, PageBase.GetLoginCode()))
            context.Response.Write("true");
        else
            context.Response.Write("false");
            
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}