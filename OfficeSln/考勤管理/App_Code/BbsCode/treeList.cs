using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using HYTD.CAPlatform.DBUtility;
using System.Web.UI;
using System.IO;
/// <summary>
///treeList 的摘要说明
/// </summary>
public class treeList
{

    int treefor = 0;
    public treeList()
    {
        //
        //TODO: 在此处添加构造函数逻辑		
        //

    }
    public struct imgModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Src { get; set; }
    }

    #region 获取当前域登陆账号名称
    public static string getUserName()
    {
        string domainAndName = HttpContext.Current.User.Identity.Name;
        string[] infoes = domainAndName.Split(new string[1] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
        string userDomainName = "";
        string userName = "";
        if (infoes.Length > 1)
        {
            userDomainName = infoes[0];
            userName = infoes[1];
            //UserInfoModel usr = ADHelper.GetUserEntity(userName);
            //string dept_Guid = "";
            //string user_Guid = "";
            //if (usr != null)
            //{
            //    userName = usr.Name;
            //}
            //else
            //{

            //}
        }
        return userName;
    }

    #endregion
    #region 根据用户编号,获取用户的后台权限
 
    #endregion

    #region 将内容写入文件中，自动创建文件和目录的方法
    /// <summary>
    /// 将内容写入文件中。如果目录和文件不存在则自动创建。如果原来文件已经存在，将自动覆盖(采用系统默认的编码)。
    /// </summary>
    /// <param name="_content">文件内容</param>
    /// <param name="path">文件路径</param>
    public static void UpdateFile(string _content, string path)
    {
        try
        {
            path = path.Replace("/", @"\");
            int pos = path.LastIndexOf(@"\");
            if (pos != -1)
            {
                string dir = path.Substring(0, pos);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                StringBuilder sbErrorMsg = new StringBuilder();
                sbErrorMsg.Append(Environment.NewLine);
                sbErrorMsg.Append(DateTime.Now.ToString() + "--" + _content + Environment.NewLine);
                try
                {
                    StreamWriter sw = File.AppendText(path);
                    sw.WriteLine(sbErrorMsg.ToString());
                    sw.Flush();
                    sw.Close();
                }
                catch (Exception ex)
                { }
                //using (FileStream fs = new FileStream(path, FileMode.Create))
                //{
                //    byte[] content = Encoding.Default.GetBytes(_content);
                //    fs.Write(content, 0, content.Length);
                //}
            }

            string strPath = "c:\\log\\";
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            string fileName = strPath + "Log" + DateTime.Now.ToString("yyyyMMddHH") + ".txt";
          
        }
        catch (Exception ex)
        {
            //throw new Exception("创建文件'" + path + "'失败。错误信息：" + ex.Message);
        }
    }
    #endregion
}