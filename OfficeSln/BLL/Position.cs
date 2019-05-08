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
Date：2013-09-17 10:59:06
Description: BLL层 Position
*/
namespace BLL
{


	public class PositionBLL
	{
   		 
   		 PositionDAL service = new PositionDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddPosition(Position entity)
        {
            service.AddPosition(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdatePosition(Position entity)
        {
            new PositionDAL().UpdatePosition(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Position GetPositionEntity(int FID)
        {
            return service.GetPositionEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeletePosition(int FID)
        {
            service.DeletePosition(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Position> GetPositionList()
        {
            return service.GetPositionList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetPositionList(PositionTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetPositionList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
	}
}