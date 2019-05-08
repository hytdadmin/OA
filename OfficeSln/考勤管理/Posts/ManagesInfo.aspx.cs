using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;


public partial class Posts_ManagesInfo : BbsPageBase
{
    public string strAdmin = string.Empty;
    public string strFeedback = string.Empty;
    public string strSuperAdmin = string.Empty;
    public string strMessage = string.Empty;
    int intID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["typecid"] != null && !string.IsNullOrEmpty(Request["typecid"].Trim()))
        {
            int.TryParse(Request["typecid"].Trim(), out intID);
            if (intID == 1002)
            {
                strMessage = "管理员管理";
            }
            else
            {
                strMessage = "反馈人员管理";
            }
            if (UserInfo.IsADUser && !UserInfo.IsSuperAdmin)
            {
                if (intID == 1002)
                {
                    if (!UserInfo.IsAdmin)
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "script", "<script>alert('您没有操作此模块的权限!');document.location.href='/index.aspx';</script>");
                    }
                }
                else
                {
                    if (!UserInfo.IsFeedback)
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "script", "<script>alert('您没有操作此模块的权限!');document.location.href='/index.aspx';</script>");
                    }
                }
            }
        }
        else
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "script", "<script>alert('您没有操作此模块的权限!');document.location.href='/index.aspx';</script>");

        if (!IsPostBack)
            getImages();
    }

    private void getImages()
    {
        string[] strAdminList = { };
        string[] strFeedbackList = { };
        string[] strSuperAdminList = { };
        string[] strAdminListNoDel = { };
        string[] strFeedbackListNoDel = { };
        StringBuilder sb = new StringBuilder();
        List<treeList.imgModel> imgList = new List<treeList.imgModel>();
        XmlDocument xmlDoc = new XmlDocument();
        string strP = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["mangers"]);
        xmlDoc.Load(strP);
        if (xmlDoc != null)
        {
            strSuperAdminList = xmlDoc.SelectSingleNode("/managers/item/SuperAdminList").InnerText.Trim().Split('|');
            strAdminList = xmlDoc.SelectSingleNode("/managers/item/AdminList").InnerText.Trim().Split('|');
            strFeedbackList = xmlDoc.SelectSingleNode("/managers/item/FeedbackList").InnerText.Trim().Split('|');
            strAdminListNoDel = xmlDoc.SelectSingleNode("/managers/item/AdminListNoDelete").InnerText.Trim().Split('|');
            strFeedbackListNoDel = xmlDoc.SelectSingleNode("/managers/item/FeedbackListNoDelete").InnerText.Trim().Split('|');
        }
        if (intID == 1001)
        {
            foreach (var v in strSuperAdminList)
            {
                sb.AppendFormat("<li><span>{0}</span><a href=\"javascript:;\"><sup>x</sup></a></li>",
                           v);
            }
        }
        else if (intID == 1002)
        {
            foreach (var v in strAdminList)
            {
                if (strAdminListNoDel.Where(c => c.ToLower().Equals(v.ToLower())).Count() > 0)
                {
                    sb.AppendFormat("<li><span>{0}</span></li>",
           v);
                }
                else
                {
                    sb.AppendFormat("<li><span>{0}</span><a href=\"javascript:;\"><sup>x</sup></a></li>",
                           v);
                }
            }
        }
        else if (intID == 1003)
        {
            foreach (var v in strFeedbackList)
            {
                if (strFeedbackListNoDel.Where(c => c.ToLower().Equals(v.ToLower())).Count() > 0)
                {
                    sb.AppendFormat("<li><span>{0}</span></li>",
           v);
                }
                else
                {
                    sb.AppendFormat("<li><span>{0}</span><a href=\"javascript:;\"><sup>x</sup></a></li>",
                           v);
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "script", "<script>alert('操作失败!');</script>");
        }
        strAdmin = sb.ToString();
    }
}