<%@ WebHandler Language="C#" Class="FAQListManage" %>

using System;
using System.Web;
using System.Data;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Net.Mime;
using System.IO;
using System.Timers;
using System.Xml;
using System.Configuration;

public class FAQListManage : IHttpHandler {
    int intcounts = 0;
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";

        string strOption = string.Empty;
        if (!string.IsNullOrEmpty(context.Request["option"]))
            strOption = context.Request["option"].ToString().Trim();
        WebLog.WriteCallCenterMYDLog(DateTime.Now + " strOption:" + strOption);
        switch (strOption)
        {
            case "Add":
                AddFAQBill(context);
                break;
            case "Edit":
                EditFAQBill(context);
                break;
            case "Delete":
                DeleteFAQBill(context);
                break;
            case "GetFaqList":
                GetFaqList(context);
                break;
            case "GetFaqContent":
                GetFaqContent(context);
                break;
            case "SendEmail":
                SendEmail(context);
                break;

            case "upload":
                upload(context);
                break;
                
        }
    }
    private void SendEmail(HttpContext ctx)
    {
        string Serverfilepath = ctx.Request["uploadfile"];
        string mailTo = string.Empty;
        string mailCC = string.Empty;
        string mailBCC = string.Empty;
        //string mailCC_Name = string.Empty;
        string mailCC_UserName = string.Empty;
        string mailtxtContent = string.Empty;
        string strDate = System.DateTime.Now.ToString();
        mailTo = ctx.Request["txtCallInEmail"];
        if (ConfigurationManager.AppSettings["mailCC"] != null)
        {
            mailCC = ConfigurationManager.AppSettings["mailCC"].ToString();
        }
        if (ConfigurationManager.AppSettings["mailBCC"] != null)
        {
            mailBCC = ConfigurationManager.AppSettings["mailBCC"].ToString();
        }
        mailCC_UserName = ctx.Request["CallInUserName"];
        mailtxtContent = ctx.Request["EContent"];
        if (Serverfilepath.Trim()=="0")
        {
            Serverfilepath = "";
        }
        SendMailInfoSet(false, mailTo, mailCC, mailBCC, strDate, ctx, mailCC_UserName, mailtxtContent, Serverfilepath);
    }
    private void upload(HttpContext ctx)
    {
        try
        {
            HttpFileCollection files = ctx.Request.Files;//这里只能用<input type="file" />才能有效果,因为服务器控件是HttpInputFile类型
            string error = string.Empty;
            if (files.Count > 0)
            {
                if (files[0].FileName != "")
                {
                    string NewFile = System.Web.HttpContext.Current.Server.MapPath("/CallCenter/SendEmailFile/") + DateTime.Now.ToString("MM-dd-HH-mm") + System.IO.Path.GetFileName(files[0].FileName);
                    files[0].SaveAs(NewFile);
                    //msg = " 成功! 文件大小为:" + files[0].ContentLength;
                    //imgurl = "/" + files[0].FileName;
                    //string res = "{msg:'" + msg + "',imgurl:'" + imgurl + "'}";
                    //string res = "[{'msg':'" + msg + "','url':'" + NewFile + "'}]";
                    ctx.Response.Write(NewFile);
                }
                else
                {
                    ctx.Response.Write("0");
                }
            }
        }
        catch (Exception ex)
        {
            ctx.Response.Write("失败");
        }
    }
    private void SendMailInfoSet(bool bolIsTest, string mailTo, string mailCC, string mailBCC, string strDate, HttpContext ctx, string mailCC_UserName, string mailtxtContent, string Emailfilepath)
    {
        string strTitle = string.Empty;
        string strBody = string.Empty;
        string strFilesPath = string.Empty;
        string mailCCTO = mailCC;
        //HYTD.BLL.Call_CustomerBLL bll = new HYTD.BLL.Call_CustomerBLL();
        //Models.Call_Customer mCustomer = new Models.Call_Customer();
        //List<Models.Call_Customer> mCustomerList = new List<Models.Call_Customer>();
        //mCustomerList = bll.GetCall_CustomerList().Where(c => c.CC_IsSend == 11).ToList();

        if (mailTo != null && mailTo.Trim().Length > 0)
        {
            //获取附件 如果没有附件则不添加
            //DataSet ds = new DataSet();
            //HYTD.BLL.Call_WorkBillBLL bllworkbill = new HYTD.BLL.Call_WorkBillBLL();
            //string strWhere = string.Format("CWB_CCID={0} and convert(varchar(7),CWB_CreateTime,120)='{1}'", 99, strDate.Substring(0, 7));
            //ds = bllworkbill.ImportExcelCustomerWorkBill(strWhere);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    strFilesPath = getFile(ds.Tables[0]);
            //    string strTypeInfo = string.Empty;
            //    strTypeInfo = getTypeInfo(ds.Tables[1]);
            //    strTitle = string.Format("{0}微软正版化平台{1}份服务解决方案", mailCC_Name, Convert.ToDateTime(strDate).ToString("yyyy年MM月"));
            //    strBody = string.Format(@"");
            //}
            //else
            {
                strFilesPath = Emailfilepath;
                strTitle = string.Format("微软正版化平台{0}份服务解决方案", Convert.ToDateTime(strDate).ToString("yyyy年MM月"));
                strBody = string.Format(@"");
            }

            strBody = mailtxtContent;
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //if (bolIsTest)
            //    strTitle = "测试邮件：" + strTitle;
            sendMail(mailTo, mailCC_UserName, mailCC,mailBCC, strTitle, strBody, strFilesPath, ctx);
            //}
        }

    }
    private void sendMail(string mailTo, string strToName, string mailCC,string mailBCC, string strTitle, string strBody, string strFilesPath, HttpContext ctx)
    {
        intcounts++;
        string SmtpAddr = "192.168.10.31"; //System.Configuration.ConfigurationManager.AppSettings["SmtpAddr"];//邮箱服务器地址
        string SendEmailAddr = "caservice@hyitech.com"; //System.Configuration.ConfigurationManager.AppSettings["SendEmailAddr"];//发送邮件的邮箱地址
        string SendEmailUserName = "环宇通达正版化售后服务部"; //System.Configuration.ConfigurationManager.AppSettings["SendEmailUserName"];//发送邮件的邮箱用户名
        string SendEmailUserPwd = "123.com"; //System.Configuration.ConfigurationManager.AppSettings["SendEmailUserPwd"];//发送邮件的邮箱用户密码

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(SendEmailAddr, SendEmailUserName, System.Text.Encoding.GetEncoding("GB2312"));
        mail.To.Add(new MailAddress(mailTo, strToName, System.Text.Encoding.GetEncoding("GB2312")));
        string[] strCCList = mailCC.Split(new[] { ",", ";", " " }, StringSplitOptions.RemoveEmptyEntries);
        string[] strBCCList = mailBCC.Split(new[] { ",", ";", " " }, StringSplitOptions.RemoveEmptyEntries);
        //抄送
        foreach (var cc in strCCList)
        {
            string strtt = "";
            //if (cc.IndexOf("") > -1)
            //    strtt = "";
            mail.CC.Add(new MailAddress(cc.Trim(), strtt, System.Text.Encoding.GetEncoding("GB2312")));
        }
        //密送
        foreach (var bcc in strBCCList)
        {
            mail.Bcc.Add(new MailAddress(bcc.Trim(), "", System.Text.Encoding.GetEncoding("GB2312")));
        }
        mail.Subject = strTitle;
        mail.Body = strBody;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        //附件
        if (!string.IsNullOrEmpty(strFilesPath))
        {
            System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(strFilesPath);//添加附件 
            attachment1.Name = System.IO.Path.GetFileName(strFilesPath);
            attachment1.NameEncoding = System.Text.Encoding.GetEncoding("gb2312");
            attachment1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            attachment1.ContentDisposition.Inline = true;
            attachment1.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Attachment;
            string cid = attachment1.ContentId;//关键性的地方，这里得到一个id数值 
            mail.Attachments.Add(attachment1);
        }

        SmtpClient smtp = new SmtpClient(SmtpAddr, 25);
        smtp.UseDefaultCredentials = false;
        smtp.EnableSsl = false;
        mail.SubjectEncoding = System.Text.Encoding.GetEncoding("GB2312");
        mail.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");
        smtp.Credentials = new System.Net.NetworkCredential("caservice", SendEmailUserPwd, "hyitech.com");
        try
        {
            smtp.Send(mail);
            smtp = null;
            ctx.Response.Write("[" + intcounts + "]" + strTitle + "发送完成" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss") + "<br />");
        }
        catch (Exception ex)
        {
            ctx.Response.Write("[" + intcounts + "]" + strTitle + "发送失败<br />");
            WebLog.WriteLog(ex.Message + ex.InnerException + ex.HelpLink + ex.Data + ex.Source + ex.StackTrace + ex.TargetSite, "sendMail");

        }
    }
    private string getFile(DataTable dt)
    {
        string strFilePath = string.Empty;
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
        else
        {
            filename = "";
        }
        return filename;
    }

    private string getTypeInfo(DataTable dt)
    {
        string strResult = string.Empty;
        foreach (DataRow dr in dt.Rows)
        {
            strResult += string.Format("{0}问题{1}个、", dr[0].ToString(), dr[1].ToString());
        }
        return strResult.Trim('、');
    }
    private void GetFaqList(HttpContext ctx)
    {
        HYTD.BLL.Call_FAQListBLL Fbll = new HYTD.BLL.Call_FAQListBLL();
        Models.Call_FAQList FModel = new Models.Call_FAQList();

        try
        {
            string strsb = "";
            List<Call_FAQList> dList1 = Fbll.GetCall_FAQListList();
            foreach (Call_FAQList s in dList1)
            {
                strsb += string.Format("<option value='{0}'>{1}</option>", s.CF_ID, s.CF_Describe);
            }
            ctx.Response.Write(strsb);
        }
        catch (Exception ex)
        {
            ctx.Response.Write("失败");
        }
    }
    private void GetFaqContent(HttpContext ctx)
    {
        int SelectID = 0;
        HYTD.BLL.Call_FAQListBLL Fbll = new HYTD.BLL.Call_FAQListBLL();
        Models.Call_FAQList FModel = new Models.Call_FAQList();
        
        if (!string.IsNullOrEmpty(ctx.Request["SelectID"]))
        {
            int.TryParse(ctx.Request["SelectID"].ToString(), out SelectID);
        }
        try
        {
            List<Call_FAQList> dList1 = Fbll.GetCall_FAQListList().Where(c => c.CF_ID == SelectID).ToList();

            //ctx.Response.Write(dList1[0].CF_Content.ToString());
            //ctx.Response.Write("[{ \"CF_Describe\":\"" + dList1[0].CF_Describe + "\", \"CF_Content\": \"" + dList1[0].CF_Content + "\" }]");
            ctx.Response.Write("[{'Describe': '" + dList1[0].CF_Describe.ToString() + "','Content':'" + dList1[0].CF_Content.ToString() + "'}]");
        }
        catch (Exception ex)
        {
            ctx.Response.Write("失败");
        }
    }

    private void AddFAQBill(HttpContext ctx)
    {
        HYTD.BLL.Call_FAQListBLL Fbll = new HYTD.BLL.Call_FAQListBLL();
        Models.Call_FAQList FModel = new Models.Call_FAQList();

        string strErrorList = string.Empty;
        string strDescribe = string.Empty;
        string strContent = string.Empty;
        int SoftTypeID = 0;
        int CreateUserID = 0;

        if (!string.IsNullOrEmpty(ctx.Request.Form["txtErrorList"]))
        {
            strErrorList = ctx.Request.Form["txtErrorList"].ToString();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtDescribe"]))
        {
            strDescribe = ctx.Request.Form["txtDescribe"].ToString();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtContent"]))
        {
           strContent= ctx.Request.Form["txtContent"].ToString();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtSoftTypeID"]))
        {
            int.TryParse(ctx.Request.Form["txtSoftTypeID"].ToString(), out SoftTypeID);
        }
        UserInfo caUserInfo = PageBase.GetCurrentUserInfo;
        int.TryParse(caUserInfo.UserCode, out CreateUserID);

        FModel.CF_ErrorList = strErrorList;
        FModel.CF_Describe = strDescribe;
        FModel.CF_AddDate = DateTime.Now;
        FModel.CF_Content = strContent;
        FModel.CF_SoftTypeID = SoftTypeID;
        FModel.CF_UserID = CreateUserID;


        try
        {
            Fbll.AddCall_FAQList(FModel);
            ctx.Response.Write("成功");
        }
        catch (Exception ex)
        {
            ctx.Response.Write("失败");
        }
    }
    private void EditFAQBill(HttpContext ctx)
    {
        HYTD.BLL.Call_FAQListBLL Fbll = new HYTD.BLL.Call_FAQListBLL();
        Models.Call_FAQList FModel = new Models.Call_FAQList();
        int intFID = 0;
        string strErrorList = string.Empty;
        string strDescribe = string.Empty;
        string strContent = string.Empty;
        int SoftTypeID = 0;
        int CreateUserID = 0;

        if (!string.IsNullOrEmpty(ctx.Request.Form["txtErrorList"]))
        {
            strErrorList = ctx.Request.Form["txtErrorList"].ToString();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtDescribe"]))
        {
            strDescribe = ctx.Request.Form["txtDescribe"].ToString();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtContent"]))
        {
            strContent = ctx.Request.Form["txtContent"].ToString();
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtSoftTypeID"]))
        {
            int.TryParse(ctx.Request.Form["txtSoftTypeID"].ToString(), out SoftTypeID);
        }
        if (!string.IsNullOrEmpty(ctx.Request.Form["txtFID"]))
        {
            int.TryParse(ctx.Request.Form["txtFID"].ToString(), out intFID);
        }
        UserInfo caUserInfo = PageBase.GetCurrentUserInfo;
        int.TryParse(caUserInfo.UserCode, out CreateUserID);
        
        FModel.CF_ID = intFID;
        FModel.CF_ErrorList = strErrorList;
        FModel.CF_Describe = strDescribe;
        FModel.CF_AddDate = DateTime.Now;
        FModel.CF_Content = strContent;
        FModel.CF_SoftTypeID = SoftTypeID;
        FModel.CF_UserID = CreateUserID;
       
       
        try
        {
            Fbll.UpdateCall_FAQList(FModel);
            ctx.Response.Write("成功");
        }
        catch (Exception ex)
        {
            ctx.Response.Write("失败");
        }
    }
    private void DeleteFAQBill(HttpContext ctx)
    {
        HYTD.BLL.Call_FAQListBLL Fbll = new HYTD.BLL.Call_FAQListBLL();
        int intFID = 0;
        
        if (!string.IsNullOrEmpty(ctx.Request["pFID"]))
        {
            int.TryParse(ctx.Request.Form["pFID"].ToString(), out intFID);
        }
        try
        {
            Fbll.DeleteCall_FAQList(intFID);
            ctx.Response.Write("成功");
        }
        catch (Exception ex)
        {
            ctx.Response.Write("失败");
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}