<%@ WebHandler Language="C#" Class="UserUC" %>

using System;
using System.Web;
using System.Web.SessionState;
using BLL;
using System.Data;
using System.Text;
using Models;
using HYTD.Common;

public class UserUC : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (!PageBase.IsLogin())
        {
            context.Response.Write("sessionError");
            return;
        }
        PageBase.ClearClientPageCache();
        context.Response.Write(ResultStr(PageBase.GetLoginCode(), context));
    }

    private string ResultStr(string userCode, HttpContext context)
    {
        StringBuilder sb = new StringBuilder();
        UserInfoBLL userinfo = new UserInfoBLL();
        Model.TO.UserInfoTO to = userinfo.GetUserInfoByCode(userCode);
        if (to != null && to.UserID > 0)
        {
            string imgsrc = "/Image/headImg/" + Check.GetString(to.HeadImg);
            string img = HttpContext.Current.Server.MapPath("~/Image/headImg/" + Check.GetString(to.HeadImg));
            if (!System.IO.File.Exists(img))
                imgsrc = "/Image/headImg/default.jpg";

            sb.AppendFormat("<table class=\"tbl_user\">");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"td4\" rowspan=\"4\">");
            sb.AppendFormat("<img onclick='ChangeHeadImg()' src=\"{0}\" class=\"raw_image\" id=\"userDiv_headImg\"/></td>", imgsrc);
            sb.AppendFormat("<td class=\"userName\" id=\"userDiv_userName\" colspan=\"2\">{0}</td>", Check.GetString(to.UserName));
            sb.AppendFormat("</tr>");
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"Dep\">{0}</td>", Check.GetString(to.DepName));
            sb.AppendFormat("<td>{0}</td>", Check.GetString(to.PosiName));
            sb.AppendFormat("</tr>");
            #region 假期统计
            sb.AppendFormat("<tr>");
            double yearDay = Convert.ToDouble(new UserInfoBLL().GetUserByUserCode(to.UserCode).AnnualLeave);//年假
            DateTime entTime = Convert.ToDateTime(new UserInfoBLL().GetUserByUserCode(to.UserCode).EntyTime);
            TimeSpan ts = DateTime.Now - entTime;
            int day = ts.Days < 365 ? 0 : ts.Days;
            int numDay = 365 - day % 365;//剩余清理天数
            double swoppedDay = 0;//调休天数
            double workDay = 0;//加班天数
            DataTable dt = new HolidaysBLL().GetUserHolidays(to.UserCode);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
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
                        yearDay =yearDay+Convert.ToDouble(dr["DayCount"]);
                    }
                     
                }
            }
            //Say say = new SayBLL().GetSayEntityByPublishId(to.UserCode);
            //if (say != null && say.Id > 0)
            //{
            //    sb.AppendFormat("<td id=\"userDiv_Say\" colspan=\"2\">{0}</td>", PageBase.ShowExpressionHtml(PageBase.EscapeJs(PageBase.GetString(PageBase.EscapeJsN(say.Contents), 30, true))));
            //}
            //else
            //if (numDay < 100 & yearDay>0)
            //{
            //    sb.AppendFormat("<td id=\"userDiv_Say\" colspan=\"2\"> 当前共有<span class=\"Dep\">{0}</span>天假期，可用年假<span class=\"Dep\">{1}</span>天加班调休<span class=\"Dep\">{2}</span>天,年假将在<span class=\"Dep\">{3}</span>天后清零</td>", (yearDay + workDay + swoppedDay) < 0 ? 0 : yearDay + workDay + swoppedDay, yearDay < 0 ? 0 : yearDay, (workDay + swoppedDay) > 0 ? workDay + swoppedDay : 0,numDay);
            //}
            //else 
            //{
                sb.AppendFormat("<td id=\"userDiv_Say\" colspan=\"2\"> 当前共有<span class=\"Dep\">{0}</span>天假期，可用年假<span class=\"Dep\">{1}</span>天加班调休<span class=\"Dep\">{2}</span>天</td>", (yearDay + workDay + swoppedDay) < 0 ? 0 : yearDay + workDay + swoppedDay, yearDay < 0 ? 0 : yearDay, (workDay + swoppedDay) > 0 ? workDay + swoppedDay : 0);
            //}         
            sb.AppendFormat("</tr>");
            #endregion
            sb.AppendFormat("<tr>");
            sb.AppendFormat("<td class=\"write\" colspan=\"2\"></td>");
            sb.AppendFormat("</tr>");
            sb.AppendFormat("</table>");
        }
        return sb.ToString();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}