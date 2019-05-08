<%@ WebHandler Language="C#" Class="HolidayMag" %>

using System;
using System.Web;
using BLL;
using Model;

public class HolidayMag : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string op = context.Request["op"];
        if (op == "delHoliday")
        {
            DeleteHoliday(context);
        }
    }
    /// <summary>
    /// 删除假期
    /// </summary>
    /// <param name="ctx"></param>
    private void DeleteHoliday(HttpContext ctx)
    {
        try
        {
            int id = Convert.ToInt32(ctx.Request["hodId"]);
            new HolidaysBLL().DeleteHolidaysTable(id);
            ctx.Response.Write("true");
        }
        catch (Exception ex)
        {
            ctx.Response.Write(HttpUtility.JavaScriptStringEncode(ex.Message));
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