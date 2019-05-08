using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///loginInfo 的摘要说明
/// </summary>
public class loginInfo
{
    /// <summary>
    /// 用户UID
    /// </summary>
    public int UID
    {
        get;
        set;
    }
    /// <summary>
    /// 用户登录名
    /// </summary>
    public string LoginName
    {
        get;
        set;
    }
    /// <summary>
    /// 用户真实姓名
    /// </summary>
    public string UserName
    {
        get;
        set;
    }

    /// <summary>
    /// 是否是管理员
    /// </summary>
    public bool IsAdmin
    {
        get;
        set;
    }
    /// <summary>
    /// 是否是反馈用户
    /// </summary>
    public bool IsFeedback
    {
        get;
        set;
    }
    /// <summary>
    /// 是否是超级管理员
    /// </summary>
    public bool IsSuperAdmin
    {
        get;
        set;
    }
    /// <summary>
    /// 是否是域账号登陆
    /// </summary>
    public bool IsADUser
    {
        get;
        set;
    }
}
