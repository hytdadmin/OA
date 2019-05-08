using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BLL;

public partial class Work_Back_EditBulletin : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
                //修改
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        {
            if (!IsPostBack)
            {
                Bind(Check.GetInt32(Request.QueryString["id"]));
            }
        }
    }

    private void Bind(int id)
    {
        Bulletin bulletin = new BulletinBLL().GetBulletinEntity(id);
        if (bulletin != null && bulletin.Id > 0)
        {
            this.txtTitle.Text = bulletin.Title;
            //this.fckMessage.Value = bulletin.Contents;
            elm1.Value = bulletin.Contents;
        }
    }
    
        //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (verify())
        {
            BulletinBLL bulletinBLL = new BulletinBLL();
            Bulletin bulletin = new Bulletin();
            bulletin.Contents = this.elm1.Value;
            bulletin.PublishUserCode = GetLoginCode(); ;
            bulletin.Title = this.txtTitle.Text.Trim();
            bulletin.IsDel = 1;
            bulletin.OrderId = 0;
            bulletin.PublishTime = DateTime.Now;
            bulletin.ScanNum = 0;
            //修改
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int id = Check.GetInt32(Request.QueryString["id"]);
                Bulletin model = bulletinBLL.GetBulletinEntity(id);
                if (model != null && model.Id > 0)
                {
                    bulletin.OrderId = model.OrderId;
                    bulletin.PublishTime = model.PublishTime;
                    bulletin.ScanNum = model.ScanNum;
                    bulletin.Id = id;
                }
                try
                {
                    bulletinBLL.UpdateBulletin(bulletin);
                    Response.Redirect("BulletinList.aspx");
                }
                catch
                {
                    Alert("修改失败，请重试");
                }
            }
            else
            {
                if (new BulletinBLL().AddBulletinReturn(bulletin))
                    Response.Redirect("BulletinList.aspx");
                else
                    Alert("添加失败，请重试");
            }
        }
    }

    
        // 提交验证
    private bool verify()
    {
        if (this.txtTitle.Text.Trim().Length == 0)
        {
            Alert("请输入公告标题");
            this.txtTitle.Focus();
            return false;
        }
        if (this.elm1.Value.Trim().Length == 0)
        {
            Alert("请输入公告内容");
            //this.elm1.Focus();
            return false;
        }
        return true;
    }
    public void Alert(string str)
    {
        string stralt = string.Format("alert('{0}')", str);
        ClientScript.RegisterStartupScript(GetType(), "alert", stralt, true);
    }
}