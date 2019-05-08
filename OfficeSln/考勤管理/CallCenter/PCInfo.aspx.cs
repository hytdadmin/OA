using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using JINCHENG.DBUtility;
using Active.Tools;
using System.Text;
using System.Configuration;

public partial class CallCenter_PCInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strErrorCode = string.Empty;
        string strUserInfo = string.Empty;
        string userName = string.Empty;
        string userTel = string.Empty;
        string[] strUserInfoList;        
        if (Request["info"] != null)
        {
            if (Request["uinfo"] != null)
            {
                strUserInfo = Request["uinfo"].ToString().Trim();
                strUserInfoList = strUserInfo.Split(new[] { "$$" }, StringSplitOptions.None);
                userName = strUserInfoList[0];
                userTel = strUserInfoList[1];
            }
            string strUrl = Request["info"].ToString();
            try
            {
                string[] strList = strUrl.Split(new[] { "$$" }, StringSplitOptions.None);
                int intCustomerID = 0;//strList[0]
                string strCustomerID = string.Empty;
                strErrorCode = getErrorCode(strUrl);
                try
                {
                    strCustomerID = DESEncrypt.Decrypt_DES(strList[0]);
                }
                catch (Exception ex)
                {
                   
                }
                int.TryParse(strCustomerID, out intCustomerID);
                string strFileName = intCustomerID + "_" + strList[2];
                byte[] sourcebytes = System.Text.Encoding.UTF8.GetBytes(strUrl);
                byte[] targetbytes = System.Text.Encoding.Convert(Encoding.Unicode, Encoding.UTF8, sourcebytes);
                string UTF8string = System.Text.Encoding.UTF8.GetString(targetbytes);
            }
            catch (Exception ex)
            {
                WebLog.WriteLog(string.Format("获取回传数据报错：{0}", ex.Message+ex.HelpLink+ex.InnerException+ex.Source+ex.StackTrace+ex.TargetSite),"Error");
            }
            toList(strUrl, strErrorCode, userName, userTel);
        }
    }
    public void toList(string strMessage, string strErrorCode, string UserName, string UserTel)
    {
        try
        {
            int intCustomerID = 0;//strList[0]
            //strUserName.Split(new[] { "cdc" }, StringSplitOptions.None);
            //string[] strList = System.Text.RegularExpressions.Regex.Split(strUserName, @"$#$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            string[] strList = strMessage.Split(new[] { "$$" }, StringSplitOptions.None);
            string strLoginName = string.Empty;
            string strProductID = "000000";
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerID", SqlDbType.Int),
                    new SqlParameter("@ProductID", SqlDbType.VarChar,50),
                    new SqlParameter("@ErrorMessage", SqlDbType.VarChar,8000),
                    new SqlParameter("@LoginName", SqlDbType.VarChar,50),
                    new SqlParameter("@isSuccessfull", SqlDbType.Int),
                    new SqlParameter("@ErrorCode", SqlDbType.VarChar,50),
                    new SqlParameter("@IP", SqlDbType.VarChar,50),
                    new SqlParameter("@UserName", SqlDbType.VarChar,50),
                    new SqlParameter("@UserTel", SqlDbType.VarChar,50)
					};
            if (strList.Length > 3)
            {

                string strCustomerID = string.Empty;
                try
                {
                    strCustomerID = DESEncrypt.Decrypt_DES(strList[0]);
                }
                catch (Exception ex)
                { }
                int.TryParse(strCustomerID, out intCustomerID);
                if (intCustomerID < 1)
                {
                    try
                    {
                        strCustomerID = DESEncrypt.Decrypt_DES_Old(strList[0]);
                    }
                    catch (Exception ex)
                    { }

                    int.TryParse(strCustomerID, out intCustomerID);
                }
                int intSuccess = 0;
                int.TryParse(strList[3], out intSuccess);
                parameters[0].Value = intCustomerID;
                parameters[1].Value = strList[1];
                parameters[2].Value = strMessage.Replace("'", "”").Replace("-", "——");
                parameters[3].Value = strList[2];
                parameters[4].Value = intSuccess;
                parameters[5].Value = strErrorCode;
                parameters[6].Value = getIP(strList[4]);
                parameters[7].Value = UserName;
                parameters[8].Value = UserTel;
                strLoginName = strList[2].Trim();
                strProductID = strList[1].Trim();
            }
            else
            {
                parameters[0].Value = -1;
                parameters[1].Value = "0000";
                parameters[2].Value = "Error";
                parameters[3].Value = strMessage;
                parameters[4].Value = 0;
                parameters[5].Value = "无";
                parameters[7].Value = UserName;
                parameters[8].Value = UserTel;
            }
            DataSet ds = SqlHelperService.RunProcedure("UP_ActiveProductInfo_ADD", parameters, "ds");
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(UserTel))
            {
                HYTD.BLL.Call_WorkBillBLL wbll = new HYTD.BLL.Call_WorkBillBLL();
                List<Models.Call_WorkBill> wModelList = wbll.GetCall_WorkBillList(intCustomerID, strProductID, strLoginName);
                if (wModelList != null && wModelList.Count < 1)
                {
                    string strMessageNew = strMessage;
                    if (strMessage.IndexOf("验证成功") > -1)
                        strMessageNew = strMessage.Substring(strMessage.IndexOf("验证成功") + 4);
                    CreateWorkBill(intCustomerID, strProductID, strLoginName, UserName, UserTel, strMessageNew);
                }
            }
        }
        catch (Exception ex)
        {
            WebLog.WriteLog(string.Format("添加数据库报错：{0}", ex.Message + ex.HelpLink + ex.InnerException + ex.Source + ex.StackTrace + ex.TargetSite), "Error");
        }

    }
    private string getIP(string str)
    {
        string[] strList = str.Trim().Split(new char[] { '#', '$' });
        if (strList.Length > 3)
        {
            return strList[3].Trim();
        }
        else
            return "未获到IP";
    }
    private string getErrorCode(string strMessage)
    {
        string strCode = string.Empty;
        string strErrorCode = "0x";
        if (ConfigurationManager.AppSettings["ErrorCode"] != null && ConfigurationManager.AppSettings["ErrorCode"].ToString() != "")
            strErrorCode = ConfigurationManager.AppSettings["ErrorCode"].ToString().Trim();
        string[] strList = strErrorCode.Split(new char['|'], System.StringSplitOptions.RemoveEmptyEntries);
        foreach (string str in strList)
        {
            if (string.IsNullOrEmpty(strCode) && strMessage.IndexOf(str) > -1)
            {
                strCode = strMessage.Substring(strMessage.IndexOf(str), 10);
            }
        }
        return strCode;
    }

    //如果填写了用户姓名和电话则创建一个工单
    private void CreateWorkBill(int intCCID, string strProductID, string strLoginName, string strCallInUserName, string strCallInTel, string strDiscription)
    {
        HYTD.BLL.Call_CustomerBLL ccbll = new HYTD.BLL.Call_CustomerBLL();
        Models.Call_Customer ccModel = new Models.Call_Customer();
        ccModel = ccbll.GetCall_CustomerEntity(intCCID);
        if (ccModel != null)
        {
            HYTD.BLL.Call_WorkBillBLL wbll = new HYTD.BLL.Call_WorkBillBLL();
            Models.Call_WorkBill wModel = new Models.Call_WorkBill();
            wModel.CWB_Status = 28;
            wModel.CWB_Type = 35;
            wModel.CWB_SoftType = 0;
            wModel.CWB_ServiceType = 10;
            wModel.CWB_CallType = 0;
            wModel.CWB_CCID = intCCID;
            wModel.CWB_CallInUserName = strCallInUserName;
            wModel.CWB_CallInTel = strCallInTel;
            wModel.CWB_CallInEmail = "";
            wModel.CWB_OperationTime = DateTime.Now;
            int intOwner = 0;
            int.TryParse(ccModel.CC_Owner, out intOwner);
            wModel.CWB_ForUser = intOwner;
            wModel.CWB_OperationUser = intOwner;
            wModel.CWB_CreateTime = wModel.CWB_OperationTime.Value;
            wModel.CWB_Creater = 16;
            wModel.CWB_Description = strDiscription;
            wModel.CWB_Remark = "用户主动上传";
            wModel.CWB_CallInLoginName = strLoginName;
            wModel.CWB_ProductID = strProductID;
            wModel.CWB_Code = wbll.WorkBillCreateCode();

            try
            {
                wbll.AddCall_WorkBill(wModel);
            }
            catch (Exception ex)
            {
            }
        }
    }
}