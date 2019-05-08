using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Models;
using Model.TO;
using System.Text;
using System.Data;
using HYTD.Common;
public partial class Work_Back_UserList : PageBase
{
    public string UserList = string.Empty;
    public string userName = string.Empty;
    public string trueName = string.Empty;
    public int DepartmentId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowUserAll();
            Bind();
        }     
    }

    private void Bind()
    { 
        List<Department> list = new DepartmentBLL().GetDepartmentList();
        if (list != null && list.Count > 0)
        {
            ddlDep.DataSource = list;
            ddlDep.DataTextField = "RoleName";
            ddlDep.DataValueField = "id";
            ddlDep.DataBind();
        }
        ddlDep.Items.Insert(0, new ListItem("请选择", "0"));
    }
    /// <summary>
    /// 用户列表
    /// </summary>
    private void ShowUserAll()
    {
        int numPerPage = 10;//每页显示数量
        string orderBy = " UserID desc ";
        int totalCount = 0;
        UserInfoBLL bll = new UserInfoBLL();
        UserInfoTO sto = new UserInfoTO();
       
        sto.UserCode = txtUsere.Text.Trim();
        sto.UserName = txtTrueName.Text.Trim();
        if (ddlDep.SelectedValue!="")
        {
            sto.DepartmentId = Convert.ToInt32(ddlDep.SelectedValue);
        }
        DataTable dt = bll.GetUserInfoList(sto, AspNetPager1.CurrentPageIndex, numPerPage, orderBy, out totalCount,"");
        StringBuilder sb = new StringBuilder();
       if (dt != null&&dt.Rows.Count > 0)
       {
           AspNetPager1.RecordCount = totalCount;
           AspNetPager1.PageSize = numPerPage;
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               DataRow dr = dt.Rows[i];
               sb.AppendFormat("<tr><td align='center'>{0}</td>", i + 1 * (numPerPage * (AspNetPager1.CurrentPageIndex - 1)+1));
               sb.AppendFormat("<td align='center'>{0}</td>", dr["UserCode"]);
               sb.AppendFormat("<td align='center'style='width:75px;'>{0}</td>", dr["UserName"]);
               sb.AppendFormat("<td align='center'style='width:75px;'>{0}</td>", dr["Name"]);
               sb.AppendFormat("<td align='center'style='width:75px;'>{0}</td>", dr["RoleName"]);
               sb.AppendFormat("<td align='center' style='width:70px;'>{0}</td>", PublicEnum.GetEnumDescription<PublicEnum.PublicIsVindicate>(dr["IsAdmin"].ToString()));
               if (Convert.ToInt32(dr["UserStatus"])==1)
               {
                   sb.AppendFormat("<td align='center'style='width:70px;'>{0}</td>", PublicEnum.GetEnumDescription<PublicEnum.PublicStatus>(dr["UserStatus"].ToString()));
               }
               else
               {
                   sb.AppendFormat("<td align='center'style='width:70px;color:Red;'>{0}</td>", PublicEnum.GetEnumDescription<PublicEnum.PublicStatus>(dr["UserStatus"].ToString()));
               }
               if (dr["EntyTime"] is DBNull)
               {
                   sb.AppendFormat("<td align='center'>{0}</td>","");
               }
               else
               {
                   sb.AppendFormat("<td align='center'>{0}</td>",Convert.ToDateTime(dr["EntyTime"]).ToString("yyyy-MM-dd"));
               }
               sb.AppendFormat("<td align='center'>{0}</td>", dr["Tel"]);
               sb.AppendFormat("<td align='center' style='width:160px;'>{0}</td>", dr["Email"]);
               sb.AppendFormat("<td align='center'><a href='UpdateUser.aspx?userID={0}'>修 改</a> &nbsp&nbsp&nbsp  <a     href='javascript:;' onclick='delUser({0})' >禁 用</a></td>", dr["UserID"]);
               sb.Append("</tr>");
           }
       }
       UserList = sb.ToString();
    }
    //分页
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        ShowUserAll();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        ShowUserAll();
        
    }
}