using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Data;
using System.Configuration;

public partial class CallCenter_CustomerInfo : System.Web.UI.Page
{
    HYTD.BLL.Call_CustomerBLL ccBll = new HYTD.BLL.Call_CustomerBLL();
    public Call_Customer cModel = new Call_Customer();
    HYTD.BLL.Call_WorkBillBLL WBll = new HYTD.BLL.Call_WorkBillBLL();
    public int CID = 0;
    public int intCounts = 0;
    public int intServiceCounts = 0;
    public string strForUser = string.Empty;
    public string strLastServiceTime = string.Empty;
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
        if (Request["cid"] != null && Request["cid"].ToString() != "")
        {
            int.TryParse(Request["cid"].ToString(), out CID);
            getDateModel(CID);
        }
    }

    private void getDateModel(int intCID)
    {
        cModel = ccBll.GetCall_CustomerEntity(intCID);
        if (intCID > 0)
            intCounts = WBll.GetCall_WorkBillCounts(CID);
        strForUser = PublicMethod.UserInfoList(cModel.CC_Owner);
        int intCount = 0;
       DataTable dt=  WBll.GetCall_WorkBillList("CWB_CCID=" + CID, 1, 1, "CWB_ID desc", out intCount);
       if (dt.Rows.Count > 0)
       { 
           DateTime dtime = DateTime.MinValue;
           DateTime.TryParse(dt.Rows[0]["CWB_CreateTime"].ToString().Trim(),out dtime);
           strLastServiceTime = dtime.ToString("yyyy-MM-dd HH:mm");
       }

    }
}