using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Text;
using HYTD.Common;
using HYTD.CAPlatform.Common;
using System.Configuration;

public partial class Work_UserControl_Top : System.Web.UI.UserControl
{
    string userName = string.Empty;
    string loginName = string.Empty;
    public string caPath = string.Empty;
    public string sUrl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (PageBase.IsLogin())
        {
            UserInfo model = new PageBase().CurrentUserInfo;
            StringEncryption strEncryption = new StringEncryption();
            lblName.Text = model.UserName;
            userName = Server.UrlEncode(strEncryption.Encrypt(model.UserCode.ToString().Trim()));
            loginName = Server.UrlEncode(strEncryption.Encrypt(model.UserName.Trim()));
            caLog.WriteTraceLog("单点登录", string.Format("原始用户名：{0}密码：{1} 加密后用户名：{2}密码：{3}url编码后用户名：{4} url编码后密码：{5}",
               model.UserCode.ToString().Trim(), model.UserName.Trim(),
               strEncryption.Encrypt(model.UserCode.ToString().Trim()),strEncryption.Encrypt(model.UserName.Trim()),
               userName, loginName));
            if (model.IsAdmin == 1)
            {
                nav9.Visible = true;
            }
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<a href=\"" + ConfigurationManager.AppSettings["caurl"] + "Sso.aspx?userName=" + userName + "&loginName=" + loginName + "\" target=\"_blank\">CA平台</a>");
            caPath = sbHtml.ToString();
            sUrl = string.Format("?uname={0}&stime={1}", model.UserName.ToString(), DateTime.Now.ToString("yyyy-MM")+"-01");
        }
    }
}