using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using BLL;
using HYTD.Common;

/// <summary>
///UserCookie 的摘要说明
/// </summary>
public class UserCookie
{
    #region private properties and methods

    private static UserCookie instance;
    private static UserInfo instanceUser;

    /// <summary>
    /// Create object follow singleton pattern
    /// </summary>
    /// <returns></returns>
    private static UserCookie currentSession()
    {
        if (instance == null)
        {
            instance = new UserCookie();
        }
        return instance;
    }

    #endregion

    #region public properties and methods

    /// <summary>
    /// Get the single object
    /// </summary>
    public static UserCookie CurrentSession
    {
        get
        {
            return currentSession();
        }
    }

    /// <summary>
    /// 存取当前用户
    /// </summary>
    public UserInfo CurrentUserInfo
    {
        get
        {
            if (HttpContext.Current.Request.Cookies["userinfo"] != null&&instanceUser != null)
                return instanceUser;
            else
            {
                string strUserCode = HttpContext.Current.Request.Cookies["userinfo"] == null ? "" : Encrypt.DecrypString(HttpContext.Current.Request.Cookies["userinfo"].Value);
                UserInfoBLL bll = new UserInfoBLL();
                UserInfo model = bll.GetUserByUserCode(strUserCode);
                if (model != null)
                {
                    instanceUser = model;
                    return model;
                }
                else
                    return null;
            }
        }
        set
        {
            HttpCookie userCookie = new HttpCookie("userinfo");
            //userCookie.Value = Encrypt.EncryptString(value.UserCode);
            userCookie.Value = value.UserCode;
            HttpContext.Current.Response.Cookies.Add(userCookie);
        }
    }
    #endregion
}