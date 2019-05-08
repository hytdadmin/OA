using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.SS.UserModel;
using System.Data;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Configuration;

/// <summary>
///ImportExcel 的摘要说明
/// </summary>
public class ImportExcel
{
	public ImportExcel()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//

	}
    /// <summary>
    /// 标版广告
    /// </summary>
    /// <param name="Chid"></param>
    /// <returns></returns>
    public IWorkbook ImportCustomerWorkBill(DataTable dt, string strCusName)
    {
        int RowNum = 1;
        IWorkbook book = new HSSFWorkbook();
        ISheet sheet = book.CreateSheet(strCusName);
        IRow row1 = sheet.CreateRow(RowNum);
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 1, 5));
        ICellStyle style = sheet.Workbook.CreateCellStyle();

        style.BorderBottom = CellBorderType.THIN;
        style.BorderLeft = CellBorderType.THIN;
        style.BorderRight = CellBorderType.THIN;
        style.BorderTop = CellBorderType.THIN;
        IFont font = sheet.Workbook.CreateFont();
        font.FontName = "宋体";

        CreateNumCell(row1, 7, style, font);
        int num1 = RowNum;
        row1.Cells[0].SetCellValue("来电类型");
        row1.Cells[1].SetCellValue("联系人");
        row1.Cells[2].SetCellValue("电话");
        row1.Cells[3].SetCellValue("其他联系方式");
        row1.Cells[4].SetCellValue("咨询时间");
        row1.Cells[5].SetCellValue("问题描述");
        row1.Cells[6].SetCellValue("解决方案");
        //row1.Cells[7].SetCellValue("回访时间");
        //row1.Cells[8].SetCellValue("回访人");		
        //row1.Cells[9].SetCellValue("回访结果");						


        RowNum++;

        if (dt != null && dt.Rows.Count > 0)
        {
            IRow row = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = sheet.CreateRow(RowNum);
                CreateNumCell(row, 7, style, font);

                row.Cells[0].SetCellValue(dt.Rows[i]["来电类型"].ToString());
                row.Cells[1].SetCellValue(dt.Rows[i]["联系人"].ToString());
                row.Cells[2].SetCellValue(dt.Rows[i]["电话"].ToString());
                row.Cells[3].SetCellValue(dt.Rows[i]["其他联系方式"].ToString());
                row.Cells[4].SetCellValue(dt.Rows[i]["咨询时间"].ToString());
                row.Cells[5].SetCellValue(dt.Rows[i]["问题描述"].ToString());
                row.Cells[6].SetCellValue(dt.Rows[i]["解决方案"].ToString());
                //row.Cells[7].SetCellValue(dt.Rows[i]["回访时间"].ToString());
                //row.Cells[8].SetCellValue(dt.Rows[i]["回访人"].ToString());
                //row.Cells[9].SetCellValue(dt.Rows[i]["回访结果"].ToString());
                RowNum++;
            }
        }
        return book;
    }

    /// <summary>
    /// 创建单元格
    /// </summary>
    /// <param name="row"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    private void CreateNumCell(IRow row, int num, ICellStyle style, IFont font)
    {
        ICell cell = null;

        for (int i = 0; i < num; i++)
        {

            cell = row.CreateCell(i);
            cell.CellStyle = style;
            cell.CellStyle.SetFont(font);
        }
    }
}