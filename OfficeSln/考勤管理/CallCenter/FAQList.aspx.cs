using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Models;
using System.Data;
using HYTD.Common;
using BLL;
using System.Configuration;
using Microsoft.Office.Interop;
using System.Runtime.InteropServices;
using HYTD.Models.TO;

public partial class CallCenter_FAQList : PageBase
{
    public string strError = string.Empty;
    public string script = string.Empty;
    public int pageIndex = 1;
    //public int pageSize = 10;
    public int rowCount = 400;
    public int intSelected = -1;
    public string strtt = string.Empty;
    public string strCumstomers = string.Empty;
    public string strUsers = string.Empty;
    public string strLoginUserName = string.Empty;
    public string strStratTime = string.Empty;
    public string strEndTime = string.Empty;
    public string sUrl = string.Empty;
    public string sUrlNew = string.Empty;
    public string strCallWorkBillStatusDrop = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo model = new PageBase().CurrentUserInfo;
        if (ConfigurationManager.AppSettings["Users"] != null)
        {
            string[] strlist = ConfigurationManager.AppSettings["Users"].ToString().Trim().Split(',');
            if (strlist.Where(c => c == model.UserCode.ToString()).ToList().Count > 0)
            {

            }
            else
            {
                Response.Write("<script>window.location.href='SatisfactionList.aspx';</script>");
                this.Response.End();
            }
        }
        else
        {
            Response.Write("<script>window.location.href='SatisfactionList.aspx';</script>");
            this.Response.End();
        }
        if (!IsPostBack)
        {
            DataBind();
            getCustomerList();
            UserRights1.intPage = 6;
            sUrl = string.Format("?uname={0}&stime={1}", CurrentUserInfo.UserName, DateTime.Now.ToString("yyyy-MM") + "-01");
            sUrlNew = string.Format("?stime={0}", DateTime.Now.ToString("yyyy-MM") + "-01");
           
        }
    }

    private void DataBind()
    {
        Call_FAQListTO sto = new Call_FAQListTO();
        #region 条件
        if (!string.IsNullOrEmpty(Request["sltType"]))
        {
            sto.CF_SoftTypeID = Convert.ToInt32(Request["sltType"].ToString().Trim());//类型
        }
        if (!string.IsNullOrEmpty(Request["pErrorList"]))
        {
            sto.CF_ErrorList = Request["pErrorList"].ToString().Trim();//标题
        }
        //else
        //    sto.CF_SoftTypeID = Convert.ToInt32(Request["uname"].ToString().Trim());//存 用户名称
        if (!string.IsNullOrEmpty(Request["stime"]))
        {
            DateTime dtStime = Convert.ToDateTime("1980-01-01");
            DateTime.TryParse(Request["stime"].ToString().Trim(), out dtStime);
            sto.CF_AddDate = dtStime;//保存操作开始时间
        }
        if (!string.IsNullOrEmpty(Request["etime"]))
        {
            DateTime dtEtime = Convert.ToDateTime("2250-01-01");
            DateTime.TryParse(Request["etime"].ToString().Trim(), out dtEtime);
            sto.CF_EndDate = dtEtime;//保存操作结束时间
        }

        #endregion
        if (!string.IsNullOrEmpty(Request.QueryString["pageIndex"]))
        {
            int.TryParse(Request.QueryString["pageIndex"], out pageIndex);
        }
        HYTD.BLL.Call_FAQListBLL bll = new HYTD.BLL.Call_FAQListBLL();
        DataTable dt = bll.GetCall_FAQListList(sto, pageIndex, pageSize, "", out rowCount);
        //分页
        string getUrl = "";
        if (Request.Url.Query.Length > 0 && Request.Url.Query.IndexOf("pageIndex") > -1 && Request.Url.Query.IndexOf("&") > -1)
        {
            getUrl = Request.Url.Query.Substring(Request.Url.Query.IndexOf("&"));
        }
        else if (Request.Url.Query.Length > 0)
            getUrl = Request.Url.Query.Substring(1);
        string url = "FAQList.aspx?pageIndex={0}&" + getUrl.Trim('&');
        strtt = DividePage.Pager(pageSize, rowCount, pageIndex, url);

        if (dt.Rows.Count > 0)
        {
            int num = 0;
            StringBuilder sbContent = new StringBuilder();
            //string strNew = string.Empty;
            string strEdit = string.Empty;
            string strDelete = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                num++;
                //strNew = string.Format("<a href=\"javascript:;\" name=\"NewFAQ\" onclick=\"NewFAQ({0})\" >新建</a>   ", dr["CF_ID"]);
                strEdit = string.Format("<a href=\"javascript:;\" name=\"EditFAQ\" onclick=\"EditFAQ({0})\">修改</a>", dr["CF_ID"].ToString());
                strDelete = string.Format("<a href=\"javascript:;\" name=\"DeleteFAQ\" onclick=\"DeleteFAQ({0})\">删除</a>", dr["CF_ID"].ToString());
                sbContent.AppendFormat("<tbody>");
                sbContent.AppendFormat("<tr>");
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", num);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\"><a href=\"javascript:;\"  onclick=\"ShowFAQ({1})\">{0}</a></td>", dr["CF_ErrorList"].ToString(), dr["CF_ID"].ToString());
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CF_Describe"]);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CF_AddDate"]);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", getSoftTypename(dr["CF_SoftTypeID"].ToString()));
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", getUsername(dr["CF_UserID"].ToString()));
                sbContent.AppendFormat("<td class=\"num\">{0}&nbsp;&nbsp;&nbsp;&nbsp;{1}</td>", strEdit, strDelete);
                sbContent.AppendFormat("</tr>");
                sbContent.AppendFormat("</tbody>");
            }

            script = sbContent.ToString();
        }
        else
        {
            script = "没有搜索到相关内容！";
        }
        //获取当前页
        //if (rowCount % pageSize == 0)
        //{
        //    pageNum = rowCount / pageSize;
        //}
        //else
        //{
        //    pageNum = (rowCount / pageSize) + 1;
        //}
    }
    private string getUsername(string userId)
    {
        int ID = 0;
        if (userId != null && userId != "")
        {
            ID = Convert.ToInt32(userId);
        }
        //HYTD.BLL.UserInfoBLL = new HYTD.BLL.Call_CustomerBLL();
        //List<UserInfo> userList = new List<UserInfo>();
        UserInfoBLL UserBLL = new UserInfoBLL();
        return UserBLL.GetUserInfoEntityByUserCode(ID) == null ? "" : UserBLL.GetUserInfoEntityByUserCode(ID);
    }
    private string getSoftTypename(string typeID)
    {
        int ID = 0;
        if (typeID != null && typeID != "")
        {
            ID = Convert.ToInt32(typeID);
        }
        List<Call_Category> ccList = new List<Call_Category>();
        HYTD.BLL.Call_CategoryBLL ccbll = new HYTD.BLL.Call_CategoryBLL();
        ccList = ccbll.GetCall_CategoryList().Where(c => c.C_ID == ID).ToList();
        return ccList.Count()>0? ccList[0].C_Name.ToString():"";
    }
    private void getCustomerList()
    {
        strCallWorkBillStatusDrop = PublicMethod.getWorkBillStatusCallCategory(2, false, false, 27);// PublicEnum.EnumBindList_Client<PublicEnum.CallWorkBillStatus>(false, false, intStatus);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        HYTD.BLL.Call_CustomerBLL bll = new HYTD.BLL.Call_CustomerBLL();
        string strKeyWord = string.Empty;
        if (!string.IsNullOrEmpty(Request["keyword"]))
        {
            strKeyWord = Request["keyword"].ToString().Trim();
        }
        System.Collections.Generic.List<Models.Call_Customer> list = bll.GetCall_CustomerList(strKeyWord);
        foreach (var m in list)
        {
            sb.Append("{ \"name\":\"" + m.CC_Name + "\", \"to\": \"" + m.CC_ID + "\" },");
        }
        strCumstomers = "[" + sb.ToString().Trim(',') + "]";
        sb.Clear();
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
    /// 获取类别
    /// </summary>
    /// <returns></returns>
    public string GetCategory(int CID)
    {
        //StringBuilder sb = new StringBuilder();
        //string str = PublicEnum.EnumBindList_Client<PublicEnum.CallWorkBillType>(true, false, intSelected);
        //sb.Append("<option value='-1'>全部</option>");
        //sb.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", 1, "来电");
        //sb.AppendFormat("<option value='{0}'>{1}</option>", 2, "上门");
        //sb.AppendFormat("<option value='{0}'>{1}</option>", 3, "回访");
        string strWorkBillDrop = PublicMethod.getWorkBillStatusCallCategory(5, true, false, intSelected);
        return strWorkBillDrop;
    }
    private string getDataFormate(string strDate)
    {
        string strRet = string.Empty;
        DateTime dtime = DateTime.MinValue;
        DateTime.TryParse(strDate, out dtime);
        if (dtime > DateTime.MinValue)
            strRet = dtime.ToString("yyyy-MM-dd HH:mm:ss");
        return strRet;

    }
}