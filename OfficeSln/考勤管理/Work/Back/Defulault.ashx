<%@ WebHandler Language="C#" Class="Defulault" %>

using System;
using System.Web;
using System.IO;
using System.Text;
using System.Linq;
public class Defulault : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string op = context.Request["op"];
        string name = context.Request["name"];
        string hid = context.Request["hid"];
        string path = System.Web.HttpContext.Current.Server.MapPath(Config.publicImage);
        if (op == "star")
        {
        }
        else
        {
            if (!string.IsNullOrEmpty(hid))
            {
                path += @"\" + hid;
            }
            else
            {
                path += @"\" + name + hid;
            }

        }

        LinqDir(path, context);
    }
    private void LinqDir(string path, HttpContext context)
    {
        DirectoryInfo Dir = new DirectoryInfo(path);
        StringBuilder sb = new StringBuilder();
        try
        {
            //文件夹
            var directory = from dir in Dir.EnumerateDirectories() select dir;
            foreach (var f in directory)
            {
                sb.AppendFormat("<li style=\"width:800px\"><a href=\"#\"  class=\"linkDir\">{0}</a><img src=\"/Work/Images/wjj.png\" ></li>", f.Name.ToString());
                //sb.AppendFormat("<td style=\"height:30px;\">{0}</td>", f.CreationTime);
                //sb.AppendFormat("<td style=\"height:30px;\">{0}</td></tr>", f.LastWriteTime);
            }
            //文件

            int beginflag = 0;
            sb.Append("<li>");
            sb.Append("<table>");
            sb.Append("<tr>");
            var files = from file in Dir.EnumerateFiles() select file;
            foreach (var f in files)
            {
                if (f.Extension == ".jpg" || f.Extension == ".png" || f.Extension == "jpeg")
                {
                    beginflag++;
                    bool b = false;
                    if ((beginflag - 1) % 5 == 0 && beginflag != 1)
                    {
                        b = true;
                        sb.Append("<tr>");
                    }

                    string strPath = (f.DirectoryName.Substring(f.DirectoryName.IndexOf(Config.publicImage.Replace("/", "\\")), f.DirectoryName.Length - f.DirectoryName.IndexOf(Config.publicImage.Replace("/", "\\"))) + "\\" + f.Name.ToString()).Replace("\\", "/");
                    string imgSrc = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + strPath;
                    sb.Append("<td width=\"160\" align=\"center\">");
                    sb.Append("<a href=\"" + imgSrc + "\" target=\"_black\"><img src=\"" + imgSrc + "\" style=\"width:150px;height:80px;\" /></a>");
                    sb.Append("<br/>");
                    sb.Append("<span style=\"width:120px;\">" + f.Name.ToString() + "</span>");
                    sb.Append("</td>");

                    if (beginflag % 5 == 0 && beginflag != 1)
                    {
                        sb.Append("</tr>");

                    }
                }
                //if (f.Extension == ".jpg" || f.Extension == ".png" || f.Extension == "jpeg")
                //{
                //    string strPath = (f.DirectoryName.Substring(f.DirectoryName.IndexOf(Config.publicImage.Replace("/", "\\")), f.DirectoryName.Length - f.DirectoryName.IndexOf(Config.publicImage.Replace("/", "\\"))) + "\\" + f.Name.ToString()).Replace("\\", "/");
                //    string imgSrc = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + strPath;
                //    sb.AppendFormat("<li style=\"height:120px;width:120px\"><a href=\"{0}\" target=\"_black\"><img src=\"{0}\" style=\"width:120px;height:80px;\" /></a><br/><span style=\"width:120px;\">{1}</span></li>", imgSrc, f.Name.ToString());
                //}
                //else
                //{
                //    //string img = FileFormat(f.Extension);
                //    //sb.AppendFormat("<tr><td><img src=\"{0}\" style=\"width:45px;height:30px;\">{1}</td>", img, f.Name.ToString());
                //}
                //sb.AppendFormat("<td style=\"height:30px;\">{0}</td>", f.CreationTime);
                //sb.AppendFormat("<td style=\"height:30px;\">{0}</td></tr>", f.LastWriteTime);
            }
            sb.Append("</table>");
            sb.Append("</li>");
        }
        catch (Exception ex)
        { 
        
        }
        context.Response.Write(sb.ToString());
    }

    /// <summary>
    /// 文件格式
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    private string FileFormat(string format)
    {
        string img = "";

        switch (format.ToLower())
        {
            case ".jpg":
            case ".png":
            case ".jpeg":
            case ".bmp":
            case ".jif":
                img = "/Work/Image/fomart/tupian.jpg"; break;
            case ".zip":
            case ".rar":
                img = "/Work/Image/fomart/rar.jpg"; break;
            case ".txt":
                img = "/Work/Image/fomart/txt.jpg"; break;
            case ".doc":
                img = "/Work/Image/fomart/doc.jpg"; break;
            case ".xls":
                img = "/Work/Image/fomart/xls.jpg"; break;
            case ".ppt":
                img = "/Work/Image/fomart/ppt.jpg"; break;
            case ".mp3":
            case ".wma":
                img = "/Work/Image/fomart/mp3.jpg"; break;
            case ".mp4":
            case ".avi":
            case ".wmv":
            case ".3gp":
            case ".rmb":
            case ".flv":
                img = "/Work/Image/fomart/mp4.jpg"; break;
            case ".pdf":
                img = "/Work/Image/fomart/pdf.jpg"; break;
            default: img = "/Work/Image/fomart/qita.jpg"; break;
        }
        return img;


    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}