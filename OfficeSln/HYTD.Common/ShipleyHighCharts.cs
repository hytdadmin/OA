using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
namespace HYTD.Common
{
    public class ShipleyHighCharts
    {
        public static string OutCharts(HighChartsType hct, string strTitle, List<HighChartsData> hcd, string strPanelID)
        {
            string strCharts = string.Empty;
            switch (hct)
            {
                case HighChartsType.Area:
                    strCharts = OutputArea(strTitle, hcd, strPanelID);
                    break;
                case HighChartsType.AreaSpline:
                    strCharts = OutputAreaSpline(strTitle, hcd, strPanelID);
                    break;
                case HighChartsType.Bar:
                    strCharts = OutputBar(strTitle, hcd, strPanelID);
                    break;
                case HighChartsType.Column:
                    strCharts = OutputColumn(strTitle, hcd, strPanelID);
                    break;
                case HighChartsType.Line:
                    strCharts = OutputLine(strTitle, hcd, strPanelID);
                    break;
                case HighChartsType.Pie:
                    strCharts = OutputPie(strTitle, hcd, strPanelID);
                    break;
                case HighChartsType.Scatter:
                    strCharts = OutputScatter(strTitle, hcd, strPanelID);
                    break;
                case HighChartsType.Spline:
                    strCharts = OutputSpline(strTitle, hcd, strPanelID);
                    break;
                case HighChartsType.ColumnTotated:
                    strCharts = OutputColumnTotated(strTitle, hcd, strPanelID);
                    break;
            }
            return strCharts;
        }
        private static string OutputLine(string strTitle, List<HighChartsData> hcd, string strPanelID)
        {
            string result = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/Common/HighCharts/ChartsTypeJs/line.js"), Encoding.UTF8))
            {
                StringBuilder buffer = new StringBuilder(reader.ReadToEnd());
                buffer.Replace("{#title#}", strTitle);
                result = buffer.ToString();
            }
            return result;
        }

        private static string OutputSpline(string strTitle, List<HighChartsData> hcd, string strPanelID)
        {
            string result = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/Common/HighCharts/ChartsTypeJs/spline.js"), Encoding.UTF8))
            {
                StringBuilder buffer = new StringBuilder(reader.ReadToEnd());
                buffer.Replace("{#title#}", strTitle);
                result = buffer.ToString();
            }
            return result;
        }

        private static string OutputArea(string strTitle, List<HighChartsData> hcd, string strPanelID)
        {
            string result = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/Common/HighCharts/ChartsTypeJs/area.js"), Encoding.UTF8))
            {
                StringBuilder buffer = new StringBuilder(reader.ReadToEnd());
                buffer.Replace("{#title#}", strTitle);
                result = buffer.ToString();
            }
            return result;
        }

        private static string OutputAreaSpline(string strTitle, List<HighChartsData> hcd, string strPanelID)
        {
            string result = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/Common/HighCharts/ChartsTypeJs/areaspline.js"), Encoding.UTF8))
            {
                StringBuilder buffer = new StringBuilder(reader.ReadToEnd());
                buffer.Replace("{#title#}", strTitle);
                result = buffer.ToString();
            }
            return result;
        }

        private static string OutputColumn(string strTitle, List<HighChartsData> hcd, string strPanelID)
        {
            string result = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/Common/HighCharts/ChartsTypeJs/column.js"), Encoding.UTF8))
            {
                StringBuilder buffer = new StringBuilder(reader.ReadToEnd());
                buffer.Replace("{#title#}", strTitle);
                result = buffer.ToString();
            }
            return result;
        }

        private static string OutputBar(string strTitle, List<HighChartsData> hcd, string strPanelID)
        {
            string result = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/Common/HighCharts/ChartsTypeJs/bar.js"), Encoding.UTF8))
            {
                StringBuilder buffer = new StringBuilder(reader.ReadToEnd());
                buffer.Replace("{#title#}", strTitle);
                buffer.Replace("{#subTitle#}", "扇形图表");
                //下面的数据应该是从数据库来，为了演示，这里直接上了  
                string data = "{name: '1812年',data: [107, 31, 635, 203, 2]}, {name: '1912年',data: [133, 156, 947, 408, 6]}, {name: '2012年',data: [973, 914, 4054, 732, 34]}";
                buffer.Replace("{#series#}", data);
                result = buffer.ToString();
            }
            return result;
        }

        private static string OutputPie(string strTitle, List<HighChartsData> hcd,string strPanelID)
        {
            string result = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/Common/HighCharts/ChartsTypeJs/pie.js"), Encoding.UTF8))
            {
                StringBuilder buffer = new StringBuilder(reader.ReadToEnd());
                StringBuilder dates = new StringBuilder();
                int i = 0;
                buffer.Replace("{#divid#}", strPanelID);
                buffer.Replace("{#title#}", strTitle);
                foreach (HighChartsData h in hcd)
                {
                    if (i == hcd.Count - 1)
                            dates.AppendFormat("['{0}',   {1}]", h.Name, h.Value);
                        else
                            dates.AppendFormat("['{0}',   {1}],", h.Name, h.Value);
                        i++;
                }
                buffer.Replace("{#datas#}", dates.ToString());
                result = buffer.ToString();
            }
            return result;
        }

        private static string OutputScatter(string strTitle, List<HighChartsData> hcd, string strPanelID)
        {
            string result = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/Common/HighCharts/ChartsTypeJs/scatter.js"), Encoding.UTF8))
            {
                StringBuilder buffer = new StringBuilder(reader.ReadToEnd());
                buffer.Replace("{#title#}", strTitle);

                result = buffer.ToString();
            }
            return result;
        }
        private static string OutputColumnTotated(string strTitle, List<HighChartsData> hcd, string strPanelID)
        {
            string result = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/Common/HighCharts/ChartsTypeJs/columntotated.js"), Encoding.UTF8))
            {
                StringBuilder buffer = new StringBuilder(reader.ReadToEnd());
                StringBuilder dateName = new StringBuilder();
                StringBuilder dateValue = new StringBuilder();
                int i = 0;
                buffer.Replace("{#divid#}", strPanelID);
                buffer.Replace("{#title#}", strTitle);
                foreach (HighChartsData h in hcd)
                {
                    if (i == hcd.Count-1)
                        dateName.AppendFormat("'{0}'", h.Name);
                    else
                        dateName.AppendFormat("'{0}',", h.Name);
                    if (i == hcd.Count - 1)
                        dateValue.AppendFormat("{0}", h.Value);
                    else
                        dateValue.AppendFormat("{0},", h.Value);
                    i++;
                }
                buffer.Replace("{#Name#}", dateName.ToString());
                buffer.Replace("{#Value#}", dateValue.ToString());
                result = buffer.ToString();
            }
            return result;
        } 
    }
    public enum HighChartsType
    {
        /// <summary>
        /// 线形图
        /// </summary>
        Line,
        /// <summary>
        /// 线形图
        /// </summary>
        Spline,
        /// <summary>
        /// 区域图
        /// </summary>
        Area,
        /// <summary>
        /// 线形图+区域图
        /// </summary>
        AreaSpline,
        /// <summary>
        /// 水平多条柱形图
        /// </summary>
        Column,
        /// <summary>
        /// 水平柱形图
        /// </summary>
        Bar,
        /// <summary>
        /// 饼图
        /// </summary>
        Pie,
        /// <summary>
        /// 点型图
        /// </summary>
        Scatter,
        /// <summary>
        /// 柱形图
        /// </summary>
        ColumnTotated
    }
}
