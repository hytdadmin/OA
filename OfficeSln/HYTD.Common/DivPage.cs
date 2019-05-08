using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYTD.Common
{
    public class DivPage
    {

        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="numPerPage">每页显示数据条数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="pageNumShown">分页控件显示多少页码</param>
        /// <param name="pageNum">当前页</param>
        /// <returns></returns>
        public static string Pager(int numPerPage, int totalCount, int pageNum, string Url)
        {

            int canDivid = 0;//可分页数
            if (totalCount % numPerPage == 0)
                canDivid = totalCount / numPerPage;
            else
                canDivid = (totalCount / numPerPage) + 1;


            StringBuilder sbd_page = new StringBuilder();
            int first_c = 0;
            int curr = pageNum;
            string temp_str = "";
            if (canDivid > 1)
            {
                while (curr > 0 && first_c < 5)
                {
                    if (curr != pageNum)
                    {
                        temp_str = "<li><a href=\"" + string.Format(Url, curr) + "\">" + curr + "</a></li>" + temp_str;
                    }
                    else
                    {
                        temp_str = "<li><a class='current' href=\"#\">" + curr + "</a></li>" + temp_str;
                    }
                    curr--;
                    first_c++;
                }
                //if (curr > 0)//到第一页
                //{
                //    temp_str = "<a href=\"" + string.Format(Url, 1) + "\">" + 1 + "..</a>" + temp_str;
                //}
                sbd_page.Append(temp_str);

                int second_c = 0;
                curr = pageNum + 1;
                int max = canDivid;
                while (curr <= canDivid)
                {
                    if (second_c > 4)//最多5个
                        break;

                    sbd_page.Append("<li><a href=\"" + string.Format(Url, curr) + "\">" + curr + "</a></li>");

                    curr++;
                    second_c++;
                }

                //补全后面的 说明前面没有出现5次
                if (curr <= canDivid && first_c < 5)
                {
                    int nx = 5 - first_c;
                    while (nx > 0)
                    {
                        sbd_page.Append("<li><a href=\"" + string.Format(Url, curr) + "\">" + curr + "</a></li>");
                        nx--;
                        curr++;
                    }
                }
            }

            //if (curr < canDivid)//到第一页
            //{
            //    sbd_page.AppendFormat("<a href=\"" + string.Format(Url, canDivid) + "\">{0}</a>", +canDivid + "..");
            //}

            StringBuilder sbd = new StringBuilder();


            sbd.Append("<div class=\"page\">");

            if (pageNum > 1)//上一页
                sbd.Append("<p><a href=\"" + string.Format(Url, pageNum - 1) + "\"><img src=\"/img/sp3.jpg\"></a></p>");


            sbd.Append("<ul>");
            sbd.Append(sbd_page);
            sbd.Append("</ul>");

            if (pageNum < canDivid)//到第一页
                sbd.Append("<p><a href=\"" + string.Format(Url, pageNum + 1) + "\"><img src=\"/img/sp4.jpg\"></a></p>");

            sbd.Append("</div>");

            return sbd.ToString();
        }

    }
}
