using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HYTD.BBS.BLL;
using System.Data;
using System.Text;
using Models;
using HYTD.BBS.Model.TO;

public partial class Posts_index : BbsPageBase
{
    PostsBLL BLL_Post = new PostsBLL();
    CategoryBLL BLL_Cate = new CategoryBLL();
    PostsTO To_Posts = new PostsTO();
    public int intIsADUser = 0;
    public string strtt = "";//分页绑定到前台
    public int pageIndex = 1;//当前页（初始为1）
    public int pageSize = 10;//显示条数
    int totalpage = 0;//总分页
    string url = "index.aspx?page={0}";//设置分页时添加url参数 

    string type_pb = string.Empty;//顶部分类导航
    string st = string.Empty;//筛选
    string px = string.Empty;//排序
    string typecid = string.Empty;//左侧板块导航

    public string CategoryListInfo = string.Empty;

    /// <summary>
    /// 内部类 查询参数
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UserInfo.IsADUser)
            intIsADUser = 1;
        type_pb = Request.QueryString["type_pb"];
        if (!string.IsNullOrEmpty(type_pb))
        {
            url += "&type_pb=" + type_pb;
        }

        st = Request.QueryString["st"];
        if (!string.IsNullOrEmpty(st))
        {
            url += "&st=" + st;
        }

        px = Request.QueryString["px"];
        if (!string.IsNullOrEmpty(px))
        {
            url += "&px=" + px;
        }

        typecid = Request.QueryString["typecid"];
        if (!string.IsNullOrEmpty(typecid))
        {
            url += "&typecid=" + typecid;
        }

        if (!IsPostBack)
        {
            ListInfoBind();
        }
    }

    /// <summary>
    /// 绑定信息列表
    /// </summary>
    public void ListInfoBind()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["page"]))//分页
        {
            pageIndex = int.Parse(Request.QueryString["page"]);
        }
        //string sstxt = this.scbar_txt.Value;//搜索
        string type = Request.QueryString["type_pb"];
        string typecid = Request.QueryString["typecid"];
        string fk = Request.QueryString["fk"];
        string ss = Request.QueryString["ss"];
        string px = Request.QueryString["px"];
        string st = Request.QueryString["st"];
        string orderBy = string.Empty;
        if (!string.IsNullOrEmpty(st))
        {
            switch (st)
            {
                case "0":
                    break;
                case "1":
                    To_Posts.ReleaseTime = DateTime.Now.AddDays(-1);
                    break;
                case "2":
                    To_Posts.ReleaseTime = DateTime.Now.AddDays(-2);
                    break;
                case "7":
                    To_Posts.ReleaseTime = DateTime.Now.AddDays(-7);
                    break;
                case "30":
                    To_Posts.ReleaseTime = DateTime.Now.AddMonths(-1);
                    break;
                case "60":
                    To_Posts.ReleaseTime = DateTime.Now.AddMonths(-2);
                    break;
            }
        }
        if (!string.IsNullOrEmpty(px))
        {
            switch (px)
            {
                case "1"://发帖时间
                    orderBy = "p.ReleaseTime desc";
                    break;
                case "2"://回复数
                    orderBy = " p.ReCount desc";
                    break;
                case "3":
                    orderBy = " p.LookCount desc";
                    break;
                case "4"://最后发表时间
                    orderBy = " p.LastTime desc";
                    break;
                case "5":
                    orderBy = " p.LookCount desc";
                    break;
            }
        }
        if (!string.IsNullOrEmpty(type))//热点问题
        {
            if (type == "hot")//热点问题
                To_Posts.HotCount = BBsConfig.HotCount;
            if (type == "back")//已反馈
                To_Posts.isback = "1";
            if (type == "noback")//未反馈
                To_Posts.isback = "0";
            if (type == "myself")//我自己的问题
            {
                To_Posts.ReleaseUser = UserInfo.LoginName;
            }
        }
        if (!string.IsNullOrEmpty(typecid))//导航
        {
            int cid = 0;
            if (int.TryParse(typecid, out cid))
            {
                To_Posts.CID = cid;
            }
        }
        if (!string.IsNullOrEmpty(ss))
        {
            To_Posts.Title = ss;
        }
        if (!string.IsNullOrEmpty(fk))
        {
            To_Posts.isFeedback = 1;
        }
        DataTable dt = BLL_Post.GetPostsList(To_Posts, pageIndex, pageSize, orderBy, out totalpage);
        if (dt.Rows.Count > 0)
        {
            Re_InfoList.DataSource = dt;
            Re_InfoList.DataBind();
        }
        strtt = DividePage.Pager(pageSize, totalpage, pageIndex, url);//分页
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

    public string CheckAdmin(string isAnonymity, string mc)
    {
        string name = string.Empty;
        if (mc.Trim().Length == 0)
            return "";
        if (UserInfo.IsAdmin)
        //if (true)
        {
            if (isAnonymity == "1")
            {
                name = "匿名<span style=\"color:#666\">[" + mc + "]</span>";
            }
            if (isAnonymity == "0")
            {
                name = "" + mc + "";
            }

        }
        else
        {
            if (isAnonymity == "0")
            {
                name = "" + mc + "";
            }
            if (isAnonymity == "1")
            {
                name = "匿名";
            }
        }
        return name;
    }
    public string CheckFanKuiAdmin(string isAnonymity, string mc)
    {
        string name = string.Empty;
        if (mc.Trim().Length == 0)
            return "";
        if (UserInfo.IsAdmin)
        {
            if (getNameisAnonymity(mc))
            {
                name = "匿名<span style=\"color:#666\">[" + mc.Split('|')[1].Trim() + "]</span>";
            }
            else
            {
                name = "" + mc + "";
            }

        }
        else
        {
            if (getNameisAnonymity(mc))
            {
                if (UserInfo.UserName == mc.Split('|')[1].Trim())
                {
                    name = "匿名<span style=\"color:#666\">[" + mc.Split('|')[1].Trim() + "]</span>";
                }
                else
                {
                    name = "匿名";
                }
            }
            else
            {
                name = "" + mc + "";
            }
        }
        return name;
    }

    /// <summary>
    /// 20131115 liuyh 
    /// 判断时候匿名回帖 如果格式为：匿名|管理员 则为匿名回帖
    /// </summary>
    /// <param name="strName"></param>
    /// <returns></returns>
    private bool getNameisAnonymity(string strName)
    {
        bool bolRet = false;
        string[] str = strName.Split('|');
        if (str.Length > 1 && str[1].Trim().Length > 0)
            bolRet = true;
        return bolRet;
    }
}