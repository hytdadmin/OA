using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using HYTD.BBS.BLL;
using Models;
using HYTD.BBS.Model.TO;

public partial class detail : BbsPageBase
{
    /// <summary>
    /// 非域用户登录不能回帖
    /// </summary>
    public int intIsADUser = 0;
    public int fatherID = 2;
    public int cID = 1;
    public string strtt = "";
    public int pageIndex = 1;
    public int pageSize = 10;
    public int rowCount = 400;
    public int pageNum = 1;
    public string title = string.Empty;
    public string userName = string.Empty;
    public string sn = string.Empty;
    DataTable dtReposts;
    string catId = string.Empty;
    string catName = string.Empty;
    public string content = "请输入搜索内容";
    RePostsTO repTo = new RePostsTO();
    RePostsBLL repBll = new RePostsBLL();
    PostsBLL pBll = new PostsBLL();
    UserBLL userBll = new UserBLL();
    List<Posts> listAllPost = new List<Posts>();
    public Posts postModel = new Posts();
    public string loginname = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UserInfo.IsFeedback)
        {
            sn = "<input type=\"radio\" name=\"isAnonymity\" value=\"0\" checked=\"checked\" />实名";
        }
        else
        {
            sn = "<input type=\"radio\" name=\"isAnonymity\" value=\"1\" />匿名<input type=\"radio\" name=\"isAnonymity\" value=\"0\" checked=\"checked\" />实名";

        }
        if (UserInfo != null)
        {
            userName = UserInfo.LoginName;
            if (UserInfo.IsADUser)
                intIsADUser = 1;
            loginname = UserInfo.LoginName;
        }
        if (!IsPostBack)
        {
            listAllPost = pBll.GetPostsList();
            if (!string.IsNullOrEmpty(Request.QueryString["fatherID"]))
            {
                fatherID = Convert.ToInt32(Request.QueryString["fatherID"].ToString());
            }
            if (!string.IsNullOrEmpty(Request.QueryString["pageIndex"]))
            {
                pageIndex = int.Parse(Request.QueryString["pageIndex"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["content"]))
            {
                content = Request.QueryString["content"].ToString().Trim().Replace("&nbsp;", "");
            }
            //增加浏览次数
            PostsBLL postBll = new PostsBLL();
            postModel = postBll.GetPostsEntity(fatherID);
            postModel.LookCount += 1;
            postBll.UpdatePosts(postModel);
            cID = Convert.ToInt32(postModel.CID);
            //导航
            CategoryBLL catBll = new CategoryBLL();
            List<Category> listCat = catBll.GetCategoryList().Where(c => c.ID == cID).ToList();
            if (listCat.Count > 0)
            {
                Category catModel = listCat[0];
                catName = catModel.Name;
                catId = catModel.ID.ToString();
            }
            title = "意见与反馈 - " + catName;
            //绑定回复数据
            if (content == "请输入搜索内容")
                content = "";
            repTo.Content = content;
            repTo.FatherID = fatherID;
            string orderBy = " isReFeedback desc,ReDatetime asc";
            dtReposts = repBll.GetRePostsList(repTo, pageIndex, pageSize, orderBy, out rowCount);
            //分页
            string url = "detail.aspx?pageIndex={0}&fatherID=" + fatherID;
            strtt = DividePage.Pager(pageSize, rowCount, pageIndex, url);
            //获取当前页
            if (rowCount % pageSize == 0)
            {
                pageNum = rowCount / pageSize;
            }
            else
            {
                pageNum = (rowCount / pageSize) + 1;
            }
        }

    }



    //获取导航
    public string GetGuider()
    {
        StringBuilder sbHtml = new StringBuilder();
        sbHtml.Append("  <a href=\"index.aspx\" class=\"nvhm\" title=\"首页\">意见与反馈</a><em>&#187;</em><a href=\"index.aspx?typecid=" + catId + "\">" + catName + "</a>");
        return sbHtml.ToString();
    }
    /// <summary>
    /// 获取分类列表
    /// </summary>
    /// <param name="type">列表形式</param>
    public string CategoryList()
    {
        PostsBLL BLL_Post = new PostsBLL();
        CategoryBLL BLL_Cate = new CategoryBLL();
        StringBuilder strb = new StringBuilder();
        List<Category> Clist = BLL_Cate.GetCategoryList().Where(c => c.satatus == 1).ToList();
        if (Clist.Count > 0)
        {
            int i = 0;

            strb.AppendFormat("<option selected=\"selected\" value=0>选择主题分类</option>");
            foreach (Category C in Clist)
            {
                if (postModel.CID != null && postModel.CID.Value == C.ID)
                {
                    strb.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", C.ID, C.Name);
                }
                else
                {
                    strb.AppendFormat("<option value='{0}'>{1}</option>", C.ID, C.Name);
                }
            }

            //   CategoryListInfo = strb.ToString();
        }
        return strb.ToString();
    }
    //获取获取主题
    public string GetBbsTitle()
    {
        List<Posts> listPost = listAllPost.Where(c => c.ID == fatherID && c.status == 1).ToList();
        StringBuilder sbHtml = new StringBuilder();
        if (listPost.Count > 0)
        {
            Posts postModel = listPost[0];
            int PID = postModel.ID;
            string Title = postModel.Title;
            string Description = postModel.Description;
            string LookCount = postModel.LookCount.ToString();
            string ReCount = postModel.ReCount.ToString();
            string LastTime = postModel.LastTime.ToString();
            string LastUser = postModel.LastUser;
            string ReleaseUser = postModel.ReleaseUser;
            string ReleaseTime = postModel.ReleaseTime.ToString();
            string isFeedback = postModel.isFeedback.ToString();
            string UserIP = postModel.UserIP;
            string isAnonymity = postModel.isAnonymity.ToString();
            int titleCount = 0;
            int reCount = 0;
            int feedbackCount = 0;
            string name = string.Empty;

            sbHtml.Append(" <table cellspacing=\"0\" cellpadding=\"0\">");
            sbHtml.Append("<tbody>");
            sbHtml.Append("<tr>");
            sbHtml.Append("<td class=\"pls ptm pbm\">");
            sbHtml.Append("<div class=\"hm\">");
            sbHtml.Append("<span class=\"xg1\">查看:</span> <span class=\"xi1\">" + LookCount + "</span><span class=\"pipe\">|</span><span class=\"xg1\">回复:</span> <span class=\"xi1\">" + ReCount + "</span>");
            sbHtml.Append(" </div>");
            sbHtml.Append("</td>");
            sbHtml.Append("<td class=\"plc ptm pbn vwthd\">");
            sbHtml.Append("<h1 class=\"ts\">");
            sbHtml.Append(" <a href=\"index.aspx?typecid=" + catId + "\">[" + catName + "]</a> " + Title);
            sbHtml.Append("</h1>");
            sbHtml.Append("</td>");
            sbHtml.Append(" </tr>");
            sbHtml.Append(" </tbody>");
            sbHtml.Append("</table>");
            //回复人信息
            User userModel = userBll.GetUserEntity(ReleaseUser);
            if (userModel != null)
            {
                titleCount = userModel.TitleCount;
                reCount = userModel.ReCount;
                feedbackCount = userModel.FeedbackCount;
                name = userModel.Name;
            }
            name = GetName(isAnonymity, ReleaseUser, userModel);

            sbHtml.Append("  <div id=\"post_11941951\">");
            sbHtml.Append("  <table id=\"pid11941951\" summary=\"pid11941951\" cellspacing=\"0\" cellpadding=\"0\">");
            sbHtml.Append(" <tbody>");
            sbHtml.Append(" <tr>");
            sbHtml.Append("<td class=\"pls\" rowspan=\"2\">");
            sbHtml.Append("<div class=\"pi\">");
            sbHtml.Append("<div class=\"authi\" >");
            sbHtml.Append(name);
            sbHtml.Append("</div>");
            sbHtml.Append("</div>");
            sbHtml.Append("<div>");
            sbHtml.Append("<div class=\"tns xg2\" style=\"text-align:center;\">");

            sbHtml.Append("<br>");
            sbHtml.Append("主题:" + titleCount);
            sbHtml.Append("&nbsp;&nbsp;");
            sbHtml.Append("回复:" + reCount);
            sbHtml.Append("&nbsp;&nbsp;");
            sbHtml.Append("反馈:" + feedbackCount);
            sbHtml.Append("  </div>");
            sbHtml.Append("</div>");

            sbHtml.Append("</td>");
            //内容
            sbHtml.Append("<td class=\"plc\">");
            sbHtml.Append("<div class=\"pi\">");
            sbHtml.Append("<div id=\"fj\" class=\"y\">");
            sbHtml.Append("</div>");
            sbHtml.Append("<strong><a id=\"postnum11941951\" style='font-size:12px;'>问题提出</a> </strong>");
            sbHtml.Append("<div class=\"pti\">");
            sbHtml.Append("<div class=\"pdbt\">");
            sbHtml.Append("</div>");
            sbHtml.Append("<div class=\"authi\">");
            sbHtml.Append("<img class=\"authicn vm\" id=\"authicon11943477\" src=\"two/online_member.gif\">");
            sbHtml.Append(" <em id=\"authorposton11943477\">发表于 " + ReleaseTime + "</em>");
            sbHtml.Append("  </div>");
            sbHtml.Append("</div>");
            sbHtml.Append("</div>");
            sbHtml.Append(" <div class=\"pct\">");
            sbHtml.Append("<div class=\"pcb\">");
            sbHtml.Append(" <div class=\"t_fsz\">");
            sbHtml.Append("<table cellspacing=\"0\" cellpadding=\"0\">");
            sbHtml.Append("<tbody>");
            sbHtml.Append("<tr>");
            sbHtml.Append(" <td class=\"t_f\" id=\"postmessage_11943477\">");
            sbHtml.Append(Description);
            sbHtml.Append("  </td>");
            sbHtml.Append(" </tr>");
            sbHtml.Append("</tbody>");
            sbHtml.Append("</table>");
            sbHtml.Append("</div>");
            sbHtml.Append("  <div id=\"comment_11943477\" class=\"cm\">");
            sbHtml.Append("</div>");
            sbHtml.Append("<div id=\"post_rate_div_11943477\">");
            sbHtml.Append(" </div>");
            sbHtml.Append(" </div>");
            sbHtml.Append(" </div>");
            sbHtml.Append("</td>");

            sbHtml.Append("</tr>");
            sbHtml.Append("<!----个性签名暂时无用别删-->");
            sbHtml.Append("<tr>");
            sbHtml.Append(" <td class=\"plc plm\">");
            sbHtml.Append(" </td>");
            sbHtml.Append("</tr>");
            sbHtml.Append("<tr>");
            sbHtml.Append("<td class=\"pls\">");
            sbHtml.Append(" </td>");
            sbHtml.Append("<td class=\"plc\" style=\"overflow: visible;\">");
            sbHtml.Append("<div class=\"po\">");
            sbHtml.Append("<div class=\"pob cl\">");
            if (UserInfo != null)
            {
                if (!UserInfo.IsFeedback)
                {
                    sbHtml.Append(" <em><a class=\"fastre\" href=\"#anchor\" onclick=\"javascript:SelectReply(" + fatherID + "," + 0 + ",0);\">跟帖</a> </em>");
                }
            }
            if (UserInfo != null)
            {
                if (UserInfo.IsFeedback)
                {
                    sbHtml.Append(" <em><a class=\"fastre2\" href=\"#anchor\" onclick=\"javascript:SelectReply(" + fatherID + "," + 0 + ",1);\">反馈</a> </em>");
                }
            }
            sbHtml.Append("<p>");
            if (UserInfo != null)
            {
                if (UserInfo.IsAdmin || UserInfo.LoginName.ToString().Trim().ToLower() == ReleaseUser.Trim().ToLower())
                {
                    sbHtml.Append("<a href=\"javascript:;\" id=\"mgc_post_11943477\" class=\"showmenu\" style=\"display: none;\"></a><a href=\"javascript:;\"  onclick=\"javascript:DeleteTopic(" + fatherID + ")\">删除</a>");
                    sbHtml.AppendFormat("<a href=\"javascript:;\" id=\"mgc_post_11943477\" class=\"showmenu\" style=\"display: none;\"></a><a href=\"javascript:;\" onclick=\"javascript:showedit({0})\">编辑</a>", PID);
                }
                if (UserInfo.LoginName.ToString().Trim().ToLower() == ReleaseUser.Trim().ToLower())
                {
                    sbHtml.AppendFormat("<a href=\"javascript:;\" id=\"mgc_post_11943477\" class=\"showmenu\" style=\"display: none;\"></a><a href=\"javascript:;\" onclick=\"javascript:showedit({0})\">编辑</a>", PID);
                }
            }
            sbHtml.Append("</p>");

            sbHtml.Append("  <ul id=\"mgc_post_11943477_menu\" class=\"p_pop mgcmn\" style=\"display: none;\">");
            sbHtml.Append("  </ul>");
            sbHtml.Append("</div>");
            sbHtml.Append(" </div>");
            sbHtml.Append("</td>");
            sbHtml.Append("</tr>");
            sbHtml.Append(" <tr class=\"ad\">");
            sbHtml.Append("<td class=\"pls\">");
            sbHtml.Append(" </td>");
            sbHtml.Append("  <td class=\"plc\">");
            sbHtml.Append(" </td>");
            sbHtml.Append("  </tr>");
            sbHtml.Append(" </tbody>");
            sbHtml.Append("  </table>");
            sbHtml.Append("</div>");

        }
        return sbHtml.ToString();
    }
    //获取回复
    public string GetBbsReply()
    {
        StringBuilder sbHtml = new StringBuilder();
        if (dtReposts != null && dtReposts.Rows.Count > 0)
        {
            int flag = 1;

            foreach (DataRow drReposts in dtReposts.Rows)
            {
                int floor = (pageIndex - 1) * pageSize + flag;
                string id = drReposts["ID"].ToString();
                string releaseFatherID = drReposts["ReleaseFatherID"].ToString();
                string reUser = drReposts["ReUser"].ToString();
                string reDatetime = drReposts["ReDatetime"].ToString();
                string isReFeedback = drReposts["isReFeedback"].ToString();
                string isFeedback = drReposts["isFeedback"].ToString();
                string UserIP = drReposts["UserIP"].ToString();
                string isAnonymity = drReposts["isAnonymity"].ToString();
                string content = drReposts["Content"].ToString();
                int titleCount = 0;
                int reCount = 0;
                int feedbackCount = 0;
                string name = string.Empty;
                //回复人信息
                User userModel = userBll.GetUserEntity(reUser);
                if (userModel != null)
                {
                    titleCount = userModel.TitleCount;
                    reCount = userModel.ReCount;
                    feedbackCount = userModel.FeedbackCount;
                    name = userModel.Name;
                }
                name = GetName(isAnonymity, reUser, userModel);
                sbHtml.Append("<div id=\"post_11943477\">");
                sbHtml.Append(" <table id=\"pid11943477\" summary=\"pid11943477\" cellspacing=\"0\" cellpadding=\"0\">");
                sbHtml.Append(" <tbody>");
                sbHtml.Append(" <tr>");
                sbHtml.Append("<td class=\"pls\" rowspan=\"2\">");
                sbHtml.Append("<div class=\"pi\">");
                sbHtml.Append("<div class=\"authi\">");
                sbHtml.Append(name);
                sbHtml.Append("</div>");
                sbHtml.Append("</div>");
                sbHtml.Append("<div>");
                sbHtml.Append("<div class=\"tns xg2\" style=\"text-align:center;\">");
                sbHtml.Append("<br>");
                sbHtml.Append("主题:" + titleCount);
                sbHtml.Append("&nbsp;&nbsp;");
                sbHtml.Append("回复:" + reCount);
                sbHtml.Append("&nbsp;&nbsp;");
                sbHtml.Append("反馈:" + feedbackCount);

                sbHtml.Append("  </div>");
                sbHtml.Append("</div>");
                sbHtml.Append("</div>");
                sbHtml.Append("</td>");
                //回复内容
                sbHtml.Append("<td class=\"plc\">");
                sbHtml.Append("<div class=\"pi\">");
                sbHtml.Append("<strong><a onclick=\"#\" id=\"postnum11943477\"><em>" + floor + "</em><sup>#</sup></a> </strong>");
                sbHtml.Append("<div class=\"pti\">");
                sbHtml.Append("<div class=\"pdbt\">");
                sbHtml.Append("</div>");
                sbHtml.Append("<div class=\"authi\">");
                sbHtml.Append("<img class=\"authicn vm\" id=\"authicon11943477\" src=\"two/online_member.gif\">");
                if (isReFeedback == "1")
                {
                    sbHtml.Append(" <em id=\"authorposton11943477\"><font color=red>反馈于</font> " + reDatetime + "</em>");
                }
                else
                {
                    sbHtml.Append(" <em id=\"authorposton11943477\">发表于 " + reDatetime + "</em>");
                }
                sbHtml.Append("  </div>");
                sbHtml.Append("</div>");
                sbHtml.Append("</div>");
                sbHtml.Append(" <div class=\"pct\">");
                sbHtml.Append("<div class=\"pcb\">");
                sbHtml.Append(" <div class=\"t_fsz\">");
                sbHtml.Append("<table cellspacing=\"0\" cellpadding=\"0\">");
                sbHtml.Append("<tbody>");
                sbHtml.Append("<tr>");
                sbHtml.Append(" <td class=\"t_f\" id=\"postmessage_11943477\">");

                //引用内容
                if (!string.IsNullOrEmpty(releaseFatherID) && releaseFatherID != "0")
                {
                    RePosts repTemp = repBll.GetRePostsEntity(Convert.ToInt32(releaseFatherID));
                    if (repTemp != null)
                    {
                        string nameTemp = string.Empty;
                        string reUserTemp = repTemp.ReUser;
                        string reDatetimeTemp = repTemp.ReDatetime.ToString();
                        string contentTemp = repTemp.Content;
                        string isReFeedbackTemp = repTemp.isReFeedback.ToString();
                        string isAnonymityTemp = repTemp.isAnonymity.ToString();
                        //回复人信息
                        User userModelTemp = userBll.GetUserEntity(reUserTemp);
                        if (userModelTemp != null)
                        {
                            nameTemp = userModelTemp.Name;
                        }
                        nameTemp = GetName(isAnonymityTemp, reUserTemp, userModelTemp);
                        sbHtml.Append("<div class=\"quote\">");
                        sbHtml.Append("<blockquote>");
                        sbHtml.Append("<font size=\"2\">");
                        sbHtml.Append("<font color=\"#999999\">");
                        if (repTemp.isReFeedback != null && repTemp.isReFeedback.Value == 1)
                        {
                            sbHtml.Append(nameTemp + " <font color='red'>反馈于</font> " + reDatetimeTemp);
                        }
                        else
                        {
                            sbHtml.Append(nameTemp + " 发表于 " + reDatetimeTemp);
                        }
                        sbHtml.Append("</font>");
                        sbHtml.Append("</font>");
                        sbHtml.Append(" <br>");
                        sbHtml.Append(contentTemp);
                        sbHtml.Append("</blockquote></div>");
                        sbHtml.Append("<br>");
                    }
                }
                //自己内容
                sbHtml.Append(content);

                sbHtml.Append("  </td>");
                sbHtml.Append(" </tr>");
                sbHtml.Append("</tbody>");
                sbHtml.Append("</table>");
                sbHtml.Append("</div>");
                sbHtml.Append("  <div id=\"comment_11943477\" class=\"cm\">");
                sbHtml.Append("</div>");
                sbHtml.Append("<div id=\"post_rate_div_11943477\">");
                sbHtml.Append(" </div>");
                sbHtml.Append(" </div>");
                sbHtml.Append(" </div>");
                sbHtml.Append("</td>");

                sbHtml.Append("</tr>");
                sbHtml.Append("<!----个性签名暂时无用别删-->");
                sbHtml.Append("<tr>");
                sbHtml.Append(" <td class=\"plc plm\">");
                sbHtml.Append(" </td>");
                sbHtml.Append("</tr>");
                sbHtml.Append("<tr>");
                sbHtml.Append("<td class=\"pls\">");
                sbHtml.Append(" </td>");
                sbHtml.Append("<td class=\"plc\" style=\"overflow: visible;\">");
                sbHtml.Append("<div class=\"po\">");
                sbHtml.Append("<div class=\"pob cl\">");

                if (UserInfo != null)
                {
                    if (!UserInfo.IsFeedback)
                    {
                        sbHtml.Append(" <em><a class=\"fastre\" href=\"#anchor\" onclick=\"javascript:SelectReply(" + id + "," + id + ",0);\">跟帖</a> </em>");
                    }
                    if (UserInfo.IsFeedback)
                    {
                        sbHtml.Append(" <em><a class=\"fastre2\" href=\"#anchor\" onclick=\"javascript:SelectReply(" + id + "," + id + ",1);\">反馈</a> </em>");
                    }
                }
                sbHtml.Append("<p>");
                if (UserInfo != null)
                {
                    if (UserInfo.IsAdmin || UserInfo.LoginName.ToString().Trim().ToLower() == reUser.Trim().ToLower())
                    {
                        sbHtml.Append("<a href=\"javascript:;\" id=\"mgc_post_11943477\" class=\"showmenu\" style=\"display: none;\"></a><a href=\"javascript:;\"  onclick=\"javascript:DeleteReply(" + id + ")\">删除</a>");
                    }
                    if (UserInfo.LoginName.ToString().Trim().ToLower() == reUser.Trim().ToLower())
                    {
                        sbHtml.Append("<a href=\"javascript:;\" id=\"mgc_post_11943477\" class=\"showmenu\" style=\"display: none;\"></a><a href=\"#anchor\"  onclick=\"javascript:GetReply(" + id + "," + isAnonymity + ")\">编辑</a>");
                    }
                }
                sbHtml.Append("</p>");

                sbHtml.Append("  <ul id=\"mgc_post_11943477_menu\" class=\"p_pop mgcmn\" style=\"display: none;\">");
                sbHtml.Append("  </ul>");
                sbHtml.Append("</div>");
                sbHtml.Append(" </div>");
                sbHtml.Append("</td>");
                sbHtml.Append("</tr>");
                sbHtml.Append(" <tr class=\"ad\">");
                sbHtml.Append("<td class=\"pls\">");
                sbHtml.Append(" </td>");
                sbHtml.Append("  <td class=\"plc\">");
                sbHtml.Append(" </td>");
                sbHtml.Append("  </tr>");
                sbHtml.Append(" </tbody>");
                sbHtml.Append("  </table>");
                sbHtml.Append("</div>");
                flag++;
            }
        }
        return sbHtml.ToString();
    }

    public string GetButtion()
    {

        StringBuilder sbHtml = new StringBuilder();
        sbHtml.Append("<div id=\"divBtnOpt\" style=\"float: left;\">");
        if (UserInfo != null)
        {
            if (UserInfo.IsFeedback)
            {
                sbHtml.Append("<button type=\"button\" name=\"replysubmit\" id=\"btnFeed\" class=\"pn pnc vm\" value=\"replysubmit\" tabindex=\"5\" onclick=\"AddReFeedBack()\">");
                sbHtml.Append("<strong>反馈</strong></button>");
            }
            else
            {
                sbHtml.Append("<button type=\"button\" name=\"replysubmit\" id=\"btnReply\" class=\"pn pnc vm\" value=\"replysubmit\" tabindex=\"5\" onclick=\"AddReply()\">");
                sbHtml.Append("<strong>发表</strong></button>");
            }
        }
        sbHtml.Append("</div>");
        sbHtml.Append("<div id=\"divBtnEdit\" style=\"float: left;display:none\">");
        sbHtml.Append("<button type=\"button\" name=\"replysubmit\" id=\"btnEdit\" class=\"pn pnc vm\" value=\"replysubmit\" tabindex=\"5\" onclick=\"EditReposts()\">");
        sbHtml.Append("<strong>编辑</strong></button>");
        sbHtml.Append("<button type=\"button\" name=\"replysubmit\" id=\"btnCancelEdit\"  class=\"pn pnc vm\" value=\"replysubmit\" tabindex=\"5\" onclick=\"CancelEdit()\">");
        sbHtml.Append("<strong>取消编辑</strong></button>");
        sbHtml.Append("</div>");
        return sbHtml.ToString();

    }
    public string GetName(string isAnonymity, string reUser, User userModel)
    {
        if (userModel == null)
            return "";
        string name = userModel.Name;
        if (isAnonymity == "1")
        {
            name = "匿名用户";//他人查看
        }
        if (UserInfo != null)
        {
            if (UserInfo.IsAdmin)
            {
                if (userModel != null)//管理员查看
                {
                    if (isAnonymity == "1")
                    {
                        name = "匿名[" + userModel.Name + "]";
                    }
                    else
                    {
                        name = userModel.Name;
                    }
                }
            }
            else if (UserInfo.LoginName == reUser)//自己查看
            {
                if (userModel != null)
                {
                    if (isAnonymity == "1")
                    {
                        name = "匿名[" + userModel.Name + "]";
                    }
                    else
                    {
                        name = userModel.Name;
                    }
                }
            }
        }
        return name;
    }
}