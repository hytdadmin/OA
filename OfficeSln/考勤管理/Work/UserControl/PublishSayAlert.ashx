<%@ WebHandler Language="C#" Class="PublishSayAlert" %>

using System;
using System.Web;
using System.Web.SessionState;
using BLL;
using System.Data;
using System.Text;
using Models;

//发表说说
public class PublishSayAlert : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
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
        //if (contents.Length > 250)
        if (PageBase.StrLength(contents)>50*2)
        {
            context.Response.Write("lengthError");
            return;
        }
        Say say = new Say();
        say.Contents =PageBase.EscapeJs(contents);
        say.PublishUserCode = Check.GetString(PageBase.GetLoginCode());
        say.IsDel = 1;
        say.PublishTime = DateTime.Now;
        say.ScanNum = 0;
        if (new SayBLL().AddSayReturn(say))
            context.Response.Write("yes");
        else
            context.Response.Write("no");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}