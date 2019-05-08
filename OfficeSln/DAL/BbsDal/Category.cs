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
Date：2013-10-22 14:34:30
Description: DAL层 Category
*/
namespace HYTD.BBS.DAL
{

    public class CategoryDAL
    {
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddCategory(Category entity)
        {
            linqHelper.InsertEntity<Category>(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateCategory(Category entity)
        {
            linqHelper.UpdateEntity<Category>(entity);
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        public Category GetCategoryEntity(int ID)
        {
            return linqHelper.GetEntity<Category>(c => c.ID == ID);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCategory(int ID)
        {
            linqHelper.DeleteEntity<Category>(c => c.ID == ID);
        }



        /// <summary>
        /// 获取全部实体集合
        /// </summary>
        public List<Category> GetCategoryList()
        {
            return linqHelper.GetList<Category>();
        }


        /// <summary>
        /// 获取实体分页
        /// </summary>
        public DataTable GetCategoryList(CategoryTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [Category] ";
            string pk = " ID ";
            string fields = " * ";
            string filter = "";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件


            //if (!string.IsNullOrEmpty(TO.MC))
            //{
            //	filter += string.Format(" and MC like '%{0}%' ", StringHelper.SQLFilter(TO.MC));
            //}

            #endregion

            string sort = " ID DESC ";//排序
            if (!string.IsNullOrEmpty(orderBy))
                sort = orderBy;

            SqlParameter[] parameters = {
                new SqlParameter("@Tables",SqlDbType.VarChar,1000),
                new SqlParameter("@PK",SqlDbType.VarChar,100),
                new SqlParameter("@Fields",SqlDbType.VarChar,1000),
                new SqlParameter("@Pageindex",SqlDbType.Int),
                new SqlParameter("@PageSize",SqlDbType.Int),
                new SqlParameter("@Filter",SqlDbType.VarChar,1000),
                new SqlParameter("@Sort",SqlDbType.VarChar,200),
                new SqlParameter("@RowCount",SqlDbType.Int)
            };
            parameters[0].Value = table;
            parameters[1].Value = pk;
            parameters[2].Value = fields;
            parameters[3].Value = pageIndex;
            parameters[4].Value = pageSize;
            parameters[5].Value = filter;
            parameters[6].Value = sort;
            parameters[7].Direction = ParameterDirection.Output;

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "CategoryList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }



    }
}