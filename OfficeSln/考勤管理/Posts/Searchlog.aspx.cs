using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using HYTD.BBS.BLL;
using Models;
using HYTD.BBS.Model.TO;
using HYTD.Common;
using BLL;

public partial class Posts_test : BbsPageBase
{
    public string script = string.Empty;
    public int type = 0;
    public string txt = string.Empty;
    public string select = string.Empty;
    public string remark = string.Empty;
    public string ip = string.Empty;
    public string time = string.Empty;
    public string endtime = string.Empty;
    public string sendUser = string.Empty;
    public string backUser = string.Empty;
    public string strUsers = string.Empty;
    public int niming = -1;
    public int fankui = -1;
    public int pageIndex = 1;
    public int pageSize = 10;
    public int rowCount = 400;
    public int pageNum = 1;
    public string strtt = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UserInfo.IsADUser && !UserInfo.IsSuperAdmin && !UserInfo.IsAdmin)
        {
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "script", "<script>alert('您没有操作此模块的权限!');document.location.href='/index.aspx';</script>");
        }
        getUsersList();
        if (!IsPostBack)
            LogList();
    }
    private void getUsersList()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        HYTD.BLL.Call_CustomerBLL bll = new HYTD.BLL.Call_CustomerBLL();
        UserInfoBLL uBll = new UserInfoBLL();
        List<Models.UserInfo> userList = new List<UserInfo>();
        userList = uBll.GetUserInfoList();
        foreach (var m in userList)
        {
            sb.Append("{ \"name\":\"" + m.UserName + "\", \"to\": \"" + m.UserCode + "\" },");
        }
        strUsers = "[" + sb.ToString().Trim(',') + "]";
    }
    /// <summary>
    /// 日志列表
    /// </summary>
    private void LogList()
    {
        RePostsBLL bll = new RePostsBLL();
        StringBuilder sb = new StringBuilder();
        RePostsTO sto = new RePostsTO();
        if (!string.IsNullOrEmpty(Request["select"]))
        {
            sto.CID = type = Convert.ToInt32(Request["select"].Trim());
        }
        if (!string.IsNullOrEmpty(Request["title"]))
        {
            sto.Title = txt = Request["title"].Trim();
        }
        if (!string.IsNullOrEmpty(Request["remark"]))
        {
            sto.Content = remark = Request["remark"].Trim();
        }
        if (!string.IsNullOrEmpty(Request["ip"]))
        {
            sto.UserIP = ip = Request["ip"].Trim();
        }
        if (!string.IsNullOrEmpty(Request["time"]))
        {
            sto.Time = time = Request["time"].Trim();
        }
        if (!string.IsNullOrEmpty(Request["endtime"]))
        {
            sto.EndTime = endtime = Request["endtime"].Trim();
        }
        if (!string.IsNullOrEmpty(Request["sendUser"]))
        {
            sto.SendUser = sendUser = Request["sendUser"].Trim();
        }
        if (!string.IsNullOrEmpty(Request["backUser"]))
        {
            sto.ReUser = backUser = Request["backUser"].Trim();
        }
        if (!string.IsNullOrEmpty(Request["niming"]))
        {
            sto.isAnonymity = niming = Convert.ToInt32(Request["niming"].Trim());
        }
        else
        {
            sto.isAnonymity = 400;
        }

        if (!string.IsNullOrEmpty(Request["fankui"]))
        {
            sto.isFeedback = fankui = Convert.ToInt32(Request["fankui"].Trim());
        }
        else
        {
            sto.isFeedback = 400;
        }
        if (!string.IsNullOrEmpty(Request.QueryString["pageIndex"]))
        {
            pageIndex = int.Parse(Request.QueryString["pageIndex"]);
        }
        DataTable dt = bll.GetNewTab(sto, pageIndex, pageSize, "", out rowCount);
        //分页
        string url = "Searchlog.aspx?pageIndex={0}&typecid=1110&select=" + type + "&title=" + txt + "&remark=" + remark + "&ip=" + ip + "&time=" + time + "&sendUser=" + sendUser + "&backUser=" + backUser + "&niming=" + niming + "&fankui=" + fankui;
        strtt = DividePage.Pager(pageSize, rowCount, pageIndex, url);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string status = dr["status"].ToString();
                string strContent = StringHelper.FilterHTML(dr["remark"].ToString().Trim()).Trim();
                if (strContent.Length == 0)
                    strContent = "&nbsp";

                string topic_ftcolor = "";
                if (Convert.ToInt32(dr["Ispos"]) == 1)
                {
                    topic_ftcolor = " style=\"font-weight:bold\"";
                }

                sb.AppendFormat("<tbody><tr><td class=\"by\" title=\"{0}\"  " + topic_ftcolor + ">[{1}]<a href=\"detail.aspx?pageIndex=1&fatherID={2}&num=0\" target=\"_back\">{3}</a></td>", dr["Title"], dr["Name"], dr["ID"],
                    HYTD.Common.StringHelper.SubString(dr["Title"].ToString(), 25));
                sb.AppendFormat("<td class=\"by\" title=\"{0}\">{1}&nbsp;</td>", strContent,
                    HYTD.Common.StringHelper.SubString(strContent, 25));
                //ispos  1表示发贴   0为回复内容
                if (Convert.ToInt32(dr["Ispos"]) == 1)
                {
                    sb.AppendFormat("<td class=\"num\">{0}&nbsp;<br />[{1}]</td>",
                        dr["SendUserName"], PublicEnum.GetEnumDescription<PublicEnum.PubliucIsVidicate>(dr["isAnonymity"].ToString()));
                    sb.AppendFormat("<td class=\"num\">{0}&nbsp;</td>", "");
                }
                else
                {
                    sb.AppendFormat("<td class=\"num\">{0}&nbsp;<br />[{1}]</td>", dr["SendUserName"], PublicEnum.GetEnumDescription<PublicEnum.PubliucIsVidicate>(dr["isAnonymity"].ToString()));
                    sb.AppendFormat("<td class=\"num\">{0}&nbsp;<br />[{1}]</td>", dr["ReUserName"], PublicEnum.GetEnumDescription<PublicEnum.PubliucIsVidicate>(dr["isAnonymity"].ToString()));
                }
                sb.AppendFormat("<td class=\"num\">{0}&nbsp;</td>", PublicEnum.GetEnumDescription<PublicEnum.PubliucIsVidicate>(dr["isFeedback"].ToString()));
                sb.AppendFormat("<td class=\"num\">{0}&nbsp;</td>", dr["retime"]);
                sb.AppendFormat("<td class=\"num\">{0}&nbsp;</td>", dr["UserIP"]);
                if (status == "1")
                {
                    sb.AppendFormat("<td class=\"num\" style=\"width:40px\"><a href=\"javascript:;\"  onclick=\"javascript:DeleteReplyforLog({0},{1})\">删除</a></td></tr></tbody>", dr["IDNew"], dr["tp"]);
                }
                else
                {
                    sb.AppendFormat("<td class=\"num\" style=\"width:40px\"><a href=\"javascript:;\"  onclick=\"javascript:RecoverReplyforLog({0},{1})\" style=\"color:red;\">恢复显示</a></td></tr></tbody>", dr["IDNew"], dr["tp"]);
                }
            }
            script = sb.ToString();
        }
        else
        {
            script = "没有搜索到相关内容！";
        }
        //获取当前页
        if (rowCount % pageSize == 0)
        {
            pageNum = rowCount / pageSize;
        }
        else
        {
            pageNum = (rowCount / pageSize) + 1;
        }
    }
    /// <summary>
    /// 获取类别
    /// </summary>
    /// <returns></returns>
    public string GetCategory(int CID)
    {
        StringBuilder sb = new StringBuilder();
        CategoryBLL cateBll = new CategoryBLL();
        List<Category> list = cateBll.GetCategoryList();
        sb.Append("<option value='0'>全部</option>");
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == CID)
                {
                    sb.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", list[i].ID, list[i].Name);
                }
                else
                {
                    sb.AppendFormat("<option value='{0}'>{1}</option>", list[i].ID, list[i].Name);
                }
            }
        }
        return sb.ToString();
    }


}