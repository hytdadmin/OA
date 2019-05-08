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
Date：2013-09-17 10:59:05
Description: BLL层 Journal
*/
namespace BLL
{


	public class JournalBLL
	{
   		 
   		 JournalDAL service = new JournalDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddJournal(Journal entity)
        {
            service.AddJournal(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateJournal(Journal entity)
        {
            new JournalDAL().UpdateJournal(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Journal GetJournalEntity(int FID)
        {
            return service.GetJournalEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteJournal(int FID)
        {
            service.DeleteJournal(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Journal> GetJournalList()
        {
            return service.GetJournalList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetJournalList(JournalTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetJournalList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }
        
        /// <summary>
        /// 多表连接查询
        /// </summary>
        public DataTable GetJournalListByTB(JournalTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetJournalListByTB(TO, pageIndex, pageSize, orderBy, out rowCount);
        }

		#endregion

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddJournalReturn(Journal entity)
        {
            return service.AddJournalReturn(entity);
        }
        /// <summary>
        /// 删除一条数据(只能删除自己的)
        /// </summary>
        /// <param name="id"></param>
        public bool DeleteJournalByUserCode(int FID, string userCode) {
            return service.DeleteJournalByUserCode(FID, userCode);
        }
	}
}