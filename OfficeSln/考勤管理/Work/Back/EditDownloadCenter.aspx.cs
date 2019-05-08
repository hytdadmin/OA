using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BLL;

public partial class Work_Back_EditDownloadCenter : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
            string type = System.Configuration.ConfigurationManager.AppSettings["DownType"];
            if (!string.IsNullOrEmpty(type))
            {
                this.litType.Text = string.Format("<span style='color: red;font-weight: inherit;'>支持的格式：{0}</span>",type);
            }
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
        DownloadCenter downloadCenter = new DownloadCenterBLL().GetDownloadCenterEntity(id);
        if (downloadCenter != null && downloadCenter.Id > 0)
        {
            this.txtTitle.Text = downloadCenter.Title;
            //this.fckMessage.Value = bulletin.Contents;
            //elm1.Value = bulletin.Contents;
            this.litFile.Text = string.Format("<a style=\"margin-right: 15px;\" href=\"{0}\">{1}</a>", downloadCenter.AffixUrl + downloadCenter.AffixNewName, downloadCenter.AffixOldName);
        }
    }

    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (verify())
        {
            string imgurl = "/Image/DownloadCenter/";
            DownloadCenterBLL downloadCenterBLL = new DownloadCenterBLL();
            DownloadCenter downloadCenter = new DownloadCenter();
            downloadCenter.PublishUserCode = PageBase.GetLoginCode();
            downloadCenter.Title = this.txtTitle.Text.Trim();
            downloadCenter.IsDel = 1;
            downloadCenter.DownNum = 0;
            downloadCenter.PublishTime = DateTime.Now;
            if (string.IsNullOrEmpty(downloadCenter.Title))
            {
                downloadCenter.Title = FileUpload1.FileName;
            }
            //修改
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int id = Check.GetInt32(Request.QueryString["id"]);
                DownloadCenter model = downloadCenterBLL.GetDownloadCenterEntity(id);

                if (model != null && model.Id > 0)
                {
                    downloadCenter.Id = id;
                    downloadCenter.AffixNewName = model.AffixNewName;
                    downloadCenter.AffixOldName = model.AffixOldName;
                    downloadCenter.AffixUrl = model.AffixUrl;
                    downloadCenter.DownNum = model.DownNum;
                    downloadCenter.PublishTime = model.PublishTime;
                    downloadCenter.PublishUserCode = model.PublishUserCode;
                    downloadCenter.Size = model.Size;
                    downloadCenter.Suffix = model.Suffix;
                    bool edit = true;
                    if (FileUpload1.HasFile)
                    {
                        downloadCenter.Size = FileUpload1.PostedFile.ContentLength;
                        downloadCenter.Suffix = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToString();
                        downloadCenter.AffixUrl = imgurl;
                        //downloadCenter.AffixOldName = FileUpload1.PostedFile.FileName;
                        downloadCenter.AffixOldName = FileUpload1.FileName;
                        //string newName = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 1000).ToString() + downloadCenter.Suffix;
                        downloadCenter.AffixNewName = FileUpload1.FileName;
                        //if(UploadFiled(FileUpload1,imgurl))
                        //    Response.Redirect("DownloadCenterList.aspx");

                        string returnStr = UploadFiled(FileUpload1, imgurl, FileUpload1.FileName);
                        if (returnStr != "上传成功")
                        {
                            edit = false;
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + returnStr + "');", true);
                            //Alert(returnStr);
                        }
                    }
                    if (edit)
                    {
                        try
                        {
                            downloadCenterBLL.UpdateDownloadCenter(downloadCenter);
                            Response.Redirect("DownloadCenterList.aspx");
                        }
                        catch
                        {
                            //Alert("修改失败，请重试");
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改失败，请重试');", true);
                        }
                    }
                }
            }
            else
            {
                downloadCenter.Size = FileUpload1.PostedFile.ContentLength;
                downloadCenter.Suffix = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToString();
                downloadCenter.AffixUrl = imgurl;
                //downloadCenter.AffixOldName = FileUpload1.PostedFile.FileName;
                downloadCenter.AffixOldName = FileUpload1.FileName;
                //string newName = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 1000).ToString()+downloadCenter.Suffix;
                downloadCenter.AffixNewName = FileUpload1.FileName;

                string returnStr = UploadFiled(FileUpload1, imgurl, FileUpload1.FileName);
                if (returnStr == "上传成功")
                {
                    if (downloadCenterBLL.AddDownloadCenterReturn(downloadCenter))
                        Response.Redirect("DownloadCenterList.aspx");
                    else
                    {
                        //Alert("添加失败，请重试");
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加失败，请重试');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + returnStr + "');", true);
                    //Alert(returnStr);
                }
            }
        }
    }

    private string UploadFiled(FileUpload FileUpload1, string imgurl, string newName)
    {
        if (FileUpload1.HasFile)
        {
            string strExt = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToString();
            string type = System.Configuration.ConfigurationManager.AppSettings["DownType"];
            int index = type.ToLower().IndexOf(strExt.ToLower());
            if (index == -1)
            {
                FileUpload1.Focus();
                //return -1;
                return "附件类型只能为：" + type;
            }
            int size = Check.GetInt32(System.Configuration.ConfigurationManager.AppSettings["DownSize"]);
            int size2 = FileUpload1.PostedFile.ContentLength;
            if (size != 0 && size2 <= size*1024)
            {
                FileUpload1.Focus();
                //return -1;
                return "附件过大，最大只能为：" + size2/1024/1024 + "Mb";
            } 
            try
            {
                //上传文件并指定上传目录的路径  
                FileUpload1.PostedFile.SaveAs(Server.MapPath(@imgurl)
                    + newName);
                /*注意->这里为什么不是:FileUpLoad1.PostedFile.FileName 
                * 而是:FileUpLoad1.FileName? 
                * 前者是获得客户端完整限定(客户端完整路径)名称 
                * 后者FileUpLoad1.FileName只获得文件名. 
                */

                //当然上传语句也可以这样写(貌似废话):  
                //FileUpLoad1.SaveAs(@"D:\"+FileUpLoad1.FileName);  

                //lblMessage.Text = "上传成功!";
                return "上传成功";
            }
            catch (Exception ex)
            {
                //lblMessage.Text = "出现异常,无法上传!";
                //lblMessage.Text += ex.Message;  
                return "上传附件失败，请重试";
            }  
        }
        return "附件不能为空";
    }

    // 提交验证
    private bool verify()
    {
        //if (this.txtTitle.Text.Trim().Length == 0)
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请输入公告标题');", true);
        //    //Alert("请输入公告标题");
        //    this.txtTitle.Focus();
        //    return false;
        //}
        if (!FileUpload1.HasFile && litFile.Text.Length==0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('附件不能为空');", true);
            //Alert("附件不能为空");
            this.FileUpload1.Focus();
            return false;
        }
        return true;
    }
    public void Alert(string str)
    {
        string stralt = string.Format("alert('{0}')", str);
        ClientScript.RegisterStartupScript(this.GetType(), "alert", stralt, true);
    }
}