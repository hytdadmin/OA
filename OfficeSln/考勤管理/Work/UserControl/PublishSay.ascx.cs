using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BLL;

public partial class Work_UserControl_PublishSay : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void imgBtnSay_Click(object sender, ImageClickEventArgs e)
    {
        //if (Session["user"] == null)
        //{
        //    string url = Request.ApplicationPath + "/Login.aspx";
        //    Response.Write("<script>alert('会话过期，请重新登录');window.location.href='"+url+"';</script>");
        //    this.Response.End();
        //}
        //string content = this.txtSay.Text;
        //int maxLength = 500;
        //if (CheckSay(content, maxLength))
        //{
        //    Journal journal = new Journal();
        //    journal.Contents = content;
        //    journal.PublishUserCode = Session["user"].ToString();
        //    journal.IsDel = 1;
        //    journal.PublishTime = DateTime.Now;
        //    journal.ScanNum = 0;
        //    journal.Title = "";
        //    if (new JournalBLL().AddJournalReturn(journal))
        //        this.txtSay.Text = "";
        //    else
        //        Alert("发布日志失败");
        //}
    }

    private bool CheckSay(string content, int maxLength)
    {
        if (content.Length == 0)
        {
            Alert("日志内容不能为空");
            return false;
        }
        if (content.Length > maxLength)
        {
            Alert("输入的字数过多，最多可输入" + maxLength + "字");
            return false;
        }

        return true;
    }
    private void Alert(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('"+str+"）", true);
    }
    
}