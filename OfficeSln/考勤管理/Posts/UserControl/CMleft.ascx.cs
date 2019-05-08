using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HYTD.BBS.Model.TO;
using Models;

public partial class UserControl_CMleft : System.Web.UI.UserControl
{
    public string strTyleInfo = string.Empty;
    private HYTD.BBS.BLL.CategoryBLL BLL_Cate = new HYTD.BBS.BLL.CategoryBLL();
    protected loginInfo lInfo = new loginInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        lInfo = (loginInfo)Session["userloined"];
        if (lInfo == null)
            Response.Redirect(Request.Url.ToString());
        //if (!IsPostBack)
        getTypeList();
    }
    /// <summary>
    /// 获取分类列表
    /// </summary>
    /// <param name="type">列表形式</param>
    private void getTypeList()
    {
        string type = "1";
        StringBuilder strb = new StringBuilder();
        List<Category> Clist = BLL_Cate.GetCategoryList().Where(c => c.satatus == 1).OrderBy(c => c.DisplayOrder).ToList();
        strb.Append("<dl class=\"a\" id=\"lf_1\"><dt><a title=\"全部类型\"  href=\"/Posts/index.aspx\">全部类型</a></dt>");
        if (Clist.Count > 0)
        {
            int i = 0;
            if (type == "1")//1 表示板块导航处 class=\"bdl_a\"
            {
                strb.Append("<dd>");
                foreach (Category C in Clist)
                {
                    if (i > 0)
                    {
                        strb.Append("<dd>");
                    }
                    strb.AppendFormat("<a href=\"javascript:;\" tp=\"{0}\" title=\"{1}\" url=\"index.aspx?typecid={0}\">{1}</a>"
                    , C.ID, C.Name);
                    strb.Append("</dd>");
                    i++;
                }
            }
            strb.Append("</dl>");
            if (lInfo != null && lInfo.IsSuperAdmin)
            {
                strb.Append("<dl class=\"a\" id=\"lf_2\"><dt><a title=\"后台管理\" hidefocus=\"true\" href=\"#\">后台管理</a></dt>");
                strb.AppendFormat("<dd><a href=\"/Posts/ManagesInfo.aspx?typecid={0}\" tp=\"{0}\" title=\"管理员管理\">管理员管理</a></dd>", 1002);
                strb.AppendFormat("<dd><a href=\"/Posts/ManagesInfo.aspx?typecid={0}\" tp=\"{0}\" title=\"反馈人员管理\">反馈人员管理</a></dd>", 1003);
                strb.AppendFormat("<dd><a href=\"/Posts/Searchlog.aspx?typecid={0}\" tp=\"{0}\" title=\"日志查询\">日志查询</a></dd>", 1110);
                strb.AppendFormat("<dd><a href=\"/Posts/SensitiveWords.aspx?typecid={0}\" tp=\"{0}\" title=\"敏感词管理\">敏感词管理</a></dd>", 1111);
                strb.AppendFormat("<dd><a href=\"/Posts/CategoryList.aspx?typecid={0}\" tp=\"{0}\" title=\"意见与反馈分类\">意见与反馈分类</a></dd>", 1112);
                strb.Append("</dl>");
            }
            else if (lInfo != null && lInfo.IsAdmin)
            {
                strb.Append("<dl class=\"a\" id=\"lf_2\"><dt><a title=\"后台管理\" hidefocus=\"true\" href=\"#\">后台管理</a></dt>");
                strb.AppendFormat("<dd><a href=\"/Posts/Searchlog.aspx?typecid={0}\" tp=\"{0}\" title=\"日志查询\">日志查询</a></dd>", 1110);
                strb.AppendFormat("<dd><a href=\"/Posts/SensitiveWords.aspx?typecid={0}\" tp=\"{0}\" title=\"敏感词管理\">敏感词管理</a></dd>", 1111);
                strb.AppendFormat("<dd><a href=\"/Posts/CategoryList.aspx?typecid={0}\" tp=\"{0}\" title=\"意见与反馈分类\">意见与反馈分类</a></dd>", 1112);
                strb.Append("</dl>");
            }

            if (type == "2")//2表示快速发问选择类型
            {
                strb.AppendFormat("<option selected=\"selected\" value=0>选择主题分类</option>");
                foreach (Category C in Clist)
                {
                    strb.AppendFormat("<option value='{0}'>{1}</option>", C.ID, C.Name);
                }
            }
            //   CategoryListInfo = strb.ToString();
        }
        strTyleInfo = strb.ToString();

    }
}