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

public partial class Work_Index : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Bind();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        Bind();
    }
    private void Bind()
    {

        ////int pageNum = 1;//页码
        //int numPerPage = 10;//每页显示数量
        //string orderBy = " PublishTime desc";
        //int totalCount = 0;
        //JournalTO journalTO = new JournalTO();
        //journalTO.Contents = this.txtKeyVal.Value.Trim().Equals("输入关键字...") ? "" : this.txtKeyVal.Value.Trim();
        //#region 列表

        //StringBuilder sbd = new StringBuilder();
        ////查询列表
        //JournalBLL journalBLL = new JournalBLL();

        //DataTable dt = journalBLL.GetJournalList(journalTO, AspNetPager1.CurrentPageIndex, numPerPage, orderBy, out totalCount);
        //if (dt != null&&dt.Rows.Count>0)
        //{
        //    this.Repeater1.DataSource = dt;
        //}
        //else
        //    this.Repeater1.DataSource = null;
        //AspNetPager1.RecordCount = totalCount;
        //AspNetPager1.PageSize = numPerPage;
        //lblCount.Text = totalCount.ToString();
        //this.Repeater1.DataBind();

        //#endregion


    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Bind();
    }
}