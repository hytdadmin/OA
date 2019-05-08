using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using HYTD.BBS.BLL;
using Models;
using HYTD.BBS.Model.TO;
using System.Data;

public partial class CategoryList : BbsPageBase
{

    public string strCategory = string.Empty;//分类
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UserInfo.IsADUser && !UserInfo.IsSuperAdmin && !UserInfo.IsAdmin)
        {
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "script", "<script>alert('您没有操作此模块的权限!');document.location.href='/index.aspx';</script>");
        }
        if (!IsPostBack)
        {
            getCategory();
        }
    }
    private void getCategory()
    {
        CategoryBLL catBll = new CategoryBLL();
        List<Category> mList = new List<Category>();
        mList = catBll.GetCategoryList().Where(c => c.satatus == 1).OrderBy(c => c.DisplayOrder).ToList();
        StringBuilder sb = new StringBuilder();
        foreach (var v in mList)
        {
            sb.AppendFormat("<tr><td>{0}</td><td><input type=\"text\" class='display_order' value='{1}' /><input type=\"button\" value=\"更改顺序\" class='order_change' tid=\"{2}\" /></td><td><a tid=\"{2}\" class=\"type_op\" href=\"javascript:;\">删除</a></td></tr>", v.Name, v.DisplayOrder, v.ID);

        }
        strCategory = sb.ToString();
    }
}