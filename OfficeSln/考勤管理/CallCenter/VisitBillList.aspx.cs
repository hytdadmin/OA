using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Models;
using System.Text;
using System.Data;
using HYTD.Model.TO;
using HYTD.Common;
using System.Configuration;

public partial class CallCenter_VisitBillList : PageBase
{
    public string script = string.Empty;
    public int pageIndex = 1;
    public int pageSize = 10;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        UserRights1.intPage = 2;
        UserInfo model = new PageBase().CurrentUserInfo;
        if (ConfigurationManager.AppSettings["Users"] != null)
        {
            string[] strlist = ConfigurationManager.AppSettings["Users"].ToString().Trim().Split(',');
            if (strlist.Where(c => c == model.UserCode.ToString()).ToList().Count > 0)
            {

            }
            else
            {
                Response.Write("<script>alert('您无权操作此页面！');window.location.href='/Index.aspx';</script>");
                this.Response.End();
            }
        }            else
            {
                Response.Write("<script>alert('您无权操作此页面！');window.location.href='/Index.aspx';</script>");
                this.Response.End();
            }
        if (!IsPostBack)
        {
            //strStratTime = DateTime.Now.ToString("yyyy-MM-dd");
            //strLoginUserName = CurrentUserInfo.UserName;
            DataBind();
            getCustomerList();
            sUrl = string.Format("?uname={0}&stime={1}", CurrentUserInfo.UserName, DateTime.Now.ToString("yyyy-MM") + "-01");
            sUrlNew = string.Format("?stime={0}", DateTime.Now.ToString("yyyy-MM") + "-01");
        }
    }

    private void DataBind()
    {
        Call_VisitBillTO sto = new Call_VisitBillTO();
        #region 条件
        if (!string.IsNullOrEmpty(Request["cname"]))
        {
            sto.CVB_Remark = Request["cname"].ToString().Trim();//存客户名称
        }
        if (!string.IsNullOrEmpty(Request["uname"]))
        {
            sto.CVB_Solution = Request["uname"].ToString().Trim();//存用户名称
        }
        else
            sto.CVB_Solution = strLoginUserName;
        if (!string.IsNullOrEmpty(Request["Owner"])) //存 客户所属工程师
        {

            sto.CVB_Description = Request["Owner"].ToString().Trim();
        }
        if (!string.IsNullOrEmpty(Request["stime"]))
        {
            DateTime dtStime = Convert.ToDateTime("1980-01-01");
            DateTime.TryParse(Request["stime"].ToString().Trim(), out dtStime);
            sto.CVB_CreateTime = dtStime;//保存操作开始时间
        }
        if (!string.IsNullOrEmpty(Request["etime"]))
        {
            DateTime dtEtime = Convert.ToDateTime("2250-01-01");
            DateTime.TryParse(Request["etime"].ToString().Trim(), out dtEtime);
            sto.CVB_OperationTime = dtEtime;//保存操作结束时间
        }
        if (!string.IsNullOrEmpty(Request["sltType"]))
        {
            sto.CVB_Type = Convert.ToInt16(Request["sltType"].ToString().Trim());
            intSelected = sto.CVB_Type;
        }

        #endregion
        if (!string.IsNullOrEmpty(Request.QueryString["pageIndex"]))
        {
            int.TryParse(Request.QueryString["pageIndex"], out pageIndex);
        }
        HYTD.BLL.Call_VisitBillBLL bll = new HYTD.BLL.Call_VisitBillBLL();
        DataTable dt = bll.GetCall_VisitBillList(sto, pageIndex, pageSize, "", out rowCount);
        //分页
        string getUrl = "";
        if (Request.Url.Query.Length > 0 && Request.Url.Query.IndexOf("pageIndex") > -1 && Request.Url.Query.IndexOf("&") > -1)
        {
            getUrl = Request.Url.Query.Substring(Request.Url.Query.IndexOf("&"));
        }
        else if (Request.Url.Query.Length > 0)
            getUrl = Request.Url.Query.Substring(1);
        string url = "VisitBillList.aspx?pageIndex={0}&" + getUrl.Trim('&');
        strtt = DividePage.Pager(pageSize, rowCount, pageIndex, url);

        if (dt.Rows.Count > 0)
        {
            StringBuilder sbContent = new StringBuilder();
            string strNew = string.Empty;
            string strEdit = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                strNew = string.Empty; ;
                strEdit = string.Format("<a href=\"#\" name=\"EditWorkBill\" onclick=\"EditWorkBill({0},{1})\">修改</a>", dr["CVB_ID"].ToString(), dr["CVB_CWB_ID"].ToString());
                sbContent.AppendFormat("<tbody>");
                sbContent.AppendFormat("<tr>");
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["C_Name"].ToString());
                sbContent.AppendFormat("<td class=\"by\" style=\"width:120px;\" title=\"{0}\">{0}&nbsp;</td>", dr["CC_Name"]);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CWB_CallInUserName"]);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CWB_CallInTel"]);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CreateUserName"]);
                sbContent.AppendFormat("<td class=\"num\" style=\"width:100px;\" title=\"{0}\">{0}&nbsp;</td>", getDataFormate(dr["CVB_CreateTime"].ToString()));
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["ServiceUserName"]);
                sbContent.AppendFormat("<td class=\"num\" style=\"width:100px;\" title=\"{0}\">{0}&nbsp;</td>", getDataFormate(dr["CVB_OperationTime"].ToString()));
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CVB_Status"].ToString());
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", "服务记录");
                sbContent.AppendFormat("<td class=\"num\">{0}&nbsp;</td>", strNew + strEdit);
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

    private void getCustomerList()
    {
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
        string strWorkBillDrop = PublicMethod.getWorkBillStatusCallCategory(1,true, false, intSelected);
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