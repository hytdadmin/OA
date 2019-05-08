using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models.TO;
using HYTD.Common;
using Models;
using Model.TO;
using System.Data;
using BLL;
using System.Text;
public partial class Work_Back_PositionList : PageBase
{
    public string positList = string.Empty;
    public string PageList = string.Empty;
    int pageIndex = 1;
    int pageSize = 10;
    int rowCount = 0;
    public string pName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetPositionList();
    }
    /// <summary>
    /// 职位列表
    /// </summary>
    private void GetPositionList()
    {
        PositionTO to = new PositionTO();
        if (!string.IsNullOrEmpty(Request.Form["PositName"]))
        {
            to.Name = pName = Request.Form["PositName"];
        }
        string orderBy = "";

        if (!string.IsNullOrEmpty(Request.Form["hidPosit"]))
        {
            pageIndex = Convert.ToInt32(Request.Form["hidPosit"]);
        }
        else
        {
            if (!string.IsNullOrEmpty(Request.QueryString["pageIndex"]))
            {
                pageIndex = Convert.ToInt32(Request.QueryString["pageIndex"]);
            }
        }
        DataTable dt = new PositionBLL().GetPositionList(to, pageIndex, pageSize, orderBy, out rowCount);
        StringBuilder sb = new StringBuilder();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (i % 2 == 0)
                    sb.Append("<tr bgcolor=\"#f8f8f8\">");
                else sb.Append("<tr>");
                sb.AppendFormat("<td height=\"40\" align=\"center\"><span class=\"line\" style=\"COLOR: #666;\">{0}</span></td>", i + 1);
                sb.AppendFormat("<td><span class=\"line cl_sp\" style=\"COLOR: #666;\">{0}</span><span class=\"line cl_inp\" style=\"COLOR: #666;display:none;\"><input type=\"text\" value=\"{0}\" style=\"width:120px;\"/></span></td>", dr["Name"]);
                sb.AppendFormat("<td><span class=\"bottom cl_block\"><a style=\"color:#3B96D3;\"  href=\"javascript:void(0)\" onclick=\"UpdatePosit({0})\">修改</a> &nbsp <a style=\"color:#3B96D3;\" onclick=\"delPosition('{1}')\"  href=\"javascript:void(0)\">删除</a></span> <span class=\"bottom cl_none\" style=\"display:none;\"><a style=\"color:#3B96D3;\"  href=\"javascript:void(0)\" onclick=\"SavePosit({0},'{1}')\">保存</a> &nbsp <a style=\"color:#3B96D3;\"  href=\"javascript:void(0)\" onclick=\"CanclePosit({0})\">取消</a></span> </td></tr>", i, dr["ID"]);
            }

        }
        else
        {
            sb.Append("<tr bgcolor=\"#f8f8f8\"><td colspan=\"10\"><span class=\"line\">没有相关信息!</span></td></tr>");
        }
        positList = sb.ToString();
        string url = "PositionList.aspx?pageIndex={0}";
        if (!string.IsNullOrEmpty(pName))
        {
            url += "&PositName=" + pName;
        }
        PageList = DivPage.Pager(pageSize, rowCount, pageIndex, url);
    }
}