using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HYTD.Common;
using BLL;

public partial class CallCenter_ServiceHistoryListForCustomer : System.Web.UI.Page
{
    public string script = string.Empty;
    private int WID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["wid"] != null && Request["wid"].ToString() != "")
        {
            int.TryParse(Request["wid"].ToString(), out WID);

            getDataBind();
        }
    }

    private void getDataBind()
    {
        StringBuilder sbContent = new StringBuilder();
        UserInfoBLL uBll = new UserInfoBLL();
        HYTD.BLL.Call_WorkBillHistoryBLL bll = new HYTD.BLL.Call_WorkBillHistoryBLL();
        List<Models.Call_WorkBillHistory> list = new List<Models.Call_WorkBillHistory>();
        list = bll.GetCall_WorkBillHistoryList().Where(c => c.CWBH_CWB_ID == WID).ToList();
        foreach (var m in list)
        {
            sbContent.AppendFormat("<tbody>");
            sbContent.AppendFormat("<tr>");
            sbContent.AppendFormat("<td class=\"by\" title=\"{0}\">{0}&nbsp;</td>", m.CWBH_Description);
            sbContent.AppendFormat("<td class=\"by\" title=\"{0}\">{0}&nbsp;</td>", m.CWBH_Solution);
            sbContent.AppendFormat("<td class=\"by\" title=\"{0}\">{0}&nbsp;</td>", m.CWBH_Remark);
            sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", PublicEnum.GetEnumDescription<PublicEnum.CallWorkBillStatus>(m.CWBH_Status.ToString()));
            sbContent.AppendFormat("<td class=\"num\" title=\"{0}\">{0}&nbsp;</td>", m.CWBH_OperationTime);
            sbContent.AppendFormat("<td class=\"num\">{0}&nbsp;</td>",uBll.GetUserInfoEntityByUserCode( m.CWBH_UserID.Value));
            int intUserID = 0;
            if (m.CWBH_OperationUser != null)
                intUserID = m.CWBH_OperationUser.Value;
            sbContent.AppendFormat("<td class=\"num\">{0}&nbsp;</td>", uBll.GetUserInfoEntityByUserCode(intUserID));
            sbContent.AppendFormat("</tr>");
            sbContent.AppendFormat("</tbody>");
        }

        script = sbContent.ToString();
    }
}