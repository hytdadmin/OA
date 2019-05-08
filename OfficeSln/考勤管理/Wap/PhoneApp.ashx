<%@ WebHandler Language="C#" Class="PhoneApp" %>

using System;
using System.Web;
using HYTD.Common;
using HYTD.Model.TO;
using Models;
using BLL;

public class PhoneApp : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string op=context.Request["op"];
        if (op == "feedback")
        {
            AddFeedBack(context);
        }
    }
    /// <summary>
    /// 意见反馈（添加到oa表中）
    /// </summary>
    /// <param name="context"></param>
    private void AddFeedBack(HttpContext context)
    {
        string mobile = context.Request["mobile"];
        try
        {
            string fbtxt = context.Request["fbtxt"];
            string contact = context.Request["contact"];
            int fbtype =Convert.ToInt32(context.Request["fbtype"]);
            string loginName = context.Request["loginName"];
            int apptype =Convert.ToInt32(context.Request["apptype"]);
            DateTime crateTime = DateTime.Now;
            Feedback fb = new Feedback();
            fb.loginName = loginName;
            fb.contact = contact;
            fb.crateTime = crateTime;
            if (mobile == "mobile")
                fb.apptype = (int)PublicEnum.FbAppType.phoneApp;
            else fb.apptype = (int)PublicEnum.FbAppType.pcApp;
            switch (fbtype)
            {
                case 0: fb.status = (int)PublicEnum.FeedbackType.advice;
                break;
                case 1: fb.status = (int)PublicEnum.FeedbackType.error;
                break;
                case 2: fb.status = (int)PublicEnum.FeedbackType.Nouse;
                break;
                case 3: fb.status = (int)PublicEnum.FeedbackType.other;
                break;  
            }
            fb.fbcontent = fbtxt;
            
            new FeedbackBLL().AddFeedback(fb);
            if(mobile=="mobile")
            context.Response.Write("index_page.InitFeedback({\"state\":\"0\",\"msg\":\"您的建议已提交,感谢您的反馈!\"})");
            else
                context.Response.Write("InitFeedback({\"state\":\"0\",\"msg\":\"您的建议已提交,感谢您的反馈!\"})");   
        }
        catch (Exception ex)
        {
            if (mobile == "mobile")
                context.Response.Write("index_page.InitFeedback({\"state\":\"-1\",\"msg\":\"" + HttpUtility.JavaScriptStringEncode(ex.ToString()) + "\"})");
            else  
            context.Response.Write("InitFeedback({\"state\":\"-1\",\"msg\":\""+HttpUtility.JavaScriptStringEncode(ex.ToString())+"\"})");
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}