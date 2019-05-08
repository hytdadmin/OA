<%@ WebHandler Language="C#" Class="Login" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;
using BLL;
using Models;
using System.Linq;

public class Login : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string user=context.Request.Form["user"].Trim();
        string pwd=context.Request.Form["pwd"].Trim();
        string UserCode = string.Empty;
        string Upwd = string.Empty;
        string UserName=string.Empty;
        UserInfoBLL bll = new UserInfoBLL();
        UserInfo entity = bll.GetUserMode(user, Md5Test.Md5str(pwd));
        UserInfo model = new UserInfo();
        //0成功 1用户名不存在 2 密码错误 3验证码错误
            if (entity != null)
            {
                if (entity.UserStatus==1)
                {
                    if (Md5Test.Md5str(pwd) == entity.Upwd)
                    {
                        context.Response.Write(0);
                        //context.Session["user"] = entity;
                        PageBase pageBase = new PageBase();
                        pageBase.CurrentUserInfo = new UserInfo() { UserCode = HYTD.Common.Encrypt.EncryptString(entity.UserCode) };
                        PageBase.AddLog(new Logs() { AddTime = DateTime.Now, Contents = "登录系统", IsDel = 1, TypeName = "登录", UserCode = entity.UserCode });
                    }
                    else
                    {
                        context.Response.Write("输入的用户名或密码错误！");
                    } 
                }else
                    context.Response.Write("该用户已被禁用！");
            }
            else
            {
                context.Response.Write("输入的用户名或密码错误！");

            }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}