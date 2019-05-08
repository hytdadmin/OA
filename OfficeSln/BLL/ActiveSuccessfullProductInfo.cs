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
Date：2014-04-25 12:42:34
Description: BLL层 ActiveSuccessfullProductInfo
*/
namespace BLL
{


	public class ActiveSuccessfullProductInfoBLL
	{
   		 
   		 ActiveSuccessfullProductInfoDAL service = new ActiveSuccessfullProductInfoDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddActiveSuccessfullProductInfo(ActiveSuccessfullProductInfo entity)
        {
            service.AddActiveSuccessfullProductInfo(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateActiveSuccessfullProductInfo(ActiveSuccessfullProductInfo entity)
        {
            service.UpdateActiveSuccessfullProductInfo(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ActiveSuccessfullProductInfo GetActiveSuccessfullProductInfoEntity(int guid)
        {
            return service.GetActiveSuccessfullProductInfoEntity(guid);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<ActiveSuccessfullProductInfo> GetActiveSuccessfullProductInfoList()
        {
            return service.GetActiveSuccessfullProductInfoList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetActiveSuccessfullProductInfoList(ActiveSuccessfullProductInfoTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetActiveSuccessfullProductInfoList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
	}
}