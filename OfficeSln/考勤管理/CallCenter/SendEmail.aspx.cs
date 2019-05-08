using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Data;
using System.Net.Mime;
using Models;
using System.Configuration;

public partial class CallCenter_SendEmail : PageBase
{
    int intcounts = 0;
    public string strSendDate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            strSendDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-01";
            txtDateForZS.Text = strSendDate;
            txtDateForCS.Text = strSendDate;
        }
        UserInfo model = new PageBase().CurrentUserInfo;
        if (ConfigurationManager.AppSettings["SendEmail"] != null)
        {
            string[] strlist = ConfigurationManager.AppSettings["SendEmail"].ToString().Trim().Split(',');
            if (strlist.Where(c => c == model.UserCode.ToString()).ToList().Count > 0)
            {

            }
            else
            {
                Response.Write("<script>alert('您无权操作此页面！');window.location.href='/Index.aspx';</script>");
                this.Response.End();
            }
        }
        else
        {
            Response.Write("<script>alert('您无权操作此页面！');window.location.href='/Index.aspx';</script>");
            this.Response.End();
        }
    }

    private void sendMail(string mailTo, string strToName, string mailCC, string mailBCC, string strTitle, string strBody, string strFilesPath)
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
            if (cc.IndexOf("yangchao") > -1)
                strtt = "项目经理";
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
        //Attachment data = new Attachment(strFilesPath);
        //// Add time stamp information for the file.
        //ContentDisposition disposition = data.ContentDisposition;
        //disposition.CreationDate = System.IO.File.GetCreationTime(strFilesPath);
        //disposition.ModificationDate = System.IO.File.GetLastWriteTime(strFilesPath);
        //disposition.ReadDate = System.IO.File.GetLastAccessTime(strFilesPath);
        //// Add the file attachment to this e-mail message.
        //mail.Attachments.Add(data);

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
            Response.Write("[" + intcounts + "]" + strTitle + "发送完成" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss") + "<br />");
        }
        catch (Exception ex)
        {
            Response.Write("[" + intcounts + "]" + strTitle + "发送失败<br />");
            WebLog.WriteLog(ex.Message + ex.InnerException + ex.HelpLink + ex.Data + ex.Source + ex.StackTrace + ex.TargetSite, "sendMail");

        }
    }
    private void sendMail1(string mailTo, string strToName, string mailCC, string mailBCC, string strTitle, string strBody, string strFilesPath)
    {
        CDO.Message msg = new CDO.Message();
        string passWord = "123.com";
        string from = "caservice@hyitech.com";
        //string from = "hyitech\\caservice";
        string server = "192.168.10.32";// "192.168.10.32";

        //发件人
        msg.From = from;
        //收件人
        msg.To = mailTo;
        //抄送
        if (mailCC.Trim().Length > 0)
            msg.CC = mailCC;
        //密送
        if (mailBCC.Trim().Length > 0)
            msg.BCC = mailBCC;
        //标题
        msg.Subject = strTitle;
        //正文
        msg.TextBody = strBody;

        if (strFilesPath.Trim().Length > 0)
            msg.AddAttachment(strFilesPath);
        CDO.IConfiguration iConfig = msg.Configuration;
        ADODB.Fields fields = iConfig.Fields;

        //fields["http://schemas.microsoft.com/cdo/configuration/sendusing"].Value = 2;
        //fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"].Value = server;
        //fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"].Value = 25;
        //fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"].Value = 1;
        ////fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"].Value = true;
        //fields["http://schemas.microsoft.com/cdo/configuration/sendemailaddress"].Value = "caservice@hyitech.com";
        //fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"].Value = passWord;

        fields["http://schemas.microsoft.com/cdo/configuration/sendusing"].Value = 2;
        fields["http://schemas.microsoft.com/cdo/configuration/sendemailaddress"].Value = "caservice@hyitech.com"; //修改这里，并保持发件人与这里的地址一致
        fields["http://schemas.microsoft.com/cdo/configuration/smtpaccountname"].Value = "caservice@hyitech.com"; //修改这里
        fields["http://schemas.microsoft.com/cdo/configuration/sendusername"].Value = "caservice";//修改这里
        fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"].Value = "123.com";//修改这里
        fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"].Value = 1;
        //value=0 代表Anonymous验证方式（不需要验证）
        //value=1 代表Basic验证方式（使用basic (clear-text) authentication.
        //The configuration sendusername/sendpassword or postusername/postpassword fields are used to specify credentials.）
        //Value=2 代表NTLM验证方式（Secure Password Authentication in Microsoft Outlook Express）
        fields["http://schemas.microsoft.com/cdo/configuration/languagecode"].Value = 0x0804;
        fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"].Value = "192.168.10.32";

        fields.Update();
        try
        {
            msg.Send();
            msg = null;
            Response.Write(strTitle + "发送完成<br />");
        }
        catch (Exception ex)
        {
            WebLog.WriteLog(ex.Message + ex.InnerException + ex.HelpLink + ex.Data + ex.Source + ex.StackTrace + ex.TargetSite, "sendMail");
        }
    }
    #region 正式发送
    protected void Button1_Click(object sender, EventArgs e)
    {
        string mailTo = string.Empty;
        string mailCC = string.Empty;
        string mailBCC = string.Empty;
        string strDate = txtDateForZS.Text.Trim();
        mailCC = txtDateForZSCC.Text.Trim();
        mailBCC = txtDateForZSBCC.Text.Trim() + ",liuyanhui@hyitech.com";
        SendMailInfoSet(false, mailTo, mailCC, mailBCC, strDate);
    }

    private void SendMailInfoSet(bool bolIsTest, string mailTo, string mailCC, string mailBCC, string strDate)
    {
        string strTitle = string.Empty;
        string strBody = string.Empty;
        string strFilesPath = string.Empty;
        string mailCCTO = mailCC;
        HYTD.BLL.Call_CustomerBLL bll = new HYTD.BLL.Call_CustomerBLL();
        Models.Call_Customer mCustomer = new Models.Call_Customer();
        List<Models.Call_Customer> mCustomerList = new List<Models.Call_Customer>();
        mCustomerList = bll.GetCall_CustomerList().Where(c => c.CC_IsSend == 1).ToList();
        int i = 0;
        foreach (var v in mCustomerList)
        {
            //i++;
            //if (i > 2)
            //    break;
            if (v.CC_Status > 0 && v.CC_Email != null && v.CC_Email.Trim().Length > 0)
            {
                if (!bolIsTest)
                {
                    mailTo = mailCC = "";
                    mailTo = v.CC_Email;
                    if (mailCCTO.Length > 0)
                        mailCC = v.CC_CCEmail + "," + mailCCTO;
                    //密送给所属工程师
                    if (v.CC_Owner == "29")
                    {
                        mailBCC += ",zhouyingchao@hyitech.com";
                    }
                    else if (v.CC_Owner == "37")
                    { mailBCC += ",zhenlinghao@hyitech.com"; }
                    else if (v.CC_Owner == "54")
                    { mailBCC += ",liuxingyang@hyitech.com"; }
                    else
                    {
                        mailBCC += ",961983187@qq.com";
                    }
                }
                else
                {
                    if (v.CC_Owner == "29")
                    {
                        mailTo = "zhouyingchao@hyitech.com";
                    }
                    else if (v.CC_Owner == "37")
                    { mailTo = "zhenlinghao@hyitech.com"; }
                    else if (v.CC_Owner == "43")
                    { mailTo = "zhangguanzhou@hyitech.com"; }
                    else
                    {
                        mailTo = "961983187@qq.com";
                    }
                }
                //获取附件 如果没有附件则不添加
                DataSet ds = new DataSet();
                HYTD.BLL.Call_WorkBillBLL bllworkbill = new HYTD.BLL.Call_WorkBillBLL();
                string strWhere = string.Format("CWB_CCID={0} and convert(varchar(7),CWB_CreateTime,120)='{1}'", v.CC_ID, strDate.Substring(0, 7));
                ds = bllworkbill.ImportExcelCustomerWorkBill(strWhere);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strFilesPath = getFile(ds.Tables[0]);
                    string strTypeInfo = string.Empty;
                    strTypeInfo = getTypeInfo(ds.Tables[1]);
                    strTitle = string.Format("{0}微软正版化平台{1}份服务报告", v.CC_Name, Convert.ToDateTime(strDate).ToString("yyyy年MM月"));
                    strBody = string.Format(@"{0}，您好：<br /><br />
    <p style='text-indent:2em; '>环宇通达售后服务部，{1}份服务报告：</p>
    <p style='text-indent:2em; '>共接到贵单位用户服务请求{2}个，其中{3}；详细服务记录请见附件。</p>  
    <p style='text-indent:2em; '>为了更好的为提供技术支持和帮助，我公司为微软正版化客户开通了服务专线：400-079-8616。所有与微软正版化软件平台相关的问题（如：正版化平台的使用和操作问题、激活问题等）均可来电请求协助解决，我公司将为您提供专业、高效的服务。
    <p style='text-indent:2em; '>在线常见问题库(可解决Windows 7激活报0xC004F035错误)：http://www.hyitech.com/Error/CaActivehelp.html </p>
您会定期收到我公司提供的服务记录报告，有任何疑问请拨打服务专线咨询，本邮件无需回复。 </p>
    <p style='text-indent:2em; '>投诉电话：010-62364559转8007
</p>", v.CC_EmailUserName, Convert.ToDateTime(strDate).ToString("yyyy年MM月"), ds.Tables[0].Rows.Count, strTypeInfo);
                }
                else
                {
                    strFilesPath = "";
                    strTitle = string.Format("{0}微软正版化平台{1}份服务报告", v.CC_Name, Convert.ToDateTime(strDate).ToString("yyyy年MM月"));
                    strBody = string.Format(@"{0}，您好：<br /><br />
    <p style='text-indent:2em; '>环宇通达售后服务部，截至到{1}底没有接到贵单位的问题反馈。</p>
    <p style='text-indent:2em; '>为了更好的为提供技术支持和帮助，我公司为微软正版化客户开通了服务专线：400-079-8616。所有与微软正版化软件平台相关的问题（如：正版化平台的使用和操作问题、激活问题等）均可来电请求协助解决，我公司将为您提供专业、高效的服务。</p>
    <p style='text-indent:2em; '>在线常见问题库(可解决Windows 7激活报0xC004F035错误)：http://www.hyitech.com/Error/CaActivehelp.html  </p>
您会定期收到我公司提供的服务记录报告，有任何疑问请拨打服务专线咨询，本邮件无需回复。 </p>
    <p style='text-indent:2em; '>投诉电话：010-62364559转8007</p> ", v.CC_EmailUserName, Convert.ToDateTime(strDate).ToString("yyyy年MM月"));
                }

                string str = @"<br /><br /><br />    <p class='x_MsoNormal'>
        <span style='font-family: &quot; 微软雅黑&quot; ,&quot; sans-serif&quot;'>多谢支持</span><span
            lang='EN-US'></span></p>
    <p class='x_MsoNormal'>
        <span lang='EN-US'>&nbsp;</span></p>
    <p class='x_MsoNormal'>
        <b><span lang='EN-US' style='font-size: 10.0pt; font-family: 微软雅黑,sans-serif&quot;
            color: #1F497D'>Best regards</span></b></p>
    <table class='x_MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='border-collapse: collapse'>
        <tbody>
            <tr>
                <td width='93' valign='top' style='width: 69.9pt; border: none; border-right: dotted windowtext 1.0pt;
                    padding: 0cm 5.4pt 0cm 5.4pt'>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'>
                        <span lang='EN-US' style='font-size: 12.0pt; line-height: 115%; font-family: 宋体;
                            color: #1F497D'>
                            <img id='x_图片_x0020_1' src='http://www.hyitech.com/logo.jpg' alt='说明: hytd' height='92'
                                width='89' border='0'></span></p>
                </td>
                <td width='497' valign='top' style='width: 372.9pt; padding: 0cm 5.4pt 0cm 5.4pt'>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'>
                        <span style='font-size: 10.0pt; line-height: 115%; font-family: 华文行楷; color: #1F497D'>
                            售后支持服务</span><span lang='EN-US' style='font-size: 10.0pt; line-height: 115%; color: #1F497D'><br>
                            </span><span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'>
                                | Fax:400-0798-616 | </span>
                    </p>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'>
                        <span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'>E-mail:
                        </span><u><span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #0563C1'>
                            <a href='#'>caservice@hyitech.com</a></span></u><span lang='EN-US' style='font-size: 8.5pt;
                                line-height: 115%; color: #1F497D'></span></p>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'>
                        <span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'>Web:
                        </span><span lang='EN-US'><a href='#' target='_blank'><span style='font-size: 8.5pt;
                            line-height: 115%'>http://www.hyitech.com</span></a></span><span lang='EN-US' style='font-size: 8.5pt;
                                line-height: 115%; color: #1F497D'>
                                <br>
                            </span><span style='font-size: 8.5pt; line-height: 115%; font-family: 宋体; color: #1F497D'>
                                北京环宇通达科技有限公司</span><span lang='DE' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'></span></p>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'><span
                            style='font-size: 8.5pt; line-height: 115%; font-family: 宋体; color: #1F497D'>微软金牌合作伙伴
                        </span><span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'>
                            | </span><span style='font-size: 8.5pt; line-height: 115%; font-family: 宋体; color: #1F497D'>
                                教育行业解决方案专家 </span><span lang='EN-US' style='font-size: 8.5pt; line-height: 115%;
                                    color: #1F497D'></span>
                    </p>
                </td>
            </tr>
        </tbody>
    </table>";
                strBody += str;
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                if (bolIsTest)
                    strTitle = "测试邮件：" + strTitle;
                sendMail(mailTo, v.CC_EmailUserName, mailCC, mailBCC, strTitle, strBody, strFilesPath);
                //}
            }
        }
    }

    private void SendMailInfoSetTest(bool bolIsTest, string mailTo, string mailCC, string mailBCC, string strDate)
    {
        string strTitle = string.Empty;
        string strBody = string.Empty;
        string strFilesPath = string.Empty;
        string mailCCTO = mailCC;
        HYTD.BLL.Call_CustomerBLL bll = new HYTD.BLL.Call_CustomerBLL();
        Models.Call_Customer mCustomer = new Models.Call_Customer();
        List<Models.Call_Customer> mCustomerList = new List<Models.Call_Customer>();
        mCustomerList = bll.GetCall_CustomerList().Where(c => c.CC_IsSend == 1).ToList();
        int i = 0;
        foreach (var v in mCustomerList)
        {
            i++;
            if (i > 2)
                break;
            if (v.CC_Status > 0 && v.CC_Email != null && v.CC_Email.Trim().Length > 0)
            {
                if (!bolIsTest)
                {
                    mailTo = mailCC = "";
                    mailTo = v.CC_Email;
                    if (mailCCTO.Length > 0)
                        mailCC = v.CC_CCEmail + "," + mailCCTO;
                    //密送给所属工程师
                    if (v.CC_Owner == "29")
                    {
                        mailBCC += ",zhouyingchao@hyitech.com";
                    }
                    else if (v.CC_Owner == "37")
                    { mailBCC += ",zhenlinghao@hyitech.com"; }
                    else if (v.CC_Owner == "54")
                    { mailBCC += ",liuxingyang@hyitech.com"; }
                    else
                    {
                        mailBCC += ",961983187@qq.com";
                    }
                }
                else
                {
                    if (v.CC_Owner == "29")
                    {
                        mailTo = "zhouyingchao@hyitech.com";
                    }
                    else if (v.CC_Owner == "37")
                    { mailTo = "zhenlinghao@hyitech.com"; }
                    else if (v.CC_Owner == "54")
                    { mailTo = "liuxingyang@hyitech.com"; }
                    else
                    {
                        mailTo = "961983187@qq.com";
                    }
                }
                //获取附件 如果没有附件则不添加
                DataSet ds = new DataSet();
                HYTD.BLL.Call_WorkBillBLL bllworkbill = new HYTD.BLL.Call_WorkBillBLL();
                //string strWhere = string.Format("CWB_CCID={0} and (convert(varchar(7),CWB_CreateTime,120)='{1}' or convert(varchar(7),CWB_CreateTime,120)='2015-04')", v.CC_ID, strDate.Substring(0, 4));
                string strWhere = string.Format("CWB_CCID={0} and convert(varchar(7),CWB_CreateTime,120)='{1}'", v.CC_ID, strDate.Substring(0, 7));
                ds = bllworkbill.ImportExcelCustomerWorkBill(strWhere);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    strFilesPath = getFile(ds.Tables[0]);
                    string strTypeInfo = string.Empty;
                    strTypeInfo = getTypeInfo(ds.Tables[1]);
                    strTitle = string.Format("{0}微软正版化平台{1}份服务报告", v.CC_Name, Convert.ToDateTime(strDate).ToString("yyyy年MM"));
                    #region
                    //                    strBody = string.Format(@"{0}，您好：<br /><br />
                    //    <p style='text-indent:2em; '>环宇通达公司微软正版化售后服务部，首先感谢您一直以来对我们的支持和鼓励。回望2014年，环宇通达是不断创新的一年。对于正版化平台：我们综合了历年来所有的服务项目与众多用户提出的宝贵意见，积极改善了正版化平台的诸多功能。对于售后服务：我们提供每月服务报告邮件通知、服务专线专人接听并解决用户问题、定期巡检及时排除客户平台故障、其它邮件和远程协助服务。</p>
                    //    <p style='text-indent:2em; '>在2015年，为了使正版化平台运行更加稳定，也为给广大用户带来更加的丰富和多彩的正版化软件体验，我们将会恪尽职守，通过我们的不懈努力，为打造一个高效而稳定的平台系统，我们在此承诺：</p>  
                    //    <p style='text-indent:2em; '>一、接到问题咨询问题，10分钟相应，1个小时之内出解决方案，争取在最短时间解决问题。完善的问题追踪制度，从我们接到问题直到问题完全解决，全程跟踪问题解决状态。</p>
                    //    <p style='text-indent:2em; '>二、以专业的精神和积极的态度，以丰富的经验和娴熟的技术确保正版化平台不断的完善和改进。</p>
                    //    <p style='text-indent:2em; '>三、继续将我们提供的服务以月报的方式反馈给管理人员。</p>
                    //    <p style='text-indent:2em; '>四、诚约老师对我们的工作进行监督，有任何意见、建议可以随时向我们沟通，意见反馈及投诉电话：010-62382945-8007。</p>
                    //    <p></p>
                    //    <p style='text-indent:2em;font-weight:bold; '>以下为{1}全年为您提供过的服务记录报告：</p>
                    //    <p style='text-indent:2em; '>共接到贵单位用户服务请求{2}个，其中{3}；详细服务记录请见附件。</p>
                    // <p></p>
                    //  <p style='text-indent:2em; '>2014已结束，令人期待的2015即将开始。环宇通达人不但要更好的服务客户，还要做到更多的创新与努力。敬请期待我们在2015中的表现！本邮件无需回复。</p>
                    //  <p style='text-indent:2em; '>感谢您一年中对我们工作的支持，环宇通达全体员工祝您,在2015年中工作顺利</p>", v.CC_EmailUserName, Convert.ToDateTime(strDate).ToString("yyyy年"), ds.Tables[0].Rows.Count, strTypeInfo);

                    //                }
                    //                else
                    //                {
                    //                    strFilesPath = "";
                    //                    strTitle = string.Format("{0}微软正版化平台{1}份服务报告", v.CC_Name, Convert.ToDateTime(strDate).ToString("yyyy年"));
                    //                    strBody = string.Format(@"{0}，您好：<br /><br />
                    //    <p style='text-indent:2em; '>环宇通达公司微软正版化售后服务部，首先感谢您一直以来对我们的支持和鼓励。回望2014年，环宇通达是不断创新的一年。对于正版化平台：我们综合了历年来所有的服务项目与众多用户提出的宝贵意见，积极改善了正版化平台的诸多功能。对于售后服务：我们提供每月服务报告邮件通知、服务专线专人接听并解决用户问题、定期巡检及时排除客户平台故障、其它邮件和远程协助服务。</p>
                    //    <p style='text-indent:2em; '>在2015年，为了使正版化平台运行更加稳定，也为给广大用户带来更加的丰富和多彩的正版化软件体验，我们将会恪尽职守，通过我们的不懈努力，为打造一个高效而稳定的平台系统，我们在此承诺：</p>  
                    //    <p style='text-indent:2em; '>一、接到问题咨询问题，10分钟相应，1个小时之内出解决方案，争取在最短时间解决问题。完善的问题追踪制度，从我们接到问题直到问题完全解决，全程跟踪问题解决状态。</p>
                    //    <p style='text-indent:2em; '>二、以专业的精神和积极的态度，以丰富的经验和娴熟的技术确保正版化平台不断的完善和改进。</p>
                    //    <p style='text-indent:2em; '>三、继续将我们提供的服务以月报的方式反馈给管理人员。</p>
                    //    <p style='text-indent:2em; '>四、诚约老师对我们的工作进行监督，有任何意见、建议可以随时向我们沟通，意见反馈及投诉电话：010-62382945-8007。</p>
                    //    <p></p>
                    //  <p style='text-indent:2em; '>2014已结束，令人期待的2015即将开始。环宇通达人不但要更好的服务客户，还要做到更多的创新与努力。敬请期待我们在2015中的表现！本邮件无需回复。</p>
                    //  <p style='text-indent:2em; '>感谢您一年中对我们工作的支持，环宇通达全体员工祝您,在2015年中工作顺利</p>", v.CC_EmailUserName, Convert.ToDateTime(strDate).ToString("yyyy年"));
                    //                }
                    #endregion
                    strBody = string.Format(@"{0}，您好：<br /><br />
    <p style='text-indent:2em; '>环宇通达售后服务部，{1}份服务报告：</p>
    <p style='text-indent:2em; '>共接到贵单位用户服务请求{2}个，其中{3}；详细服务记录请见附件。</p>  
    <p style='text-indent:2em; '>为了更好的为提供技术支持和帮助，我公司为微软正版化客户开通了服务专线：400-079-8616。所有与微软正版化软件平台相关的问题（如：正版化平台的使用和操作问题、激活问题等）均可来电请求协助解决，我公司将为您提供专业、高效的服务。
    <p style='text-indent:2em; '>在线常见问题库(可解决Windows 7激活报0xC004F035错误)：http://www.hyitech.com/Error/CaActivehelp.html </p>
您会定期收到我公司提供的服务记录报告，有任何疑问请拨打服务专线咨询，本邮件无需回复。 </p>
    <p style='text-indent:2em; '>投诉电话：010-62364559转8007
</p>", v.CC_EmailUserName, Convert.ToDateTime(strDate).ToString("yyyy年MM月"), ds.Tables[0].Rows.Count, strTypeInfo);
                }
                else
                {
                    strFilesPath = "";
                    strTitle = string.Format("{0}微软正版化平台{1}份服务报告", v.CC_Name, Convert.ToDateTime(strDate).ToString("yyyy年MM月"));
                    strBody = string.Format(@"{0}，您好：<br /><br />
    <p style='text-indent:2em; '>环宇通达售后服务部，截至到{1}底没有接到贵单位的问题反馈。</p>
    <p style='text-indent:2em; '>为了更好的为提供技术支持和帮助，我公司为微软正版化客户开通了服务专线：400-079-8616。所有与微软正版化软件平台相关的问题（如：正版化平台的使用和操作问题、激活问题等）均可来电请求协助解决，我公司将为您提供专业、高效的服务。</p>
    <p style='text-indent:2em; '>在线常见问题库(可解决Windows 7激活报0xC004F035错误)：http://www.hyitech.com/Error/CaActivehelp.html  </p>
您会定期收到我公司提供的服务记录报告，有任何疑问请拨打服务专线咨询，本邮件无需回复。 </p>
    <p style='text-indent:2em; '>投诉电话：010-62364559转8007</p> ", v.CC_EmailUserName, Convert.ToDateTime(strDate).ToString("yyyy年MM月"));
                }
                string str = @"<br /><br /><br />    <p class='x_MsoNormal'>
        <span style='font-family: &quot; 微软雅黑&quot; ,&quot; sans-serif&quot;'>多谢支持</span><span
            lang='EN-US'></span></p>
    <p class='x_MsoNormal'>
        <span lang='EN-US'>&nbsp;</span></p>
    <p class='x_MsoNormal'>
        <b><span lang='EN-US' style='font-size: 10.0pt; font-family: 微软雅黑,sans-serif&quot;
            color: #1F497D'>Best regards</span></b></p>
    <table class='x_MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='border-collapse: collapse'>
        <tbody>
            <tr>
                <td width='93' valign='top' style='width: 69.9pt; border: none; border-right: dotted windowtext 1.0pt;
                    padding: 0cm 5.4pt 0cm 5.4pt'>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'>
                        <span lang='EN-US' style='font-size: 12.0pt; line-height: 115%; font-family: 宋体;
                            color: #1F497D'>
                            <img id='x_图片_x0020_1' src='http://www.hyitech.com/logo.jpg' alt='说明: hytd' height='92'
                                width='89' border='0'></span></p>
                </td>
                <td width='497' valign='top' style='width: 372.9pt; padding: 0cm 5.4pt 0cm 5.4pt'>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'>
                        <span style='font-size: 10.0pt; line-height: 115%; font-family: 华文行楷; color: #1F497D'>
                            售后支持服务</span><span lang='EN-US' style='font-size: 10.0pt; line-height: 115%; color: #1F497D'><br>
                            </span><span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'>
                                | Fax:400-0798-616 | </span>
                    </p>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'>
                        <span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'>E-mail:
                        </span><u><span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #0563C1'>
                            <a href='#'>caservice@hyitech.com</a></span></u><span lang='EN-US' style='font-size: 8.5pt;
                                line-height: 115%; color: #1F497D'></span></p>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'>
                        <span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'>Web:
                        </span><span lang='EN-US'><a href='#' target='_blank'><span style='font-size: 8.5pt;
                            line-height: 115%'>http://www.hyitech.com</span></a></span><span lang='EN-US' style='font-size: 8.5pt;
                                line-height: 115%; color: #1F497D'>
                                <br>
                            </span><span style='font-size: 8.5pt; line-height: 115%; font-family: 宋体; color: #1F497D'>
                                北京环宇通达科技有限公司</span><span lang='DE' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'></span></p>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'><span
                            style='font-size: 8.5pt; line-height: 115%; font-family: 宋体; color: #1F497D'>微软金牌合作伙伴
                        </span><span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'>
                            | </span><span style='font-size: 8.5pt; line-height: 115%; font-family: 宋体; color: #1F497D'>
                                教育行业解决方案专家 </span><span lang='EN-US' style='font-size: 8.5pt; line-height: 115%;
                                    color: #1F497D'></span>
                    </p>
                </td>
            </tr>
        </tbody>
    </table>";
                strBody += str;
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                if (bolIsTest)
                    strTitle = "测试邮件：" + strTitle;
                sendMail(mailTo, v.CC_EmailUserName, mailCC, mailBCC, strTitle, strBody, strFilesPath);
                //}
            }
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
    #endregion

    #region 测试发送
    protected void Button2_Click(object sender, EventArgs e)
    {
        string mailTo = string.Empty;
        string mailCC = string.Empty;
        string mailBCC = string.Empty;
        string strDate = txtDateForCS.Text.Trim();
        mailTo = txtDateForCSCC.Text.Trim();
        mailCC = mailTo;
        mailBCC = txtDateForCSBCC.Text.Trim() + ";liuyanhui@hyitech.com";
        SendMailInfoSetTest(true, mailTo, mailCC, mailBCC, strDate);
    }
    #endregion
    protected void Button3_Click(object sender, EventArgs e)
    {
        string mailTo = string.Empty;
        string mailCC = string.Empty;
        string mailBCC = string.Empty;
        string strDate = txtDateForQFS.Text.Trim();
        mailTo = "";
        mailCC =  txtDateForQFSCC.Text.Trim();
        mailBCC = txtDateForQFSBCC.Text.Trim() + ";1@qq.com";
        SendMailInfoSetTestQF(true, mailTo, mailCC, mailBCC, strDate);
    }
    private void SendMailInfoSetTestQF(bool bolIsTest, string mailTo, string mailCC, string mailBCC, string strDate)
    {
        string strTitle = string.Empty;
        string strBody = string.Empty;
        string strFilesPath = string.Empty;
        //string mailCCTO = mailCC;
        HYTD.BLL.Call_CustomerBLL bll = new HYTD.BLL.Call_CustomerBLL();
        Models.Call_Customer mCustomer = new Models.Call_Customer();
        List<Models.Call_Customer> mCustomerList = new List<Models.Call_Customer>();
        mCustomerList = bll.GetCall_CustomerList().Where(c => c.CC_IsSend == 1).ToList();
        //int i = 0;
        foreach (var v in mCustomerList)
        {
            strBody = string.Empty;
            //i++;
            //if (i > 2)
            //    break;
            if (v.CC_Status > 0 && v.CC_Email != null && v.CC_Email.Trim().Length > 0)
            {
                mailTo = v.CC_Email;
                //获取附件 如果没有附件则不添加
                HYTD.BLL.Call_WorkBillBLL bllworkbill = new HYTD.BLL.Call_WorkBillBLL();
                {
                    strFilesPath = "";
                    strTitle = string.Format("关于近期收到我公司客服邮件的说明");
                }
                string str = @"尊敬的客户：

 <p style='text-indent:2em; '>为了更好的服务客户，完善服务流程，提高服务效率和满意度，近期对我公司客服系统进行了升级，升级期间造成邮件误发的情况，在此深表歉意。客服系统升级后，客服人员记录工单的效率将大大提高，客服热线占线的情况将得到一定程度缓解。</p>


 <p style='text-indent:2em; '>非常感谢您一直以来对环宇通达的支持与厚爱， 值此中秋佳节来临之际,环宇通达公司全体员工，向您致以最诚挚的问候！ 祝您节日愉快，阖家欢乐，幸福安康！ </p>
";
                string strqian = @"<br /><br /><br />    <p class='x_MsoNormal'>
        <span style='font-family: &quot; 微软雅黑&quot; ,&quot; sans-serif&quot;'>多谢支持</span><span
            lang='EN-US'></span></p>
    <p class='x_MsoNormal'>
        <span lang='EN-US'>&nbsp;</span></p>
    <p class='x_MsoNormal'>
        <b><span lang='EN-US' style='font-size: 10.0pt; font-family: 微软雅黑,sans-serif&quot;
            color: #1F497D'>Best regards</span></b></p>
    <table class='x_MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='border-collapse: collapse'>
        <tbody>
            <tr>
                <td width='93' valign='top' style='width: 69.9pt; border: none; border-right: dotted windowtext 1.0pt;
                    padding: 0cm 5.4pt 0cm 5.4pt'>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'>
                        <span lang='EN-US' style='font-size: 12.0pt; line-height: 115%; font-family: 宋体;
                            color: #1F497D'>
                            <img id='x_图片_x0020_1' src='http://www.hyitech.com/logo.jpg' alt='说明: hytd' height='92'
                                width='89' border='0'></span></p>
                </td>
                <td width='497' valign='top' style='width: 372.9pt; padding: 0cm 5.4pt 0cm 5.4pt'>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'>
                        <span style='font-size: 10.0pt; line-height: 115%; font-family: 华文行楷; color: #1F497D'>
                            售后支持服务</span><span lang='EN-US' style='font-size: 10.0pt; line-height: 115%; color: #1F497D'><br>
                            </span><span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'>
                                | Fax:400-0798-616 | </span>
                    </p>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'>
                        <span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'>E-mail:
                        </span><u><span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #0563C1'>
                            <a href='#'>caservice@hyitech.com</a></span></u><span lang='EN-US' style='font-size: 8.5pt;
                                line-height: 115%; color: #1F497D'></span></p>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'>
                        <span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'>Web:
                        </span><span lang='EN-US'><a href='#' target='_blank'><span style='font-size: 8.5pt;
                            line-height: 115%'>http://www.hyitech.com</span></a></span><span lang='EN-US' style='font-size: 8.5pt;
                                line-height: 115%; color: #1F497D'>
                                <br>
                            </span><span style='font-size: 8.5pt; line-height: 115%; font-family: 宋体; color: #1F497D'>
                                北京环宇通达科技有限公司</span><span lang='DE' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'></span></p>
                    <p class='x_MsoNormal' style='line-height: 115%;margin:5px'><span
                            style='font-size: 8.5pt; line-height: 115%; font-family: 宋体; color: #1F497D'>微软金牌合作伙伴
                        </span><span lang='EN-US' style='font-size: 8.5pt; line-height: 115%; color: #1F497D'>
                            | </span><span style='font-size: 8.5pt; line-height: 115%; font-family: 宋体; color: #1F497D'>
                                教育行业解决方案专家 </span><span lang='EN-US' style='font-size: 8.5pt; line-height: 115%;
                                    color: #1F497D'></span>
                    </p>
                </td>
            </tr>
        </tbody>
    </table>";
                strBody = str + strqian;
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                if (bolIsTest)
                    strTitle = "" + strTitle;
                sendMail(mailTo, v.CC_EmailUserName, mailCC, mailBCC, strTitle, strBody, strFilesPath);
                //}
            }
        }
    }
}