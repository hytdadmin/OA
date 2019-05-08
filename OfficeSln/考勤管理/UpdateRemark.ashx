<%@ WebHandler Language="C#" Class="UpdateRemark" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Models;
using System.Web.SessionState;
using System.Linq;
using BLL;
using Model;
public class UpdateRemark : IHttpHandler,IRequiresSessionState
{

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string hidRemark = context.Request["remark"];
        string hidDate = context.Request["date"];
        if (!string.IsNullOrEmpty(hidRemark)&&!string.IsNullOrEmpty(hidDate))
        {
            updateRemark(hidRemark, hidDate, context);
            context.Response.Write("0");
        }
        else
        {
            updateRemark(hidRemark, hidDate, context);
            context.Response.Write("1");
        }
        
    }
    /// <summary>
    /// 修改备注
    /// </summary>
    /// <param name="remark"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    private void updateRemark(string remark, string date,HttpContext context)
    {
        //判断这一天是否打卡了
        PageBase basePage = new PageBase();
        AttendanceListBLL bll = new AttendanceListBLL();
        string userName = context.Request["userName"];
        if (basePage.CurrentUserInfo!=null)
        {

            System.Collections.Generic.List<UserInfo> list = new UserInfoBLL().GetUserInfoList().Where(c => c.UserName == userName).ToList();
            string UserCode = list[0].UserCode;
            string sqlCard = "select count(*) from AttendanceRemark where WorkDay=@CardDate and UserCode=@userCode";
            SqlParameter[] spCard = { new SqlParameter("@userCode",UserCode),
                                    new SqlParameter("@CardDate",date)};
           int count=Convert.ToInt32(SqlHelper.GetSingle(sqlCard, spCard));
            //bll.GetAttendanceListList().Where(c => c.WorkDay==date && c.UserCode == UserCode).Count()> 0
           if (count>0)
            {
                string sql = "update AttendanceRemark set Remark=@remark where UserCode=@userCode and WorkDay=@date";
                SqlParameter[] sp ={new SqlParameter("@remark",remark),
                         new SqlParameter("@userCode",UserCode),
                         new SqlParameter("@date",date)};
                SqlHelper.ExecuteSql(sql, sp);
            }
            else
            {
                string insertSql = "insert into AttendanceRemark(UserCode, WorkDay, Remark) values(@userCode,@workDay,@Remark)";
                SqlParameter[] sparms = {new SqlParameter("@userCode",UserCode),
                                    new SqlParameter("@workDay",date),
                                    new SqlParameter("@Remark",remark)};
                SqlHelper.ExecuteSql(insertSql, sparms);
            }
        }
       

    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}