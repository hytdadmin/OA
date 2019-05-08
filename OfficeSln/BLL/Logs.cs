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
Date：2013-10-12 10:45:17
Description: BLL层 Logs
*/
namespace BLL
{


	public class LogsBLL
	{
   		 
   		 LogsDAL service = new LogsDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddLogs(Logs entity)
        {
            service.AddLogs(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateLogs(Logs entity)
        {
            new LogsDAL().UpdateLogs(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Logs GetLogsEntity(int FID)
        {
            return service.GetLogsEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteLogs(int FID)
        {
            service.DeleteLogs(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Logs> GetLogsList()
        {
            return service.GetLogsList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetLogsList(LogsTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetLogsList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddLogReturn(Logs entity)
        {
            return service.AddLogReturn(entity);
        }
        
        /// <summary>
        /// 访问量
        /// </summary>
        /// <returns></returns>
        public int GetLoginCount()
        {
            return service.GetLoginCount();
        }
	}
}