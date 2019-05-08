<%@ WebHandler Language="C#" Class="RePostsAshx" %>

using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using Models;
using HYTD.BBS.Model.TO;
using HYTD.BBS.BLL;


public class RePostsAshx : PageBase, IHttpHandler
{
    int fatherID = 0;
    int cID = 0;
    int releaseFatherID = 0;
    string content = "";
    string reUser = "";
    int isReFeedback = 0;
    int isFeedback = 0;
    string userIP = "";
    int isAnonymity = 0;
    int replyId = 0;
    string opt = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        PageBase.ClearClientPageCache();

        if (!string.IsNullOrEmpty(context.Request["opt"]))
        {
            opt = context.Request["opt"].ToString();
        }
        if (!string.IsNullOrEmpty(context.Request["replyId"]))
        {
            replyId = Convert.ToInt32(context.Request["replyId"].ToString());
        }
        if (!string.IsNullOrEmpty(context.Request["fatherID"]))
        {
            fatherID = Convert.ToInt32(context.Request["fatherID"].ToString());
        }
        if (!string.IsNullOrEmpty(context.Request["cID"]))
        {
            cID = Convert.ToInt32(context.Request["cID"].ToString());
        }
        if (!string.IsNullOrEmpty(context.Request["releaseFatherID"]))
        {
            releaseFatherID = Convert.ToInt32(context.Request["releaseFatherID"].ToString());
        }
        if (!string.IsNullOrEmpty(context.Request["content"]))
        {
            content = context.Request["content"].ToString();
        }
        if (!string.IsNullOrEmpty(context.Request["reUser"]))
        {
            reUser = context.Request["reUser"].ToString();
        }
        if (!string.IsNullOrEmpty(context.Request["isFeedback"]))
        {
            isFeedback = Convert.ToInt32(context.Request["isFeedback"].ToString());
        }
        if (!string.IsNullOrEmpty(context.Request["isReFeedback"]))
        {
            isReFeedback = Convert.ToInt32(context.Request["isReFeedback"].ToString());
        }
        userIP = System.Web.HttpContext.Current.Request.UserHostAddress;
        if (!string.IsNullOrEmpty(context.Request["isAnonymity"]))
        {
            isAnonymity = Convert.ToInt32(context.Request["isAnonymity"].ToString());
        }
        if (opt == "add")
        {
            AddReposts(context, fatherID, cID, releaseFatherID, content, reUser, isReFeedback, isFeedback, userIP, isAnonymity);
        }
        else if (opt == "delete")
        {
            DeleteReposts(context, replyId);
        }
        else if (opt == "recover")
        {
            RecoverReposts(context, replyId);
        }
        else if (opt == "get")
        {
            GetReposts(context, replyId);
        }
        else if (opt == "edit")
        {
            EditReposts(context, fatherID, replyId, content, reUser, isReFeedback, isFeedback, userIP, isAnonymity);
        }
    }

    protected void AddReposts(HttpContext context, int fatherID, int CID, int ReleaseFatherID, string Content, string ReUser, int isReFeedback, int isFeedback, string UserIP, int isAnonymity)
    {
        PostsBLL postBll = new PostsBLL();
        Posts postModel = postBll.GetPostsEntity(fatherID);

        RePostsBLL repBll = new RePostsBLL();
        RePosts repModel = new RePosts();
        repModel.CID = CID;
        repModel.FatherID = fatherID;
        repModel.ReleaseFatherID = ReleaseFatherID;
        repModel.Content = Content;
        repModel.ReUser = ReUser;
        repModel.isFeedback = isFeedback;
        repModel.isReFeedback = isReFeedback;
        repModel.UserIP = UserIP;
        repModel.ReDatetime = DateTime.Now;
        repModel.isAnonymity = isAnonymity;
        repModel.status = 1;
        repBll.AddRePosts(repModel);

        //修改状态
        if (isReFeedback == 1)
        {
            //修改子表状态
            if (releaseFatherID != 0)
            {
                RePosts repModelTemp = repBll.GetRePostsEntity(releaseFatherID);
                repModelTemp.isFeedback = 1;
                repBll.UpdateRePosts(repModelTemp);
            }
            else
            {
                //修改主题状态
                postModel.isFeedback = 1;
                postBll.UpdatePosts(postModel);
            }
        }

        //最后修改时间，最后修改人，修改状态
        postModel.LastTime = DateTime.Now;
        postModel.LastUser = ReUser;
        postBll.UpdatePosts(postModel);
        //计算回帖数
        postBll.CountRePosts(fatherID);
        //计算数量
        postBll.CountPosts(repModel.ReUser);
        context.Response.Write("ok");
        if (repModel.ID > 0)
        {
            context.Response.Write(repModel.ID);
        }
        else
        {
            context.Response.Write("fail");
        }
    }
    protected void DeleteReposts(HttpContext context, int replyId)
    {
        RePostsBLL repBll = new RePostsBLL();
        try
        {
            //删除回复
            RePosts repModel = repBll.GetRePostsEntity(replyId);
            repModel.status = 0;
            repBll.UpdateRePosts(repModel);
            //删除子回复
            repBll.UpdateRePostsStatusByRelFatherId(repModel.ID, 0);
            //计算回帖数
            PostsBLL postBll = new PostsBLL();
            postBll.CountRePosts(Convert.ToInt32(repModel.FatherID));
            //计算数量
            postBll.CountPosts(repModel.ReUser);
            context.Response.Write("ok");
        }
        catch (Exception ex)
        {
            context.Response.Write("fail");
        }
    }
    protected void RecoverReposts(HttpContext context, int replyId)
    {
        RePostsBLL repBll = new RePostsBLL();
        try
        {
            //恢复回复
            RePosts repModel = repBll.GetRePostsEntity(replyId);
            repModel.status = 1;
            repBll.UpdateRePosts(repModel);
            //恢复子回复
            repBll.UpdateRePostsStatusByRelFatherId(repModel.ID, 1);
            //计算回帖数
            PostsBLL postBll = new PostsBLL();
            postBll.CountRePosts(Convert.ToInt32(repModel.FatherID));
            //计算数量
            postBll.CountPosts(repModel.ReUser);
            context.Response.Write("ok");
        }
        catch (Exception ex)
        {
            context.Response.Write("fail");
        }
    }
    protected void GetReposts(HttpContext context, int replyId)
    {
        RePostsBLL repBll = new RePostsBLL();
        RePosts repModel = repBll.GetRePostsEntity(replyId);
        context.Response.Write(repModel.Content);
    }
    protected void EditReposts(HttpContext context, int fatherID, int replyId, string Content, string ReUser, int isReFeedback, int isFeedback, string UserIP, int isAnonymity)
    {
        RePostsBLL repBll = new RePostsBLL();
        RePosts repTemp = repBll.GetRePostsEntity(replyId);
        RePosts repModel = new RePosts();
        repModel.ID = replyId;
        repModel.CID = repTemp.CID;
        repModel.FatherID = fatherID;
        repModel.ReleaseFatherID = repTemp.ReleaseFatherID;
        repModel.Content = Content;
        repModel.ReUser = ReUser;
        repModel.isReFeedback = repTemp.isReFeedback;
        repModel.isFeedback = repTemp.isFeedback;
        repModel.status = repTemp.status;
        repModel.UserIP = UserIP;
        repModel.ReDatetime = DateTime.Now;
        repModel.isAnonymity = isAnonymity;
        repBll.UpdateRePosts(repModel);

        //最后修改时间，最后修改人，修改状态
        PostsBLL postBll = new PostsBLL();
        Posts postModel = postBll.GetPostsEntity(fatherID);
        postModel.LastTime = DateTime.Now;
        postModel.LastUser = ReUser;
        postBll.UpdatePosts(postModel);

        context.Response.Write("ok");
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}