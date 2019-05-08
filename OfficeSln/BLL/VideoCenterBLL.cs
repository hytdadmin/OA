using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using Models.TO;
using Models;
using System.IO;
using System.Text.RegularExpressions;

namespace BLL
{
    public class VideoCenterBLL
    {
        VideoCenterDAL service = new VideoCenterDAL();

        public DataTable GetVideoCenterListByFileds(VideosTo TO, int pageIndex, int pageSize, string orderBy, string fields, out int rowCount)
        {
            return service.GetVideoCenterListByFileds(TO, pageIndex, pageSize, orderBy, fields, out rowCount);
        }

        public DataSet GetVideosManageInfo()
        {
            return service.GetVideosManageInfo();
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddUser(VideoCategory_UserInfo entity)
        {
            service.AddUser(entity);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DelUser(VideoCategory_UserInfo entity)
        {
            service.DelUser(entity);
        }

        public bool ReflashVideos(string path)
        {
            return service.ReflashVideos(path);
        }


        #region 修改视频分类

        public void UpdateCategory(Models.VideoCategory videoCategory)
        {
            service.UpdateCategory(videoCategory);
        }

        #endregion

        #region 删除视频分类

        public void DeleteCategory(int CategoryID)
        {
            service.DeleteCategory(CategoryID);
        }

        #endregion

        #region 添加视频到分类

        public void AddVideoToCategory(Videos video)
        {
            service.AddVideoToCategory(video);
        }

        #endregion

        #region 删除视频到分类

        public void DeleteVideoCategory(Videos video)
        {
            service.DeleteVideoCategory(video);
        }

        #endregion

        #region 获取视频信息

        public Models.VideoCategory GetVideoCategory(string VideoCategoryName)
        {
            return service.GetVideoCategory(VideoCategoryName);
        }
        #endregion

        #region 获取视频分类列表

        public List<Models.VideoCategory> GetVideoCategories()
        {
            return service.GetVideoCategories();
        }

        #endregion
    }
}
