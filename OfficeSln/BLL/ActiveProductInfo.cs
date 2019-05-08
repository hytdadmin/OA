using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYTD.DAL;
using HYTD.Model;
using System.Data;
using HYTD.Model.TO;
using DAL;
using Models;
using Models.TO;
/*
Author：liulei
Version：1.0
Date：2014-04-15 12:16:00
Description: BLL层 ActiveProductInfo
*/
namespace BLL
{


	public class ActiveProductInfoBLL
	{
   		 
   		 ActiveProductInfoDAL service = new ActiveProductInfoDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddActiveProductInfo(ActiveProductInfo entity)
        {
            service.AddActiveProductInfo(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateActiveProductInfo(ActiveProductInfo entity)
        {
            service.UpdateActiveProductInfo(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActiveProductInfo GetActiveProductInfoEntity(int guid)
        {
            return service.GetActiveProductInfoEntity(guid);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<ActiveProductInfo> GetActiveProductInfoList()
        {
            return service.GetActiveProductInfoList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetActiveProductInfoList(ActiveProductInfoTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetActiveProductInfoList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
	}
}