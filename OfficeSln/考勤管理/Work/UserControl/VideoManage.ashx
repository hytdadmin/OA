<%@ WebHandler Language="C#" Class="VideoManage" %>

using System;
using System.Web;
using BLL;
using Models.TO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Models;
using System.Text.RegularExpressions;

public class VideoManage : IHttpHandler {
    
    VideoCenterBLL videoCenterBLL = new VideoCenterBLL();
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        string action = context.Request.Form["action"];
        
        
        switch (action)
        {
            case "Department":
                context.Response.Write(DepartmentUsers(context));
                break;
            case "Category":
                context.Response.Write(CategoryUsers(context));
                break;
            case "AddUser":
            case "DelUser":
                context.Response.Write(UserOption(context));
                break;
            case "reflash":
                context.Response.Write(ReflashVideos());
                break;
            case "GetCategories":
                context.Response.Write(GetCategories());
                break;
            case "UpdateCategory":
                context.Response.Write(UpdateCategory(context));
                break;
            case "DeleCategory":
                break;
            case "AddVideoToCategory":
                break;
            case "DeleteVideoFromCategoy":
                break;
            default:
                context.Response.Write("no this action");
                break;
        }

    }

    private string DepartmentUsers(HttpContext context)
    {
        DataSet ds = videoCenterBLL.GetVideosManageInfo();

        List<DepartmentUserInfoViewModel> dpUserInfos = new List<DepartmentUserInfoViewModel>();

        foreach (DataRow row in ds.Tables[0].AsEnumerable())
        {

            if (dpUserInfos.Exists(item => item.RoleName == row.Field<string>("RoleName")))
            {
                DepartmentUserInfoViewModel DpuserInfo = dpUserInfos.Find(item => item.RoleName == row.Field<string>("RoleName"));
                DpuserInfo.Users.Add(new VideoManageUserInfoViewModel() { UserCode = row.Field<string>("UserCode"), UserName = row.Field<string>("UserName"), RoleName = row.Field<string>("RoleName"), CategoryName = row.Field<string>("VideoCategoryName"), CategoryID = row.Field<int>("VideoCategoryID") });
            }
            else
            {
                DepartmentUserInfoViewModel DpuserInfoNew = new DepartmentUserInfoViewModel();
                DpuserInfoNew.ID = row.Field<int>("Id");
                DpuserInfoNew.RoleName = row.Field<string>("RoleName");
                DpuserInfoNew.Users.Add(new VideoManageUserInfoViewModel() { UserCode = row.Field<string>("UserCode"), UserName = row.Field<string>("UserName"), RoleName = row.Field<string>("RoleName"), CategoryName = row.Field<string>("VideoCategoryName"), CategoryID = row.Field<int>("VideoCategoryID") });
                dpUserInfos.Add(DpuserInfoNew);
            }  
        }

       

       

        using (MemoryStream ms = new MemoryStream())
        {
            DataContractJsonSerializer dpJsonSerializer = new DataContractJsonSerializer(typeof(List<DepartmentUserInfoViewModel>));
            dpJsonSerializer.WriteObject(ms, dpUserInfos);
            return Encoding.UTF8.GetString(ms.ToArray()); 
            
        }
    }

    private string CategoryUsers(HttpContext context)
    {
        DataSet ds = videoCenterBLL.GetVideosManageInfo();
        List<VideoManageUserInfoAssignViewModel> VideosCategoryUsersInfo = new List<VideoManageUserInfoAssignViewModel>();
        foreach (DataRow row in ds.Tables[1].AsEnumerable())
        {
            if (VideosCategoryUsersInfo.Exists(item => item.VideoCategoryName ==  row.Field<string>("VideoCategoryName")))
            {
                VideoManageUserInfoAssignViewModel videosUsersInfo = VideosCategoryUsersInfo.Find(item => item.VideoCategoryName == row.Field<string>("VideoCategoryName"));
                videosUsersInfo.Users.Add(new VideoManageUserInfoViewModel() { UserCode = row.Field<string>("UserCode"), UserName = row.Field<string>("UserName"), RoleName = row.Field<string>("RoleName") });

            }
            else
            {
                VideoManageUserInfoAssignViewModel VideosCategoryUsersInfoNew = new VideoManageUserInfoAssignViewModel();
                VideosCategoryUsersInfoNew.ID = row.Field<int>("ID");
                VideosCategoryUsersInfoNew.VideoCategoryName = row.Field<string>("VideoCategoryName");
                VideosCategoryUsersInfoNew.Description = row.Field<string>("Description");
                VideosCategoryUsersInfoNew.Users.Add(new VideoManageUserInfoViewModel() { UserCode = row.Field<string>("UserCode"), UserName = row.Field<string>("UserName"), RoleName = row.Field<string>("RoleName") });
                VideosCategoryUsersInfo.Add(VideosCategoryUsersInfoNew);
            }
        }

        using (MemoryStream ms = new MemoryStream())
        {
            DataContractJsonSerializer vdJsonSerializer = new DataContractJsonSerializer(typeof(List<VideoManageUserInfoAssignViewModel>));
            vdJsonSerializer.WriteObject(ms, VideosCategoryUsersInfo);
            return Encoding.UTF8.GetString(ms.ToArray());

        }
        
    }

    private string UserOption(HttpContext context)
    {
        VideoCategory_UserInfo VideoCategoryUserInfo = new VideoCategory_UserInfo();
        string UserCode = context.Request.Form["UserCode"].Trim();
        string CategoryID = context.Request.Form["CategoryID"].Trim();
        string Option = context.Request.Form["action"].Trim();
        if(string.IsNullOrEmpty(UserCode))
        {
            return "false";
        }
        if(string.IsNullOrEmpty(CategoryID))
        {
            return "false";
        }
        if (string.IsNullOrEmpty(Option))
        {
            return "false";
        }

        VideoCategoryUserInfo.UserID = Convert.ToInt32(UserCode);
        VideoCategoryUserInfo.VideoCategoryID = CategoryID;
        VideoCenterBLL videoCenterBLL = new VideoCenterBLL();
        try
        {
            if (Option == "AddUser")
                videoCenterBLL.AddUser(VideoCategoryUserInfo);
            else if (Option == "DelUser")
                videoCenterBLL.DelUser(VideoCategoryUserInfo);
            else
                return "false";
            return "true";
        }
        catch(Exception ex){
            return "false";
        }
    }

    private string ReflashVideos()
    {
        string AppPath = "";
        HttpContext HttpCurrent = HttpContext.Current;
        if (HttpCurrent != null)
        { 
            AppPath = HttpContext.Current.Server.MapPath("~");
        }
        else
        {
            AppPath = AppDomain.CurrentDomain.BaseDirectory;
            if (Regex.Match(AppPath, @"\\$", RegexOptions.Compiled).Success)
            {
                AppPath = AppPath.Substring(0, AppPath.Length - 1);
            }
        }

        videoCenterBLL.ReflashVideos(AppPath);
        return "";
    }

    #region 修改视频分类

    private string UpdateCategory(HttpContext context)
    {
        string Description = context.Request.Form["Description"];
        string CategoryID = context.Request.Form["CategoryID"];
        string VideoCategoryName = context.Request.Form["VideoCategoryName"];

        if (string.IsNullOrEmpty(Description))
        {
            return "false";
        }

        try
        {

            if (videoCenterBLL.GetVideoCategory(Description) != null)
            {
                return "false";
            }

            Models.VideoCategory videoCategory = new Models.VideoCategory() { ID = Convert.ToInt32(CategoryID), VideoCategoryName = VideoCategoryName, Description = Description };

            videoCenterBLL.UpdateCategory(videoCategory);
                
            return "true";
        }
        catch (Exception ex)
        {
            return "false";
        }
        
    }
    
    #endregion

    #region 删除视频分类

    private string DeleteCategory(HttpContext context)
    {
        string CategoryID = context.Request.Form["CategoryID"];

        if (string.IsNullOrEmpty(CategoryID))
        {
            return "false";
        }

        try
        {
            videoCenterBLL.DeleteCategory(Convert.ToInt32(CategoryID));

            return "true";
        }
        catch (Exception ex)
        {
            return "false";
        }
        
    }
    
    #endregion

    #region 添加视频到分类

    public string AddVideoToCategory(HttpContext context)
    {
        
        
        string ID = context.Request.Form["ID"].Trim();
        string VideoName =  context.Request.Form["VideoName"].Trim();
        string Url = context.Request.Form["Url"].Trim();
        string CreateTime = context.Request.Form["CreateTime"].Trim();
        string VideoCategoryID = context.Request.Form["CategoryID"].Trim();
        if (string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(VideoName) || string.IsNullOrEmpty(Url) || string.IsNullOrEmpty(CreateTime) || string.IsNullOrEmpty(VideoCategoryID))
        {
            return "false";
        }

        try
        {
            Videos video = new Videos() { ID = Convert.ToInt32(ID), VideoName = VideoName, Url = Url, CreateTime = Convert.ToDateTime(CreateTime), VideoCategoryID = Convert.ToInt32(VideoCategoryID) };

            videoCenterBLL.AddVideoToCategory(video);

            return "true";
        }
        catch (Exception ex)
        {
            return "false";
        }
        
        
        
    }
    
    #endregion

    #region 从分类删除视频

    public string DeleteVideoCategory(HttpContext context)
    {
        string ID = context.Request.Form["ID"].Trim();
        string VideoName = context.Request.Form["VideoName"].Trim();
        string Url = context.Request.Form["Url"].Trim();
        string CreateTime = context.Request.Form["CreateTime"].Trim();
        if (string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(VideoName) || string.IsNullOrEmpty(Url) || string.IsNullOrEmpty(CreateTime))
        {
            return "false";
        }

        try
        {
            Videos video = new Videos() { ID = Convert.ToInt32(ID), VideoName = VideoName, Url = Url, CreateTime = Convert.ToDateTime(CreateTime) };

            videoCenterBLL.AddVideoToCategory(video);

            return "true";
        }
        catch (Exception ex)
        {
            return "false";
        }
        
        
    }
    
    #endregion

    #region 获取视频分类

    private string GetCategories()
    {
        List<Models.VideoCategory> VideoCategories = videoCenterBLL.GetVideoCategories();

        using (MemoryStream ms = new MemoryStream())
        {
            DataContractJsonSerializer JsonSerializer = new DataContractJsonSerializer(typeof(List<Models.VideoCategory>));

            JsonSerializer.WriteObject(ms, VideoCategories);

            return Encoding.UTF8.GetString(ms.ToArray());

        }
    }
    #endregion


    public bool IsReusable {
        get {
            return false;
        }
    }

}