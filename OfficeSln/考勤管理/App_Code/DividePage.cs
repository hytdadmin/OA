using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
/*
Author：liulei
Version：1.0
Date： 2013年4月23日16:03:19
Description: 分页控件专用类，返回分页控件拼串
*/
/// <summary>
///分页控件专用类
/// </summary>
public class DividePage
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

        int rint = 0;//随机数
        Random ra = new Random();
        rint = ra.Next(1000, 9999);
        int canDivid = 1;//可分页数
        if (totalCount <= numPerPage)
        {
            canDivid = 1;
        }
        else
        {
            canDivid = (totalCount / numPerPage);
            if (totalCount % numPerPage > 0)
                canDivid = canDivid + 1;
        }
        // canDivid = 1;
        StringBuilder sbd_page = new StringBuilder();
        int first_c = 0;
        int curr = pageNum;
        string temp_str = "";
        while (curr > 0 && first_c < 5)
        {
            if (curr != pageNum)
            {
                temp_str = "<a href=\"" + string.Format(Url, curr) + "\">" + curr + "</a>" + temp_str;
            }
            else
            {
                temp_str = "<strong>" + curr + "</strong>" + temp_str;
            }
            curr--;
            first_c++;
        }
        if (curr > 0)//到第一页
        {
            temp_str = "<a href=\"" + string.Format(Url, 1) + "\">" + 1 + "..</a>" + temp_str;
        }
        sbd_page.Append(temp_str);

        int second_c = 0;
        curr = pageNum + 1;
        int max = canDivid;
        while (curr <= canDivid)
        {
            if (second_c >= 4)//最多5个
                break;

            sbd_page.AppendFormat("<a href=\"" + string.Format(Url, curr) + "\">{0}</a>", curr);
            curr++;
            second_c++;
        }

        int before_count = first_c + second_c;

        //补全后面的 说明前面没有出现5次
        if (curr <= canDivid && before_count < 9)
        {
            int nx = 9 - before_count;
            while (nx > 0 && curr <= canDivid)
            {
                sbd_page.AppendFormat("<a href=\"" + string.Format(Url, curr) + "\">{0}</a>", curr);
                nx--;
                curr++;
            }
        }

        if (curr < canDivid)//到第一页
        {
            sbd_page.AppendFormat("<a href=\"" + string.Format(Url, canDivid) + "\">{0}</a>", +canDivid + "..");
        }

        StringBuilder sbd = new StringBuilder();

        //sbd.Append("<div class=\"pgt\">");
        sbd.Append("<div class=\"pg\">");
        if (pageNum > 1)
            sbd.AppendFormat("<a href=\"" + string.Format(Url, pageNum - 1) + "\">上一页</a>");
        sbd.Append(sbd_page);
        sbd.Append("<label>");
        //跳转方法 写这里
        //sbd.Append("<input type=\"text\" id=\"custompage" + rint + "\" name=\"custompage\" class=\"px\" size=\"2\" title=\"输入页码，按回车快速跳转\" ");
        //sbd.AppendFormat(" value=\"{0}\">", pageNum);
        sbd.AppendFormat("  <span title=\"共 {0} 页 {2} 条\"> 共{1} 页 {2} 条 </span></label>", pageNum, canDivid,totalCount);
        if (pageNum < canDivid)//到第一页
            sbd.AppendFormat("<a href=\"" + string.Format(Url, pageNum + 1) + "\">下一页</a>");
        sbd.Append("</div>");
        sbd.Append("<script>function gotopage(page,url){ var u = url.replace(\"{0}\",page); document.location.href=u;}");
        sbd.Append("  $(\"#custompage" + rint + "\").keyup(function(){  if(event.keyCode == 13){   var url=\"" + Url + "\"; var p=$(this).val();  gotopage(url,p);    }    }); </script>");
        return sbd.ToString();

    }


}