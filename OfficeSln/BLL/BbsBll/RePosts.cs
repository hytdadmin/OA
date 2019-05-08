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
Date：2013-10-22 14:34:34
Description: BLL层 RePosts
*/
namespace HYTD.BBS.BLL
{


    public class RePostsBLL
    {

        RePostsDAL service = new RePostsDAL();



        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddRePosts(RePosts entity)
        {
            service.AddRePosts(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateRePosts(RePosts entity)
        {
            new RePostsDAL().UpdateRePosts(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public RePosts GetRePostsEntity(int FID)
        {
            return service.GetRePostsEntity(FID);
        }



        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteRePosts(int FID)
        {
            service.DeleteRePosts(FID);
        }

        /// <summary>
        /// 删除多条数据1
        /// </summary>
        /// <param name="fatherId"></param>
        public void DeleteRePostsByFid(int fatherId)
        {
            service.DeleteRePostsByFid(fatherId);
        }


        /// <summary>
        /// 删除多条数据2
        /// </summary>
        /// <param name="fatherId"></param>
        public void DeleteRePostsByRelFatherId(int relFatherId)
        {
            service.DeleteRePostsByRelFatherId(relFatherId);
        }

        /// <summary>
        /// 修改多条数据状态1
        /// </summary>
        /// <param name="fatherId"></param>
        public void UpdateRePostsStatuByFid(int fatherId, int status)
        {
            service.UpdateRePostsStatuByFid(fatherId, status);
        }

        /// <summary>
        ///  修改多条数据状态2
        /// </summary>
        /// <param name="fatherId"></param>
        public void UpdateRePostsStatusByRelFatherId(int relFatherId, int status)
        {
            service.UpdateRePostsStatusByRelFatherId(relFatherId, status);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<RePosts> GetRePostsList()
        {
            return service.GetRePostsList();
        }


        /// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetRePostsList(RePostsTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetRePostsList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


        #endregion


        /// <summary>
        /// 自定义获得搜索数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetNewTab(RePostsTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetNewTab(TO, pageIndex, pageSize, orderBy, out rowCount);
        }

    }
}