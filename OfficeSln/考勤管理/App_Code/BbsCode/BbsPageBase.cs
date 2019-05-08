using System;
using System.Linq;
using System.Web;
using Models;



/// <summary>
///PageBase 的摘要说明
/// </summary>
public class BbsPageBase : System.Web.UI.Page
{
    public HYTD.BBS.BLL.UserBLL bll = new HYTD.BBS.BLL.UserBLL();
    /// <summary>
    /// 当前登录人信息
    /// </summary>
    public loginInfo UserInfo;
    public BbsPageBase()
    {
        UserInfo = new loginInfo();
        //
        //TODO: 在此处添加构造函数逻辑
        //
        UserInfo = getUserInfo();

    }
    protected override void OnInit(EventArgs e)
    {
        Session["userloined"] = UserInfo;
    }

    private loginInfo getUserInfo()
    {
        UserInfo caUserInfo = PageBase.GetCurrentUserInfo;
        loginInfo info = new loginInfo();
        User user = new User();
        string strLoginName = caUserInfo.UserCode;
        user = bll.GetUserEntity(strLoginName);
        if (user != null)
        {
            info.UID = user.ID;
            info.LoginName = user.LoginName;
            info.UserName = user.Name;
            info.IsAdmin = false;
            info.IsFeedback = false;
            info.IsSuperAdmin = false;
            string[] SuperAdminList = BBsConfig.SuperAdminList.ToString().Split('|');
            if (SuperAdminList.Where(c => c.ToLower().Equals(info.LoginName.ToLower())).Count() > 0)
            {
                info.IsSuperAdmin = true;
            }
            string[] strAdminList = BBsConfig.AdminList.ToString().Split('|');
            if (strAdminList.Where(c => c.ToLower().Equals(info.LoginName.ToLower())).Count() > 0)
            {
                info.IsAdmin = true;
            }
            string[] strFeedbackList = BBsConfig.FeedbackList.ToString().Split('|');
            if (strFeedbackList.Where(c => c.ToLower().Equals(info.LoginName.ToLower())).Count() > 0)
            {
                info.IsFeedback = true;
            }
        }
        else
        {
            //添加用户
            User userNew = new User();
            userNew.LoginName = caUserInfo.UserCode.ToString();
            userNew.Name = caUserInfo.UserName.ToString();
            userNew.Email = caUserInfo.Email;
            userNew.Phone = "";
            userNew.DeptID = caUserInfo.DepartmentId;
            userNew.UpadateTime = DateTime.Now;
            userNew.Guid = caUserInfo.UserID.ToString();
            userNew.TitleCount = 0;
            userNew.ReCount = 0;
            userNew.FeedbackCount = 0;
            bll.AddUser(userNew);
        }

        return info;
    }



}