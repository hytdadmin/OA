using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Work_UserControl_NavigateUC : System.Web.UI.UserControl
{
    private string _defaultvalue = string.Empty;

    public string Defaultvalue
    {
        get { return _defaultvalue; }
        set { _defaultvalue = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Bind();
    }

    private void Bind()
    {
        litMe.Text = "<span class=\"right_nav_l_span\"><a href=\"" + Request.ApplicationPath + "Work/Index.aspx\" style=\"color:Black;text-decoration: none;font-weight:normal;\">我自己</a></span>";
        litOthers.Text = "<span class=\"right_nav_l_span\"><a href=\"" + Request.ApplicationPath + "Work/Others.aspx\" style=\"color:Black;text-decoration: none;font-weight:normal;\">其他人</a></span>";
        litMyColleague.Text = "<span class=\"right_nav_l_span\"><a href=\"" + Request.ApplicationPath + "Work/MyColleague.aspx\" style=\"color:Black;text-decoration: none;font-weight:normal;\">我的同事</a></span>";
        litDownloadCenter.Text = "<span class=\"right_nav_l_span\"><a href=\"" + Request.ApplicationPath + "Work/DownloadCenter.aspx\" style=\"color:Black;text-decoration: none;font-weight:normal;\">下载中心</a></span>";
        litVideos.Text = "<span class=\"right_nav_l_span\"><a href=\"" + Request.ApplicationPath + "Work/VideoCenter.aspx\" style=\"color:Black;text-decoration: none;font-weight:normal;\">视频中心</a></span>";
        string str = Defaultvalue == "Others" ? "其他人" : Defaultvalue == "MyColleague" ? "我的同事" : Defaultvalue == "DownloadCenter" ? "下载中心" :Defaultvalue =="VideoCenter" ? "视频中心": "我自己";
        string url = Defaultvalue == "Others" ? "Others.aspx" : Defaultvalue == "MyColleague" ? "MyColleague.aspx" : Defaultvalue == "DownloadCenter" ? "DownloadCenter.aspx" : Defaultvalue == "VideoCenter" ? "VideoCenter.aspx" : "Index.aspx";

        if (Defaultvalue == "Others")
            this.litOthers.Text = SetUrlCss(str, url);
        else if (Defaultvalue == "MyColleague")
            this.litMyColleague.Text = SetUrlCss(str, url);
        else if (Defaultvalue == "DownloadCenter")
            this.litDownloadCenter.Text = SetUrlCss(str, url);
        else if (Defaultvalue == "Me")
            this.litMe.Text = SetUrlCss(str, url);
        else if (Defaultvalue == "VideoCenter")
            this.litVideos.Text = SetUrlCss(str, url);
    }

    //返回当前页导航的样式
    private string SetUrlCss(string str,string url) {
        return string.Format("<span class=\"right_nav_l_span\" style=\"border-bottom: 2px solid #229BD7;\"><a href=\"{0}\" style=\"color:Black;text-decoration: none;font-weight:bold;\">{1}</a></span>", url, str);
    }
}