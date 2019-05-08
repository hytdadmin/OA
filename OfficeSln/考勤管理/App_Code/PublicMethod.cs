using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Xml;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.OleDb;
using BLL;
using Models;
using HYTD.Common;

/// <summary>
///PublicMethod 的摘要说明
/// </summary>
public class PublicMethod
{
    public PublicMethod()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public static string  getProductName(string strID)
    {
        string strRet = string.Empty;
        switch (strID)
        {
            case "1000":
                strRet = "Windows 7";
                break;
            case "2000":
                strRet = "Windows Vista";
                break;
            case "3000":
                strRet = "Windows XP";
                break;
            case "4000":
                strRet = "Office 2010";
                break;
            case "5000":
                strRet = "Office 2007";
                break;
            case "6000":
                strRet = "Office 2003";
                break;
            case "7000":
                strRet = "Windows 8";
                break;
            case "8000":
                strRet = "Office 2013";
                break;
            case "9000":
                strRet = "windows 8.1";
                break;
            case "9100":
                strRet = "windows 8.1";
                break;
            case "10000":
                strRet = "";
                break;
            default:
                strRet = strID;
                break;
        }
        return strRet;
    }
    public static void Alert(System.Web.UI.Page page, string strMessage, params string[] strURL)
    {
        if (strURL.Length == 0)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script>alert('" + strMessage + "');</script>");
        }
        else
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script>alert('" + strMessage + "');location.href='" + strURL[0] + "';</script>");
        }
    }
    #region 在服务端创建临时文件、删除临时文件
    // 在服务端创建临时文件
    // <returns>全路径(包含临时文件名)</returns>
    public static string CreateTempFile(HtmlInputFile __fileObj, string __strLogonName)
    {
        string strFilePath = @"\TempFile";
        string strPath = TempFileFolderPath + strFilePath;
        if (!Directory.Exists(strPath))
        {
            Directory.CreateDirectory(strPath);
        }
        strPath += @"\" + __strLogonName + "-" + Guid.NewGuid() + ".xls";
        __fileObj.PostedFile.SaveAs(strPath);
        return strPath;
    }
    //删除临时文件
    public static bool DeleteTempFile(string __strLogonName)
    {
        try
        {
            string strFilePath = @"\TempFile";
            string[] tmp = Directory.GetFiles(TempFileFolderPath + strFilePath);
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i].IndexOf(__strLogonName + "-") > -1)
                {
                    File.Delete(tmp[i]);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    // 临时文件夹 此路径后总是有"\"
    public static string TempFileFolderPath
    {
        get
        {
            string strPath = "C:";
            //strPath = strPath.TrimEnd(new Char[] { '\\' });
            strPath = strPath + "\\";
            return strPath;
        }
    }

    #endregion


    #region 取得Excel中的数据
    /// <summary>
    /// 取得Excel中的数据
    /// </summary>
    /// <param name="strFileName"></param>
    /// <returns></returns>
    public static DataSet GetExcelData(string strFileName)
    {
        if (strFileName == "")
            return new DataSet();
        DataSet myDataSet = new DataSet();

        string strConn;
        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + strFileName + ";" + "Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1;\"";
        OleDbConnection oleD = new OleDbConnection(strConn);
        try
        {
            oleD.Open();
            System.Data.DataTable tb;
            tb = oleD.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            OleDbDataAdapter myOleDbDataAdapter = null;
            myOleDbDataAdapter = new OleDbDataAdapter("SELECT * FROM [" + tb.Rows[0]["TABLE_NAME"] + "]", strConn);
            if (myOleDbDataAdapter != null)
                myOleDbDataAdapter.Fill(myDataSet, "ExcelInfo");
        }
        catch (Exception eImport)
        {
            throw new Exception(strConn + "---" + eImport.Message + "-------" + eImport.Source.ToString());
        }
        finally
        {
            oleD.Close();
        }
        return myDataSet;
    }
    #endregion

    /// <summary>
    /// 部门列表下拉框
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string DepartmentList(int id)
    {
        DepartmentBLL bll = new DepartmentBLL();
        StringBuilder sb = new StringBuilder();
        List<Department> list = bll.GetDepartmentList();
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (id == list[i].Id)
                {
                    sb.AppendFormat("<option selected='selected' value='{0}'>{1}</option>", id, list[i].RoleName);
                }
                else
                {
                    sb.AppendFormat("<option  value='{0}'>{1}</option>", list[i].Id, list[i].RoleName);
                }
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 基本状态
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string PublicStatus(int id)
    {

        StringBuilder sb = new StringBuilder();
        sb.Append("<option selected='selected' value=''>请选择</option>");
        if (id == 0)
        {
            sb.Append("<option selected='selected' value='0'>禁用</option>");
            sb.Append("<option  value='1'>启用</option>");
        }
        else if (id == 1)
        {
            sb.Append("<option selected='selected' value='1'>启用</option>");
            sb.Append("<option  value='0'>禁用</option>");
        }
        else
        {
            sb.Append("<option  value='0'>禁用</option>");
            sb.Append("<option  value='1'>启用</option>");
        }

        return sb.ToString();
    }

    /// <summary>
    /// 职位列表下拉框
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string PositionList(int id)
    {
        PositionBLL bll = new PositionBLL();
        StringBuilder sb = new StringBuilder();
        List<Position> list = bll.GetPositionList();
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (id == list[i].Id)
                {
                    sb.AppendFormat("<option selected='selected' value='{0}'>{1}</option>", list[i].Id, list[i].Name);
                }
                else
                {
                    sb.AppendFormat("<option  value='{0}'>{1}</option>", list[i].Id, list[i].Name);
                }
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 部门人员列表下拉框
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string UserInfoList(int id)
    {
        UserInfoBLL bll = new UserInfoBLL();
        StringBuilder sb = new StringBuilder();
        List<UserInfo> list = bll.GetUserInfoList().Where(c => c.UserStatus == 1).ToList();
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (id.ToString()== list[i].UserCode)
                {
                    sb.AppendFormat("<option selected='selected' value='{0}'>{1}</option>", id, list[i].UserName);
                }
                else
                {
                    sb.AppendFormat("<option  value='{0}'>{1}</option>", list[i].UserCode, list[i].UserName);
                }
            }
        }
        return sb.ToString();
    }
    /// <summary>
    /// 人员列表下拉框
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string UserInfoList(string id)
    {
        UserInfoBLL bll = new UserInfoBLL();
        StringBuilder sb = new StringBuilder();
        List<UserInfo> list = bll.GetUserInfoList().Where(c => c.UserStatus == 1).ToList();
        sb.AppendFormat("<option  value='{0}'>{1}</option>", "0", "请选择");
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (id == list[i].UserCode)
                {
                    sb.AppendFormat("<option selected='selected' value='{0}'>{1}</option>", id, list[i].UserName);
                }
                else
                {
                    sb.AppendFormat("<option  value='{0}'>{1}</option>", list[i].UserCode, list[i].UserName);
                }
            }
        }
        return sb.ToString();
    }
    public static string getWorkBillStatusCallCategory(int intType, bool blnIsAll, bool blnIsLine, int CID)
    {
        List<Call_Category> ccList = new List<Call_Category>();
        HYTD.BLL.Call_CategoryBLL ccbll = new HYTD.BLL.Call_CategoryBLL();
        ccList = ccbll.GetCall_CategoryList().Where(c => c.C_Type == intType).ToList();
        string str = string.Empty;
        if (blnIsAll)
        {
            str = string.Format("<option  value=\"{0}\">{1}</option>", PublicConst.PUBLICSTATUSALL_VALUE, "全部");
        }
        if (blnIsLine)
        {
            str = string.Format("<option  value=\"{0}\">{1}</option>", PublicConst.PUBLICSTATUSLINE_VALUE, "请选择");
        }
        return MakeTree(ccList, CID, "C_FID", "0", "C_ID", "C_Name", str, -1);

    }
    public static string getCallCategory(bool blnIsAll, bool blnIsLine, int CID)
    {
        List<Call_Category> ccList = new List<Call_Category>();
        HYTD.BLL.Call_CategoryBLL ccbll = new HYTD.BLL.Call_CategoryBLL();
        ccList = ccbll.GetCall_CategoryList().Where(c => c.C_Type == 0).ToList();
        string str = string.Empty;
        if (blnIsAll)
        {
            str = string.Format("<option  value=\"{0}\">{1}</option>", PublicConst.PUBLICSTATUSALL_VALUE, "全部");
        }
        if (blnIsLine)
        {
            str = string.Format("<option  value=\"{0}\">{1}</option>", PublicConst.PUBLICSTATUSLINE_VALUE, "请选择");
        }
        return MakeTree(ccList, CID, "C_FID", "0", "C_ID", "C_Name", str, -1);
    }

    ///   <summary>      
    ///   绑定生成一个有树结构的下拉菜单      
    ///   </summary>      
    ///   <param   name="dtNodeSets">菜单记录数据所在的表</param>      
    ///   <param   name="strParentColumn">表中用于标记父记录的字段</param>      
    ///   <param   name="strRootValue">第一层记录的父记录值(通常设计为0或者-1或者Null)用来表示没有父记录</param>      
    ///   <param   name="strIndexColumn">索引字段，也就是放在DropDownList的Value里面的字段</param>      
    ///   <param   name="strTextColumn">显示文本字段，也就是放在DropDownList的Text里面的字段</param>      
    ///   <param   name="drpBind">需要绑定的DropDownList</param>      
    ///   <param   name="i">用来控制缩入量的值，请输入-1</param>  
    public static string MakeTree(List<Call_Category> dList, int intDepID, string strParentColumn, string strRootValue, string strIndexColumn, string strTextColumn, string strsb, int i)
    {
        //每向下一层，多一个缩入单位      
        i++;
        List<Call_Category> dList1 = new List<Call_Category>();
        dList1 = dList.Where(c => c.C_FID.ToString() == strRootValue).ToList();
        string strPading = "";     //缩入字符     

        //通过i来控制缩入字符的长度，我这里设定的是一个全角的空格      
        for (int j = 0; j < i; j++)
            strPading += "　";//如果要增加缩入的长度，改成两个全角的空格就可以了     

        foreach (Call_Category s in dList1)
        {
            if (intDepID > 0 && intDepID == s.C_ID)
            {
                strsb += string.Format("<option value='{0}' selected=\"selected\">{1}├{2}</option>", s.C_ID, strPading, s.C_Name);
            }
            else
            {
                strsb += string.Format("<option value='{0}' >{1}├{2}</option>", s.C_ID, strPading, s.C_Name);
            }
            //strsb+=   strPading + "├" + drv[strTextColumn].ToString(), drv[strIndexColumn].ToString();
            strsb = MakeTree(dList, intDepID, strParentColumn, s.C_ID.ToString(), strIndexColumn, strTextColumn, strsb, i);
        }

        //递归结束，要回到上一层，所以缩入量减少一个单位      
        i--;
        return strsb;
    }

    /// <summary>
    /// 假期类型下拉框
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string HoildayTypeList(int typeID)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<option  selected=\"selected\" value='{0}'>请选择</option>", 0);
        if (typeID == (int)PublicEnum.HolidaysType.yearDay)
            sb.AppendFormat("<option  selected=\"selected\" value='{0}'>年假</option>", (int)PublicEnum.HolidaysType.yearDay);
        else
            sb.AppendFormat("<option  value='{0}'>年假</option>", (int)PublicEnum.HolidaysType.yearDay);
        if (typeID == (int)PublicEnum.HolidaysType.Swopped)
            sb.AppendFormat("<option  selected=\"selected\" value='{0}'>倒休</option>", (int)PublicEnum.HolidaysType.Swopped);
        else
            sb.AppendFormat("<option  value='{0}'>倒休</option>", (int)PublicEnum.HolidaysType.Swopped);
        if (typeID == (int)PublicEnum.HolidaysType.workDay)
            sb.AppendFormat("<option  selected=\"selected\" value='{0}'>加班</option>", (int)PublicEnum.HolidaysType.workDay);
        else
            sb.AppendFormat("<option  value='{0}'>加班</option>", (int)PublicEnum.HolidaysType.workDay);
        return sb.ToString();
    }
}