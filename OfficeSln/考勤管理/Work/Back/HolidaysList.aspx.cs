using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Models.TO;
using System.Data;
using HYTD.Common;

public partial class Work_Back_HolidaysList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindHolidaysList();
            BindHolidaysType();
        }
    }
    /// <summary>
    /// 绑定假期类型
    /// </summary>
    private void BindHolidaysType()
    {
        HolidaysType.Items.Add("请选择");
        HolidaysType.Items.Add(PublicEnum.GetEnumDescription<PublicEnum.HolidaysType>(PublicEnum.HolidaysType.yearDay.ToString()));
        HolidaysType.Items.Add(PublicEnum.GetEnumDescription<PublicEnum.HolidaysType>(PublicEnum.HolidaysType.Swopped.ToString()));
        HolidaysType.Items.Add(PublicEnum.GetEnumDescription<PublicEnum.HolidaysType>(PublicEnum.HolidaysType.workDay.ToString()));
        
    }
    /// <summary>
    /// 绑定数据源
    /// </summary>
    private void BindHolidaysList()
    {
        HolidaysBLL BLL = new HolidaysBLL();
        HolidaysTO to = new HolidaysTO();
        int numPerPage = 10;//每页显示数量
        string orderBy = " ID desc ";
        int rowCount = 0;
        #region 选择条件
        if (!string.IsNullOrEmpty(txtUserName.Text))
        {
            to.UserName = txtUserName.Text;
        }
        if (!string.IsNullOrEmpty(HolidaysType.SelectedValue) && HolidaysType.SelectedValue != "请选择")
        {
            to.HolidaysType = PublicEnum.GetEnumDescriptionvalue<PublicEnum.HolidaysType>(HolidaysType.SelectedValue);
        }
        if (!string.IsNullOrEmpty(txtStartDate.Text))
        {
            to.StratTime = txtStartDate.Text;
        }
        if (!string.IsNullOrEmpty(txtEndDate.Text))
        {
            to.EndTime = txtEndDate.Text;
        }
        #endregion
        DataTable dt = BLL.GetHolidaysTableList(to, AspNetPager1.CurrentPageIndex, numPerPage, orderBy, out rowCount);
        if (dt != null && dt.Rows.Count > 0)
        {
            RepeaterHoliday.DataSource = dt;
        }
        else
            RepeaterHoliday.DataSource = new DataTable();
        AspNetPager1.RecordCount = rowCount;
        AspNetPager1.PageSize = numPerPage;
        RepeaterHoliday.DataBind();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindHolidaysList();
    }
    //删除
    protected void RepeaterHoliday_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")//删除
        {

            string delID = ((CheckBox)e.Item.FindControl("che")).ToolTip.ToString();
            if (new HolidaysBLL().DeleteHolidaysTable(Convert.ToInt32(delID)))
            {
                string stralt = string.Format("alert('{0}');", "删除成功！");
                ScriptManager.RegisterStartupScript(this.HolidaysType, this.GetType(), "click", stralt, true);
            }
            else
            {
                string stralt = string.Format("alert('{0}');", "删除失败！");
                ScriptManager.RegisterStartupScript(this.HolidaysType, this.GetType(), "click", stralt, true);
            }
            
        }
        BindHolidaysList();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindHolidaysList();
    }
}