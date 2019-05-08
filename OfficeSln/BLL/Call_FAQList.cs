using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYTD.DAL;
using HYTD.Model;
using System.Data;
using Models;
using HYTD.Models.TO;

namespace HYTD.BLL
{
    public class Call_FAQListBLL
    {
        Call_FAQListDAL service = new Call_FAQListDAL();



        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddCall_FAQList(Call_FAQList entity)
        {
            service.AddCall_FAQList(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCall_FAQList(Call_FAQList entity)
        {
            new Call_FAQListDAL().UpdateCall_FAQList(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Call_FAQList GetCall_FAQListEntity(int FID)
        {
            return service.GetCall_FAQListEntity(FID);
        }



        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_FAQList(int FID)
        {
            service.DeleteCall_FAQList(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Call_FAQList> GetCall_FAQListList()
        {
            return service.GetCall_FAQList();
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Call_FAQList> GetCall_FAQListList(string strName)
        {
            return service.GetCall_FAQList(strName);
        }

        /// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetCall_FAQListList(Call_FAQListTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetCall_FAQList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


        #endregion
    }
}
