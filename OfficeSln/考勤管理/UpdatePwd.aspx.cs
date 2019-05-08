using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdatePwd :PageBase
{
    protected string UserCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"]!=null)
        {
            UserCode = Session["user"].ToString();
        }
    }
}