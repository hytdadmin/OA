using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class Work_Back_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.Title = PageBase.GetPageTitle() + Page.Title;
        if (PageBase.IsLogin())
        {
            UserInfo model = new PageBase().CurrentUserInfo;
            lblName.Text = model.UserName;
            if (model.IsAdmin != 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", " <script lanuage=javascript> alert('您不是管理员，无权查看此页面');location.href='/Work/Index.aspx';</script>");   
            }
        }
        else
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", " <script lanuage=javascript> alert('会话过期，请重新登录');location.href='/Login.aspx';</script>");   
    }
}
