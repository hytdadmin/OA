using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using HYTD.Common;
using BLL;

public partial class CallCenter_FAQ : PageBase
{
    public Call_FAQList cModel = new Call_FAQList();
    private int FID = 0;
    public string strOperation = string.Empty;
    public string strFAQInput = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["cid"] != null && Request["cid"].ToString() != "")
        {
            int.TryParse(Request["cid"].ToString(), out FID);
        }
        if (Request["op"] != null && Request["op"].ToString() != "")
        {
            strOperation = Request["op"].ToString();
        }
        //int.TryParse(GetCurrentUserInfo.UserCode, out CreateUserID);
        if (strOperation != "Add")
        {
            BindData();
        }
        else
        {
            GetCategory(-1);
        }
        getFAQList();
    }

    private void BindData()
    {
        HYTD.BLL.Call_FAQListBLL ccBll = new HYTD.BLL.Call_FAQListBLL();

        HYTD.BLL.Call_CustomerBLL CBll = new HYTD.BLL.Call_CustomerBLL();
        if (FID > 0)
        {
            cModel = ccBll.GetCall_FAQListEntity(FID);
        }
        if (cModel == null)
            cModel = new Call_FAQList();
        GetCategory(cModel.CF_SoftTypeID);
    }

    /// <summary>
    /// 获取类别
    /// </summary>
    /// <returns></returns>
    public string GetCategory(int TypeID)
    {
        string strWorkBillDrop = PublicMethod.getWorkBillStatusCallCategory(5, true, false, TypeID);
        return strWorkBillDrop;
    }
    private void getFAQList()
    {
        //strCallWorkBillStatusDrop = PublicMethod.getWorkBillStatusCallCategory(2, false, false, 27);// PublicEnum.EnumBindList_Client<PublicEnum.CallWorkBillStatus>(false, false, intStatus);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        
        UserInfoBLL uBll = new UserInfoBLL();
        List<Models.UserInfo> userList = new List<UserInfo>();
        userList = uBll.GetUserInfoList();
        foreach (var m in userList)
        {
            sb.Append("{ \"name\":\"" + m.UserName + "\", \"to\": \"" + m.UserCode + "\" },");
        }
        strFAQInput = "[" + sb.ToString().Trim(',') + "]";
    }
}