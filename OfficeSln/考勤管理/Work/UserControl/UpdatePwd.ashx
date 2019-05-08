<%@ WebHandler Language="C#" Class="UpdatePwd" %>

using System;
using System.Web;
using System.Text;
using BLL;
using System.Web.SessionState;
using Models;
using HYTD.Common;
public class UpdatePwd : IHttpHandler,IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string oldPwd = context.Request["oldPwd"];
        string newPwd = context.Request["newPwd"];
        string newPwd1 = context.Request["newPwd1"];
        PageBase pageBase = new PageBase();
        UserInfoBLL bll = new UserInfoBLL();
        int userId = pageBase.CurrentUserInfo.UserID;
        UserInfo model=bll.GetUserInfoEntity(userId);
        //0修改成功 1原始密码错误 2修改失败
        if (model!=null)
        {
            if (Md5Test.Md5str(oldPwd)==model.Upwd)
            {
                model.Upwd = Md5Test.Md5str(newPwd);
                bll.UpdateUserInfo(model);
                context.Response.Write(0);
            }
            else
            {
                context.Response.Write(1);
            }
        }
        else
        {
            context.Response.Write(2);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}