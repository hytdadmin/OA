using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using HYTD.CAPlatform.Common;

public partial class Import :PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtMonth.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");

        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        // 获取Excep文件的完整路径
        string strFileName = this.fileExcel.PostedFile.FileName;
        try
        {
            if (strFileName.Substring(strFileName.LastIndexOf('.') + 1) == "xls" || strFileName.Substring(strFileName.LastIndexOf('.') + 1) == "xlsx")
            {
                //创建临时文件
                string strTempFileName = PublicMethod.CreateTempFile(this.fileExcel, "hytd");
                //导入数据
                string strReturn=GetAttendanceDetail(strTempFileName);
                //删除临时文件
                PublicMethod.DeleteTempFile("hytd");
                //提示信息
                if (strReturn != string.Empty)
                {
                    lblMsg.InnerText = strReturn;
                }
                else
                {
                    lblMsg.InnerText = "导入成功";
                    PublicMethod.Alert(this, "导入成功");
                   
                }
            }
            else
            {
                lblMsg.InnerText = "您选择的不是Excel文件！";
                PublicMethod.Alert(this, "您选择的不是Excel文件！");
            }
        }
        catch
        {
            lblMsg.InnerText="导入失败!";
            PublicMethod.Alert(this, "导入失败");
        }
    }

      //将本地Excel导入服务上临时的Excel文件
    public string GetAttendanceDetail(string __strFileName)
    {
        string strReturn = string.Empty;
        if (__strFileName != null && __strFileName.Trim().Length > 0)
        {
            DataSet dsExcel = new DataSet();
            caLog.WriteTraceLog("Excel导入服务上临时的Excel文件", "Excel导入服务上临时的Excel文件");
            try
            {
                dsExcel = PublicMethod.GetExcelData(__strFileName);
            }
            catch (Exception ex)
            {

                caLog.WriteTraceLog("PublicMethod.GetExcelData异常", ex.Message);
                throw new Exception(ex.Message);
            }

            if (dsExcel.Tables.Count > 0 && dsExcel.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsExcel.Tables[0].Rows.Count; i++)
                {
                    if (!dsExcel.Tables[0].Rows[i].IsNull("考勤号") && dsExcel.Tables[0].Rows[i]["考勤号"].ToString().Trim().Length > 0 && !dsExcel.Tables[0].Rows[i].IsNull("日期/时间") && dsExcel.Tables[0].Rows[i]["日期/时间"].ToString().Trim().Length > 0)
                    {
                        string sql = "insert into dbo.AttendanceDetail(UserCode,Time) values ('" + dsExcel.Tables[0].Rows[i]["考勤号"].ToString() + "','" + dsExcel.Tables[0].Rows[i]["日期/时间"].ToString() + "')";
                        SqlHelper.ExecuteSql(sql);
                    }

                }

            }
            else
            {
                strReturn = "该Excel文件中没有记录！";
            }
        }
        return strReturn;
    }





    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@_month", SqlDbType.VarChar)
            };
            parameters[0].Value = DateTime.Parse(txtMonth.Text).ToString("yyyyMM");
            SqlHelper.RunProcedure("GetAttendanceList", parameters, out rowsAffected);
            PublicMethod.Alert(this,"执行成功");
    }
}