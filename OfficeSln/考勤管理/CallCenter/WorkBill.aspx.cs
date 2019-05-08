using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using HYTD.Common;
using BLL;

public partial class CallCenter_WorkBill : PageBase
{
    private int CID = 0;
    public int WID = 0;
    public int HuiFang = 0;
    public string strWorkBillDrop = string.Empty;
    public string strCallServiceTypeDrop = string.Empty;
    public string strCallConsultSoftwareDrop = string.Empty;
    public string strCallConsultTypeDrop = string.Empty;
    public string strCallWorkBillStatusDrop = string.Empty;
    public Call_WorkBill wModel = new Call_WorkBill();
    public Call_Customer cModel = new Call_Customer();
    public int intCounts = 0;
    int intWorkBill = 1;
    int intServiceType = 1;
    int intSoftWare = 1;
    int intConsultType = 1;
    int intStatus = 28;
    int intUserCode = 0;
    int intForUser = 0;
    public int intOperater = 0;
    public int intCreater = 0;
    public string strUserName = string.Empty;
    public string strCreateTime = string.Empty;
    public string strOptionTime = string.Empty;
    public string strBillCode = string.Empty;

    public string strCreateUser = string.Empty;
    public string strForUser = string.Empty;
    public string strFAQList = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["cid"] != null && Request["cid"].ToString() != "")
        {
            int.TryParse(Request["cid"].ToString(), out CID);
        }
        if (Request["wid"] != null && Request["wid"].ToString() != "")
        {
            int.TryParse(Request["wid"].ToString(), out WID);
        }
        if (Request["hf"] != null && Request["hf"].ToString() != "")
        {
            int.TryParse(Request["hf"].ToString(), out HuiFang);
        }
        int.TryParse(GetCurrentUserInfo.UserCode, out intOperater);
        BindData();
        getDropdownList();
        getFAQList();
    }

    private void getDropdownList()
    {
        //工单类型
        //strWorkBillDrop = PublicEnum.EnumBindList_Client<PublicEnum.CallWorkBillType>(false, false, intWorkBill);
        //服务类型
        //strCallServiceTypeDrop = PublicEnum.EnumBindList_Client<PublicEnum.CallServiceType>(false, false, intServiceType);
        ////咨询软件
        //strCallConsultSoftwareDrop = PublicEnum.EnumBindList_Client<PublicEnum.CallConsultSoftware>(false, false, intSoftWare);
        ////咨询类型 
        //strCallConsultTypeDrop = PublicEnum.EnumBindList_Client<PublicEnum.CallConsultType>(false, false, intConsultType);
        //工单状态
        strCallWorkBillStatusDrop = PublicMethod.getWorkBillStatusCallCategory(2, false, false, intStatus);//PublicEnum.EnumBindList_Client<PublicEnum.CallWorkBillStatus>(false, false, intStatus);
        strWorkBillDrop = PublicMethod.getWorkBillStatusCallCategory(1, false, false, intWorkBill);
        strCallServiceTypeDrop = PublicMethod.getCallCategory(false, false, intServiceType);
        //strCreateUser = PublicMethod.UserInfoList(intUserCode);
        strForUser = PublicMethod.UserInfoList(intForUser);
    }

    private void BindData()
    {
        HYTD.BLL.Call_WorkBillBLL WBll = new HYTD.BLL.Call_WorkBillBLL();
        HYTD.BLL.Call_CustomerBLL ccBll = new HYTD.BLL.Call_CustomerBLL();

        HYTD.BLL.Call_CustomerBLL CBll = new HYTD.BLL.Call_CustomerBLL();
        //Call_Customer ccModel = new Call_Customer();
        if (CID > 0)
        {
            cModel = ccBll.GetCall_CustomerEntity(CID);
        }
        if (cModel == null)
            cModel = new Call_Customer();
        if (WID > 0)
        {

            //获取工单信息
            wModel = WBll.GetCall_WorkBillEntity(WID);
            if (wModel == null)
                wModel = new Call_WorkBill();

            CID = wModel.CWB_CCID;
           
            intWorkBill = wModel.CWB_Type;
            if (wModel.CWB_ServiceType != null)
                intServiceType = wModel.CWB_ServiceType.Value;
            if (wModel.CWB_SoftType != null)
                intSoftWare = wModel.CWB_SoftType.Value;
            if (wModel.CWB_CallType != null)
                intConsultType = wModel.CWB_CallType.Value;
            intStatus = wModel.CWB_Status;
            UserInfoBLL userBll = new UserInfoBLL();
            strUserName = userBll.GetUserInfoEntityByUserCode(wModel.CWB_Creater);
            strCreateTime = getDateToString(wModel.CWB_CreateTime);
            if (wModel.CWB_OperationTime != null)
                strOptionTime = getDateToString(wModel.CWB_OperationTime.Value);
            else
                strOptionTime = getDateToString(DateTime.Now);
            intCreater = wModel.CWB_Creater;
            if (wModel.CWB_ForUser != null && wModel.CWB_ForUser.Value > 0)
                intForUser = wModel.CWB_ForUser.Value;
            strBillCode = wModel.CWB_Code;
        }
        else
        {
            wModel.CWB_CallInEmail = cModel.CC_Email;
            wModel.CWB_CallInTel = cModel.CC_Tel;
            wModel.CWB_CallInUserName = cModel.CC_UserName;

            strUserName = CurrentUserInfo.UserName;
            strOptionTime = getDateToString(DateTime.Now);
            strCreateTime = strOptionTime;
            intCreater = Convert.ToInt32(CurrentUserInfo.UserCode);
            if (cModel != null)
                intForUser = Convert.ToInt32(cModel.CC_Owner);
        }
        //获取客户信息
        cModel = CBll.GetCall_CustomerEntity(CID);
        if (cModel == null)
            cModel = new Call_Customer();
        if (CID > 0)
            intCounts = WBll.GetCall_WorkBillCounts(CID);

    }

    private string getDateToString(DateTime dtime)
    {
        if (dtime > Convert.ToDateTime("1981-01-01"))
        {
            return dtime.ToString("yyyy-MM-dd HH:mm");
        }
        return "";
    }
    private void getFAQList()
    {
        //strCallWorkBillStatusDrop = PublicMethod.getWorkBillStatusCallCategory(2, false, false, 27);// PublicEnum.EnumBindList_Client<PublicEnum.CallWorkBillStatus>(false, false, intStatus);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //HYTD.BLL.Call_CustomerBLL bll = new HYTD.BLL.Call_CustomerBLL();
        //string strKeyWord = string.Empty;
        //if (!string.IsNullOrEmpty(Request["keyword"]))
        //{
        //    strKeyWord = Request["keyword"].ToString().Trim();
        //}
        //System.Collections.Generic.List<Models.Call_Customer> list = bll.GetCall_CustomerList(strKeyWord);
        //foreach (var m in list)
        //{
        //    sb.Append("{ \"name\":\"" + m.CC_Name + "\", \"to\": \"" + m.CC_ID + "\" },");
        //}
        //strCumstomers = "[" + sb.ToString().Trim(',') + "]";
        sb.Clear();
        HYTD.BLL.Call_FAQListBLL uBll = new HYTD.BLL.Call_FAQListBLL();
        List<Models.Call_FAQList> FAQList = new List<Call_FAQList>();
        FAQList = uBll.GetCall_FAQListList();
        foreach (var m in FAQList)
        {
            sb.Append("{ \"name\":\"" + m.CF_ErrorList + "\", \"to\": \"" + m.CF_ID + "\" },");
        }
        strFAQList = "[" + sb.ToString().Trim(',') + "]";
    }
}