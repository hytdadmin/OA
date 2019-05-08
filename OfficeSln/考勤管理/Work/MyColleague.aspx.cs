using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models.TO;
using System.Text;
using BLL;
using System.Data;
using Models;
using Model.TO;

public partial class MyColleague : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Bind();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Bind(); 
    }
    protected string GetDep()
    {
        int totalCount = 0;
        int pageIndex = 1;
        if (pageIndex == 0)
        {
            pageIndex = 1;
        }
        int pageSize = 15;
        string orderBy = " ";

        StringBuilder sbHtml = new StringBuilder();
        sbHtml.Append("&nbsp;<a onclick=\"SearchUser(0,0)\">全部</a>&nbsp;|");
        BLL.DepartmentBLL bllDep = new DepartmentBLL();
        DepartmentTO depTo = new DepartmentTO();
        DataTable dtDep = bllDep.GetDepartmentList(depTo, pageIndex, pageSize, "", out totalCount);
        if (dtDep.Rows.Count > 0)
        {
            int flag = 0;
            foreach (DataRow drDep in dtDep.Rows)
            {
                string depName = drDep["roleName"].ToString();
                string depId = drDep["id"].ToString();
                if (flag == dtDep.Rows.Count - 1)
                {
                    sbHtml.Append("&nbsp;<a onclick=\"SearchUser(0," + depId + ")\">" + depName + "</a>&nbsp;");
                }
                else
                {
                    sbHtml.Append("&nbsp;<a onclick=\"SearchUser(0," + depId + ")\">" + depName + "</a>&nbsp;|");
                }
                flag++;
            }
        }
        return sbHtml.ToString();


    }

}