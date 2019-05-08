using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using Model.TO;
using Models;


public partial class Work_Back_DownloadCenterList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
            DdlBind();
        }
    }

    /// <summary>
    /// 绑定部门
    /// </summary>
    public void DdlBind(){
        List<Department> list = new DepartmentBLL().GetDepartmentList();
        if (list != null && list.Count > 0)
        {
            ddlDep.DataSource = list;
            ddlDep.DataTextField = "RoleName";
            ddlDep.DataValueField = "id";
            ddlDep.DataBind();
        }
        ddlDep.Items.Insert(0, new ListItem("请选择", "0"));
    }
        
        /// <summary>
        /// 获取参数
        /// </summary>
    private DownloadCenterTO GetCriteria()
    {
        DownloadCenterTO criteria = new DownloadCenterTO();
        if (this.txtStartDate.Text.Trim() != "")
            criteria.StartDt = Convert.ToDateTime(this.txtStartDate.Text.Trim());
        if (this.txtEndDate.Text.Trim() != "")
            criteria.EndDt = Convert.ToDateTime(this.txtEndDate.Text.Trim());

        criteria.Title = this.txtTitle.Text.Trim();
        criteria.PubDepId = Check.GetInt32(ddlDep.SelectedValue);
        return criteria;
    }
    private void Bind()
    {
        
        int numPerPage = 10;//每页显示数量
        string orderBy = " PublishTime desc";
        int totalCount = 0;
        DataTable dt = new DownloadCenterBLL().GetDownloadCenterListByBack(GetCriteria(), AspNetPager1.CurrentPageIndex, numPerPage, orderBy, out totalCount);

        if (dt != null && dt.Rows.Count > 0)
        {
            Repeater1.DataSource = dt;
        }
        else
            Repeater1.DataSource = new DataTable();
        AspNetPager1.RecordCount = totalCount;
        AspNetPager1.PageSize = numPerPage;
        Repeater1.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Bind();
    }

    #region//删除一个和批量删除
    /// <summary>
    /// 删除一个
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void repShow_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")//删除
        {
            string delID = ((CheckBox)e.Item.FindControl("che")).ToolTip.ToString();

            Del(delID);
        }
        Bind();
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDel_Click(object sender, EventArgs e)
    {
        string ids = ReturnSelectedRowId();
        if (ids != null && ids != "")
        {
            Del(ids);
        }
        else
        {
            //this.AlertPanel1.MasterPageLabel.Text = "请至少选择一条记录进行删除操作！";
            //this.AlertPanel1.ModalPopupExtender.Show();
            Alert("请至少选择一条记录进行删除操作");
        }
    }
    void Del(string ids)
    {
        if (new BLL.DownloadCenterBLL().DelDownloadCenters(ids))
        {
            Alert("删除成功");
            Bind();
        }
        else
        {
            Alert("删除失败");
        }
    }
    #endregion


    /// <summary>
    /// 获得选择框返回值
    /// </summary>
    /// <returns></returns>
    string ReturnSelectedRowId()
    {
        string ids = null;
        for (int i = 0; i < Repeater1.Items.Count; i++)
        {
            RepeaterItem gvr = Repeater1.Items[i];
            CheckBox cb = (CheckBox)gvr.FindControl("che");
            if (cb.Checked)
            {
                if (ids != null && ids != "")
                {
                    ids += "," + cb.ToolTip.ToString();
                }
                else
                {
                    ids = cb.ToolTip.ToString();
                }
            }
        }
        return ids;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        Bind();
    }

    public void Alert(string str)
    {
        string stralt = string.Format("alert('{0}')", str);
        //ClientScript.RegisterStartupScript(GetType(), "alert", stralt, true);
        ScriptManager.RegisterStartupScript(this.updatePanle1, this.GetType(), "click", stralt, true);  
    }
}