<%@ WebHandler Language="C#" Class="VideoCenter" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Data;
using Models.TO;
using BLL;

public class VideoCenter : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (!PageBase.IsLogin())
        {
            context.Response.Write("sessionError");
            return;
        }
        //参数
        //string keyValue = context.Request.Params.Get("keyValue");
        //if (string.IsNullOrEmpty(keyValue))
        //{
        //    context.Response.Write("keyValue");
        //    return;
        //}

        string CategoryName = context.Request.Form["CategoryName"].Trim();

        PageBase.ClearClientPageCache();



        context.Response.Write(GetVideosList(CategoryName, context, PageBase.GetLoginCode()));
    }

    private string GetVideosList(string CategoryName, HttpContext context, string userCode)
    {
        int numPerPage = 10;
        string orderBy = " CreateTime desc ";
        int totalCount =0;
        int pageindex = Check.GetInt32(context.Request["p"]);
        if (pageindex == 0)
            pageindex = 1;
       
        VideosTo VideoCenterTO = new VideosTo();
        if (string.IsNullOrEmpty(CategoryName))
        { 
             VideoCenterTO.VideoCategoryName = CategoryName;
        }
        
        VideoCenterTO.UserCode = userCode;
        string fields = "";

        DataTable dt = new VideoCenterBLL().GetVideoCenterListByFileds(VideoCenterTO, pageindex, numPerPage, orderBy, fields, out totalCount);

        List<VideoCategory> VideoCategories = new List<VideoCategory>();
        foreach (var row in dt.AsEnumerable())
        {
            if (VideoCategories.Exists(item => item.VideoCategoryName == row.Field<string>("VideoCategoryName")))
            {
                VideoCategory videoCategory = VideoCategories.Find(item => item.VideoCategoryName == row.Field<string>("VideoCategoryName"));
                videoCategory.Videos.Add(new Video() { CategoryID = row.Field<int>("VideoCategoryID"), Name = row.Field<string>("VideoName"), Url = row.Field<string>("Url"), CreateTime = row.Field<DateTime>("CreateTime").ToString() });
            }
            else
            {
                VideoCategory videoCategorynew = new VideoCategory();
                videoCategorynew.ID = row.Field<int>("VideoCategoryID");
                videoCategorynew.VideoCategoryName = row.Field<string>("VideoCategoryName");
                videoCategorynew.RowCount = totalCount;
                videoCategorynew.Description = row.Field<string>("Description");
                videoCategorynew.Videos.Add(new Video() { Name = row.Field<string>("VideoName"), Url = row.Field<string>("Url"), CategoryID = row.Field<int>("VideoCategoryID"), CreateTime = row.Field<DateTime>("CreateTime").ToString() });
                VideoCategories.Add(videoCategorynew);
            }
            
        }

        using (MemoryStream ms = new MemoryStream())
        {
            DataContractJsonSerializer JsonSerializer = new DataContractJsonSerializer(typeof(List<VideoCategory>));
            JsonSerializer.WriteObject(ms, VideoCategories);
            return Encoding.UTF8.GetString(ms.ToArray());
        }
        
        
    }

    
    
   
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}