using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Models.TO;
using Models;
using DAL;

namespace BLL
{
    public class FeedbackBLL
    {

        FeedbackDAL service = new FeedbackDAL();



        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddFeedback(Feedback entity)
        {
            service.AddFeedback(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateFeedback(Feedback entity)
        {
            new FeedbackDAL().UpdateFeedback(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Feedback GetFeedbackEntity(int FID)
        {
            return service.GetFeedbackEntity(FID);
        }



        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteFeedback(int FID)
        {
            service.DeleteFeedback(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Feedback> GetFeedbackList()
        {
            return service.GetFeedbackList();
        }


        /// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetFeedbackList(FeedbackTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetFeedbackList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


        #endregion

    }
}
