using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Configuration;
using System.Data;
using System.Text;

public partial class CallCenter_useracounte : System.Web.UI.Page
{
    HYTD.BLL.Call_CustomerBLL ccBll = new HYTD.BLL.Call_CustomerBLL();
    public Call_Customer cModel = new Call_Customer();
    HYTD.BLL.Call_WorkBillBLL WBll = new HYTD.BLL.Call_WorkBillBLL();
    protected int strCallInCounte = 0;
    protected int strmonthCallInCounte = 0;
    protected int strdateCallInCounte = 0;
    //按月份统计
    public StringBuilder sbNowMonth = new StringBuilder();//本月统计数据
    public StringBuilder sbPrevMonth = new StringBuilder();//上月统计数据
    public StringBuilder sbOldYearMonth = new StringBuilder();//去年本月统计数据
    public string strNowMonth = "";
    public string strPrevMonth = "";
    public string strOldYearMonth = "";
    //按年统计
    public StringBuilder sbNowYear = new StringBuilder();//今年数据
    public StringBuilder sbPrevYear = new StringBuilder();//去年数据
    public StringBuilder sbPrevPrevYear = new StringBuilder();//前年数据
    public string strNowYear = "";
    public string strPrevYear = "";
    public string strPrevPrevYear = "";
    //按时间统计
    public StringBuilder sbNowTime = new StringBuilder();//本月统计数据
    public StringBuilder sbPrevTime = new StringBuilder();//上月统计数据
    public StringBuilder sbPrevYearTime = new StringBuilder();//去年本月统计数据
    public string strNowTime = "";
    public string strPrevTime = "";
    public string strPrevYearTime = "";


