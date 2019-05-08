<%@ WebHandler Language="C#" Class="UpdatePwd" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;

public class UpdatePwd : IHttpHandler,IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string oldPwd=context.Request.Form["oldPwd"];
        string newPwd=context.Request.Form["newPwd"];
        string pwd=context.Request.Form["pwd"];
        //string UserCode = context.Request.Form["UserCode"];
        string UserCode=PageBase.GetLoginCode();//登录人
        //判断输入的旧密码是否正确
        string sql = "select COUNT(*) from UserInfo where Upwd=@Upwd and UserCode=@UserCode";
        SqlParameter[] sp = {new SqlParameter("@Upwd",Md5Test.Md5str(oldPwd)),
                            new SqlParameter("@UserCode",UserCode)};
        //0 修改成功 1 旧密码错误 2修改出错
        if (int.Parse(SqlHelper.GetSingle(sql,sp).ToString())>0)
        {
            //修改密码
            string updateSql = "update UserInfo set Upwd=@Upwd where UserCode=@UserCode";
            SqlParameter[] parames = { new SqlParameter("@Upwd",SqlDbType.NVarChar),
                                     new SqlParameter("@UserCode",SqlDbType.VarChar)};
            parames[0].Value = Md5Test.Md5str(pwd);
            parames[1].Value = UserCode;
            if (SqlHelper.ExecuteSql(updateSql,parames)>0)
            {
                context.Response.Write(0);
            }
            else
            {
                context.Response.Write(2);
            }
        }
        else
        {
            context.Response.Write(1);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}