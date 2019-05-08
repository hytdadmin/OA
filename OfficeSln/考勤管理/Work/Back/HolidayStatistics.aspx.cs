using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using HYTD.Common;
using Models.TO;
using System.Text;

public partial class Work_Back_HolidayStatistics : System.Web.UI.Page
{
    int pageIndex = 1;
    int pageSize = 10;
    int rowCount = 0;
    public string holidayList = "";
    public string userCode ="";
    protected void Page_Load(object sender, EventArgs e)
    {
        StatisHoliday();
    }

    private void StatisHoliday()
    {
        #region 假期统计
        double yearDay = 0;//年假
        double swoppedDay = 0;//调休天数
        double workDay = 0;//加班天数
        double yearLeave = 0;
        HolidaysTO to = new HolidaysTO();
        string orderBy = " UserName ";
        if (!string.IsNullOrEmpty(Request.Form["slectCode"]))
        {
            userCode = to.UserCode = Request.Form["slectCode"];
        }
        DataTable dt = new HolidaysBLL().GetHolidaysStatisInfor(to, AspNetPager1.CurrentPageIndex, pageSize, orderBy, out rowCount);
        StringBuilder sb = new StringBuilder();
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                yearDay = Convert.ToDouble(dr["AnnualLeave"]);
                //1年假 2 倒休 3加班
               swoppedDay = Convert.ToDouble(dr["daoxiu"]);

               workDay = Convert.ToDouble(dr["jiaban"]);

               yearLeave = Convert.ToDouble(dr["nianjia"]);

                sb.Append("<tr>");
                sb.AppendFormat("<td align='center'>{0}</td>", i + 1 * (pageSize * (pageIndex - 1) + 1));
                sb.AppendFormat("<td align='center'>{0}</td>", dr["UserName"]);
                sb.AppendFormat("<td align='center'>{0}</td>", Convert.ToDouble(dr["AnnualLeave"]));
                sb.AppendFormat("<td align='center'>{0}</td>", workDay);
                sb.AppendFormat("<td align='center'>{0}</td>", swoppedDay * -1);
                sb.AppendFormat("<td align='center'>{0}</td>", yearLeave < 0 ? yearLeave*-1 : yearLeave);
                sb.AppendFormat("<td align='center'>{0}</td>", (yearDay + workDay + swoppedDay + yearLeave) < 0 ? 0 : yearDay + workDay + swoppedDay + yearLeave);
            }
        }
        holidayList = sb.ToString();
        AspNetPager1.RecordCount = rowCount;
        AspNetPager1.PageSize = pageSize;
        #endregion
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        StatisHoliday();
    }
}