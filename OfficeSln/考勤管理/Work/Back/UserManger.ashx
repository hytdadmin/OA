<%@ WebHandler Language="C#" Class="UserManger" %>

using System;
using System.Web;
using BLL;
using Models;
using System.Linq;
using HYTD.Common;
public class UserManger : IHttpHandler
{
    UserInfoBLL bll = new UserInfoBLL();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string op = context.Request["op"];
        if (op == "add")
        {
            AddUser(context);
        }
        else if (op == "update")
        {
            UpdateUser(context);
        }
        else if (op == "del")
        {
            DeleteUser(context);
        }
        else if (op == "holidayMag")
        {
            AddHoliadys(context);
        }
        else if (op == "holidayUpadte")
        {
            UpadteHoliadys(context);
        }
        else if (op=="setPosit")
        {
            SetPosition(context);
        }
        else if (op=="delPosit")
        {
            DelPosition(context);
        }
        else if (op=="addPosit")
        {
            AddPosition(context);
        }
        else if (op == "addDepart")
        {
            AddDepartment(context);
        }
        else if (op == "setDepart")
        {
            SetDepartment(context);
        }
        else if (op == "delDepart")
        {
            DelDepartment(context);
        }
        
    }
    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="context"></param>
    private void AddUser(HttpContext context)
    {
        try
        {
            string userCode = context.Request.Form["userCode"];
            string userName = context.Request.Form["userName"];
            string pwd = context.Request.Form["pwd"];
            string isAdmin = context.Request.Form["isAdmin"];
            string tel = context.Request.Form["tel"];
            string email = context.Request.Form["email"];
            string seletDepartment = context.Request.Form["seletDepartment"];
            string selectPosition = context.Request.Form["selectPosition"];
            string entyTime = context.Request.Form["entyTime"];
            int id = Convert.ToInt32(context.Request.QueryString["uid"]);
            if (bll.GetUserInfoList().Where(c => c.UserCode == userCode).Count() > 0) throw new Exception("用户名已存在！");
            UserInfo entity = new UserInfo();
            entity.UserCode = userCode;
            entity.UserName = userName;
            entity.IsAdmin = Convert.ToInt32(isAdmin);
            entity.Tel = tel;
            entity.Email = email;
            entity.Upwd = Md5Test.Md5str(pwd);
            entity.DepartmentId = Convert.ToInt32(seletDepartment);
            entity.PosiId = Convert.ToInt32(selectPosition);
            entity.UserStatus = 1;
            entity.AnnualLeave = 0;
            if (!string.IsNullOrEmpty(entyTime))
            {
                entity.EntyTime = Convert.ToDateTime(entyTime);
            }
            //else
            //    entity.EntyTime = null;
            bll.AddUserInfo(entity);
            context.Response.Write(1);
        }
        catch (Exception ex)
        {
            context.Response.Write(HttpUtility.JavaScriptStringEncode(ex.Message));
        }
    }
    /// <summary>
    /// 修改用户信息
    /// </summary>
    /// <param name="context"></param>
    private void UpdateUser(HttpContext context)
    {
        try
        {
            string userCode = context.Request.Form["userCode"];
            string userName = context.Request.Form["userName"];
            string isAdmin = context.Request.Form["isAdmin"];
            string tel = context.Request.Form["tel"];
            string email = context.Request.Form["email"];
            string seletSatus = context.Request.Form["seletSatus"];
            string seletDepartment = context.Request.Form["seletDepartment"];
            string selectPosition = context.Request.Form["selectPosition"];
            int id = Convert.ToInt32(context.Request.QueryString["uid"]);
            string entyTime = context.Request.Form["entyTime"];
            UserInfo entity = bll.GetUserInfoEntity(id);
            //if (userCode!=entity.UserCode)
            //{
            //    if (bll.GetUserInfoList().Where(c => c.UserCode == userCode).Count() > 0) throw new Exception("用户名已存在！"); 
            //}
            entity.UserName = userName;
            entity.IsAdmin = Convert.ToInt32(isAdmin);
            entity.Tel = tel;
            entity.Email = email;
            if (string.IsNullOrEmpty(seletSatus)) throw new Exception("请选择状态！");
            //if (Convert.ToInt32(seletDepartment) == 0) throw new Exception("请选择部门！");
            entity.UserStatus = Convert.ToInt32(seletSatus);
            entity.DepartmentId = Convert.ToInt32(seletDepartment);
            entity.PosiId = Convert.ToInt32(selectPosition);
            if (!string.IsNullOrEmpty(entyTime))
            {
                entity.EntyTime = Convert.ToDateTime(entyTime);
            }
            else
            {
                entity.EntyTime = null;
            }
            bll.UpdateUserInfo(entity);
            context.Response.Write(1);
        }
        catch (Exception ex)
        {
            context.Response.Write(HttpUtility.JavaScriptStringEncode(ex.Message));
        }
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="context"></param>
    private void DeleteUser(HttpContext context)
    {
        try
        {
            if (!string.IsNullOrEmpty(context.Request["userID"]))
            {
                int id = Convert.ToInt32(context.Request["userID"]);
                UserInfo user = bll.GetUserInfoEntity(id);
                if (user != null)
                {
                    if (user.UserStatus == 0)
                    {
                        context.Response.Write("该用户已被禁用！");
                    }
                    else
                    {
                        user.UserStatus = 0;
                        bll.UpdateUserInfo(user);
                        context.Response.Write("1");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }

    }
    /// <summary>
    /// 假期管理
    /// </summary>
    /// <param name="context"></param>
    private void AddHoliadys(HttpContext context)
    {
        try
        {
            string userCode = context.Request["userCode"];
            int holidayType = Convert.ToInt32(context.Request["holidayType"]);
            double dayCount = Convert.ToDouble(context.Request["dayCount"]);
            string startTime = context.Request["startTime"];
            string endTime = context.Request["endTime"];
            string remark = context.Request["remark"];
            HolidaysTable Model = new HolidaysTable();
            HolidaysBLL HolidaysBLL = new HolidaysBLL();
            if (string.IsNullOrEmpty(context.Request["startTime"]))
                throw new Exception("请选择时间段！");
            if (string.IsNullOrEmpty(context.Request["endTime"]))
                throw new Exception("请选择时间段！");
           
            startTime = context.Request["startTime"];
            endTime = context.Request["endTime"];
            if (Convert.ToDateTime(endTime) < Convert.ToDateTime(startTime))
                throw new Exception("结束时间不能小于开始时间");
            //TimeSpan ts = Convert.ToDateTime(endTime) - Convert.ToDateTime(startTime);
            //if(ts.TotalDays!=dayCount)
            //    throw new Exception("时间段和天数不一致！");
            
            Model.UserCode = userCode;
            Model.HolidaysType = holidayType;
            if (holidayType == 3)//1.年假  2.倒休 3.加班
            {
                Model.DayCount = dayCount;
            }
            else
            {
                Model.DayCount = -dayCount;
            }
            GetHolidays(userCode, Model.DayCount, holidayType);
            Model.startTime = Convert.ToDateTime(startTime);
            Model.endTime = Convert.ToDateTime(endTime);
            Model.Remark = remark;
            HolidaysBLL.AddHolidaysTable(Model);
            context.Response.Write("true");
        }
        catch (Exception ex)
        {
            context.Response.Write(HttpUtility.JavaScriptStringEncode(ex.Message));
        }
    }
    /// <summary>
    /// 假期统计
    /// </summary>
    /// <param name="userCode"></param>
    private static void GetHolidays(string userCode, double dayCount, int holidayType)
    {
        double yearDay = Convert.ToDouble(new UserInfoBLL().GetUserByUserCode(userCode).AnnualLeave);//年假
        double swoppedDay = 0;//调休天数
        double workDay = 0;//加班天数
        System.Data.DataTable dt = new HolidaysBLL().GetUserHolidays(userCode);
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                System.Data.DataRow dr = dt.Rows[i];
                if (Convert.ToInt32(dr["HolidaysType"]) == (int)PublicEnum.HolidaysType.Swopped)
                {
                    swoppedDay = Convert.ToDouble(dr["DayCount"]);
                  
                }
                else if (Convert.ToInt32(dr["HolidaysType"]) == (int)PublicEnum.HolidaysType.workDay)
                {
                    workDay = Convert.ToDouble(dr["DayCount"]);
                }
                else if (Convert.ToInt32(dr["HolidaysType"]) == (int)PublicEnum.HolidaysType.yearDay)
                {
                    yearDay = yearDay + Convert.ToDouble(dr["DayCount"]);                 
                }
            }
            if (holidayType==(int)PublicEnum.HolidaysType.yearDay)
            {
                if (yearDay + dayCount < 0)
                    throw new Exception("当前可用年假天数不足" + dayCount * -1 + "天！");
            }
            else if (holidayType == (int)PublicEnum.HolidaysType.Swopped)
            {
                if ((workDay + swoppedDay + dayCount) < 0)
                    throw new Exception("当前可用调休天数不足" + dayCount * -1 + "天！");
            }             
        }
        
    }
    /// <summary>
    /// 修改假期
    /// </summary>
    /// <param name="context"></param>
    private void UpadteHoliadys(HttpContext context)
    {
        try
        {
            string userCode = context.Request["userCode"];
            int holidayType = Convert.ToInt32(context.Request["holidayType"]);
            double dayCount = Convert.ToDouble(context.Request["dayCount"]);
            string startTime = context.Request["startTime"];
            string endTime = context.Request["endTime"];
            string remark = context.Request["remark"];
            HolidaysBLL HolidaysBLL = new HolidaysBLL();
            HolidaysTable Model = HolidaysBLL.GetHolidaysTableEntity(Convert.ToInt32(context.Request["hidID"]));
            if (string.IsNullOrEmpty(context.Request["startTime"]))
                throw new Exception("请选择时间段！");
            if (string.IsNullOrEmpty(context.Request["endTime"]))
                throw new Exception("请选择时间段！");
            startTime = context.Request["startTime"];
            endTime = context.Request["endTime"];
            Model.UserCode = userCode;
            Model.HolidaysType = holidayType;
            if (holidayType == 3)//1.年假  2.倒休 3.加班
            {
                Model.DayCount = dayCount;
            }
            else
            {
                Model.DayCount = -dayCount;
            }
            Model.startTime =Convert.ToDateTime(startTime);
            Model.endTime = Convert.ToDateTime(endTime);
            Model.Remark = remark;
            HolidaysBLL.UpdateHolidaysTable(Model);
            context.Response.Write("true");
        }
        catch (Exception ex)
        {
            context.Response.Write(HttpUtility.JavaScriptStringEncode(ex.Message));
        }
    }
    /// <summary>
    /// 修改员工职位信息
    /// </summary>
    /// <param name="context"></param>
    private void SetPosition(HttpContext context)
    {
        try
        {
            string val=context.Request["val"];
            int pId = Convert.ToInt32(context.Request["PosId"]);
            Position model = new PositionBLL().GetPositionEntity(pId);
            model.Name = val;
            new PositionBLL().UpdatePosition(model);
            context.Response.Write("true");
        }
        catch (Exception ex)
        {
            context.Response.Write(HttpUtility.JavaScriptStringEncode(ex.Message));
        }
        
    }
    /// <summary>
    /// 删除员工职位
    /// </summary>
    /// <param name="context"></param>
    private void DelPosition(HttpContext context)
    {
        try
        {
            string val = context.Request["val"];
            int pId = Convert.ToInt32(context.Request["PosId"]);
            new PositionBLL().DeletePosition(pId);
            context.Response.Write("true");
        }
        catch (Exception ex)
        {
            context.Response.Write(HttpUtility.JavaScriptStringEncode(ex.Message));
        }
    }
    /// <summary>
    /// 添加员工职位
    /// </summary>
    /// <param name="context"></param>
    private void AddPosition(HttpContext context)
    {
        try
        {
            string pName = context.Request["pName"];
            int count = new PositionBLL().GetPositionList().Where(c => c.Name == pName).ToList().Count();
            if (count>0)
            {
              context.Response.Write("same");
            }
            else
            {
                Position model = new Position();
                model.Name = pName;
                model.IsDel=1;
                new PositionBLL().AddPosition(model);
                context.Response.Write("true");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write(HttpUtility.JavaScriptStringEncode(ex.Message));
        }
    }
    /// <summary>
    /// 添加部门
    /// </summary>
    /// <param name="context"></param>
    private void AddDepartment(HttpContext context)
    {
        try
        {
            string DName = context.Request["DName"];
            int count = new DepartmentBLL().GetDepartmentList().Where(c => c.RoleName == DName).ToList().Count();
            if (count > 0)
            {
                context.Response.Write("same");
            }
            else
            {
                Department model = new Department();
                model.RoleName = DName;
                model.ParentId = 0;
                model.IsDel = 1;
                new DepartmentBLL().AddDepartment(model);
                context.Response.Write("true");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write(HttpUtility.JavaScriptStringEncode(ex.Message));
        }
    }
    /// <summary>
    /// 修改部门信息
    /// </summary>
    /// <param name="context"></param>
    private void SetDepartment(HttpContext context)
    {
        try
        {
            string val = context.Request["val"];
            int DId = Convert.ToInt32(context.Request["DId"]);
            Department model = new DepartmentBLL().GetDepartmentEntity(DId);
            model.RoleName = val;
            new DepartmentBLL().UpdateDepartment(model);
            context.Response.Write("true");
        }
        catch (Exception ex)
        {
            context.Response.Write(HttpUtility.JavaScriptStringEncode(ex.Message));
        }

    }
    /// <summary>
    /// 删除部门
    /// </summary>
    /// <param name="context"></param>
    private void DelDepartment(HttpContext context)
    {
        try
        {
            int DId = Convert.ToInt32(context.Request["DId"]);
            new DepartmentBLL().DeleteDepartment(DId);
            context.Response.Write("true");
        }
        catch (Exception ex)
        {
            context.Response.Write(HttpUtility.JavaScriptStringEncode(ex.Message));
        }
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}