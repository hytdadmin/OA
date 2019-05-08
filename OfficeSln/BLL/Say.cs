using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;
using System.Data;
using Model.TO;
using Models.TO;
/*
Author：liulei
Version：1.0
Date：2013-09-17 10:59:09
Description: BLL层 Say
*/
namespace BLL
{


	public class SayBLL
	{
   		 
   		 SayDAL service = new SayDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddSay(Say entity)
        {
            service.AddSay(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateSay(Say entity)
        {
            new SayDAL().UpdateSay(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Say GetSayEntity(int FID)
        {
            return service.GetSayEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSay(int FID)
        {
            service.DeleteSay(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Say> GetSayList()
        {
            return service.GetSayList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetSayList(SayTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetSayList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddSayReturn(Say entity)
        {
            return service.AddSayReturn(entity);
        }
        
        /// <summary>
        /// 获取最新的一条说说
        /// </summary>
        public Say GetSayEntityByPublishId(string publishUserCode)
        {
            return service.GetSayEntityByPublishId(publishUserCode);
        }
        
        /// <summary>
        /// 获取最新N条说说消息
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<SayList> GetSayListByTop(int top)
        {
            return service.GetSayListByTop(top);
        }
        public int GetSayListPagerCount(string contents)
        {
            return service.GetSayListPagerCount(contents);
        }

        public List<Say> GetSayListPager(int pageIndex, int pageSize, string contents)
        {
            return service.GetSayListPager(pageIndex,pageSize,contents);
        }
        
        /// <summary>
        /// 左侧说说列表
        /// 获取实体分页
        /// </summary>
        public DataTable GetSayListByLeft(SayTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetSayListByLeft(TO, pageIndex, pageSize,orderBy,out rowCount);
        }
        /// <summary>
        /// 多表连接查询
        /// </summary>
        public DataTable GetSayListByTB(SayTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetSayListByTB(TO, pageIndex, pageSize, orderBy, out rowCount);
        }
	}
    
}