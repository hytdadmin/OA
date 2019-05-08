<%@ WebHandler Language="C#" Class="PicUploadHander" %>

using System;
using System.Web;
using System.Web.SessionState;
using BLL;
using System.Data;
using System.Text;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;

public class PicUploadHander : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (!PageBase.IsLogin())
        {
            context.Response.Write("sessionError");
            return;
        }
        PageBase.ClearClientPageCache();
        int type = 0;//类型，1为数据库修改头像
        string types = context.Request.Params.Get("typeId");
        int.TryParse(types, out type);
        if (type == 0)
        {
            //验证上传的权限TODO
            string _fileNamePath = "";
            try
            {
                _fileNamePath = context.Request.Files[0].FileName;
                //开始上传
                string _savedFileResult = UpLoadImage(_fileNamePath, context);
                context.Response.Write(_savedFileResult);
            }
            catch
            {
                context.Response.Write("上传提交出错");
            }
        }
        else if (type == 1)
        {
            string hedImg = context.Request.Params.Get("hedImg");
            //参数
            if (string.IsNullOrEmpty(hedImg))
            {
                context.Response.Write("parError");
                return;
            }
            if (ChangeHeadImg(hedImg, PageBase.GetLoginCode()))
                context.Response.Write("true");
            else
                context.Response.Write("false");
                
        }
    }

    //数据库修改图片
    public bool ChangeHeadImg(string headImg,string userCode)
    {
        UserInfoBLL userInfoBLL=new UserInfoBLL();
        UserInfo user = userInfoBLL.GetUserByUserCode(userCode);
        user.HeadImg = headImg;
        try
        {
            userInfoBLL.UpdateUserInfo(user);
            return true;
        }
        catch {
            return false;
        }
    }

    public string UpLoadImage(string fileNamePath, HttpContext context)
    {
        try
        {
            string imgUlrPath = @"Image\headImg\";
            string imgUlr = @"Image/headImg";
            string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
            string toFilePath = Path.Combine(serverPath, imgUlrPath);
            //获取要保存的文件信息
            FileInfo file = new FileInfo(fileNamePath);
            //获得文件扩展名
            string fileNameExt = file.Extension;
            //验证合法的文件
            if (CheckImageExt(fileNameExt))
            {
                //生成将要保存的随机文件名
                string fileName = GetImageName() + fileNameExt;
                //获得要保存的文件路径
                string serverFileName = toFilePath + fileName;
                //物理完整路径                    
                string toFileFullPath = serverFileName; //HttpContext.Current.Server.MapPath(toFilePath);
                //将要保存的完整文件名                
                string toFile = toFileFullPath;//+ fileName;
                ///创建WebClient实例       
                WebClient myWebClient = new WebClient();
                //设定windows网络安全认证   方法1
                myWebClient.Credentials = CredentialCache.DefaultCredentials;
                ////设定windows网络安全认证   方法2
                context.Request.Files[0].SaveAs(toFile);
                //上传成功后网站内源图片相对路径
                string relativePath = System.Web.HttpContext.Current.Request.ApplicationPath
                                      + string.Format(imgUlr+"/{0}", fileName);
                /*
                  比例处理
                  微缩图高度（DefaultHeight属性值为 400）
                */
                System.Drawing.Image img = System.Drawing.Image.FromFile(toFile);
                int width = img.Width;
                int height = img.Height;
                float ratio = (float)width / height;
                //微缩图高度和宽度
                int newHeight = height <= DefaultHeight ? height : DefaultHeight;
                int newWidth = height <= DefaultHeight ? width : Convert.ToInt32(DefaultHeight * ratio);
                FileInfo generatedfile = new FileInfo(toFile);
                string newFileName = "Thumb_" + generatedfile.Name;
                string newFilePath = Path.Combine(generatedfile.DirectoryName, newFileName);
                PictureHandler.CreateThumbnailPicture(toFile, newFilePath, newWidth, newHeight);
                string thumbRelativePath = System.Web.HttpContext.Current.Request.ApplicationPath
                                      + string.Format(imgUlr+"/{0}", newFileName);
                //if (relativePath != null && relativePath != "")
                //{
                //    //删图
                //    string path = HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + relativePath);
                //    FilePicDelete(path);
                //}
                //返回微缩图的网站相对路径
               // relativePath = System.Web.HttpContext.Current.Request.ApplicationPath + thumbRelativePath;
                //string relativePath = System.Web.HttpContext.Current.Request.ApplicationPath + string.Format(@"Image/headImg/{0}", fileName);

                //return relativePath;
                return thumbRelativePath;
            }
            else
            {
                return "文件格式非法，请上传gif、jpg、bmp格式的文件";
                //throw new Exception("文件格式非法，请上传gif或jpg格式的文件。");
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    #region Private Methods
    /// <summary>
    /// 检查是否为合法的上传图片
    /// </summary>
    /// <param name="_fileExt"></param>
    /// <returns></returns>
    private bool CheckImageExt(string _ImageExt)
    {
        string[] allowExt = new string[] { ".gif", ".jpg", ".jpeg", ".bmp" };
        for (int i = 0; i < allowExt.Length; i++)
        {
            if (allowExt[i] == _ImageExt) { return true; }
        }
        return false;

    }

    private string GetImageName()
    {
        Random rd = new Random();
        StringBuilder serial = new StringBuilder();
        serial.Append(DateTime.Now.ToString("yyyyMMddHHmmssff"));
        serial.Append(rd.Next(0, 999999).ToString());
        return serial.ToString();

    }

    public int DefaultHeight
    {
        get
        {
            return 100;
        }
    }

    public static void FilePicDelete(string path)
    {
        FileInfo file = new FileInfo(path);
        if (file.Exists)
        {
            file.Delete();
        }
    }
    #endregion

    //缩略图处理相关类
    public static class PictureHandler
    {
        /// <summary>
        /// 图片微缩图处理
        /// </summary>
        /// <param name="srcPath">源图片</param>
        /// <param name="destPath">目标图片</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void CreateThumbnailPicture(string srcPath, string destPath, int width, int height)
        {
            //根据图片的磁盘绝对路径获取 源图片 的Image对象
            System.Drawing.Image img = System.Drawing.Image.FromFile(srcPath);
            //bmp： 最终要建立的 微缩图 位图对象。
            Bitmap bmp = new Bitmap(width, height);
            //g: 绘制 bmp Graphics 对象
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            //为Graphics g 对象 初始化必要参数，很容易理解。
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //源图片宽和高
            int imgWidth = img.Width;
            int imgHeight = img.Height;
            //绘制微缩图
            g.DrawImage(img, new System.Drawing.Rectangle(0, 0, width, height), new System.Drawing.Rectangle(0, 0, imgWidth, imgHeight)
                        , GraphicsUnit.Pixel);
            ImageFormat format = img.RawFormat;
            ImageCodecInfo info = ImageCodecInfo.GetImageEncoders().SingleOrDefault(i => i.FormatID == format.Guid);
            EncoderParameter param = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = param;
            img.Dispose();
            //保存已生成微缩图，这里将GIF格式转化成png格式。
            if (format == ImageFormat.Gif)
            {
                destPath = destPath.ToLower().Replace(".gif", ".png");
                bmp.Save(destPath, ImageFormat.Png);
            }
            else
            {
                if (info != null)
                {
                    bmp.Save(destPath, info, parameters);
                }
                else
                {
                    bmp.Save(destPath, format);
                }
            }
            img.Dispose();
            g.Dispose();
            bmp.Dispose();
        }
    }
}