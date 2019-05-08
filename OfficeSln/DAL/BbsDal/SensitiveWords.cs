using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using Models;
using HYTD.BBS.Model.TO;
/*
Author：liulei
Version：1.0
Date：2013-10-24 10:07:14
Description: DAL层 SensitiveWords
*/
namespace HYTD.BBS.DAL
{

	public class SensitiveWordsDAL
	{
       LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddSensitiveWords(SensitiveWords entity)
		{
			linqHelper.InsertEntity<SensitiveWords>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdateSensitiveWords(SensitiveWords entity)
		{
			linqHelper.UpdateEntity<SensitiveWords>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public SensitiveWords GetSensitiveWordsEntity(int ID)
		{
			return linqHelper.GetEntity<SensitiveWords>(c => c.ID == ID);
		}
		
		
		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSensitiveWords(int ID)
        {
            linqHelper.DeleteEntity<SensitiveWords>(c => c.ID == ID);
        }
		
		
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<SensitiveWords> GetSensitiveWordsList()
		{
			return linqHelper.GetList<SensitiveWords>(c=>c.status==1);
		}

   
	}
}