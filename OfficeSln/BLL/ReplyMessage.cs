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
Date：2013-09-17 10:59:07
Description: BLL层 ReplyMessage
*/
namespace BLL
{


	public class ReplyMessageBLL
	{
   		 
   		 ReplyMessageDAL service = new ReplyMessageDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddReplyMessage(ReplyMessage entity)
        {
            service.AddReplyMessage(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateReplyMessage(ReplyMessage entity)
        {
            new ReplyMessageDAL().UpdateReplyMessage(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ReplyMessage GetReplyMessageEntity(int FID)
        {
            return service.GetReplyMessageEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteReplyMessage(int FID)
        {
            service.DeleteReplyMessage(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<ReplyMessage> GetReplyMessageList()
        {
            return service.GetReplyMessageList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetReplyMessageList(ReplyMessageTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetReplyMessageList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
	}
}