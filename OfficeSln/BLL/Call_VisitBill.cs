using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYTD.DAL;
using HYTD.Model;
using System.Data;
using HYTD.Model.TO;
using Models;
/*
Author：liulei
Version：1.0
Date：2014-02-08 15:49:39
Description: BLL层 Call_VisitBill
*/
namespace HYTD.BLL
{


    public class Call_VisitBillBLL
    {

        Call_VisitBillDAL service = new Call_VisitBillDAL();



        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddCall_VisitBill(Call_VisitBill entity)
        {
            service.AddCall_VisitBill(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCall_VisitBill(Call_VisitBill entity)
        {
            new Call_VisitBillDAL().UpdateCall_VisitBill(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Call_VisitBill GetCall_VisitBillEntity(int FID)
        {
            return service.GetCall_VisitBillEntity(FID);
        }



        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_VisitBill(int FID)
        {
            service.DeleteCall_VisitBill(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Call_VisitBill> GetCall_VisitBillList()
        {
            return service.GetCall_VisitBillList();
        }


        /// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetCall_VisitBillList(Call_VisitBillTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetCall_VisitBillList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


        #endregion
        #region 自定义方法

        public bool AddANDUpdataCallVisitBill(Call_VisitBill entity, int intHF)
        {
            bool bolRet = false;

            if (entity.CVB_ID > 0)
            {
                UpdateCall_VisitBill(entity);
            }
            else
            {
                AddCall_VisitBill(entity);
            }

            //Call_WorkBillHistoryBLL cwbhBll = new Call_WorkBillHistoryBLL();
            //Models.Call_WorkBillHistory model = new Call_WorkBillHistory();
            //model.CWBH_CWB_ID = entity.CWB_ID;
            //model.CWBH_Description = entity.CWB_Description;
            //model.CWBH_OperationTime = entity.CWB_OperationTime;
            //model.CWBH_Solution = entity.CWB_Solution;
            //model.CWBH_Status = entity.CWB_Status;
            //model.CWBH_UserID = entity.CWB_ForUser;
            //model.CWBH_OperationUser = entity.CWB_OperationUser;
            //model.CWBH_Remark = entity.CWB_Remark;
            //cwbhBll.AddCall_WorkBillHistory(model);
            return bolRet;
        }
        #endregion
    }
}