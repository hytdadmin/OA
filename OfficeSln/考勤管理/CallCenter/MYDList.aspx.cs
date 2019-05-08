using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Text;
using BLL;
using Models;
using HYTD.Common;
using HYTD.Model.TO;

public partial class CallCenter_MYDList : PageBase
{
    public string strError = string.Empty;
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
            UserRights1.intPage = 1;
            sUrl = string.Format("?uname={0}&stime={1}", CurrentUserInfo.UserName, DateTime.Now.ToString("yyyy-MM") + "-01");
            sUrlNew = string.Format("?stime={0}", DateTime.Now.ToString("yyyy-MM") + "-01");
        }
    }

    private void DataBind()
    {
        Call_SatisfactionNewTO sto = new Call_SatisfactionNewTO();
        #region 条件
        //if (!string.IsNullOrEmpty(Request["code"]))
        //{
        //    sto.CWB_Code = Request["code"].ToString().Trim();//存 客户名称
        //}
        //if (!string.IsNullOrEmpty(Request["cname"]))
        //{
        //    sto.CWB_Remark = Request["cname"].ToString().Trim();//存 客户名称
        //}
        //if (!string.IsNullOrEmpty(Request["uname"]))
        //{
        //    sto.CWB_Solution = Request["uname"].ToString().Trim();//存 用户名称
        //}
        //else
        //    sto.CWB_Solution = strLoginUserName;
        //if (!string.IsNullOrEmpty(Request["stime"]))
        //{
        //    DateTime dtStime = Convert.ToDateTime("1980-01-01");
        //    DateTime.TryParse(Request["stime"].ToString().Trim(), out dtStime);
        //    sto.CWB_CreateTime = dtStime;//保存操作开始时间
        //}
        //if (!string.IsNullOrEmpty(Request["etime"]))
        //{
        //    DateTime dtEtime = Convert.ToDateTime("2250-01-01");
        //    DateTime.TryParse(Request["etime"].ToString().Trim(), out dtEtime);
        //    sto.CWB_OperationTime = dtEtime;//保存操作结束时间
        //}

        //if (!string.IsNullOrEmpty(Request["Owner"])) //存 客户所属工程师
        //{

        //    sto.CWB_Description = Request["Owner"].ToString().Trim();
        //}
        //if (!string.IsNullOrEmpty(Request["sltType"]))
        //{
        //    sto.CWB_Type = Convert.ToInt16(Request["sltType"].ToString().Trim());
        //    intSelected = sto.CWB_Type;
        //}
        //if (!string.IsNullOrEmpty(Request["status"]))
        //{
        //    int intStatus = 0;
        //    int.TryParse(Request["status"].ToString().Trim(), out intStatus);

        //    sto.CWB_Status = intStatus;
        //    intSelected = sto.CWB_Type;
        //}
        #endregion
        if (!string.IsNullOrEmpty(Request.QueryString["pageIndex"]))
        {
            int.TryParse(Request.QueryString["pageIndex"], out pageIndex);
        }
        HYTD.BLL.Call_SatisfactionNewBLL bll = new HYTD.BLL.Call_SatisfactionNewBLL();
        DataTable dt = bll.GetCall_SatisfactionNewList(sto, pageIndex, pageSize, "", out rowCount);
        //分页
        string getUrl = "";
        if (Request.Url.Query.Length > 0 && Request.Url.Query.IndexOf("pageIndex") > -1 && Request.Url.Query.IndexOf("&") > -1)
        {
            getUrl = Request.Url.Query.Substring(Request.Url.Query.IndexOf("&"));
        }
        else if (Request.Url.Query.Length > 0)
            getUrl = Request.Url.Query.Substring(1);
        string url = "MYDList.aspx?pageIndex={0}&" + getUrl.Trim('&');
        strtt = DividePage.Pager(pageSize, rowCount, pageIndex, url);

        if (dt.Rows.Count > 0)
        {
            StringBuilder sbContent = new StringBuilder();
            string strNew = string.Empty;
            string strEdit = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                strEdit = string.Format("<a href=\"javascript:;\" name=\"EditWorkBill\" onclick=\"EditWorkBill({0})\">查看</a>", dr["CSN_ID"].ToString());
                //if (dr["CWB_Status"].ToString() != ((int)PublicEnum.CallWorkBillStatus.Visited).ToString())
                //    strEdit += string.Format("&nbsp;<a href=\"javascript:;\" name=\"EditWorkBill\" onclick=\"HuifangWorkBill({0})\">回访</a>", dr["CSN_ID"].ToString());
                //sbContent.AppendFormat("<tbody>");
                //if (dr["CWB_Status"].ToString() == "服务中")
                //    sbContent.AppendFormat("<tr style=\"color:red;\">");
                //else
                //    sbContent.AppendFormat("<tr>");
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\"><a href=\"javascript:;\" >{0}</a></td>", dr["CC_Name"].ToString() );
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CSN_UserIP"].ToString());
                sbContent.AppendFormat("<td class=\"by\" style=\"width:120px;\" title=\"{0}\">{0}&nbsp;</td>", dr["CSN_UserUrl"]);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CSN_UserCode"]);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CSN_ServiceUserCode"]);
                sbContent.AppendFormat("<td class=\"num\" style=\"width:100px;\" title=\"{0}\">{0}&nbsp;</td>", getDataFormate(dr["CSN_CreateDate"].ToString()));
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CSN_IsSolve"]=="0"?"未解决":"已解决");
                int intI = 0;
                int.TryParse(dr["CSN_ServiceEvaluation"].ToString(),out intI);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>",getResultForDC( intI));
                intI = 0;
                int.TryParse(dr["CSN_ServiceEfficiency"].ToString(), out intI);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", getResultForDC(intI));
                intI = 0;
                int.TryParse(dr["CSN_ServiceAttitude"].ToString(), out intI);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", getResultForDC(intI));
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CSN_Improvement"]);
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

    private string getResultForDC(int intFenShu)
    {
        string strResult = string.Empty;
        switch (intFenShu)
        { 
            case 1:
                strResult = "非常满意";
                    break;
            case 2:
                    strResult = "满意";
                    break;
            case 3:
                    strResult = "一般";
                    break;
            case 4:
                    strResult = "不满意";
                    break;
        }
        return strResult;
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
        string strWorkBillDrop = PublicMethod.getWorkBillStatusCallCategory(1, true, false, intSelected);
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
    #region 导出excel
    protected void Button3_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();// strWhere = "1=1";
        sb.Append("1=1");
        string browser = Request.Browser.Browser;
        if (!string.IsNullOrEmpty(Request["code"]))
        {
            sb.AppendFormat(" and CWB_Code like '%{0}%'", Request["code"].ToString().Trim());//;;//存 客户名称
        }
        if (!string.IsNullOrEmpty(Request["cname"]))
        {
            sb.AppendFormat(" and b.CC_Name like '%{0}%'", Request["cname"].ToString().Trim());//存 客户名称
        }
        if (!string.IsNullOrEmpty(Request["uname"]))
        {
            sb.AppendFormat(" and d.UserName like '%{0}%'", Request["uname"].ToString().Trim());//存 用户名称
        }
        if (!string.IsNullOrEmpty(Request["stime"]))
        {
            DateTime dtStime = Convert.ToDateTime("1980-01-01");
            DateTime.TryParse(Request["stime"].ToString().Trim(), out dtStime);
            sb.AppendFormat(" and CWB_CreateTime > '{0}'", dtStime.AddDays(-1));//保存操作开始时间
        }
        if (!string.IsNullOrEmpty(Request["etime"]))
        {
            DateTime dtEtime = Convert.ToDateTime("2250-01-01");
            DateTime.TryParse(Request["etime"].ToString().Trim(), out dtEtime);
            sb.AppendFormat(" and CWB_CreateTime < '{0}'", dtEtime.AddDays(1));//保存操作结束时间
        }
        if (!string.IsNullOrEmpty(Request["Owner"])) //存 客户所属工程师
        {

            sb.AppendFormat(" and CWB_Description = '{0}'", Request["Owner"].ToString().Trim());
        }
        if (!string.IsNullOrEmpty(Request["sltType"]) && Request["sltType"].ToString().Trim() != "0")
        {
            sb.AppendFormat(" and CWB_Type = '{0}'", Convert.ToInt16(Request["sltType"].ToString().Trim()));
        }
        if (!string.IsNullOrEmpty(Request["status"]))
        {
            int intStatus = 0;
            int.TryParse(Request["status"].ToString().Trim(), out intStatus);
            int intStatus1 = 27;
            int.TryParse(ConfigurationManager.AppSettings["workbillStatusAll"].ToString(), out intStatus1);
            if (intStatus1 == intStatus)
            {

            }
            else
            {
                sb.AppendFormat(" and CWB_Status = '{0}'", intStatus);
            }

        }

        DataTable dt = new DataTable();
        HYTD.BLL.Call_WorkBillBLL bll = new HYTD.BLL.Call_WorkBillBLL();
        dt = bll.ImportExcelCustomerWorkBill(sb.ToString()).Tables[0];
        NPOI.SS.UserModel.IWorkbook book1 = new ImportExcel().ImportCustomerWorkBill(dt, "客户服务记录");
        string strFile = string.Empty;
        string filename = string.Empty;
        string strPath = HttpContext.Current.Server.MapPath("/" + System.Configuration.ConfigurationManager.AppSettings["CustomerFiles"].ToString().Replace("\\", "/") + "/")
           + "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + "\\";
        if (dt.Rows.Count > 0)
        {
            strFile = dt.Rows[0]["客户名称"].ToString();
            filename = strPath + dt.Rows[0]["客户名称"].ToString() + ".xls";
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filename)))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filename));
            }
            try
            {
                System.IO.FileStream file1 = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                book1.Write(file1);

                file1.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        Response.Clear();
        Response.BufferOutput = false;
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        if (browser.ToLower() != "firefox")
        {
            Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFile) + ".xls");
        }
        else
        {
            Response.AddHeader("Content-Disposition", "attachment;filename=" + strFile + ".xls");
        }
        Response.ContentType = "application/ms-excel";
        book1.Write(Response.OutputStream);
        book1.Dispose();
    }

    #endregion
}