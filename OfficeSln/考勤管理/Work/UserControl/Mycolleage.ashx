<%@ WebHandler Language="C#" Class="Mycolleage" %>

using System;
using System.Web;
using System.Data;
using Models;
using Model.TO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

public class Mycolleage : IHttpHandler
{

    string keyValue = string.Empty;
    int deptId = 0;
    public void ProcessRequest(HttpContext context)
    {
        //清除缓存
        PageBase.ClearClientPageCache();
        if (!string.IsNullOrEmpty(context.Request["keyValue"]))
        {
            keyValue = context.Request["keyValue"].ToString();
        }
        if (!string.IsNullOrEmpty(context.Request["deptid"]))
        {
            string str = context.Request["deptid"].ToString();
            deptId = Convert.ToInt32(context.Request["deptid"].ToString());
        }
        GetColleage(deptId, keyValue, context);

    }
    //获取同事
    private void GetColleage(int deptId, string keyValue, HttpContext context)
    {

        int totalCount = 0;
        int pageIndex = Check.GetInt32(context.Request["p"]);
        if (pageIndex == 0)
        {
            pageIndex = 1;
        }
        int pageSize = 15;
        string orderBy = " UserId desc";

        BLL.UserInfoBLL bllUser = new BLL.UserInfoBLL();
        BLL.DepartmentBLL bllDep = new BLL.DepartmentBLL();
        BLL.PositionBLL bllPosition = new BLL.PositionBLL();

        PositionTO pTo = new PositionTO();
        UserInfoTO userTo = new UserInfoTO();
        userTo.UserName = keyValue.Equals("输入关键字...") ? "" : keyValue;
        if (deptId != 0)
        {
            userTo.DepartmentId = deptId;
        }
        DepartmentTO depTo = new DepartmentTO();
        DataTable dtDep = bllDep.GetDepartmentList(depTo, 1, 20, "", out totalCount);
        DataTable dtPosition = bllPosition.GetPositionList(pTo, 1, 20, "", out totalCount);
        DataTable dtUser = bllUser.GetUserInfoList(userTo, pageIndex, pageSize, orderBy, out totalCount);
        StringBuilder sbHtml = new StringBuilder();
        sbHtml.Append("<div class=\"right_content_title\">");
        sbHtml.Append("全部同事");
        sbHtml.Append(totalCount);
        sbHtml.Append("人</div>");
        sbHtml.Append("<br>");
        sbHtml.Append(GetDep(deptId));
        sbHtml.Append("<br>");
        sbHtml.Append("<div class=\"right_conten_mr\" id=\"div_mycolleague\">");

        if (dtUser.Rows.Count > 0)
        {
            sbHtml.Append("<table>");
            sbHtml.Append("<tr>");
            int beginflag = 1;
            int endFalg = 3;
            foreach (DataRow drUser in dtUser.Rows)
            {
                string uid = drUser["userCode"].ToString();
                string tel = drUser["tel"].ToString();
                string email = drUser["email"].ToString();
                string userName = drUser["UserName"].ToString();
                string headimg = drUser["headimg"].ToString();
                string url = "../Work/Others_Journal.aspx?uid=" + uid;
                int depId = Convert.ToInt32(drUser["departmentId"].ToString());
                int posiId = Convert.ToInt32(drUser["posiId"].ToString());
                string depName = string.Empty;
                string positionName = string.Empty;
                if (dtDep.Select(" id=" + depId).Length > 0)
                {
                    DataRow[] drArr = dtDep.Select(" id='" + depId + "'");
                    depName = drArr[0]["roleName"].ToString();
                }
                if (dtPosition.Select(" id=" + posiId).Length > 0)
                {
                    DataRow[] drArr = dtPosition.Select(" id='" + posiId + "'");
                    positionName = drArr[0]["name"].ToString();
                }

                bool b = false;
                if ((beginflag - 1) % 3 == 0 && beginflag != 1)
                {
                    b = true;
                    sbHtml.Append("<tr>");
                }
                sbHtml.Append("<td width=\"250\">");
                sbHtml.Append("<table>");
                sbHtml.Append("<tr>");
                sbHtml.Append("<td rowspan=\"2\">");
                sbHtml.Append(" <a  onmouseover=\"popCenterWindow(event,'" + userName + "','" + tel + "','" + email + "')\"  href=\"" + url + "\" target=\"_self\"><img onerror=\"javascript:this.src='/Image/headImg/default.jpg'\" src=\"../../Image/headImg/" + headimg + "\" width=\"50\" height=\"50\" /></a>");

                sbHtml.Append(" </td>");
                sbHtml.Append("<td align=\"left\">");
                sbHtml.Append("&nbsp;");
                sbHtml.Append("<a href=\"" + url + "\" target=\"_self\">");
                sbHtml.Append(userName);
                sbHtml.Append("</a>");
                sbHtml.Append("</td>");
                sbHtml.Append(" </tr>");
                sbHtml.Append("<tr>");
                sbHtml.Append("<td colspan=\"2\" align=\"center\" valign=\"bottom\">");
                sbHtml.Append(" <span class=\"dep\">" + depName + "</span> ");
                sbHtml.Append(positionName);
                sbHtml.Append(" </td>");
                sbHtml.Append("</tr>");
                sbHtml.Append(" </table>");
                sbHtml.Append("  </td>");

                if (beginflag % 3 == 0 && beginflag != 1)
                {
                    sbHtml.Append("</tr>");
                    sbHtml.Append("<tr>");
                    sbHtml.Append("<td align=\"center\" height=\"20\">");
                    sbHtml.Append("&nbsp;");
                    sbHtml.Append("</td>");
                    sbHtml.Append("</tr>");
                }

                beginflag++;
                endFalg++;

            }
            sbHtml.Append("</tr>");
            sbHtml.Append(" </table>");
        }
        sbHtml.Append("</div>");
        //总页数
        sbHtml.AppendFormat("<input type=\"hidden\" id=\"pagecount\" value=\"{0}\"/>", totalCount);
        //每页数量
        sbHtml.AppendFormat("<input type=\"hidden\" id=\"pageSize\" value=\"{0}\"/>", pageSize);

        context.Response.Write(sbHtml.ToString());
    }
    protected string GetDep(int depId)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<table style=\"width:100%\">");
        sb.AppendFormat("<tr>");
        sb.AppendFormat("<td><a id=\"0\" class=\"{0}\" href=\"javascript:SearchUser(0,{1})\">全部</a>", depId == 0 ? "depB" : "depA", 0);
        List<Department> list = new BLL.DepartmentBLL().GetDepListByIsdel();
        if (list != null && list.Count > 0)
        {
            foreach (Department dep in list)
            {
                sb.AppendFormat("丨<a id=\"{1}\" class=\"{2}\" href=\"javascript:SearchUser(0,{1})\">{0}</a>", dep.RoleName, dep.Id, dep.Id == depId ? "depB" : "depA", dep.Id);
            }
        }
        sb.AppendFormat("</td>");
        sb.AppendFormat("</tr>");
        sb.AppendFormat("</table>");
        return sb.ToString();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}