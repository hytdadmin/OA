using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.TO;
using System.Text;
using System.Data;
using BLL;

public partial class Work_Bulletins : PageBase
{
    /// <summary>
    /// 内部类 查询参数
    /// </summary>
    public class Parms
    {
        //分页
        public string DataRowList = "";//列表数据拼串
        public int pageNum = 1;//当前页
        public int totalCount = 0;//总记录数
        public int numPerPage = 1;//每页20条
        public int pageNumShown = Config.PageNumShown;//显示分页页码数量

        //查询字段
        public string DeptID = "";
        public string DeptName = "";//部门名称查询条件
        public string DeptCode = "";//部门编码查询条件

        //排序
        public string DeptNameOrder = "";
        public string DeptCodeOrder = "";

        //顺序,倒序
        public string orderDirection = "";
        public string orderField = "";
    }

    public Parms parm = new Parms();
    protected void Page_Load(object sender, EventArgs e)
    {
        Bind();
    }

    private void Bind() {
        //int pagecount = new DAL.Bulletin().GetProcListCountByPK("Bulletin","");
        //IList<Models.Bulletin> list = new List<Models.Bulletin>();
        //if (pagecount > 0)
        //    list = new DAL.Bulletin().GetBulletinByList();

        //AspNetPager1.RecordCount = pagecount;
        //AspNetPager1.PageSize = pageSize;
        //this.Repeater1.DataSource = list;
        //this.Repeater1.DataBind();

        #region 分页相关
        //此处正常情况下不需要更改
        int _pageNum = 1;//当前页
        if (!string.IsNullOrEmpty(Request.Form["pageNum"]))
        {
            _pageNum = int.Parse(Request.Form["pageNum"]);
            parm.pageNum = _pageNum;
        }
        if (!string.IsNullOrEmpty(Request.Form["numPerPage"]))
        {
            parm.numPerPage = int.Parse(Request.Form["numPerPage"]); //每页显示条数(默认20)
        }
        #endregion


        #region 查询
        /***********查询条件*********************/
        BulletinTO sto = new BulletinTO();
        if (!string.IsNullOrEmpty(Request.Form["Title"]))
            parm.DeptName = sto.Title = Request.Form["Title"];
        //if (!string.IsNullOrEmpty(Request.Form["DeptCode"]))
        //    parm.DeptCode = sto.DeptCode = Request.Form["DeptCode"];
        //if (!string.IsNullOrEmpty(Request.Form["DeptID"]))
        //{
        //    parm.DeptID = sto.DeptID = Request.Form["DeptID"];
        //    if (sto.DeptID == "0") sto.DeptID = "";
        //}
        //else
        //    parm.DeptID = "0";


        #endregion

        #region 排序
        /************排序*************************/
        string columName = "";//排序的列明
        /***********排序字段*********************/
        if (!string.IsNullOrEmpty(Request.Form["orderField"]))
        {
            if (string.IsNullOrEmpty(Request.Form["orderDirection"]))
                parm.orderDirection = " asc ";
            else
                parm.orderDirection = Request.Form["orderDirection"];

            parm.orderField = Request.Form["orderField"];

            if (Request.Form["orderField"] == "DeptName")//xiaoqu order
            {
                parm.DeptNameOrder = parm.orderDirection;
                columName = " DeptName ";//需要加表别名防止重名报错
            }
            else if (Request.Form["orderField"] == "DeptCode")//jigou mc order
            {
                parm.DeptCodeOrder = parm.orderDirection;
                columName = " DeptCode ";
            }
        }
        string orderBy = "";
        if (!string.IsNullOrEmpty(Request.Form["orderField"]))
            orderBy = columName + " " + parm.orderDirection;
        else
            parm.DeptCodeOrder = "asc";

        #endregion


        #region 列表

        StringBuilder sbd = new StringBuilder();
        //查询列表
        BulletinBLL bulletinBLL = new BulletinBLL();

        DataTable dt = bulletinBLL.GetBulletinList(sto, parm.pageNum, parm.numPerPage, orderBy, out parm.totalCount);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            //sbd.AppendFormat("<tr target=\"sid_user\" rel=\"{0}\">", dr["DeptID"]);
            //sbd.AppendFormat("<td>{0}</td>", (i + 1) + (_pageNum - 1) * parm.numPerPage);//编号
            //sbd.AppendFormat("<td>{0}</td>", dr["DeptCode"]);
            //sbd.AppendFormat("<td>{0}</td>", dr["DeptName"]);
            //sbd.Append("</tr>");

            //sbd.AppendFormat("<div id=\"u68\" class=\"u68_container\"   >");
            //sbd.AppendFormat("<div id=\"u68_img\" >");
            //sbd.AppendFormat("<img src=\"Resoures/Bulletin/u63_normal.gif\"  class=\"raw_image\"></div>");
            //sbd.AppendFormat("<div id=\"u69\" class=\"u69\" style=\"visibility:hidden;\"  >");
            //sbd.AppendFormat("<div id=\"u69_rtf\"></div>");
            //sbd.AppendFormat("</div>");
            //sbd.AppendFormat("</div>");
            //sbd.AppendFormat("<div id=\"u70\" class=\"u70\"  >");
            //sbd.AppendFormat("<div id=\"u70_rtf\"><p style=\"text-align:left;\"><span style=\"font-family:Arial;font-size:14px;font-weight:bold;font-style:normal;text-decoration:none;color:#2C629E;\">关于中秋放假休息的通知</span><span style=\"font-family:Arial;font-size:14px;font-weight:bold;font-style:normal;text-decoration:none;color:#2C629E;\">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style=\"font-family:Arial;font-size:14px;font-weight:bold;font-style:normal;text-decoration:none;color:#999999;\">&nbsp; &nbsp;&nbsp; </span></p></div>");
            //sbd.AppendFormat("</div>");
            //sbd.AppendFormat("<div id=\"u71\" class=\"u71\"  >");
            //sbd.AppendFormat("<div id=\"u71_rtf\"><p style=\"text-align:left;\"><span style=\"font-family:Arial;font-size:13px;font-weight:normal;font-style:normal;text-decoration:none;color:#333333;\">根据国务院《关于修改&lt;全国年节及纪念日放假办法&gt;的决定》，2013年9月19日至9月21日放假</span><span style=\"font-family:Arial;font-size:13px;font-weight:normal;font-style:normal;text-decoration:none;color:#333333;\">共</span><span style=\"font-family:Arial;font-size:13px;font-weight:normal;font-style:normal;text-decoration:none;color:#333333;\">3</span><span style=\"font-family:Arial;font-size:13px;font-weight:normal;font-style:normal;text-decoration:none;color:#333333;\">天。其中9月19日(星期四，农历中秋节)法定节假日，9月21日(星期六)公休日，9月22日(星期日)公休调至9月20日，9月22日(星期日)照常上班。</span></p></div>");
            //sbd.AppendFormat("</div><div id=\"u72\" class=\"u72\" >");
            //sbd.AppendFormat("<DIV id=\"u72_line\" class=\"u72_line\" ></DIV>");
            //sbd.AppendFormat("</div>");


            sbd.AppendFormat("<div style='width:100%;'>{0}</div>", dr["Title"]);
        }
        parm.DataRowList = sbd.ToString();

        #endregion
    }
}