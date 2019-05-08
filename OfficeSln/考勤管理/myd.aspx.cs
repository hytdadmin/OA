using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class myd : System.Web.UI.Page
{
    protected string strCustomerIP = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string strName = Request.Form["un"];
        string strIP = Request.Form["up"];
         strCustomerIP = GetUserIP();
        //Response.Write(string.Format("un:{0} np:{1} cp:{2}", strName, strIP, strCustomerIP));
    }

    #region 获取登录用户的IP
    public static string GetUserIP()
    {
        string userIP = "";
        if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                userIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else
                userIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        }
        else
        {
            userIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        }
        return userIP;
    }
    #endregion
}