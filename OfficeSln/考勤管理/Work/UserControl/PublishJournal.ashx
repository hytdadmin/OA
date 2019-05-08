<%@ WebHandler Language="C#" Class="PublishJournal" %>

using System;
using System.Web;
using System.Web.SessionState;
using BLL;
using System.Data;
using System.Text;
using Models;
using System.Collections.Generic;
using Models.TO;

//发表工作日志
public class PublishJournal : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (!PageBase.IsLogin())
        {
            context.Response.Write("sessionError");
            return;
        }
        PageBase.ClearClientPageCache();

        string contents = context.Request.Params.Get("contents");
        //参数
        if (string.IsNullOrEmpty(contents))
        {
            context.Response.Write("parError");
            return;
        }
        //if (contents.Length > 500)
        if (PageBase.StrLength(contents) > 500 * 2)
        {
            context.Response.Write("lengthError");
            return;
        }
        Journal journal = new Journal();
        journal.Contents = PageBase.EscapeJs(contents);
        journal.PublishUserCode = Check.GetString(PageBase.GetLoginCode());
        journal.IsDel = 1;
        journal.PublishTime = DateTime.Now;
        journal.ScanNum = 0;
        journal.Title = "";
        int addId=new JournalBLL().AddJournalReturn(journal);
        if (addId>0)
            context.Response.Write(addId);
        else
            context.Response.Write("no");
    }
        
    public bool IsReusable {
        get {
            return false;
        }
    }

}