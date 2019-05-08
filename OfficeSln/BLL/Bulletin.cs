using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;
using System.Data;
using Model.TO;
using Models.Result;
/*
Author：liulei
Version：1.0
Date：2013-09-17 10:59:01
Description: BLL层 Bulletin
*/
namespace BLL 
{


	public class BulletinBLL
	{
   		 
   		 BulletinDAL service = new BulletinDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddBulletin(Bulletin entity)
        {
            service.AddBulletin(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateBulletin(Bulletin entity)
        {
            new BulletinDAL().UpdateBulletin(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Bulletin GetBulletinEntity(int FID)
        {
            return service.GetBulletinEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBulletin(int FID)
        {
            service.DeleteBulletin(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Bulletin> GetBulletinList()
        {
            return service.GetBulletinList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetBulletinList(BulletinTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetBulletinList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
        
        /// <summary>
        /// 返回前N条数据
        /// </summary>
        public List<Bulletin> GetBulletinListTop(int top)
        {
           return service.GetBulletinListTop(top);
        }
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddBulletinReturn(Bulletin entity)
        {
            return service.AddBulletinReturn(entity);
        }
        
        /// <summary>
        /// 获取公告详情
        /// </summary>
        public BulletinDetailResult GetBulletinDetailById(int ID)
        {
            return service.GetBulletinDetailById(ID);
        }
        
        /// <summary>
        /// 获取实体分页
        /// 后台使用
        /// </summary>
        public DataTable GetBulletinListByBack(BulletinTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetBulletinListByBack(TO, pageIndex, pageSize, orderBy, out rowCount);
        }
        
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">所有id，格式,1,2,3,4,</param>
        /// <returns></returns>
        public bool DelBulletins(string ids)
        {
            return service.DelBulletins(ids);
        }
	}
}