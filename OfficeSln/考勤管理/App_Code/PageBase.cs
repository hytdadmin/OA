using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using Models;
using HYTD.Common;
using BLL;
using System.Xml.Linq;

/// <summary>
///PageBase 的摘要说明
/// </summary>
public class PageBase : System.Web.UI.Page
{
    public static int pageSize = 10;//每页数
    //public PageBase()
    //{
    //    this.Load += new EventHandler(PageBase_Load);
    //}

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (CurrentUserInfo == null)
        {
            string url = Request.ApplicationPath + "Login.aspx";
            Response.Write("<script>alert('会话过期，请重新登录');window.location.href='" + url + "';</script>");
            this.Response.End();
        }
        this.Load += new System.EventHandler(PageBase_Load);
    }

    private void PageBase_Load(object sender, EventArgs e)
    {
        Page.Title = GetPageTitle() + "-" + this.Title;
        if (CurrentUserInfo == null)
        {
            string url = Request.ApplicationPath + "Login.aspx";
            Response.Write("<script>alert('会话过期，请重新登录');window.location.href='" + url + "';</script>");
            this.Response.End();
        }
    }

    /// <summary>
    /// 是否登录
    /// </summary>
    /// <returns>登录true 未登录false</returns>
    public static bool IsLogin()
    {

        if (new PageBase().CurrentUserInfo == null)
            return false;
        return true;
    }

    /// <summary>
    /// 获取登录用户的usercode
    /// </summary>
    /// <returns>登录返回usercode，未登录返回空字符串</returns>
    public static string GetLoginCode()
    {

        string strUserCode = HttpContext.Current.Request.Cookies["userinfo"] == null ? "" : Encrypt.DecrypString(HttpContext.Current.Request.Cookies["userinfo"].Value);
        return strUserCode;
    }

    /// <summary>
    /// 存取当前用户
    /// </summary>
    public UserInfo CurrentUserInfo
    {
        //get
        //{
        //    return UserCookie.CurrentSession.CurrentUserInfo;
        //}
        //set
        //{
        //    new UserCookie().CurrentUserInfo = value;
        //}
        get
        {
            string strUserCode = HttpContext.Current.Request.Cookies["userinfo"] == null ? "" : Encrypt.DecrypString(HttpContext.Current.Request.Cookies["userinfo"].Value);
            UserInfoBLL bll = new UserInfoBLL();
            UserInfo model = bll.GetUserByUserCode(strUserCode);
            if (model != null)
            {
                return model;
            }
            else
                return null;
        }
        set
        {
            HttpCookie userCookie = new HttpCookie("userinfo");
            //userCookie.Value = Encrypt.EncryptString(value.UserCode);
            userCookie.Value = value.UserCode;
            HttpContext.Current.Response.Cookies.Add(userCookie);
        }
    }



    /// <summary>
    /// 存取当前用户
    /// </summary>
    public static UserInfo GetCurrentUserInfo
    {
        get
        {
            string strUserCode = HttpContext.Current.Request.Cookies["userinfo"] == null ? "" : Encrypt.DecrypString(HttpContext.Current.Request.Cookies["userinfo"].Value);
            UserInfoBLL bll = new UserInfoBLL();
            UserInfo model = bll.GetUserByUserCode(strUserCode);
            if (model != null)
            {
                return model;
            }
            else
                return null;
        }
        set
        {
            HttpCookie userCookie = new HttpCookie("userinfo");

            userCookie.Value = value.UserCode;
            HttpContext.Current.Response.Cookies.Add(userCookie);
        }
    }
    /// <summary>
    /// 获取公共的标题部分
    /// </summary>
    /// <returns></returns>
    public static String GetPageTitle()
    {
        string secret = System.Configuration.ConfigurationManager.AppSettings["PageTitle"];
        return secret;
    }

    public static void Alert(System.Web.UI.Page page, string str)
    {
        page.RegisterStartupScript(new Random().Next().ToString(), string.Format("<script>window.parent.myclose(\"{0}\");</script>", str));
    }


    #region 截取字符串
    /// <summary>
    /// 功能:截取字符串长度......
    /// </summary>
    /// <param name="str">要截取的字符串</param>
    /// <param name="length">字符串长度</param>
    /// <param name="flg">true:加......,flase:不加</param>
    /// <returns></returns>
    public static string GetString(string str, int length, bool flg)
    {
        int i = 0, j = 0;
        foreach (char chr in str)
        {
            if ((int)chr > 127)
            {
                i += 2;
            }
            else
            {
                i++;
            }
            if (i > length)
            {
                str = str.Substring(0, j);
                if (flg)
                    str += "...";
                break;
            }
            j++;
        }
        return str;
    }
    #endregion

    #region 获得选择框返回值
    /// <summary>
    /// 获得选择框返回值
    /// </summary>
    /// <returns></returns>
    public static string ReturnSelectedRowId(Repeater rpt, string controlName)
    {
        string IdString = "";
        foreach (RepeaterItem rp in rpt.Items)
        {
            if (rp.ItemType == ListItemType.Item || rp.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox cb = (CheckBox)rp.FindControl(controlName);
                if (cb.Checked)
                {
                    IdString = IdString + cb.ToolTip.ToString() + ",";
                }
            }
        }
        if (IdString.Length != 0)
            return IdString.Substring(0, IdString.Length - 1);
        else
            return "";
    }
    #endregion

    #region 一般处理程序

    public static void ClearClientPageCache()
    {
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.Expires = 0;
        HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        HttpContext.Current.Response.AddHeader("pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("cache-control", "private");
        HttpContext.Current.Response.CacheControl = "no-cache";
    }
    #endregion


    #region 发布时间处理

    /// <summary>
    /// 发布时间在一个小时内用分钟显示：刚刚、5、10、20、30、40、50分钟前，其他按datetype类型显示
    /// </summary>
    /// <param name="dt">时间</param>
    /// <param name="dateType">日志类型，yyyy-M-d HH:mm </param>
    /// <returns></returns>
    public static string GetDateByPub(object dt, string dateType)
    {
        DateTime newdt = Check.GetDateTime(dt);
        long min = GetIntervalOf2DateTime(DateTime.Now, newdt, "m");
        if (min <= 5)
            //return "刚刚";
            return "5分钟内";
        if (min <= 10)
            return "5分钟前";
        if (min <= 20)
            return "10分钟前";
        if (min <= 30)
            return "20分钟前";
        if (min <= 40)
            return "30分钟前";
        if (min <= 50)
            return "40分钟前";
        if (min <= 60)
            return "50分钟前";

        return newdt.ToString(dateType);
    }

    ///   <summary>      
    ///   得到2个日期的指定格式间隔
    ///   </summary>   
    ///   <param   name="dt1">日期1</param>   
    ///   <param   name="dt2">日期2</param>   
    ///   <param   name="dateformat">间隔格式: y:年 M:月 d:天 h:小时 m:分钟 s:秒 fff:毫秒 ffffff:微秒 fffffff:100毫微秒</param>   
    ///   <returns>间隔   long型</returns>   
    public static long GetIntervalOf2DateTime(DateTime dt1, DateTime dt2, string dateformat)
    {
        try
        {
            long interval = dt1.Ticks - dt2.Ticks;
            DateTime dt11;
            DateTime dt22;

            switch (dateformat)
            {
                case "fffffff"://100毫微妙   
                    break;
                case "ffffff"://微妙   
                    interval /= 10;
                    break;
                case "fff"://毫秒   
                    interval /= 10000;
                    break;
                case "s"://秒   
                    interval /= 10000000;
                    break;
                case "m"://分鐘   
                    interval /= 600000000;
                    break;
                case "h"://小時   
                    interval /= 36000000000;
                    break;
                case "d"://天   
                    interval /= 864000000000;
                    break;
                case "M"://月   
                    dt11 = (dt1.CompareTo(dt2) >= 0) ? dt2 : dt1;
                    dt22 = (dt1.CompareTo(dt2) >= 0) ? dt1 : dt2;
                    interval = -1;
                    while (dt22.CompareTo(dt11) >= 0)
                    {
                        interval++;
                        dt11 = dt11.AddMonths(1);
                    }
                    break;
                case "y"://年   
                    dt11 = (dt1.CompareTo(dt2) >= 0) ? dt2 : dt1;
                    dt22 = (dt1.CompareTo(dt2) >= 0) ? dt1 : dt2;
                    interval = -1;
                    while (dt22.CompareTo(dt11) >= 0)
                    {
                        interval++;
                        dt11 = dt11.AddMonths(1);
                    }
                    interval /= 12;
                    break;
            }
            return interval;
        }
        catch (Exception ex)
        {
            string error = ex.Message;
            return 0;
        }
    }
    #endregion
    #region 去除html标签

    /// <summary>

    /// 去除HTML标记

    /// </summary>

    /// <param name="NoHTML">包括HTML的源码 </param>

    /// <returns>已经去除后的文字</returns>

    public static string NoHTML(string Htmlstring)
    {

        //删除脚本

        Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);

        //删除HTML

        Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);



        Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);



        Htmlstring.Replace("<", "");

        Htmlstring.Replace(">", "");

        Htmlstring.Replace("\r\n", "");

        Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();



        return Htmlstring;

    }
    #endregion

    #region 字符串中英文  获取长度、截取

    /// <summary>
    /// 字符串长度(按字节算)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int StrLength(string str)
    {
        int len = 0;
        byte[] b;

        for (int i = 0; i < str.Length; i++)
        {
            b = Encoding.Default.GetBytes(str.Substring(i, 1));
            if (b.Length > 1)
                len += 2;
            else
                len++;
        }

        return len;
    }

    /// <summary>
    /// 截取指定长度字符串(按字节算)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string StrCut(string str, int length)
    {
        int len = 0;
        byte[] b;
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < str.Length; i++)
        {
            b = Encoding.Default.GetBytes(str.Substring(i, 1));
            if (b.Length > 1)
                len += 2;
            else
                len++;

            if (len >= length)
                break;

            sb.Append(str[i]);
        }

        return sb.ToString();
    }
    #endregion

    //尖括号转移，防止脚本注入
    public static string EscapeJs(string str)
    {
        return str.Replace("<", "&lt;").Replace(">", "&gt;");
    }
    //尖括号转移，防止脚本注入
    public static string EscapeJsN(string str)
    {
        return str.Replace("&lt;", "<").Replace("&gt;", ">");
    }

    /// <summary>
    /// @人名添加颜色
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string GetstrByName(string str)
    {
        StringBuilder strbu = new StringBuilder();
        str += " ";
        if (str.IndexOf("@") >= 0)
        {
            string[] strGroup = str.Split('@');
            for (int i = 0; i < strGroup.Length; i++)
            {
                if (i == 0)
                {
                    strbu.AppendFormat(strGroup[0]);
                    continue;
                }
                string s = strGroup[i];
                int len = s.IndexOf(" ");
                if (len > 0 && len <= 3)//人名在三个以内
                {
                    strbu.AppendFormat("<span style=\"color:#0A8CD2\">@" + s.Substring(0, s.IndexOf(" ")) + "</span>" + s.Substring(s.IndexOf(" "), s.Length - s.IndexOf(" ")));
                }
                else
                    strbu.AppendFormat("@" + s);
            }
        }
        else
            strbu.AppendFormat(str);
        string a = strbu.ToString();
        return a.Length > 0 && a.Substring(0, a.Length - 1) == " " ? a.Substring(0, a.Length - 1) : a;
    }

    #region 插入表情and显示表情

    public static string Expressionxml = "Expressionxml.xml";

    /// <summary>
    /// 获取表情图片的html
    /// </summary>
    /// <returns></returns>
    public static string GetExpressionHtml()
    {
        var query = from m in XElement.Load(System.Web.HttpContext.Current.Server.MapPath("~/config/" + Expressionxml)).Elements("pic")
                    select new
                    {
                        img = m.Element("img").Value,
                        name = m.Element("name").Value
                    };

        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<p>");
        if (query != null)
        {
            foreach (var a in query)
            {
                sb.AppendFormat("<img src=\"/image/Expression/{0}\" onClick=\"setExpressionVal('[{1}]')\" style=\"cursor:pointer;width:20px;height:20px;margin: 3px;\" title=\"{2}\" alt=\"{2}\">", a.img, a.name, a.name);
            }
        }
        sb.AppendFormat("</p>");
        return sb.ToString();
    }


    /// <summary>
    /// 验证字符串是否还有表情 表情标示为：[表情字]
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static bool vPic(string str)
    {
        if (!string.IsNullOrEmpty(str) && str.IndexOf("[") >= 0 && str.IndexOf("]") > 0 && str.LastIndexOf("]") > str.IndexOf("["))
            return true;
        return false;
    }

    /// <summary>
    /// 替换字符串中表情符号，改为表情图片
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ShowExpressionHtml(string str)
    {
        if (vPic(str))
        {
            var query = from m in XElement.Load(System.Web.HttpContext.Current.Server.MapPath("~/config/" + Expressionxml)).Elements("pic")
                        select new
                        {
                            img = m.Element("img").Value,
                            name = m.Element("name").Value
                        };
            foreach (var a in query)
            {
                if (!vPic(str))
                    break;
                str = str.Replace("[" + a.name + "]", string.Format("<img src=\"/image/Expression/{0}\" style=\"width:20px;height:20px;margin-bottom: -3px;cursor:auto;\" title=\"{1}\" alt=\"{1}\"/>", a.img, a.name));
            }
        }
        return str;
    }
    #endregion

    #region 添加log日志

    /// <summary>
    /// 添加log日志
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    public static bool AddLog(Logs log)
    {
        return new LogsBLL().AddLogReturn(log);
    }
    #endregion
}