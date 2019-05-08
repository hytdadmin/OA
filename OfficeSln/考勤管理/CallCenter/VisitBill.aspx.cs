using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BLL;
using HYTD.Common;

public partial class CallCenter_VisitBill : PageBase
{
    private int VID = 0;
    private int CID = 0;
    public int WID = 0;
    public int HuiFang = 0;
    public string strWorkBillDrop = string.Empty;
    public string strCallServiceTypeDrop = string.Empty;
    public string strCallConsultSoftwareDrop = string.Empty;
    public string strCallConsultTypeDrop = string.Empty;
    public string strCallWorkBillStatusDrop = string.Empty;
    public Call_VisitBill vModel = new Call_VisitBill();
    public Call_Customer cModel = new Call_Customer();
    int intWorkBill = 1;
    int intServiceType = 1;
    int intSoftWare = 1;
    int intConsultType = 1;
    int intStatus = 33;
    int intUserCode = 0;
    public int intForUser = 0;
    public int intOperater = 0;
    public int intCreater = 0;
    public string strUserName = string.Empty;
    public string strCreateTime = string.Empty;
    public string strOptionTime = string.Empty;
    public string strVisitTime = string.Empty;

    public string strCreateUser = string.Empty;
    public string strForUser = string.Empty;

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
        if (Request["vid"] != null && Request["vid"].ToString() != "")
        {
            int.TryParse(Request["vid"].ToString(), out VID);
        }
        if (Request["hf"] != null && Request["hf"].ToString() != "")
        {
            int.TryParse(Request["hf"].ToString(), out HuiFang);
        }
        int.TryParse(GetCurrentUserInfo.UserCode, out intOperater);
        BindData();
        getDropdownList();
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
        strCallWorkBillStatusDrop = PublicMethod.getWorkBillStatusCallCategory(3, false, false, 33);// PublicEnum.EnumBindList_Client<PublicEnum.CallWorkBillStatus>(false, false, intStatus);
        strWorkBillDrop = PublicMethod.getWorkBillStatusCallCategory(1,false, false, intWorkBill);
        strCallServiceTypeDrop = PublicMethod.getCallCategory(false, false, intServiceType);
        //strCreateUser = PublicMethod.UserInfoList(intUserCode);
        strForUser = PublicMethod.UserInfoList(intForUser);
    }

    private void BindData()
    {
        HYTD.BLL.Call_VisitBillBLL VBll = new HYTD.BLL.Call_VisitBillBLL();
        HYTD.BLL.Call_CustomerBLL CBll = new HYTD.BLL.Call_CustomerBLL();

        HYTD.BLL.Call_WorkBillBLL WBll = new HYTD.BLL.Call_WorkBillBLL();
        Call_WorkBill wbModel = new Call_WorkBill();
        if (VID > 0)
        {

            //获取工单信息
            vModel = VBll.GetCall_VisitBillEntity(VID);
            WID = vModel.CVB_CWB_ID;
        }
        else if (WID > 0)
        {
            wbModel = WBll.GetCall_WorkBillEntity(WID);
            vModel.CVB_CallInEmail = wbModel.CWB_CallInEmail;
            vModel.CVB_CallInTel = wbModel.CWB_CallInTel;
            vModel.CVB_CallInUserName = wbModel.CWB_CallInUserName;
            vModel.CVB_CallType = wbModel.CWB_CallType;
            vModel.CVB_CCID = wbModel.CWB_CCID;
            vModel.CVB_Creater = wbModel.CWB_Creater;
            vModel.CVB_CreateTime = wbModel.CWB_CreateTime;
            vModel.CVB_Description = wbModel.CWB_Description;
            vModel.CVB_ForUser = wbModel.CWB_ForUser;
            vModel.CVB_OperationTime = wbModel.CWB_OperationTime;
            vModel.CVB_OperationUser = wbModel.CWB_OperationUser;
            vModel.CVB_Remark = wbModel.CWB_Remark;
            vModel.CVB_ServiceType = wbModel.CWB_ServiceType;
            vModel.CVB_SoftType = wbModel.CWB_SoftType;
            vModel.CVB_Solution = wbModel.CWB_Solution;
            vModel.CVB_Status = wbModel.CWB_Status;
            vModel.CVB_Type = wbModel.CWB_Type;
        }

        if (vModel == null)
            vModel = new Call_VisitBill();
        CID = vModel.CVB_CCID;
        intWorkBill = vModel.CVB_Type;
        if (vModel.CVB_ServiceType != null)
            intServiceType = vModel.CVB_ServiceType.Value;
        if (vModel.CVB_SoftType != null)
            intSoftWare = vModel.CVB_SoftType.Value;
        if (vModel.CVB_CallType != null)
            intConsultType = vModel.CVB_CallType.Value;
        if (vModel.CVB_Status!=null)
        intStatus = vModel.CVB_Status.Value;
          
        UserInfoBLL userBll = new UserInfoBLL();
        strUserName = userBll.GetUserInfoEntityByUserCode(vModel.CVB_Creater);
        strCreateTime = getDateToString(vModel.CVB_CreateTime);
        if (vModel.CVB_OperationTime != null)
            strOptionTime = getDateToString(vModel.CVB_OperationTime.Value);
        else
            strOptionTime = getDateToString(DateTime.Now);
        intCreater = vModel.CVB_Creater;
        if (vModel.CVB_ForUser != null && vModel.CVB_ForUser.Value > 0)
            intForUser = vModel.CVB_ForUser.Value;
        if (vModel.CVB_VisitTime != null)
            strVisitTime = getDateToString(vModel.CVB_VisitTime.Value);
        else
            strVisitTime = getDateToString(DateTime.Now);
        //else
        //{
        //    strUserName = CurrentUserInfo.UserName;
        //    strOptionTime = getDateToString(DateTime.Now);
        //    strCreateTime = strOptionTime;
        //    intCreater = Convert.ToInt32(CurrentUserInfo.UserCode);
        //    intForUser = intCreater;
        //}
        //获取客户信息
        cModel = CBll.GetCall_CustomerEntity(CID);
        if (cModel == null)
            cModel = new Call_Customer();
        strUserName = CurrentUserInfo.UserName;
    }

    private string getDateToString(DateTime dtime)
    {
        if (dtime > Convert.ToDateTime("1981-01-01"))
        {
            return dtime.ToString("yyyy-MM-dd HH:mm");
        }
        return "";
    }
}