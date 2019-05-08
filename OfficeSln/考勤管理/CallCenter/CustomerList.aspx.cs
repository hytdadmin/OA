using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HYTD.Common;
using System.Data;
using HYTD.Model.TO;
using System.Configuration;
using Models;

public partial class CallCenter_CustomerList : System.Web.UI.Page
{
    public string script = string.Empty;
    public int pageIndex = 1;
    public int pageSize = 10;
    public int rowCount = 400;
    public int pageNum = 1;
    public string strtt = string.Empty;
    public string strCumstomers = string.Empty;
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
                Response.Write("<script>alert('您无权操作此页面！');window.location.href='/Index.aspx';</script>");
                this.Response.End();
            }
        }            else
            {
                Response.Write("<script>alert('您无权操作此页面！');window.location.href='/Index.aspx';</script>");
                this.Response.End();
            }
        UserRights1.intPage = 7;
        getCustomers();
        DataBind();
    }
    private void getCustomers()
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
    }
    private void DataBind()
    {
        Call_CustomerTO sto = new Call_CustomerTO();
        #region 条件
        if (!string.IsNullOrEmpty(Request["cname"]))
        {
            sto.CC_Name = Request["cname"].ToString().Trim();//存客户名称
        }
        if (!string.IsNullOrEmpty(Request["uname"]))
        {
            sto.CC_UserName = Request["uname"].ToString().Trim();//存用户名称
        }
        if (!string.IsNullOrEmpty(Request["Owner"])) //存 客户所属工程师
        {

            sto.CC_Owner = Request["Owner"].ToString().Trim();
        }

        #endregion
        if (!string.IsNullOrEmpty(Request.QueryString["pageIndex"]))
        {
            pageIndex = int.Parse(Request.QueryString["pageIndex"]);
        }
        HYTD.BLL.Call_CustomerBLL bll = new HYTD.BLL.Call_CustomerBLL();
        DataTable dt = bll.GetCall_CustomerList(sto, pageIndex, pageSize, "", out rowCount);
        //分页
        string url = "CustomerList.aspx?pageIndex={0}&cname=" + sto.CC_Name + "&uname=" + sto.CC_UserName + "&owner=" + sto.CC_Owner;
        strtt = DividePage.Pager(pageSize, rowCount, pageIndex, url);

        if (dt.Rows.Count > 0)
        {
            StringBuilder sbContent = new StringBuilder();
            string strNew = string.Empty;
            string strEdit = string.Empty; sbContent.AppendFormat("<tbody>");
            foreach (DataRow dr in dt.Rows)
            {
                //strNew = string.Format("<a href=\"javascript:;\" onclick=\"EditCustomerInfo({0})\"  target=\"_block\">新建</a>", dr["CC_ID"].ToString());
                strEdit = string.Format("<a href=\"javascript:void(0);\" onclick=\"EditCustomerInfo({0})\">修改</a>", dr["CC_ID"].ToString());

                sbContent.AppendFormat("<tr>");
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["UserName"]);
                sbContent.AppendFormat("<td class=\"by\" title=\"{0}\">[{0}]{1}&nbsp;</td>", dr["CC_ID"], dr["CC_Name"]);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CC_UserName"]);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CC_Tel"]);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", getDateTime(dr["CC_CreateTime"].ToString(),0));
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", getDateTime(dr["CC_ServiceStartTime"].ToString(),1));
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", getDateTime(dr["CC_ServiceEndTime"].ToString(), 1));
                //sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CC_Url"]);
                //sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", dr["CC_Remark"]);
                sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>",getStatusName( dr["CC_Status"].ToString().Trim()));
                sbContent.AppendFormat("<td class=\"num\" >{0}&nbsp;</td>", strNew + strEdit);
                sbContent.AppendFormat("</tr>");

            }
            sbContent.AppendFormat("</tbody>");
            script = sbContent.ToString();
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
    private string getDateTime(string strTime,int intType)
    {
        DateTime dtime = DateTime.MinValue;
        string strResult = string.Empty;
        DateTime.TryParse(strTime, out dtime);
        if (intType==1)
        strResult = dtime.ToString("yyyy-MM-dd");
        else
            strResult = dtime.ToString("yyyy-MM-dd HH:mm");
        return strResult;
    }

    private string getStatusName(string strStatus)
    {
        string strResult = string.Empty;
        switch (strStatus)
        { 
            case "1":
                strResult = "可用";
                break;
            case "0":
                strResult = "停用";
                break;
            case "2":
                strResult = "删除";
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

        //sb.Append("<option value='-1'>全部</option>");
        //sb.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", 1, "大学");
        //sb.AppendFormat("<option value='{0}'>{1}</option>", 2, "政府");
        string str = PublicEnum.EnumBindList_Client<PublicEnum.CallCunstomerType>(true, false, 1);
        return str;
    }
}