using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Models.TO;
using DBUtility;
using HYTD.Common;
using Models;
using System.IO;


namespace DAL
{
    public class VideoCenterDAL
    {

        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();

        /// <summary>
        /// 获取实体分页
        /// </summary>
        public DataTable GetVideoCenterListByFileds(VideosTo TO, int pageIndex, int pageSize, string orderBy, string fields, out int rowCount)
        {

            string table = " Videos JOIN  VideoCategory ON  Videos.VideoCategoryID = VideoCategory.ID JOIN VideoCategory_UserInfo ON VideoCategory_UserInfo.VideoCategoryID = VideoCategory.ID ";
            string pk = " ID ";
            fields = string.IsNullOrEmpty(fields) ? " VideoCategory.ID AS VideoCategoryID,VideoName,Url,VideoCategoryName,CreateTime,VideoCategory.Description " : fields;
            string filter = string.Format(" VideoCategory_UserInfo.UserID ={0} ",TO.UserCode);// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件


            if (!string.IsNullOrEmpty(TO.VideoCategoryName))
            {
                filter += string.Format(" AND VideoCategory.VideoCategoryName = '{0}' ", StringHelper.SQLFilter(TO.VideoCategoryName));
            }

            #endregion

            string sort = " Videos.ID DESC ";//排序
            if (!string.IsNullOrEmpty(orderBy))
                sort = orderBy;

            SqlParameter[] parameters = {
                new SqlParameter("@Tables",SqlDbType.VarChar,1000),
                new SqlParameter("@PK",SqlDbType.VarChar,100),
                new SqlParameter("@Fields",SqlDbType.VarChar,1000),
                new SqlParameter("@Pageindex",SqlDbType.Int),
                new SqlParameter("@PageSize",SqlDbType.Int),
                new SqlParameter("@Filter",SqlDbType.VarChar,1000),
                new SqlParameter("@Sort",SqlDbType.VarChar,200),
                new SqlParameter("@RowCount",SqlDbType.Int)
            };
            parameters[0].Value = table;
            parameters[1].Value = pk;
            parameters[2].Value = fields;
            parameters[3].Value = pageIndex;
            parameters[4].Value = pageSize;
            parameters[5].Value = filter;
            parameters[6].Value = sort;
            parameters[7].Direction = ParameterDirection.Output;

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "VideoCenter");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }

        public DataSet GetVideosManageInfo()
        {
            SqlParameter[] parameters = new SqlParameter[1];
            DataSet ds = SqlHelper.RunProcedureDataSet("pVideosManage", parameters);
            return ds;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddUser(VideoCategory_UserInfo entity)
        {
            linqHelper.InsertEntity<VideoCategory_UserInfo>(entity);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DelUser(VideoCategory_UserInfo entity)
        {
            linqHelper.DeleteEntity<VideoCategory_UserInfo>(c => c.UserID == entity.UserID && c.VideoCategoryID == entity.VideoCategoryID);
        }

        public bool ReflashVideos(string path)
        {
            Videos[] videos = Directory.GetFiles(path + "Videos\\", "*", SearchOption.AllDirectories).Where(item => { return (item.EndsWith(".flv") || item.EndsWith(".mp4")); }).Select(item => new Videos() { VideoName = item.Substring(item.LastIndexOf('\\') + 1), Url = item.Substring(path.Length - 1) }).ToArray();

            string[] sourceVideosName = linqHelper.GetList<Videos>().Select( item => item.VideoName).ToArray();
            Videos[] result = videos.Where( item => !sourceVideosName.Contains(item.VideoName)).ToArray();

            string[] directories = Directory.GetDirectories(path + "Videos\\").Select(item => item.Substring(item.LastIndexOf("\\") + 1)).ToArray();

            string[] sourceDirectories = linqHelper.GetList<Models.VideoCategory>().Select(item => item.VideoCategoryName).ToArray();

            string[] newDirectories = directories.Where(item => !sourceDirectories.Contains(item)).ToArray();

            try
            { 
                

                foreach (string item in newDirectories)
                {
                    Models.VideoCategory videoCategory = new Models.VideoCategory();

                    videoCategory.VideoCategoryName = item.Substring(item.LastIndexOf("\\")+1);

                    linqHelper.InsertEntity<Models.VideoCategory>(videoCategory);
                }

                foreach (Videos video in result)
                {

                    string CategoryName = video.Url.Split(new string[]{"\\"},StringSplitOptions.None)[2];

                    int VideoCategoryID = linqHelper.GetEntity<Models.VideoCategory>(item => item.VideoCategoryName == CategoryName).ID;

                    video.VideoCategoryID = VideoCategoryID;

                    video.CreateTime = DateTime.Now;
                    

                    linqHelper.InsertEntity<Videos>(video);
                }


                return true;
            }
            catch(Exception ex)
            {
                return false;
            }


        }

        #region 增加视频分类

        public void UpdateCategory(Models.VideoCategory videoCategory)
        {
            linqHelper.UpdateEntity<Models.VideoCategory>(videoCategory);
        }

        #endregion

        #region 删除视频分类

        public void DeleteCategory(int CategoryID)
        {
            linqHelper.DeleteEntity<Models.VideoCategory>(item => item.ID == CategoryID);
        }

        #endregion

        #region 增加视频到分类

        public void AddVideoToCategory(Videos video)
        {
            linqHelper.UpdateEntity<Videos>(video);
        }

        #endregion

        #region 删除视频到分类

        public void DeleteVideoCategory(Videos video)
        {
            linqHelper.UpdateEntity<Videos>(video);
        }

        #endregion

        #region 获取视频分类

        public Models.VideoCategory GetVideoCategory(string VideoCategoryName)
        {
            return linqHelper.GetEntity<Models.VideoCategory>(item => item.VideoCategoryName == VideoCategoryName);
        }

        #endregion

        #region 获取视频分类列表

        public List<Models.VideoCategory> GetVideoCategories()
        {
            return linqHelper.GetList<Models.VideoCategory>().ToList();
        }

        #endregion



    }
}
