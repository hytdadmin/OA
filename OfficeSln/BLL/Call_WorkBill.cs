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
Date：2014-01-10 16:22:18
Description: BLL层 Call_WorkBill
*/
namespace HYTD.BLL
{


    public class Call_WorkBillBLL
    {

        Call_WorkBillDAL service = new Call_WorkBillDAL();



        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddCall_WorkBill(Call_WorkBill entity)
        {
            service.AddCall_WorkBill(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCall_WorkBill(Call_WorkBill entity)
        {
            new Call_WorkBillDAL().UpdateCall_WorkBill(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Call_WorkBill GetCall_WorkBillEntity(int FID)
        {
            return service.GetCall_WorkBillEntity(FID);
        }
        /// <summary>
        /// 获取实体分页 
        /// </summary>
        public int GetCall_WorkBillCounts(int intCusID)
        {
            return service.GetCall_WorkBillCounts(intCusID);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_WorkBill(int FID)
        {
            service.DeleteCall_WorkBill(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Call_WorkBill> GetCall_WorkBillList(int intCCID, string strProductID, string strLoginName)
        {
            return service.GetCall_WorkBillList(intCCID, strProductID, strLoginName);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Call_WorkBill> GetCall_WorkBillList()
        {
            return service.GetCall_WorkBillList();
        }

        /// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetCall_WorkBillList(string strWhere, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetCall_WorkBillList(strWhere, pageIndex, pageSize, orderBy, out rowCount);
        }
        /// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetCall_WorkBillList(Call_WorkBillTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetCall_WorkBillList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


        #endregion

        #region 自定义方法

        public bool AddANDUpdataCallWorkBill(Call_WorkBill entity, int intHF)
        {
            bool bolRet = false;
            if (intHF > 0)
            {
                BLL.Call_VisitBillBLL CVBBbll = new BLL.Call_VisitBillBLL();
                Call_VisitBill entity1 = new Call_VisitBill();

                entity1.CVB_CallInEmail = entity.CWB_CallInEmail;
                entity1.CVB_CallInTel = entity.CWB_CallInTel;
                entity1.CVB_CallInUserName = entity.CWB_CallInUserName;
                entity1.CVB_CallType = entity.CWB_CallType;
                entity1.CVB_CCID = entity.CWB_CCID;
                entity1.CVB_Creater = entity.CWB_Creater;
                entity1.CVB_CreateTime = entity.CWB_OperationTime.Value;
                entity1.CVB_Description = entity.CWB_Description;
                entity1.CVB_ForUser = entity.CWB_ForUser;
                entity1.CVB_OperationTime = entity.CWB_OperationTime;
                entity1.CVB_OperationUser = entity.CWB_OperationUser;
                entity1.CVB_Remark = entity.CWB_Remark;
                entity1.CVB_ServiceType = entity.CWB_ServiceType;
                entity1.CVB_SoftType = entity.CWB_SoftType;
                entity1.CVB_Solution = entity.CWB_Solution;
                entity1.CVB_Status = entity.CWB_Status;
                entity1.CVB_Type = entity.CWB_Type;
                entity1.CVB_ID = 0;
                entity1.CVB_CWB_ID = entity.CWB_ID;
                CVBBbll.AddCall_VisitBill(entity1);
            }
            else
            {
                if (entity.CWB_ID > 0)
                {
                    UpdateCall_WorkBill(entity);
                }
                else
                {
                    AddCall_WorkBill(entity);
                }
            }
            Call_WorkBillHistoryBLL cwbhBll = new Call_WorkBillHistoryBLL();
            Call_WorkBillHistory model = new Call_WorkBillHistory();
            model.CWBH_CWB_ID = entity.CWB_ID;
            model.CWBH_Description = entity.CWB_Description;
            model.CWBH_OperationTime = entity.CWB_OperationTime;
            model.CWBH_Solution = entity.CWB_Solution;
            model.CWBH_Status = entity.CWB_Status;
            model.CWBH_UserID = entity.CWB_ForUser;
            model.CWBH_OperationUser = entity.CWB_OperationUser;
            model.CWBH_Remark = entity.CWB_Remark;
            cwbhBll.AddCall_WorkBillHistory(model);
            return bolRet;
        }

        /// <summary>
        /// 生成工单编号
        /// </summary>
        /// <returns></returns>
        public string WorkBillCreateCode()
        {
            string strResult = string.Empty;
            string strDate = DateTime.Today.ToString("yyyy-MM");
            strResult = service.WorkBillCreateCode(strDate);
            return strResult;
        }
        #endregion


        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public DataSet ImportExcelCustomerWorkBill(string strWhere)
        {
            return service.ImportExcelCustomerWorkBill(strWhere);
        }
        #region 统计信息
        /// <summary>
        /// 根据用户code获取用户工单统计信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet getInfoForCounteByUser(string strWhere)
        {
            return service.getInfoForCounteByUser(strWhere);
        }
        /// <summary>
        /// 获取所有工单统计信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet getInfoForCounteByALL(string strWhere)
        {
            return service.getInfoForCounteByALL(strWhere);
        }
        /// <summary>
        /// 按月份获取当前所有工单的统计信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet getInfoForCounteByMonth(DateTime dtDateTime)
        {
            return service.getInfoForCounteByMonth(dtDateTime);
        }

        /// <summary>
        /// 按年份获取当前所有工单的统计信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet getInfoForCounteBySelectedYear(DateTime dtDateTime)
        {
            return service.getInfoForCounteBySelectedYear(dtDateTime);
        }
        /// <summary>
        /// 按年份获取当前所有工单在不同时间段的统计信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet getInfoForCounteBySelectedYearTime(DateTime dtDateTime)
        {
            return service.getInfoForCounteBySelectedYearTime(dtDateTime);
        }

        #endregion

    }
}