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
Date：2013-10-22 14:34:29
Description: BLL层 Category
*/
namespace HYTD.BBS.BLL
{

    public class CategoryBLL
    {

        CategoryDAL service = new CategoryDAL();



        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddCategory(Category entity)
        {
            service.AddCategory(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCategory(Category entity)
        {
            new CategoryDAL().UpdateCategory(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Category GetCategoryEntity(int FID)
        {
            return service.GetCategoryEntity(FID);
        }



        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCategory(int FID)
        {
            service.DeleteCategory(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategoryList()
        {
            return service.GetCategoryList();
        }


        /// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetCategoryList(CategoryTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetCategoryList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


        #endregion

    }
}