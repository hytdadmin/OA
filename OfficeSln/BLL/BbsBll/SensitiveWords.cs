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
Date：2013-10-24 10:07:14
Description: BLL层 SensitiveWords
*/
namespace HYTD.BBS.BLL
{


	public class SensitiveWordsBLL
	{
   		 
   		 SensitiveWordsDAL service = new SensitiveWordsDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddSensitiveWords(SensitiveWords entity)
        {
            service.AddSensitiveWords(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateSensitiveWords(SensitiveWords entity)
        {
            new SensitiveWordsDAL().UpdateSensitiveWords(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public SensitiveWords GetSensitiveWordsEntity(int FID)
        {
            return service.GetSensitiveWordsEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSensitiveWords(int FID)
        {
            service.DeleteSensitiveWords(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<SensitiveWords> GetSensitiveWordsList()
        {
            return service.GetSensitiveWordsList();
        }



		#endregion
   
	}
}