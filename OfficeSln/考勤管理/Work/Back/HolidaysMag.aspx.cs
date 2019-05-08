using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BLL;

public partial class Work_Back_HolidaysMag : System.Web.UI.Page
{
    
    public HolidaysTable entity = null;
    public string startTime = "";
    public string endTime = "";
    public string userCode ="";
    public string day = "";
    public int typeID = 0;
    public string remark = "";
    public int holidayID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string op = Request.QueryString["op"];
        if (op == "update")
        {
            UpdateHolidays();
        }
    }
    /// <summary>
    /// 修改假期
    /// </summary>
    private void UpdateHolidays()
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);
        entity = new HolidaysBLL().GetHolidaysTableEntity(id);
        holidayID = id;
        userCode = entity.UserCode;
        startTime = entity.startTime.ToString("yyyy-MM-dd HH:mm:ss");
        endTime = entity.endTime.ToString("yyyy-MM-dd HH:mm:ss");
        day = entity.DayCount < 0?(entity.DayCount*-1).ToString():entity.DayCount.ToString();
        typeID = Convert.ToInt32(entity.HolidaysType);
        remark = entity.Remark;
    }
}