<%@ WebHandler Language="C#" Class="PostsAshx" %>

using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using Models;
using HYTD.BBS.Model.TO;
using HYTD.BBS.BLL;


public class PostsAshx : PageBase, IHttpHandler
{

    string opt = string.Empty;
    int id = 0;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.ContentType = "text/plain";
        PageBase.ClearClientPageCache();
        if (!string.IsNullOrEmpty(context.Request["opt"]))
        {
            opt = context.Request["opt"].ToString();
        }
        if (!string.IsNullOrEmpty(context.Request["id"]))
        {
            id = Convert.ToInt32(context.Request["id"].ToString());
        }
        if (opt == "delete")
        {
            DeletePosts(context, id);
        }
        if (opt == "recover")
        {
            ReoverPosts(context, id);
        }
        if (opt == "get")
        {
            GetReposts(context, id);
        }
    }
    protected void DeletePosts(HttpContext context, int id)
    {
        try
        {
            //删除回复
            RePostsBLL repostBll = new RePostsBLL();
            repostBll.UpdateRePostsStatuByFid(id, 0);
            //删除主题
            PostsBLL postBll = new PostsBLL();
            Posts postModel = postBll.GetPostsEntity(id);
            postModel.status = 0;
            postBll.UpdatePosts(postModel);
            //计算数量
            postBll.CountPosts(postModel.ReleaseUser);
            context.Response.Write("ok");
        }
        catch (Exception ex)
        {
            context.Response.Write("fail");
        }
    }
    protected void ReoverPosts(HttpContext context, int id)
    {
        try
        {
            //恢复回复
            RePostsBLL repostBll = new RePostsBLL();
            repostBll.UpdateRePostsStatuByFid(id, 1);
            //恢复主题
            PostsBLL postBll = new PostsBLL();
            Posts postModel = postBll.GetPostsEntity(id);
            postModel.status = 1;
            postBll.UpdatePosts(postModel);
            //计算数量
            postBll.CountPosts(postModel.ReleaseUser);
            context.Response.Write("ok");
        }
        catch (Exception ex)
        {
            context.Response.Write("fail");
        }
    }
    protected void GetReposts(HttpContext context, int id)
    {
        PostsBLL postBll = new PostsBLL();
        Posts postsModel = postBll.GetPostsEntity(id);
        context.Response.Write(postsModel.Title);
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}