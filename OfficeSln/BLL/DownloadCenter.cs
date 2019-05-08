using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;
using System.Data;
using Model.TO;
/*
Author：liulei
Version：1.0
Date：2013-09-17 10:59:03
Description: BLL层 DownloadCenter
*/
namespace BLL
{


	public class DownloadCenterBLL
	{
   		 
   		 DownloadCenterDAL service = new DownloadCenterDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddDownloadCenter(DownloadCenter entity)
        {
            service.AddDownloadCenter(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateDownloadCenter(DownloadCenter entity)
        {
            new DownloadCenterDAL().UpdateDownloadCenter(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public DownloadCenter GetDownloadCenterEntity(int FID)
        {
            return service.GetDownloadCenterEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteDownloadCenter(int FID)
        {
            service.DeleteDownloadCenter(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<DownloadCenter> GetDownloadCenterList()
        {
            return service.GetDownloadCenterList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetDownloadCenterList(DownloadCenterTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetDownloadCenterList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }
        

        /// <summary>
        /// 获取实体分页(可设定返回值)
        /// </summary>
        public DataTable GetDownloadCenterListByFileds(DownloadCenterTO TO, int pageIndex, int pageSize, string orderBy, string fileds, out int rowCount)
        {
            return service.GetDownloadCenterListByFileds(TO, pageIndex, pageSize, orderBy,fileds, out rowCount);
        }

		#endregion
   

        /// <summary>
        /// 获取实体分页
        /// 后台使用
        /// </summary>
        public DataTable GetDownloadCenterListByBack(DownloadCenterTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetDownloadCenterListByBack(TO, pageIndex, pageSize, orderBy, out rowCount);
        }
        
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">所有id，格式,1,2,3,4,</param>
        /// <returns></returns>
        public bool DelDownloadCenters(string ids)
        {
            return service.DelDownloadCenters(ids);
        }
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddDownloadCenterReturn(DownloadCenter entity)
        {
            return service.AddDownloadCenterReturn(entity);
        }
	}
}