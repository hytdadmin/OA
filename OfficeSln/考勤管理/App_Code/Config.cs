using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
/*
Author：liulei
Version：1.0
Date： 2013年4月23日16:03:19
Description: 读取Web.config中的信息
*/
/// <summary>
///读取Web.config中的信息
/// </summary>
public class Config
{
    /// <summary>
    /// 默认提醒天数
    /// </summary>
    public static int RemindDate
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["remindDate"]); }
    }
    /// <summary>
    /// 分页每页显示数据条数
    /// </summary>
    public static int NumPerPage
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["numPerPage"]); }
    }

    /// <summary>
    /// 分页控件页码数量
    /// </summary>
    public static int PageNumShown
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["pageNumShown"]); }
    }

    /// <summary>
    /// 分页控件页码数量
    /// </summary>
    public static string ResourceFile
    {
        get { return ConfigurationManager.AppSettings["ResourceFile"].ToString(); }
    }
    /// <summary>
    /// 分页控件页码数量
    /// </summary>
    public static string publicImage
    {
        get { return ConfigurationManager.AppSettings["publicImage"].ToString(); }
    }
    /// <summary>
    /// 分页控件页码数量
    /// </summary>
    public static string publicImage_Thumbnail
    {
        get { return ConfigurationManager.AppSettings["publicImage_Thumbnail"].ToString(); }
    }

    /// <summary>
    /// 在线统计人数，间隔多少分钟获取一次
    /// </summary>
    public static string visitMin
    {
        get { return ConfigurationManager.AppSettings["visitMin"].ToString(); }
    }
    /// <summary>
    /// 工单类型主类下ID
    /// </summary>
    public static int WorkBillStatusID
    {
        get {
            int intStatus = 27;
            int.TryParse(ConfigurationManager.AppSettings["workbillStatusAll"].ToString(),out intStatus);
            return intStatus;
        }
    }
}