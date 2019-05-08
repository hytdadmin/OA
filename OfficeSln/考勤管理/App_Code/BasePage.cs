using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///BasePage 的摘要说明
/// </summary>
public class BasePage : System.Web.UI.Page
{
	public BasePage()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    #region event handlers

    protected override void OnInit(EventArgs e)
    {

        if (HttpContext.Current.Session["user"] == null)
        {
            HttpContext.Current.Response.Redirect("Login.aspx");
        }
    }

    #endregion
}