    protected string strMonthX = string.Empty;
    protected string strYearX = string.Empty;
    protected string strTimeX = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo model = new PageBase().CurrentUserInfo;
        UserRights1.intPage = 10;
        if (ConfigurationManager.AppSettings["Users"] != null)
        {
            string[] strlist = ConfigurationManager.AppSettings["Users"].ToString().Trim().Split(',');
            if (strlist.Where(c => c == model.UserCode.ToString()).ToList().Count > 0)
            {

            }
            else
            {
                Response.Write("<script>alert('您无权操作此页面！');window.location.href='/Index.aspx';</script>");
                this.Response.End();
            }
        }
        else
        {
            Response.Write("<script>alert('您无权操作此页面！');window.location.href='/Index.aspx';</script>");
            this.Response.End();
        }
        //if (Request["cid"] != null && Request["cid"].ToString() != "")
        //{
        //    int.TryParse(Request["cid"].ToString(), out CID);
        getDateModel(model.UserCode, model.UserName);
        //}
    }


    private void getDateModel(string intCID, string strUserName)
    {
        //获取工单总工单信息
        DataSet dsall = WBll.getInfoForCounteByALL(intCID);
        if (dsall != null && dsall.Tables.Count > 0)
        {
            ltlTotalCount.Text = dsall.Tables[0].Rows[0][0].ToString();
            ltlMonthCount.Text = dsall.Tables[0].Rows[0][1].ToString();
            ltlDateCount.Text = dsall.Tables[0].Rows[0][2].ToString();
        }
        //获取当前用户的工单信息   
        DataSet dsuser = WBll.getInfoForCounteByUser(intCID);
        StringBuilder sb = new StringBuilder();
        StringBuilder sbUser = new StringBuilder();
        if (dsuser != null && dsuser.Tables.Count > 0)
        {
            string str = "<font class='xingming'>{0}</font>工单总数为：<font class='shuzi'>{1}</font>；本月工单总数：<font class='shuzi'>{2}</font>；本日工单总数：<font class='shuzi'>{3}</font>；未解决工单总数：<font class='shuzihongse'>{4}</font>";
            sb.AppendFormat(str, strUserName, dsuser.Tables[0].Rows[0][0].ToString(), dsuser.Tables[0].Rows[0][1].ToString(),
                dsuser.Tables[0].Rows[0][2].ToString(), dsuser.Tables[0].Rows[0][3].ToString());
            for (int i = 0; i < dsuser.Tables[1].Rows.Count; i++)
            {
                sbUser.AppendFormat("{0}：<font class='shuzi'>{1}</font>  ", dsuser.Tables[1].Rows[i][0].ToString(), dsuser.Tables[1].Rows[i][1].ToString());
            }
        }
        ltlUserCount.Text = sb.ToString();
        ltlUserWorkBillCount.Text = sbUser.ToString();


        #region //按月生成报表，查看本月、上一月、去年本月份的统计数据
        DataSet dsMonth = WBll.getInfoForCounteByMonth(DateTime.Today);
        strMonthX = "{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31}";
        //去年本月数据
        strOldYearMonth = "去年本月";//DateTime.Today.AddYears(-1).ToString("yyyy-MM");
        if (dsMonth != null && dsMonth.Tables.Count > 0)
        {
            for (int i = 0; i < dsMonth.Tables[0].Rows.Count; i++)
            {
                if (i > 0)
                    sbOldYearMonth.Append(",");
                sbOldYearMonth.Append(dsMonth.Tables[0].Rows[i][1].ToString());
            }
        }
        //本月数据
        strNowMonth = "本月";//DateTime.Today.ToString("yyyy-MM");
        if (dsMonth != null && dsMonth.Tables.Count > 1)
        {
            for (int i = 0; i < dsMonth.Tables[1].Rows.Count; i++)
            {
                if (i > 0)
                    sbNowMonth.Append(",");
                sbNowMonth.Append(dsMonth.Tables[1].Rows[i][1].ToString());
            }
        }
        //上月数据
        strPrevMonth = "上个月";// DateTime.Today.AddMonths(-1).ToString("yyyy-MM");
        if (dsMonth != null && dsMonth.Tables.Count > 2)
        {
            for (int i = 0; i < dsMonth.Tables[2].Rows.Count; i++)
            {
                if (i > 0)
                    sbPrevMonth.Append(",");
                sbPrevMonth.Append(dsMonth.Tables[2].Rows[i][1].ToString());
            }
        }
        #endregion

        #region 按年份统计
        DataSet dsYear = WBll.getInfoForCounteBySelectedYear(DateTime.Today);
        strYearX = "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}";
        //今年本月数据
        strNowYear = DateTime.Today.ToString("yyyy");
        if (dsYear != null && dsYear.Tables.Count > 0)
        {
            for (int i = 0; i < dsYear.Tables[0].Rows.Count; i++)
            {
                if (i > 0)
                    sbNowYear.Append(",");
                sbNowYear.Append(dsYear.Tables[0].Rows[i][1].ToString());
            }
        }
        //去年
        strPrevYear = DateTime.Today.AddYears(-1).ToString("yyyy");
        if (dsYear != null && dsYear.Tables.Count > 1)
        {
            for (int i = 0; i < dsYear.Tables[1].Rows.Count; i++)
            {
                if (i > 0)
                    sbPrevYear.Append(",");
                sbPrevYear.Append(dsYear.Tables[1].Rows[i][1].ToString());
            }
        }
        // 前年
        strPrevPrevYear = DateTime.Today.AddYears(-2).ToString("yyyy");
        if (dsYear != null && dsYear.Tables.Count > 2)
        {
            for (int i = 0; i < dsYear.Tables[2].Rows.Count; i++)
            {
                if (i > 0)
                    sbPrevPrevYear.Append(",");
                sbPrevPrevYear.Append(dsYear.Tables[2].Rows[i][1].ToString());
            }
        }
        #endregion


        #region 按时间段统计


        DataSet dsYeartime = WBll.getInfoForCounteBySelectedYearTime(DateTime.Today);
        strYearX = "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23}";
        //今年本月数据
        strNowTime = DateTime.Today.ToString("yyyy");
        if (dsYeartime != null && dsYeartime.Tables.Count > 0)
        {
            for (int i = 0; i < dsYeartime.Tables[0].Rows.Count; i++)
            {
                if (i > 0)
                    sbNowTime.Append(",");
                sbNowTime.Append(dsYeartime.Tables[0].Rows[i][1].ToString());
            }
        }
        //去年
        strPrevTime = DateTime.Today.AddYears(-1).ToString("yyyy");
        if (dsYeartime != null && dsYeartime.Tables.Count > 1)
        {
            for (int i = 0; i < dsYeartime.Tables[1].Rows.Count; i++)
            {
                if (i > 0)
                    sbPrevTime.Append(",");
                sbPrevTime.Append(dsYeartime.Tables[1].Rows[i][1].ToString());
            }
        }
        // 前年
        strPrevYearTime = DateTime.Today.AddYears(-2).ToString("yyyy");
        if (dsYeartime != null && dsYeartime.Tables.Count > 2)
        {
            for (int i = 0; i < dsYeartime.Tables[2].Rows.Count; i++)
            {
                if (i > 0)
                    sbPrevYearTime.Append(",");
                sbPrevYearTime.Append(dsYeartime.Tables[2].Rows[i][1].ToString());
            }
        }
        #endregion


    }
}