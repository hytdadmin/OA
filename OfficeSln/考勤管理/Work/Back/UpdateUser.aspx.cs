using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Models;
using HYTD.Common;

public partial class Work_Back_UpdateUser :PageBase
{
    public UserInfo user=null;
    public string Status = string.Empty;
    public string isAdmin = string.Empty;
    public string EntyTime = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateUserInfomation();
            int str = Convert.ToInt32(Context.Request.QueryString["userID"]);
        }
    }
    /// <summary>
    /// 用户信息
    /// </summary>
    private void UpdateUserInfomation()
    {
        if (!string.IsNullOrEmpty(Context.Request.QueryString["userID"]))
        {
            int id = Convert.ToInt32(Context.Request.QueryString["userID"]);
            UserInfoBLL bll = new UserInfoBLL();
            user = new UserInfo();
            user = bll.GetUserInfoEntity(id);
            if (user.EntyTime!=null)
            {
                EntyTime = Convert.ToDateTime(user.EntyTime).ToString("yyyy-MM-dd");
            }
           
            //Status = PublicEnum.GetEnumDescription<PublicEnum.PublicStatus>(user.UserStatus.ToString());\
            if (user.IsAdmin!=null&&!string.IsNullOrEmpty(user.IsAdmin.ToString()))
            {
                if (Convert.ToInt32(user.IsAdmin) == 0)
                    isAdmin = "<input type='radio' name='rdAdmin' value='1'id='radio' style='width:50px;' />是 <input type='radio' name='rdAdmin' value='0'checked='checked' id='radio'style='width:50px;' />否";
                else if(Convert.ToInt32(user.IsAdmin) == 1)
                    isAdmin = "<input type='radio' name='rdAdmin' value='1' id='radio' checked='checked'style='width:50px;' />是 <input type='radio' name='rdAdmin' value='0'  id='radio' style='width:50px;' />否";
              
            }
        }
    }
   
}