using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Xml;

/// <summary>
///Config 的摘要说明
/// </summary>
public class BBsConfig
{
    public BBsConfig()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 超级管理员名单
    /// </summary>
    public static string SuperAdminList
    {
        get
        {
            string str = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            string strP = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["mangers"]);
            xmlDoc.Load(strP);
            if (xmlDoc != null)
            {
                str = xmlDoc.SelectSingleNode("/managers/item/SuperAdminList").InnerText.Trim();
            }
            return str;
        }
    }
    /// <summary>
    /// 管理员名单
    /// </summary>
    public static string AdminList
    {
        get
        {
            string str = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            string strP = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["mangers"]);
            xmlDoc.Load(strP);
            if (xmlDoc != null)
            {
                str = xmlDoc.SelectSingleNode("/managers/item/AdminList").InnerText.Trim();
            }
            return str;
        }
    }
    /// <summary>
    /// 反馈人员名单
    /// </summary>
    public static string FeedbackList
    {
        get
        {
            string str = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            string strP = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["mangers"]);
            xmlDoc.Load(strP);
            if (xmlDoc != null)
            {
                str = xmlDoc.SelectSingleNode("/managers/item/FeedbackList").InnerText.Trim();
            }
            return str;
        }
    }
    /// <summary>
    /// 内网ip段
    /// </summary>
    public static string IP
    {
        get { return ConfigurationManager.AppSettings["IP"]; }
    }
    /// <summary>
    /// 域名称
    /// </summary>
    public static string ADName
    {
        get { return ConfigurationManager.AppSettings["ADName"]; }
    }
    /// <summary>
    ///帖子热点数量设置
    /// </summary>
    public static string HotCount
    {
        get { return ConfigurationManager.AppSettings["HotCount"]; }
    }
    /// <summary>
    ///帖子热点数量设置
    /// </summary>
    public static int TZCounts
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["tongzhicounts"].ToString()); }
    }

    /// <summary>
    ///帖子热点数量设置
    /// </summary>
    public static int FileCounts
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["wenjiancounts"].ToString()); }
    }
    /// <summary>
    ///帖子热点数量设置
    /// </summary>
    public static int FeedbackCounts
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["Feedbackcounts"].ToString()); }
    }
}