using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Models;
using HYTD.BBS.Model.TO;
using HYTD.BBS.DAL;
/*
Author：liulei
Version：1.0
Date：2013-10-22 14:34:32
Description: BLL层 Posts
*/
namespace HYTD.BBS.BLL
{


    public class PostsBLL
    {

        PostsDAL service = new PostsDAL();



        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddPosts(Posts entity)
        {
            service.AddPosts(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdatePosts(Posts entity)
        {
            new PostsDAL().UpdatePosts(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Posts GetPostsEntity(int FID)
        {
            return service.GetPostsEntity(FID);
        }



        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeletePosts(int FID)
        {
            service.DeletePosts(FID);
        }

        /// <summary>
        /// 计算次数
        /// </summary>
        /// <param name="userName"></param>
        public void CountPosts(string userName)
        {
            service.CountPosts(userName);
        }

        /// <summary>
        /// 计算回帖数
        /// </summary>
        /// <param name="userName"></param>
        public void CountRePosts(int fatherId)
        {
            service.CountRePosts(fatherId);
        }



        public List<Posts> PostList()
        {
            var post = GetPostsList();
            var category = new CategoryBLL().GetCategoryList();

            var result = from c in post
                         join d in category
                         on c.CID equals d.ID
                         where d.satatus == 1 && c.status == 1
                         orderby c.ReleaseTime descending
                         select c;

            return result.ToList();
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Posts> GetPostsList()
        {
            return service.GetPostsList();
        }
        public DataTable GetPostsInfoList()
        {
            return service.GetPostsInfoList();
        }


        /// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetPostsList(PostsTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetPostsList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


        #endregion

    }
}