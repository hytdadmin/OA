using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HYTD.BBS.BLL;
using Models;
using System.Text;

public partial class Posts_SensitiveWords : BbsPageBase
{
    public string strWords = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UserInfo.IsADUser && !UserInfo.IsSuperAdmin && !UserInfo.IsAdmin)
        {
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "script", "<script>alert('您没有操作此模块的权限!');document.location.href='/index.aspx';</script>");
        }
        if (!IsPostBack)
        {
            getWords();
        }
    }

    private void getWords()
    {
        SensitiveWordsBLL bll = new SensitiveWordsBLL();
        List<SensitiveWords> mList = new List<SensitiveWords>();
        mList = bll.GetSensitiveWordsList();
        StringBuilder sb = new StringBuilder();
        foreach (var v in mList)
        {
            sb.AppendFormat("<li><span>{0}</span><a id=\"{1}\" href=\"javascript:;\"><sup>x</sup></a></li>",
                v.sensitiveWord,v.ID);
        }
        strWords = sb.ToString();
    }
}