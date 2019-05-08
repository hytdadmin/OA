using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Models;


public partial class Login : System.Web.UI.Page
{
    public string script = string.Empty;
    public string hidName = string.Empty;
    public string hidPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string exti = Context.Request.QueryString["exti"];
        if (exti == "0")
        {
            //Session.Clear();
            if (Request.Cookies["userinfo"] != null)
            {
                PageBase.AddLog(new Logs() { AddTime = DateTime.Now, Contents = "退出系统", IsDel = 1, TypeName = "退出", UserCode = PageBase.GetLoginCode() });
                HttpCookie myCookie = new HttpCookie("userinfo");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
        }
       
    }

}