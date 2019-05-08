using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Configuration;
using System.Text;

public partial class CallCenter_UserRights : System.Web.UI.UserControl
{
    /// <summary>
    /// pagefrom
    /// </summary>
    public int intPage { get; set; }
    /// <summary>
    /// Url
    /// </summary>
    public string strInfo { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        topRights(intPage);
    }

    private void topRights(int intPageID)
    {
        StringBuilder sb = new StringBuilder();
        UserInfo model = new PageBase().CurrentUserInfo;
        string sUrl = string.Format("?uname={0}&stime={1}", model.UserName, DateTime.Now.ToString("yyyy-MM") + "-01");
        string sUrlNew = string.Format("?stime={0}", DateTime.Now.ToString("yyyy-MM") + "-01");
        string class1 = string.Empty;
        string class2 = string.Empty;
        string class3 = string.Empty;
        string class4 = string.Empty;
        string class5 = string.Empty;
        string class6 = string.Empty;
        string class7 = string.Empty;
        string class8 = string.Empty;
        string class9 = string.Empty;
        string class10 = string.Empty;

        string class11 = string.Empty;
        string class12 = string.Empty;
        string class13 = string.Empty;
        string class14 = string.Empty;
        string class15 = string.Empty;
        string class16 = string.Empty;
        string class17 = string.Empty;
        string class18 = string.Empty;
       string class19 = string.Empty;
       string class20 = string.Empty;
       class1 = class2 = class3 = class4 = class5 = class6 = class7 = class8 = class9 = class10 = "";
        switch (intPageID)
        {
            case 1:
                class1 = "class=\"forrights1\"";
                class11 = "style=\"color:White\"";
                break;
            case 2:
                class2 = "class=\"forrights1\"";
                class12 = "style=\"color:White\"";
                break;
            case 3:
                class3 = "class=\"forrights1\"";
                class13 = "style=\"color:White\"";
                break;
            case 4:
                class4 = "class=\"forrights1\"";
                class14 = "style=\"color:White\"";
                break;
            case 5:
                class5 = "class=\"forrights1\"";
                class15 = "style=\"color:White\"";
                break;
            case 6:
                class6 = "class=\"forrights1\"";
                class16 = "style=\"color:White\"";
                break;
            case 7:
                class7 = "class=\"forrights1\"";
                class17 = "style=\"color:White\"";
                break;
            case 8:
                class8 = "class=\"forrights1\"";
                class18 = "style=\"color:White\"";
                break;
            case 9:
                class9 = "class=\"forrights1\"";
                class19 = "style=\"color:White\"";
                break;
            case 10:
                class10 = "class=\"forrights1\"";
                class20 = "style=\"color:White\"";
                break;
        }
        //class1 = class2 = class3 = class4 = class5 = class6 = "";


        if (ConfigurationManager.AppSettings["Users"] != null)
        {
            string[] strlist = ConfigurationManager.AppSettings["Users"].ToString().Trim().Split(',');
            if (strlist.Where(c => c == model.UserCode.ToString()).ToList().Count > 0)
            {
                sb.AppendFormat("<li lt=\"1\" {0}><a href=\"useracounte.aspx{1}\" {2}>首页</a></li>", class10, sUrlNew, class20);
                sb.AppendFormat("<li lt=\"1\" {0}><a href=\"WorkBillList.aspx{1}\" {2}>工单列表</a></li>", class1, sUrlNew, class11);
                sb.AppendFormat("<li lt=\"1\" {0}><a href=\"CustomerList.aspx{1}\" {2}>客户列表</a></li>", class7, sUrlNew, class17);
                sb.AppendFormat("<li lt=\"3\" {0}><a href=\"VisitBillList.aspx{1}\" {2}>回访列表</a></li>", class2, sUrlNew, class12);
                sb.AppendFormat("<li  style='width:139px' {0} {2}><a href=\"CustomerErrorList.aspx{1}\">客户激活错误列表</a></li>", class4, sUrlNew, class14);
                sb.AppendFormat("<li  style='width:139px' {0} {2}><a href=\"CustomerSuccessfullList.aspx{1}\">客户激活成功列表</a></li>", class5, sUrlNew, class15);
                sb.AppendFormat("<li  style='width:139px' {0} {2}><a href=\"MYDList.aspx{1}\">用户满意度</a></li>", class8, sUrlNew, class18);
                if (ConfigurationManager.AppSettings["SendEmailUsers"] != null && model.UserCode == ConfigurationManager.AppSettings["SendEmailUsers"].ToString())
                {
                    sb.AppendFormat("<li  style='width:139px' {0} {2}><a href=\"SendEmail.aspx{1}\">发送邮件</a></li>", class9, sUrlNew, class19);
                }
            }
        }
        if (ConfigurationManager.AppSettings["Satisfactioner"] != null)
        {
            string[] strlist = ConfigurationManager.AppSettings["Satisfactioner"].ToString().Trim().Split(',');
            if (strlist.Where(c => c == model.UserCode.ToString()).ToList().Count > 0)
            {
                sb.AppendFormat("<li style='width:120px' {0}><a href=\"SatisfactionList.aspx{1}\" {2}>满意度调查列表</a></li>", class3, sUrlNew, class13);
            }
        }
        //sb.AppendFormat("<li lt=\"6\"{0} ><a target=\"_blank\" href=\"http://www.hyitech.com/Error/caActivehelp.html\" {2}>云知识库</a></li>", class6, sUrlNew, class16);
        sb.AppendFormat("<li lt=\"6\" {0}><a href=\"FAQList.aspx{1}\" {2}>云知识库</a></li>", class6, sUrlNew, class16);

        strInfo = sb.ToString();
    }
}