<%@ WebHandler Language="C#" Class="Control" %>

using System;
using System.Web;
using System.Data;
using Models;
using HYTD.BBS.Model.TO;
using HYTD.BBS.BLL;
using System.Collections.Generic;

public class Control : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string type = context.Request.QueryString["type"];
        string check = context.Request.Form["type"];
        if (type == "add")
        {
            AddInfo(context);
        }
        if (type == "edit")
        {
            EditInfo(context);
        }
        if (check == "check")
        {
            string infoword = context.Request.Form["info"];
            context.Response.Write(CheckInfo(infoword));
        }

    } /// <summary>
    /// 添加发表主题
    /// </summary>
    public void EditInfo(HttpContext context)
    {
        PostsBLL bll = new PostsBLL();
        string id = context.Request.Form["pid"];//类别id
        string cid = context.Request.Form["Cid"];//类别id
        string title = context.Request.Form["subject"];//标题
        string info = context.Request.Form["txtContent"];//内容
        string nametrue = context.Request.Form["radioname"];//是否匿名
        string strLoginName = context.Request.Form["loginname"];//是否匿名
        Posts Model_P = new Posts();
        int intID = 0;
        int.TryParse(id, out intID);
        if (intID > 0)
        {
            try
            {
                Model_P = bll.GetPostsEntity(intID);
                if (!string.IsNullOrEmpty(cid))
                {
                    Model_P.CID = Convert.ToInt32(cid);
                }
                if (!string.IsNullOrEmpty(title))
                {
                    Model_P.Title = title;
                }
                if (!string.IsNullOrEmpty(info))
                {
                    Model_P.Description = info;
                }
                if (!string.IsNullOrEmpty(nametrue))
                {
                    Model_P.isAnonymity = Convert.ToInt32(nametrue);
                }
                //Model_P.LookCount = 0;
                //Model_P.ReCount = 0;
                //Model_P.LastUser = "";
                //Model_P.ReleaseUser = strLoginName;
                //Model_P.isFeedback = 0;
                //Model_P.Top = 0;
                Model_P.UserIP = context.Request.UserHostAddress;
                Model_P.ReleaseTime = DateTime.Now;
                bll.UpdatePosts(Model_P);
                context.Response.Redirect("/Posts/detail.aspx?fatherID=" + Model_P.ID);
            }
            catch (Exception ex)
            {
                context.Response.Write("0");
            }
        }
        else
        {
            context.Response.Write("0");
        }
    }
    /// <summary>
    /// 添加发表主题
    /// </summary>
    public void AddInfo(HttpContext context)
    {
        string cid = context.Request.Form["Cid"];//类别id
        string title = context.Request.Form["subject"];//标题
        string info = context.Request.Form["txtContent"];//内容
        string nametrue = context.Request.Form["radioname"];//是否匿名
        string strLoginName = context.Request.Form["loginname"];//是否匿名
        Posts Model_P = new Posts();
        if (!string.IsNullOrEmpty(cid))
        {
            Model_P.CID = Convert.ToInt32(cid);
        }
        if (!string.IsNullOrEmpty(title))
        {
            Model_P.Title = title;
        }
        if (!string.IsNullOrEmpty(info))
        {
            Model_P.Description = info;
        }
        if (!string.IsNullOrEmpty(nametrue))
        {
            Model_P.isAnonymity = Convert.ToInt32(nametrue);
        }
        Model_P.LookCount = 0;
        Model_P.ReCount = 0;
        Model_P.LastUser = "";
        Model_P.ReleaseUser = strLoginName;
        Model_P.isFeedback = 0;
        Model_P.Top = 0;
        Model_P.status = 1;
        Model_P.UserIP = context.Request.UserHostAddress;
        Model_P.ReleaseTime = DateTime.Now;
        PostsBLL bll = new PostsBLL();
        bll.AddPosts(Model_P);
        //context.Response.Redirect("/Posts/detail.aspx?fatherID=" + Model_P.ID);
        context.Response.Redirect("/Posts/index.aspx");
    }
    /// <summary>
    /// 判断违法字符
    /// </summary>
    /// <param name="info"></param>
    public string CheckInfo(string info)
    {
        string back = "";
        SensitiveWordsBLL BLL_Sw = new SensitiveWordsBLL();
        List<SensitiveWords> SwList = BLL_Sw.GetSensitiveWordsList();
        foreach (SensitiveWords Model_Sw in SwList)
        {
            if (info.IndexOf(Model_Sw.sensitiveWord) > -1)
            {
                back = "false";
                break;
            }

        }
        return back;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}