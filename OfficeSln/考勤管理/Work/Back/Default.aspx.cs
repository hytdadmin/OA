using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text;
using System.Linq;
using System.Xml.Linq;

public partial class Work_Back_Default : System.Web.UI.Page
{
    public string list = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 绑定文件夹
    /// </summary>
    private void DateBind()
    {
        string path = @"E:\桌面\weibo";
        /// <summary>
        /// 获取指定文件夹下目录
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns></returns>
        DataTable dt = new DataTable();
        DataColumn dc;
        DataRow dr;

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.String");
        dc.ColumnName = "目录名";
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.DateTime");
        dc.ColumnName = "创建时间";
        dt.Columns.Add(dc);

        dc = new DataColumn();
        dc.DataType = System.Type.GetType("System.DateTime");
        dc.ColumnName = "修改时间";
        dt.Columns.Add(dc);


        if (!System.IO.Directory.Exists(path))
        {
            throw new Exception("不存在相应目录");
        }
        else
        {
            DirectoryInfo thisone = new DirectoryInfo(path);
            DirectoryInfo[] subdirectories = thisone.GetDirectories();//获得目录
            FileInfo[] fileinfo = thisone.GetFiles();//文件
            foreach (DirectoryInfo dirinfo in subdirectories)
            {
                dr = dt.NewRow();
                dr["目录名"] = dirinfo.Name.ToString();
                dr["创建时间"] = dirinfo.CreationTime;
                dr["修改时间"] = dirinfo.LastWriteTime;
                dt.Rows.Add(dr);
            }
            foreach (FileInfo dirinfo in fileinfo)
            {
                dr = dt.NewRow();
                dr["目录名"] = dirinfo.Name.ToString();
                dr["创建时间"] = dirinfo.CreationTime;
                dr["修改时间"] = dirinfo.LastWriteTime;
                dt.Rows.Add(dr);
            }
        }
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dtr = dt.Rows[i];
            sb.AppendFormat("<td><img src='../Images/文件夹.jpg.png' style='width:45px;height:30px;'>{0}</td>", dtr["目录名"]);
            sb.AppendFormat("<td style='height:30px;'>{0}</td>", dtr["创建时间"]);
            sb.AppendFormat("<td style='height:30px;'>{0}</td></tr>", dtr["修改时间"]);
        }
        list = sb.ToString();
    }

    private void LinqDir()
    {
        DirectoryInfo Dir = new DirectoryInfo(@"E:\桌面\weibo");
        StringBuilder sb = new StringBuilder();
        var directory = from dir
                       in Dir.EnumerateDirectories()
                        select dir;
        foreach (var f in directory)
        {
            Console.WriteLine("DirectotyName:{0}", f);
            sb.AppendFormat("<tr><td><img src='../Images/文件夹.jpg.png' style='width:45px;height:30px;' ><a href='#'class='linkDir'>{0}</a></td>", f.Name.ToString());
            sb.AppendFormat("<td style='height:30px;'>{0}</td>", f.CreationTime);
            sb.AppendFormat("<td style='height:30px;'>{0}</td></tr>", f.LastWriteTime);
        }
        var files = from file
                        in Dir.EnumerateFiles()
                    select file;
        
        foreach (var f in files)
        {
            Console.WriteLine("filename:{0}", f);
            string img="";
            if (f.Extension == ".jpg" || f.Extension == ".png")
            {
                img="../Images/tupian.jpg";
            }
            else
            {
                img = "../Images/raz.jpg"; 
            }
            sb.AppendFormat("<tr><td><img src='{0}' style='width:45px;height:30px;'>{1}</td>",img,f.Name.ToString());
            sb.AppendFormat("<td style='height:30px;'>{0}</td>",f.CreationTime);
            sb.AppendFormat("<td style='height:30px;'>{0}</td></tr>",f.LastWriteTime);
        }
        list = sb.ToString();
    }
  


}