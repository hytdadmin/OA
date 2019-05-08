<%@ WebHandler Language="C#" Class="CallCenter" %>

using System;
using System.Web;
using System.Data;
public class CallCenter : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string strOption = string.Empty;
        if (!string.IsNullOrEmpty(context.Request["option"]))
            strOption = context.Request["option"].ToString().Trim();
        WebLog.WriteCallCenterMYDLog(DateTime.Now + " strOption:" + strOption);
        switch (strOption)
        {
            case "edit":
                EditWorkBill(context);
                break;
            case "edithf":
                EditVisitBill(context);
                break;
            case "customer":
                getCustomerList(context);
                break;
            case "editcustomer":
                EditCustomerInfo(context);
                break;
            case "manyidu":
                ManYiDuInfo(context);
                break;
            case "myd":
                ManYiDuDiaoCha(context);
                break;
        }
    }
    private void ManYiDuDiaoCha(HttpContext ctx)
    {
        HYTD.BLL.Call_SatisfactionNewBLL bll = new HYTD.BLL.Call_SatisfactionNewBLL();
        Models.Call_SatisfactionNew model = new Models.Call_SatisfactionNew();
        WebLog.WriteCallCenterMYDLog(DateTime.Now + " 开始执行方法:ManYiDuDiaoCha");
        try
        {
            string[] strList = null;
            if (!string.IsNullOrEmpty(ctx.Request["info"]))
            {
                strList = ctx.Request["info"].ToString().Trim().Split(new string[] { "$$" }, StringSplitOptions.None);
            }
            else
            {
                WebLog.WriteCallCenterMYDLog(string.Format("获取ctx.Request[\"info\"]失败："));
            }
            //为了避免重复提交 同一个IP1个小时内只能提交一次

            if (strList != null)
            {
                WebLog.WriteCallCenterMYDLog(string.Format("获取用户提交数据：{0}", ctx.Request["info"].Trim()));
                if (checkRepeat(strList[6].Trim()))
                {
                    WebLog.WriteCallCenterMYDLog(string.Format("请勿重复提交,提交IP：{0}", strList[6].Trim()));
                    ctx.Response.Write("请勿重复提交！");
                }
                else
                {
                    int intI = 0;
                    model.CSN_CC_ID = 0;
                    model.CSN_CreateDate = DateTime.Now;
                    int.TryParse(strList[0].Trim(), out intI);//是否解决
                    model.CSN_IsSolve = intI;
                    intI = 0;
                    int.TryParse(strList[1].Trim(), out intI);//您对工程师的服务及时性如何评价
                    model.CSN_ServiceEvaluation = intI;
                    intI = 0;
                    int.TryParse(strList[2].Trim(), out intI);//您对工程师解决问题的效率如何评价
                    model.CSN_ServiceEfficiency = intI;
                    intI = 0;
                    int.TryParse(strList[3].Trim(), out intI);//您工程师服务态度如何评价
                    model.CSN_ServiceAttitude = intI;
                    intI = 0;
                    //为您服务的工程师编号
                    model.CSN_ServiceUserCode = strList[4].Trim();
                    model.CSN_Improvement = strList[5].Trim();
                    model.CSN_UserIP = strList[6].Trim();
                    int.TryParse(strList[7].Trim(), out intI);//客户ID
                    model.CSN_CC_ID = intI == 0 ? 87 : intI;
                    model.CSN_UserCode = "16";
                    bll.AddCall_SatisfactionNew(model);
                    WebLog.WriteCallCenterMYDLog(string.Format("提交成功"));
                    ctx.Response.Write("提交成功！");
                }
            }
            else
            {
                WebLog.WriteCallCenterMYDLog(string.Format("您提交的数据可能不完整，提交失败"));
                ctx.Response.Write("您提交的数据可能不完整，提交失败！");
            }
        }
        catch (Exception ex)
        {
            WebLog.WriteCallCenterMYDLog(string.Format("提交时报程序错误,提交失败:{0}", ex.Message + ex.Source + ex.StackTrace + ex.TargetSite + ex.InnerException + ex.HelpLink));
            ctx.Response.Write("提交失败！");
        }
    }
    private bool checkRepeat(string strIP)
    {
        bool bolResult = false;
        HYTD.BLL.Call_SatisfactionNewBLL bll = new HYTD.BLL.Call_SatisfactionNewBLL();
        System.Collections.Generic.List<Models.Call_SatisfactionNew> modelList = new System.Collections.Generic.List<Models.Call_SatisfactionNew>();
        modelList = bll.GetCall_SatisfactionNewList(strIP.Trim());
        if (modelList != null && modelList.Count > 0)
        {
            bolResult = true;
        }
        return bolResult;

    }
    private void ManYiDuInfo(HttpContext ctx)
    {
        HYTD.BLL.Call_WorkBillBLL wbll = new HYTD.BLL.Call_WorkBillBLL();
        Models.Call_WorkBill wModel = new Models.Call_WorkBill();
        Models.Call_Satisfaction sModel = new Models.Call_Satisfaction();
        HYTD.BLL.Call_SatisfactionBLL csBll = new HYTD.BLL.Call_SatisfactionBLL();
        HYTD.BLL.Call_Satisfaction_ItemBLL csiBll = new HYTD.BLL.Call_Satisfaction_ItemBLL();
        Models.Call_Satisfaction_Item siModel = new Models.Call_Satisfaction_Item();
        string strCallInUserName = string.Empty;
        string strCallInTel = string.Empty;
        string strCallInEmail = string.Empty;
        int intStatus = 0;
        //int intCreater = 0;
        int intForUser = 0;
        int intWID = 0;
        int intOperater = 0;
        int intHF = 0;
        if (!string.IsNullOrEmpty(ctx.Request.Form["hdnHF"]))
        {
            int.TryParse(ctx.Request.Form["hdnHF"].ToString(), out intHF);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["hdnWID"]))
        {
            int.TryParse(ctx.Request.Form["hdnWID"].ToString(), out intWID);
        }
        if (intWID > 0)
        {
            wModel = wbll.GetCall_WorkBillEntity(intWID);

            if (!string.IsNullOrEmpty(ctx.Request.Form["htnOperater"]))
            {
                int.TryParse(ctx.Request.Form["htnOperater"].ToString(), out intOperater);
            }
            if (!string.IsNullOrEmpty(ctx.Request.Form["sltUser"]))
            {
                int.TryParse(ctx.Request.Form["sltUser"].ToString(), out intForUser);
            }
            if (!string.IsNullOrEmpty(ctx.Request.Form["sltStatus"]))
            {
                int.TryParse(ctx.Request.Form["sltStatus"].ToString(), out intStatus);
            }

            wModel.CWB_MYDStatus = intStatus;
            //wModel.CWB_OperationTime = DateTime.Now;
            //wModel.CWB_OperationUser = intOperater;

            //满意度表
            sModel = csBll.GetCall_SatisfactionEntityBYCWBID(wModel.CWB_ID);
            if (sModel == null)
                sModel = new Models.Call_Satisfaction();
            sModel.CS_CWB_ID = wModel.CWB_ID;
            sModel.CS_CreateDate = DateTime.Now;
            sModel.CS_UserCode = intOperater.ToString();
            if (!string.IsNullOrEmpty(ctx.Request.Form["txtBZ"]))
            {
                if (sModel.CS_Remark != null && sModel.CS_Remark.ToString().Trim().Length > 0)
                    sModel.CS_Remark += "<br />";
                sModel.CS_Remark += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<br />" + ctx.Request.Form["txtBZ"].ToString().Trim();
            }
            if (!string.IsNullOrEmpty(ctx.Request.Form["txtYJ"]))
            {
                if (sModel.CS_Satisfaction != null && sModel.CS_Satisfaction.ToString().Trim().Length > 0)
                    sModel.CS_Satisfaction += "<br />";
                sModel.CS_Satisfaction += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<br />" + ctx.Request.Form["txtYJ"].ToString().Trim();
            }

            //调查项目 评分表
            siModel = csiBll.GetCall_Satisfaction_ItemEntityBYCSID(sModel.CS_ID);
            if (siModel == null)
                siModel = new Models.Call_Satisfaction_Item();
            siModel.CSI_type = 1;
            siModel.CSI_Name = "公司服务满意度评价";
            if (!string.IsNullOrEmpty(ctx.Request.Form["RadioButtonList1"]))
            {
                int intResult = 0;
                int.TryParse(ctx.Request.Form["RadioButtonList1"].ToString().Trim(), out intResult);
                siModel.CSI_Result = intResult;
            }
            //siModel.CSI_Result = 1; 

            try
            {
                if (intWID > 0)
                {
                    if (sModel.CS_ID == 0)
                        csBll.AddCall_Satisfaction(sModel);
                    else
                        csBll.UpdateCall_Satisfaction(sModel);
                    siModel.CSI_CS_ID = sModel.CS_ID;
                    if (siModel.CSI_ID == 0)
                        csiBll.AddCall_Satisfaction_Item(siModel);
                    else
                        csiBll.UpdateCall_Satisfaction_Item(siModel);
                    wbll.UpdateCall_WorkBill(wModel);
                }

                //wbll.UpdateCall_WorkBill(wModel);
                ctx.Response.Write("成功");
            }
            catch (Exception ex)
            {
                ctx.Response.Write("失败");
            }
        }
        else
        {
            ctx.Response.Write("没有找到工单，请重试");
        }
    }

    private void EditCustomerInfo(HttpContext ctx)
    {
        int intForUser = 0;
        HYTD.BLL.Call_CustomerBLL bll = new HYTD.BLL.Call_CustomerBLL();
        Models.Call_Customer cModel = new Models.Call_Customer();
        int intCID = 0;
        if (!string.IsNullOrEmpty(ctx.Request.Form["hdnCID"]))
        {
            int.TryParse(ctx.Request.Form["hdnCID"].ToString(), out intCID);
        }
        if (intCID > 0)
            cModel = bll.GetCall_CustomerEntity(intCID);
        if (!string.IsNullOrEmpty(ctx.Request.Form["sltUser"]))
        {
            int.TryParse(ctx.Request.Form["sltUser"].ToString(), out intForUser);
            cModel.CC_Owner = intForUser.ToString();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["cName"]))
        {
            cModel.CC_Name = ctx.Request.Form["cName"].ToString().Trim();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["httime"]))
        {
            DateTime dthttime = DateTime.Now.AddYears(-100);
            DateTime.TryParse(ctx.Request.Form["httime"].ToString().Trim(), out dthttime);
            cModel.CC_HTTime = dthttime;
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtSSTime"]))
        {
            DateTime dtSSTime = DateTime.Now.AddYears(-100);
            DateTime.TryParse(ctx.Request.Form["txtSSTime"].ToString().Trim(), out dtSSTime);
            cModel.CC_ServiceStartTime = dtSSTime;
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtSETime"]))
        {
            DateTime dtSETime = DateTime.Now.AddYears(-100);
            DateTime.TryParse(ctx.Request.Form["txtSETime"].ToString().Trim(), out dtSETime);
            cModel.CC_ServiceEndTime = dtSETime;
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtUpdateTime"]))
        {
            DateTime dtUpdateTime = DateTime.Now.AddYears(-100);
            DateTime.TryParse(ctx.Request.Form["txtUpdateTime"].ToString().Trim(), out dtUpdateTime);
            cModel.CC_UpdateTime = dtUpdateTime;
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtOtherTel"]))
        {
            cModel.CC_OtherTel = ctx.Request.Form["txtOtherTel"].ToString().Trim();
        }
        else
        {
            cModel.CC_OtherTel = "";
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtuserName"]))
        {
            cModel.CC_UserName = ctx.Request.Form["txtuserName"].ToString().Trim();
        }
        else
        {
            cModel.CC_UserName = "";
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtccTel"]))
        {
            cModel.CC_Tel = ctx.Request.Form["txtccTel"].ToString().Trim();
        }
        else
        {
            cModel.CC_Tel = "";
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtVistion"]))
        {
            cModel.CC_Vistion = ctx.Request.Form["txtVistion"].ToString().Trim();
        }
        else
        {
            cModel.CC_Vistion = "";
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtToEmailuserName"]))
        {
            cModel.CC_EmailUserName = ctx.Request.Form["txtToEmailuserName"].ToString().Trim();
        }
        else
        {
            cModel.CC_EmailUserName = "";
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtToEmail"]))
        {
            cModel.CC_Email = ctx.Request.Form["txtToEmail"].ToString().Trim();
        }
        else
        {
            cModel.CC_Email = "";
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtToEmailCCAddr"]))
        {
            cModel.CC_CCEmail = ctx.Request.Form["txtToEmailCCAddr"].ToString().Trim();
        }
        else
        {
            cModel.CC_CCEmail = "";
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtRemark"]))
        {
            cModel.CC_Remark = ctx.Request.Form["txtRemark"].ToString().Trim();
        }
        else
        {
            cModel.CC_Remark = "";
        }
        try
        {
            if (intCID > 0)
            {
                bll.UpdateCall_Customer(cModel);
                ctx.Response.Write("成功");
                //cModel.CC_CreateTime = DateTime.Now;
                //bll.AddCall_Customer(cModel);
            }
            else
                ctx.Response.Write("保存失败，未找到客户ID");

        }
        catch (Exception ex)
        {
            ctx.Response.Write("失败" + ex.Message);
        }
    }

    /// <summary>
    /// 回访工单
    /// </summary>
    /// <param name="ctx"></param>
    private void EditVisitBill(HttpContext ctx)
    {
        HYTD.BLL.Call_VisitBillBLL wbll = new HYTD.BLL.Call_VisitBillBLL();
        Models.Call_VisitBill wModel = new Models.Call_VisitBill();
        int intCCID = 0;
        string strCallInUserName = string.Empty;
        string strCallInTel = string.Empty;
        string strCallInEmail = string.Empty;
        int intType = 0;
        int intSoftType = 0;
        int intCallInType = 0;
        int intServiceType = 0;
        int intStatus = 0;
        int intCreater = 0;
        int intForUser = 0;
        int intVID = 0;
        int intWID = 0;
        int intOperater = 0;
        int intHF = 0;
        if (!string.IsNullOrEmpty(ctx.Request.Form["hdnHF"]))
        {
            int.TryParse(ctx.Request.Form["hdnHF"].ToString(), out intHF);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["hdnVID"]))
        {
            int.TryParse(ctx.Request.Form["hdnVID"].ToString(), out intVID);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["hdnWID"]))
        {
            int.TryParse(ctx.Request.Form["hdnWID"].ToString(), out intWID);
        }
        if (intVID > 0)
            wModel = wbll.GetCall_VisitBillEntity(intVID);
        if (!string.IsNullOrEmpty(ctx.Request.Form["htnOperater"]))
        {
            int.TryParse(ctx.Request.Form["htnOperater"].ToString(), out intOperater);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["htnCreater"]))
        {
            int.TryParse(ctx.Request.Form["htnCreater"].ToString(), out intCreater);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["sltUser"]))
        {
            int.TryParse(ctx.Request.Form["sltUser"].ToString(), out intForUser);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["hdnCCID"]))
        {
            int.TryParse(ctx.Request.Form["hdnCCID"].ToString(), out intCCID);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["sltStatus"]))
        {
            int.TryParse(ctx.Request.Form["sltStatus"].ToString(), out intStatus);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["sltSoftWare"]))
        {
            int.TryParse(ctx.Request.Form["sltSoftWare"].ToString(), out intSoftType);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["sltWorkBillType"]))
        {
            int.TryParse(ctx.Request.Form["sltWorkBillType"].ToString(), out intType);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["sltServiceType"]))
        {
            int.TryParse(ctx.Request.Form["sltServiceType"].ToString(), out intServiceType);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["sltSolutionType"]))
        {
            int.TryParse(ctx.Request.Form["sltSolutionType"].ToString(), out intCallInType);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtCallInUserName"]))
        {
            strCallInUserName = ctx.Request.Form["txtCallInUserName"].ToString();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtCallInTel"]))
        {
            strCallInTel = ctx.Request.Form["txtCallInTel"].ToString();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtCallInEmail"]))
        {
            strCallInEmail = ctx.Request.Form["txtCallInEmail"].ToString();
        }
        WebLog.WriteCallCenterLog("获取值完成");
        wModel.CVB_Status = intStatus;
        wModel.CVB_Type = intType;
        wModel.CVB_SoftType = intSoftType;
        wModel.CVB_ServiceType = intServiceType;
        wModel.CVB_CallType = intCallInType;
        wModel.CVB_CCID = intCCID;
        wModel.CVB_CallInUserName = strCallInUserName;
        wModel.CVB_CallInTel = strCallInTel;
        wModel.CVB_CallInEmail = strCallInEmail;
        wModel.CVB_OperationTime = DateTime.Now;
        wModel.CVB_ForUser = intForUser;
        wModel.CVB_OperationUser = intOperater;
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtDescription"]))
        {
            wModel.CVB_Description = ctx.Request.Form["txtDescription"].ToString().Trim();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtSolution"]))
        {
            wModel.CVB_Solution = ctx.Request.Form["txtSolution"].ToString().Trim();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtRemark"]))
        {
            wModel.CVB_Remark = ctx.Request.Form["txtRemark"].ToString().Trim();
        }
        try
        {

            if (intVID == 0)
            {
                WebLog.WriteCallCenterLog("是创建");
                wModel.CVB_Creater = intCreater;
                wModel.CVB_CreateTime = DateTime.Now;
                wModel.CVB_CWB_ID = intWID;
                //wbll.AddCall_WorkBill(wModel);
            }
            else
            {
                //wbll.UpdateCall_WorkBill(wModel);
            }
            WebLog.WriteCallCenterLog("开始执行插入回访表方法 intHF" + intHF);
            wbll.AddANDUpdataCallVisitBill(wModel, intHF);
            WebLog.WriteCallCenterLog("结束执行插入回访表方法");
            HYTD.BLL.Call_WorkBillBLL workbillbll = new HYTD.BLL.Call_WorkBillBLL();
            Models.Call_WorkBill model = new Models.Call_WorkBill();
            WebLog.WriteCallCenterLog("修改工单状态的ID：" + wModel.CVB_CWB_ID);
            model = workbillbll.GetCall_WorkBillEntity(wModel.CVB_CWB_ID);

            model.CWB_Status = (int)HYTD.Common.PublicEnum.CallWorkBillStatus.Visited;
            WebLog.WriteCallCenterLog("开始执行更新工单表状态方法");
            workbillbll.UpdateCall_WorkBill(model);
            WebLog.WriteCallCenterLog("结束执行更新工单表状态方法");
            ctx.Response.Write("Ok");
        }
        catch (Exception ex)
        {
            WebLog.WriteCallCenterLog("添加回访报错:" + ex.Message + ex.Source + ex.StackTrace + ex.TargetSite + ex.InnerException + ex.HelpLink);
            ctx.Response.Write("error");
        }
    }
    private void getCustomerList(HttpContext ctx)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        HYTD.BLL.Call_CustomerBLL bll = new HYTD.BLL.Call_CustomerBLL();
        string strKeyWord = string.Empty;
        if (!string.IsNullOrEmpty(ctx.Request["keyword"]))
        {
            strKeyWord = ctx.Request["keyword"].ToString().Trim();
        }
        System.Collections.Generic.List<Models.Call_Customer> list = bll.GetCall_CustomerList(strKeyWord);
        foreach (var m in list)
        {
            sb.Append("{ \"name\":\"" + m.CC_Name + "\", \"to\": \"" + m.CC_ID + "\" },");
        }
        //
        ctx.Response.Write("[" + sb.ToString().Trim(',') + "]");
    }

    private void EditWorkBill(HttpContext ctx)
    {
        HYTD.BLL.Call_WorkBillBLL wbll = new HYTD.BLL.Call_WorkBillBLL();
        Models.Call_WorkBill wModel = new Models.Call_WorkBill();
        int intCCID = 0;
        string strCallInUserName = string.Empty;
        string strCallInTel = string.Empty;
        string strCallInEmail = string.Empty;
        int intType = 0;
        int intSoftType = 0;
        int intCallInType = 0;
        int intServiceType = 0;
        int intStatus = 0;
        int intCreater = 0;
        int intForUser = 0;
        int intWID = 0;
        int intOperater = 0;
        int intHF = 0;
        if (!string.IsNullOrEmpty(ctx.Request.Form["hdnHF"]))
        {
            int.TryParse(ctx.Request.Form["hdnHF"].ToString(), out intHF);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["hdnWID"]))
        {
            int.TryParse(ctx.Request.Form["hdnWID"].ToString(), out intWID);
        }
        if (intWID > 0)
            wModel = wbll.GetCall_WorkBillEntity(intWID);
        if (!string.IsNullOrEmpty(ctx.Request.Form["htnOperater"]))
        {
            int.TryParse(ctx.Request.Form["htnOperater"].ToString(), out intOperater);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["htnCreater"]))
        {
            int.TryParse(ctx.Request.Form["htnCreater"].ToString(), out intCreater);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["sltUser"]))
        {
            int.TryParse(ctx.Request.Form["sltUser"].ToString(), out intForUser);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["hdnCCID"]))
        {
            int.TryParse(ctx.Request.Form["hdnCCID"].ToString(), out intCCID);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["sltStatus"]))
        {
            int.TryParse(ctx.Request.Form["sltStatus"].ToString(), out intStatus);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["sltSoftWare"]))
        {
            int.TryParse(ctx.Request.Form["sltSoftWare"].ToString(), out intSoftType);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["sltWorkBillType"]))
        {
            int.TryParse(ctx.Request.Form["sltWorkBillType"].ToString(), out intType);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["sltServiceType"]))
        {
            int.TryParse(ctx.Request.Form["sltServiceType"].ToString(), out intServiceType);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["sltSolutionType"]))
        {
            int.TryParse(ctx.Request.Form["sltSolutionType"].ToString(), out intCallInType);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtCallInUserName"]))
        {
            strCallInUserName = ctx.Request.Form["txtCallInUserName"].ToString();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtCallInTel"]))
        {
            strCallInTel = ctx.Request.Form["txtCallInTel"].ToString();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtCallInEmail"]))
        {
            strCallInEmail = ctx.Request.Form["txtCallInEmail"].ToString();
        }
        wModel.CWB_Status = intStatus;
        wModel.CWB_Type = intType;
        wModel.CWB_SoftType = intSoftType;
        wModel.CWB_ServiceType = intServiceType;
        wModel.CWB_CallType = intCallInType;
        wModel.CWB_CCID = intCCID;
        wModel.CWB_CallInUserName = strCallInUserName;
        wModel.CWB_CallInTel = strCallInTel;
        wModel.CWB_CallInEmail = strCallInEmail;
        wModel.CWB_OperationTime = DateTime.Now;
        wModel.CWB_ForUser = intForUser;
        wModel.CWB_OperationUser = intOperater;
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtDescription"]))
        {
            wModel.CWB_Description = ctx.Request.Form["txtDescription"].ToString().Trim();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtSolution"]))
        {
            wModel.CWB_Solution = ctx.Request.Form["txtSolution"].ToString().Trim();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtRemark"]))
        {
            wModel.CWB_Remark = ctx.Request.Form["txtRemark"].ToString().Trim();
        }
        try
        {
            if (intWID == 0)
            {
                wModel.CWB_MYDStatus = 36;
                wModel.CWB_Code = wbll.WorkBillCreateCode();
                wModel.CWB_Creater = intCreater;
                wModel.CWB_CreateTime = DateTime.Now;
                //wbll.AddCall_WorkBill(wModel);
            }
            else
            {
                //wbll.UpdateCall_WorkBill(wModel);
            }
            wbll.AddANDUpdataCallWorkBill(wModel, intHF);
            ctx.Response.Write("成功");
        }
        catch (Exception ex)
        {
            ctx.Response.Write("失败");
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}