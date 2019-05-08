using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BLL;
using Models;
using System.Configuration;
using System.Web.Configuration;
using HYTD.Common;

public partial class Index : PageBase
{
    protected string DateList = string.Empty;
    public DataTable dtShow = new DataTable();
    protected string UserName = string.Empty;
    protected string UserCode = string.Empty;
    public string InputData = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsLogin())
        {
            UserCode = CurrentUserInfo.UserCode;
            GetName();
        }

        if (!IsPostBack)
        {
            txtMonth.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
            dtShow = TimeList(DateTime.Parse(txtMonth.Text).ToString("yyyyMM"), UserCode);
            string[] strs = txtMonth.Text.Split('-');
            string year = strs[0];
            string month = strs[1];
            //加载人名
            PeopleList();
            //显示日期列表
            ShowList(int.Parse(year), int.Parse(month));
            getData();
        }

    }
    ///// <summary>
    ///// 修改备注
    ///// </summary>
    ///// <param name="remark"></param>
    ///// <param name="date"></param>
    ///// <returns></returns>
    //private void updateRemark(string remark, string date)
    //{
    //    //判断这一天是否打卡了
    //    string sqlCard = "select count(*) from AttendanceList where WorkDay=@CardDate and UserCode=@userCode";
    //    SqlParameter[] spCard = { new SqlParameter("@userCode",UserCode),
    //                                new SqlParameter("@CardDate",date)};
    //    if (Convert.ToInt32(SqlHelper.GetSingle(sqlCard, spCard)) > 0)
    //    {
    //        string sql = "update AttendanceRemark set Remark=@remark where UserCode=@userCode and WorkDay=@date";
    //        SqlParameter[] sp ={new SqlParameter("@remark",remark),
    //                     new SqlParameter("@userCode",UserCode),
    //                     new SqlParameter("@date",date)};
    //        SqlHelper.ExecuteSql(sql, sp);
    //    }
    //    else
    //    {
    //        string insertSql = "insert into AttendanceRemark(UserCode, WorkDay, Remark) values(@userCode,@workDay,@Remark)";
    //        SqlParameter[] sparms = {new SqlParameter("@userCode",UserCode),
    //                                new SqlParameter("@workDay",date),
    //                                new SqlParameter("@Remark",remark)};
    //        SqlHelper.ExecuteSql(insertSql, sparms);
    //    }

    //}
    //加载人名
    private void GetName()
    {
        //string sql = "select UserName from UserInfo where UserCode=@UserCode;";
        //SqlParameter sp = new SqlParameter("@UserCode", SqlDbType.VarChar);
        //sp.Value = UserCode;
        //UserName = SqlHelper.GetSingle(sql, sp).ToString().Trim();
        UserName = CurrentUserInfo.UserName;
        if (!string.IsNullOrEmpty(CurrentUserInfo.IsAdmin.ToString()))
        {
            int isAdmin = Convert.ToInt32(CurrentUserInfo.IsAdmin);
            if (isAdmin == 1)
            {
                InputData = @"<input type='button'  value='导入数据' onclick='Input_Click()' style='width:100px'/>";
            }
        }

    }
    /* 检验该年是否为闰年 */
    bool Leap(int year)
    {
        if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
            return true;
        else
            return false;
    }
    /// <summary>
    /// 判断当前年月的第一天是星期几
    /// </summary>
    /// <param name="y"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    int Day(int y, int m)
    {
        int i, n = 1;
        /* 计算该年份（如2010年）1月1日是星期几 */
        for (i = 1; i < y; i++)
        {
            if (Leap(i))
                n = (n + 366) % 7;
            else
                n = (n + 365) % 7;
        }
        /* 计算该月份1日是星期几 */
        for (i = 1; i < m; i++)
        {
            switch (i)
            {
                case 2:
                    if (Leap(y)) n = (n + 29) % 7;
                    else n = (n + 28) % 7; break;
                case 4:
                case 6:
                case 9:
                case 11:
                    n = (n + 30) % 7; break;
                default:
                    n = (n + 31) % 7; break;
            }

        }
        ///* 最后，计算这一天是星期几 */
        //n = (n + d - 1) % 7;
        return n;
    }
    List<int> list = new List<int>();
    void ShowList(int year, int Month)
    {
        //修改备注权限
        string strUser = ConfigurationManager.AppSettings["UserCode"];
        string[] strs = strUser.Split(',');
        List<string> strList = new List<string>(strs);
        //判断当前月是多少天
        int cont = DayCount(year, Month);
        int day = Day(year, Month);
        DateTime dtime;//上午打卡
        DateTime etime;//下午打卡
        string earlTime = " 08:31";//早上上班时间
        string lastTime = " 17:30";//晚上下班时间
        string FlexSTime = " 09:16";//弹性开始工作时间
        DateTime late_time=DateTime.Now;//早上迟到时间后下午应晚走时间
        DateTime ModElastic_time = Convert.ToDateTime("2014-12-15");//开始执行弹性时间规定的日期
        TimeSpan tp_elastic=new TimeSpan();//弹性时间制度分隔日期
        if (day == 0)//0表示的是星期日
        {
            day = 7;
        }
        StringBuilder sb = new StringBuilder();
        for (int i = 1; i <= cont + day - 1; i++)
        {
            //09:00 - 18:00
            string strTime = "";
            string startTime = string.Empty;
            string remark = string.Empty;//备注
            string strDate = string.Format("{0}{1}{2}", year, Month.ToString().PadLeft(2, '0'), (i - day + 1).ToString().PadLeft(2, '0'));
            DataRow[] dr = dtShow.Select("WorkDay=" + strDate);
            //周六周日
            if (i == 6 || i == 7 || i == 13 || i == 14 || i == 20 || i == 21 || i == 27 || i == 28 || i == 34 || i == 35)
            {
                list.Add(i);
            }
            //打卡记录
            if (dr.Length > 0)
            {
                DateTime.TryParse(dr[0][1].ToString(), out dtime);
                DateTime.TryParse(dr[0][0].ToString(), out etime);
                strTime = CardShow(year, Month, day, i, strTime, dr);
                //if (strTime == "" && dr.Length > 1)
                //{
                //    dr[1][3] = dr[0][3];
                //    DateTime.TryParse(dr[1][1].ToString(), out dtime);
                //    DateTime.TryParse(dr[1][0].ToString(), out etime);
                //    strTime = CardShow(year, Month, day, i, strTime, dr);
                //}

                remark = dr[0][3].ToString();
                startTime = string.Format("{0}-{1}-{2}", year, Month.ToString().PadLeft(2, '0'), (i - day + 1).ToString().PadLeft(2, '0'));
                string adjust = ConfigurationManager.AppSettings["adjust"];//考勤调整时间
                string newAdjust = ConfigurationManager.AppSettings["newAdjust"];//二次考勤修改时间
               // TimeSpan ntp = Convert.ToDateTime(startTime) - Convert.ToDateTime(newAdjust);
                TimeSpan tp = Convert.ToDateTime(startTime) - Convert.ToDateTime(adjust);
                if (tp.TotalDays >= 0 && Convert.ToDateTime(startTime) < Convert.ToDateTime(newAdjust))
                {
                    earlTime = " 09:01";
                    lastTime = " 18:00";
                }
                else 
                {
                    earlTime = " 08:31";
                    lastTime = " 17:30";
                    FlexSTime = " 08:46";
                   // ModElastic_time = Convert.ToDateTime("2015-06-15");//二次开始执行弹性时间规定的日期
                }
                tp_elastic = Convert.ToDateTime(startTime) - Convert.ToDateTime(ModElastic_time);//弹性制度分割日期
            }
            else
            {
                dtime = DateTime.Parse("00:00");
                etime = DateTime.Parse("23:00");
            }

            //空白的天数（不是当前月的天数）
            if (i < day)
            {
                sb.Append("<td>");
                sb.Append("<div class='timecard_form'><span class='reason'></span><span class='data nodis'></span></div>");
                sb.Append("<div class='time'></div>");
                sb.Append("<div class='info'></div>");
                sb.Append("<div class='edit'><a href='#'></a></div>");
                sb.Append("</td>");
            }
            else //当前月的天数开始位置
            {
                //判断早上迟到的时间
                if (dtime>=DateTime.Parse(startTime + earlTime)&&dtime < DateTime.Parse(startTime + FlexSTime))
                {
                    TimeSpan tspan =dtime-DateTime.Parse(startTime + earlTime);//早上迟到的时间
                    late_time = DateTime.Parse(startTime + lastTime).AddMinutes(tspan.Minutes);
                }
                //换行 周一
                if (i == 8 || i == 15 || i == 22 || i == 29 || i == 36)
                {
                    sb.Append("<tr><td>");
                    sb.AppendFormat("<div class='timecard_form'><span class='reason'></span><span class='data'>{0}</span></div>", i - day + 1);
                    if (tp_elastic.TotalDays >=0)//弹性时间制度分隔日期
                    {
                        //判断时间是否>9：00且<18：00 为迟到  <9:00 
                        if (dtime < DateTime.Parse(startTime + earlTime) && strTime.Length != 0 && etime > DateTime.Parse(startTime + lastTime))
                        {
                            #region
                            //if (!list.Contains(i))//周六日不标识出来
                            //{
                            //    sb.Append("<div class='time on'>" + strTime + "</div>");
                            //    if (!string.IsNullOrEmpty(remark))//备注不为空
                            //    {
                            //        //是否是当前登录人（当前登录人可以添加自己备注说明）
                            //        if (DropDownList1.SelectedValue == CurrentUserInfo.UserName)
                            //        {
                            //            sb.Append("<div class='info'><label style='display:none;'>" + strDate + "</label><span class='dvSpan' style='cursor:pointer;color:#CC0000;'>" + remark + "</span></div>");
                            //        }
                            //        else
                            //        {
                            //            sb.Append("<div class='info'><span  style='color:#CC0000;'>" + remark + "</span></div>");
                            //        }
                            //    }
                            //    else//之前没有添加备注
                            //    {
                            //        if (DropDownList1.SelectedValue == CurrentUserInfo.UserName)
                            //        {
                            //            sb.Append("<div class='info'><label style='display:none;'>" + strDate + "</label><span class='dvSpan' style='cursor:pointer'>备注说明</span></div>");
                            //        }
                            //        else
                            //        {
                            //            sb.Append("<div class='info'></div>");
                            //        }
                            //    }

                            //  }
                            #endregion
                            NotLastDate(sb, i, strTime, remark, strDate, strList, (int)PublicEnum.AttendanceElasticType.NoElastic);//没有迟到
                        }
                        else if (DateTime.Parse(startTime + earlTime) <= dtime && dtime < DateTime.Parse(startTime + FlexSTime) && strTime.Length != 0 && etime > late_time)
                        {                                               //9:16
                            NotLastDate(sb, i, strTime, remark, strDate, strList, (int)PublicEnum.AttendanceElasticType.elastic);//没有迟到(弹性时间)                       
                        }
                        else //迟到
                        {
                            //sb.Append("<div class='time'>" + strTime + "</div>");
                            //sb.Append("<div class='info'></div>");
                            LastWork(sb, i, strTime, remark, strDate, strList);//迟到的
                        }
                    }
                    else
                    {
                          //判断时间是否>9：00且<18：00 为迟到
                        if (dtime>=DateTime.Parse(startTime + earlTime)||strTime.Length== 0||etime<DateTime.Parse(startTime + lastTime))
                        {
                            LastWork(sb, i, strTime, remark, strDate, strList);//迟到的
                        }
                        else
                        {
                            NotLastDate(sb, i, strTime, remark, strDate, strList, (int)PublicEnum.AttendanceElasticType.NoElastic);//没有迟到
                        }
                    }
                    sb.Append("<div class='edit'><a href='#'></a></div>");
                    sb.Append("</td>");
                }
                else
                {
                    //考勤表一行结束  周日
                    if (i == 14 || i == 21 || i == 28 || i == 35)
                    {
                        sb.Append("<td>");
                        sb.AppendFormat("<div class='timecard_form'><span class='reason'></span><span class='data'>{0}</span></div>", i - day + 1);
                        if (tp_elastic.TotalDays >= 0)//弹性时间制度分隔日期
                        {
                            if (dtime < DateTime.Parse(startTime + earlTime) && strTime.Length != 0 && etime > DateTime.Parse(startTime + lastTime))
                            {
                                #region
                                //if (!list.Contains(i))//是否包含周六日
                                //{
                                //    sb.Append("<div class='time on'>" + strTime + "</div>");
                                //}
                                //else
                                //{
                                //    sb.Append("<div class='time'>" + strTime + "</div>");
                                //    if (!string.IsNullOrEmpty(remark))
                                //    {
                                //        if (DropDownList1.SelectedValue == CurrentUserInfo.UserName)
                                //        {
                                //            sb.Append("<div class='info'><label style='display:none;'>" + strDate + "</label><span class='dvSpan' style='cursor:pointer;color:#CC0000;'>" + remark + "</span></div>");
                                //        }
                                //        else
                                //        {
                                //            sb.Append("<div class='info'><span  style='color:#CC0000;'>" + remark + "</span></div>");
                                //        }
                                //    }
                                //    else
                                //    {
                                //        if (DropDownList1.SelectedValue == CurrentUserInfo.UserName)
                                //        {
                                //            sb.Append("<div class='info'><label style='display:none;'>" + strDate + "</label><span class='dvSpan' style='cursor:pointer'>备注说明</span></div>");
                                //        }
                                //        else
                                //        {
                                //            sb.Append("<div class='info'></div>");
                                //        }
                                //    }
                                //}
                                #endregion

                                NotLastDate(sb, i, strTime, remark, strDate, strList, (int)PublicEnum.AttendanceElasticType.NoElastic);//没有迟到
                            }
                            else if (DateTime.Parse(startTime + earlTime) <= dtime && dtime < DateTime.Parse(startTime + FlexSTime) && strTime.Length != 0 && etime > late_time)
                            {                                               //9:16
                                NotLastDate(sb, i, strTime, remark, strDate, strList, (int)PublicEnum.AttendanceElasticType.elastic);//没有迟到(弹性时间)
                            }
                            else//没有迟到的
                            {
                                #region
                                //sb.Append("<div class='time'>" + strTime + "</div>");
                                //if (!string.IsNullOrEmpty(remark))
                                //{
                                //    if (DropDownList1.SelectedValue == CurrentUserInfo.UserName)
                                //    {
                                //        sb.Append("<div class='info'><label style='display:none;'>" + strDate + "</label><span class='dvSpan' style='cursor:pointer;color:#CC0000;'>" + remark + "</span></div>");
                                //    }
                                //    else
                                //    {
                                //        sb.Append("<div class='info'><span  style='color:#CC0000;'>" + remark + "</span></div>");
                                //    }
                                //}
                                //else
                                //{
                                //    if (DropDownList1.SelectedValue == CurrentUserInfo.UserName)
                                //    {
                                //        sb.Append("<div class='info'><label style='display:none;'>" + strDate + "</label><span class='dvSpan' style='cursor:pointer'>备注说明</span></div>");
                                //    }
                                //    else
                                //    {
                                //        sb.Append("<div class='info'></div>");
                                //    }
                                //}
                                #endregion

                                LastWork(sb, i, strTime, remark, strDate, strList);//c迟到
                            }
                        }
                        else
                        {
                            //判断时间是否>9：00且<18：00 为迟到
                            if (dtime >= DateTime.Parse(startTime + earlTime) || strTime.Length == 0 || etime < DateTime.Parse(startTime + lastTime))
                            {
                                LastWork(sb, i, strTime, remark, strDate, strList);//迟到的
                            }
                            else
                            {
                                NotLastDate(sb, i, strTime, remark, strDate, strList, (int)PublicEnum.AttendanceElasticType.NoElastic);//没有迟到
                            }
                        }
                        sb.Append("<div class='edit'><a href='#'></a></div>");
                        sb.Append("</td></tr>");
                    }
                    else
                    {
                        if (i == 32)
                        {
                            string ins = i.ToString();
                        }
                        sb.Append("<td>");
                        sb.AppendFormat("<div class='timecard_form'><span class='reason'></span><span class='data'>{0}</span></div>", i - day + 1);
                        if (tp_elastic.TotalDays >= 0)//弹性时间制度分隔日期
                        {
                            if (dtime < DateTime.Parse(startTime + earlTime) && strTime.Length != 0 && etime > DateTime.Parse(startTime + lastTime))
                            {
                                NotLastDate(sb, i, strTime, remark, strDate, strList, (int)PublicEnum.AttendanceElasticType.NoElastic);//没有迟到
                            }
                            else if (DateTime.Parse(startTime + earlTime) <= dtime && dtime < DateTime.Parse(startTime + FlexSTime) && strTime.Length != 0 && etime > late_time)
                            {
                                NotLastDate(sb, i, strTime, remark, strDate, strList, (int)PublicEnum.AttendanceElasticType.elastic);//没有迟到(弹性时间)
                            }
                            else  //除掉开头和结束 剩下没有迟到的
                            {
                                LastWork(sb, i, strTime, remark, strDate, strList);//迟到
                            }
                        }
                        else
                        {
                            //判断时间是否>9：00且<18：00 为迟到
                            if (dtime >= DateTime.Parse(startTime + earlTime) || strTime.Length == 0 || etime < DateTime.Parse(startTime + lastTime))
                            {
                                LastWork(sb, i, strTime, remark, strDate, strList);//迟到的
                            }
                            else
                            {
                                NotLastDate(sb, i, strTime, remark, strDate, strList, (int)PublicEnum.AttendanceElasticType.NoElastic);//没有迟到
                            }
                        }
                        sb.Append("<div class='edit'><a href='#'></a></div>");
                        sb.Append("</td>");
                    }

                }

            }
        }
        DateList = sb.ToString();
    }
    /// <summary>
    /// 上班迟到
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="i"></param>
    /// <param name="strTime"></param>
    /// <param name="remark"></param>
    /// <param name="strDate"></param>
    private void LastWork(StringBuilder sb, int i, string strTime, string remark, string strDate, List<string> strList)
    {

        if (!list.Contains(i))//是否包含周六日
        {
            sb.Append("<div class='time on'>" + strTime + "</div>");
            if (!string.IsNullOrEmpty(remark))
            {
                if (DropDownList1.SelectedValue == CurrentUserInfo.UserName || strList.Contains(CurrentUserInfo.UserCode))
                {
                    sb.Append("<div class='info'><label style='display:none;'>" + strDate + "-" + DropDownList1.SelectedValue + "</label><span class='dvSpan' style='cursor:pointer;color:#CC0000;'>" + remark + "</span></div>");
                }
                else
                {
                    sb.Append("<div class='info'><span  style='color:#CC0000;'>" + remark + "</span></div>");
                }
            }
            else
            {
                if (DropDownList1.SelectedValue == CurrentUserInfo.UserName || strList.Contains(CurrentUserInfo.UserCode))
                {
                    sb.Append("<div class='info'><label style='display:none;'>" + strDate + "-" + DropDownList1.SelectedValue + "</label><span class='dvSpan' style='cursor:pointer'>备注说明</span></div>");
                }
                else
                {
                    sb.Append("<div class='info'></div>");
                }
            }
        }
        else//周六、周日
        {
            sb.Append("<div class='time'>" + strTime + "</div>");
            if (!string.IsNullOrEmpty(remark))
            {
                if (DropDownList1.SelectedValue == CurrentUserInfo.UserName || strList.Contains(CurrentUserInfo.UserCode))
                {
                    sb.Append("<div class='info'><label style='display:none;'>" + strDate + "-" + DropDownList1.SelectedValue + "</label><span class='dvSpan' style='cursor:pointer;color:#CC0000;'>" + remark + "</span></div>");
                }
                else
                {
                    sb.Append("<div class='info'><span  style='color:#CC0000;'>" + remark + "</span></div>");
                }
            }
            else
            {
                if (DropDownList1.SelectedValue == CurrentUserInfo.UserName || strList.Contains(CurrentUserInfo.UserCode))
                {
                    sb.Append("<div class='info'><label style='display:none;'>" + strDate + "-" + DropDownList1.SelectedValue + "</label><span class='dvSpan' style='cursor:pointer'>备注说明</span></div>");
                }
                else
                {
                    sb.Append("<div class='info'></div>");
                }
            }
        }
    }

    /// <summary>
    /// 没有迟到的
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="i"></param>
    /// <param name="strTime"></param>
    /// <param name="remark"></param>
    /// <param name="strDate"></param>
    private void NotLastDate(StringBuilder sb, int i, string strTime, string remark, string strDate, List<string> strList,int type)
    {
        if (!list.Contains(i))//是不是周六日
        {
            if (type ==(int)PublicEnum.AttendanceElasticType.elastic)
            {
                sb.Append("<div class='time off'>" + strTime + "</div>");
            }
            else {
                sb.Append("<div class='time'>" + strTime + "</div>");
            }
            //if (!string.IsNullOrEmpty(remark))
            //{
            //    if (DropDownList1.SelectedValue == CurrentUserInfo.UserName || strList.Contains(CurrentUserInfo.UserCode))
            //    {
            //        sb.Append("<div class='info'><label style='display:none;'>" + strDate + "-" + DropDownList1.SelectedValue + "</label><span class='dvSpan' style='cursor:pointer;color:#CC0000;'>" + remark + "</span></div>");
            //    }
            //    else
            //    {
            //        sb.Append("<div class='info'><span  style='color:#CC0000;'>" + remark + "</span></div>");
            //    }
            //}
        }
        else//周六、周日
        {
            sb.Append("<div class='time'>" + strTime + "</div>");
            if (!string.IsNullOrEmpty(remark))
            {
                if (DropDownList1.SelectedValue == CurrentUserInfo.UserName || strList.Contains(CurrentUserInfo.UserCode))
                {
                    sb.Append("<div class='info'><label style='display:none;'>" + strDate + "-" + DropDownList1.SelectedValue + "</label><span class='dvSpan' style='cursor:pointer;color:#CC0000;'>" + remark + "</span></div>");
                }
                else
                {
                    sb.Append("<div class='info'><span  style='color:#CC0000;'>" + remark + "</span></div>");
                }
            }
            else
            {
                if (DropDownList1.SelectedValue == CurrentUserInfo.UserName || strList.Contains(CurrentUserInfo.UserCode))
                {
                    sb.Append("<div class='info'><label style='display:none;'>" + strDate + "-" + DropDownList1.SelectedValue + "</label><span class='dvSpan' style='cursor:pointer'>备注说明</span></div>");
                }
                else
                {
                    sb.Append("<div class='info'></div>");
                }
            }
        }
    }

    //打卡说明
    private static string CardShow(int year, int Month, int day, int i, string strTime, DataRow[] dr)
    {
        #region MyRegion
        //DateTime dtime;
        //string strSW = "上午";
        //string strXW = "下午";
        //DateTime.TryParse(dr[0][1].ToString(), out dtime);
        //string strDateNew = string.Format("{0}-{1}-{2}", year, Month.ToString().PadLeft(2, '0'), (i - day + 1).ToString().PadLeft(2, '0'));
        ////有重复的天数
        //if (dr.Length > 1)
        //{
        //    if (!(dr[1][1] is DBNull))
        //    {
        //        strSW = DateTime.Parse(dr[1][1].ToString()).ToString("HH:mm");
        //    }
        //    if (!(dr[1][0] is DBNull))
        //    {
        //        strXW = DateTime.Parse(dr[1][0].ToString()).ToString("HH:mm");
        //        strTime = string.Format("{0}—{1}", strSW, strXW);
        //    }
        //}
        //else
        //{
        //    if (!(dr[0][1] is DBNull))
        //    {
        //        strSW = DateTime.Parse(dr[0][1].ToString()).ToString("HH:mm");
        //    }
        //    if (!(dr[0][0] is DBNull))
        //    {
        //        strXW = DateTime.Parse(dr[0][0].ToString()).ToString("HH:mm");
        //        strTime = string.Format("{0}—{1}", strSW, strXW);
        //    }
        //}

        //return strTime; 
        #endregion
        DateTime dtime;
        string strSW = "上午";
        string strXW = "下午";
        DateTime.TryParse(dr[0][1].ToString(), out dtime);
        string strDateNew = string.Format("{0}-{1}-{2}", year, Month.ToString().PadLeft(2, '0'), (i - day + 1).ToString().PadLeft(2, '0'));
        //if (dtime > DateTime.Parse(strDateNew + " 12:00"))
        //    strSW = "上午未打卡";
        //else
        if (!(dr[0][1] is DBNull))
        {
            strSW = DateTime.Parse(dr[0][1].ToString()).ToString("HH:mm");
        }
        //DateTime.TryParse(dr[0][0].ToString(), out dtime);
        //if (dtime > DateTime.Parse(strDateNew + " 23:00") && dtime < DateTime.Parse(strDateNew + " 23:59"))
        //    strXW = "下午未打卡";
        //else
        if (!(dr[0][0] is DBNull))
        {
            strXW = DateTime.Parse(dr[0][0].ToString()).ToString("HH:mm");
            strTime = string.Format("{0}—{1}", strSW, strXW);
        }

        return strTime;
    }

    /// <summary>
    /// 得到当年每个月的天数
    /// </summary>
    /// <param name="year"></param>
    /// <param name="Month"></param>
    /// <returns></returns>
    int DayCount(int year, int Month)
    {
        int num = 0;
        if (Month != 2)
        {
            switch (Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12: num = 31; break;
                case 4:
                case 6:
                case 9:
                case 11: num = 30; break;
                default: num = 0; break;
            }
        }
        else
        {
            //闰年
            if ((year % 4 == 0 && year % 100 != 0 || year % 400 == 0))
            {
                num = 29;
            }
            else
            {
                num = 28;
            }
        }

        return num;
    }

    /// <summary>
    /// 生成考勤记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        getData();
    }
    private void getData()
    {
        string str = DropDownList1.SelectedItem.Value;//选择的人员
        string[] strs = txtMonth.Text.Split('-');
        int InputDate = int.Parse(strs[0] + strs[1]);//选择的日期  
        //根据用户名得到userCode
        string SqlCode = "select UserCode from UserInfo where UserName=@UserName";
        SqlParameter sp = new SqlParameter("@UserName", str);
        UserCode = SqlHelper.GetSingle(SqlCode, sp).ToString();
        dtShow = TimeList(DateTime.Parse(txtMonth.Text).ToString("yyyyMM"), UserCode);
        ShowList(int.Parse(strs[0]), int.Parse(strs[1]));
    }

    ////考勤时间列表
    DataTable TimeList(string inputDate, string name)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select MAX(MaxTime) as MaxTime,MAX(MinTime) as MinTime,WorkDay,max(Remark) as Remark,UserCode from (select a.UserCode,a.WorkDay,b.Remark,a.MaxTime,a.MinTime from dbo.AttendanceList a join AttendanceRemark b on a.UserCode=b.UserCode and a.WorkDay=b.WorkDay and (b.Remark<>'' or b.Remark is not null) where a.UserCode='" + name + "' and SUBSTRING(a.WorkDay,0,7)='" + inputDate + "'");
        sb.Append(" union select a.UserCode,a.WorkDay,a.Remark,a.MaxTime,a.MinTime  from dbo.AttendanceRemark a left join  AttendanceRemark b on a.UserCode<>b.UserCode where a.UserCode='" + name + "'  and SUBSTRING(a.WorkDay,0,7)='" + inputDate + "' ");
        sb.Append(" union select a.UserCode,a.WorkDay,a.Remark,a.MaxTime,a.MinTime from dbo.AttendanceList a where a.UserCode='" + name + "' and SUBSTRING(a.WorkDay,0,7)='" + inputDate + "'  and (a.Remark is null or a.Remark=''))a group by a.UserCode,a.WorkDay order by WorkDay;");
        //sb.Append(" left join AttendanceList on UserInfo.UserCode=AttendanceList.UserCode and SUBSTRING(AttendanceList.WorkDay,0,7)='" + inputDate + "'");
        //sb.Append(" left join AttendanceRemark on  AttendanceList.WorkDay=AttendanceRemark.WorkDay  and AttendanceList.UserCode=AttendanceRemark.UserCode where UserInfo.userName='" + name + "'");


        //string sql = "select MaxTime,MinTime,WorkDay,Remark from UserInfo left join AttendanceList on UserInfo.UserCode=AttendanceList.UserCode where WorkDay like  @workDay and UserName=@userName";
        //SqlParameter[] parmets = { new SqlParameter("@workDay",SqlDbType.VarChar),
        //                         new SqlParameter("@userName",SqlDbType.NVarChar)};

        //parmets[0].Value = inputDate + "%";
        //parmets[1].Value = name;
        DataSet ds = SqlHelper.Query(sb.ToString());
        DataTable dt = ds.Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string str = dt.Rows[i].ToString();
        }
        return dt;
    }

    //加载人名
    void PeopleList()
    {
        string sql = "select UserName from UserInfo where UserStatus=1 order by UserName";
        DropDownList1.DataSource = SqlHelper.Query(sql).Tables[0];
        DropDownList1.DataBind();
        DropDownList1.SelectedValue = UserName;
    }
}