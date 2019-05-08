using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using HYTD.BBS.BLL;
using HYTD.BBS.Model.TO;
using Models;

public partial class Posts_Publish : BbsPageBase
{
    PostsBLL BLL_Post = new PostsBLL();
    CategoryBLL BLL_Cate = new CategoryBLL();
    PostsTO To_Posts = new PostsTO();
    public int intIsADUser = 0;
    public string loginname = "";
    /// <summary>
    /// 内部类 查询参数
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UserInfo.IsADUser)
            intIsADUser = 1;
        loginname = UserInfo.LoginName;
    }

    /// <summary>
    /// 获取分类列表
    /// </summary>
    /// <param name="type">列表形式</param>
    public string CategoryList(string type)
    {
        StringBuilder strb = new StringBuilder();
        List<Category> Clist = BLL_Cate.GetCategoryList().Where(c => c.satatus == 1).ToList();
        if (Clist.Count > 0)
        {
            int i = 0;
            if (type == "1")//1 表示板块导航处
            {
                strb.Append("<dd id=cid1>");
                foreach (Category C in Clist)
                {

                    if (i > 0)
                    {
                        strb.Append("<dd id=cid" + C.ID + ">");
                    }
                    strb.Append("<a onclick=\"getinfopost('tpcid','" + C.ID + "')\" href=\"javascript:;\">" + C.Name + "</a>");
                    strb.Append("</dd>");
                    i++;
                }
            }
            if (type == "2")//2表示快速发问选择类型
            {
                strb.AppendFormat("<option selected=\"selected\" value=0>选择分类</option>");
                foreach (Category C in Clist)
                {
                    strb.AppendFormat("<option value='{0}'>{1}</option>", C.ID, C.Name);
                }
            }
            //   CategoryListInfo = strb.ToString();
        }
        return strb.ToString();
    }

}