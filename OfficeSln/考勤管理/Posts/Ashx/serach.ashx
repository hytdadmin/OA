<%@ WebHandler Language="C#" Class="serach" %>

using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Text;
using Models;
using HYTD.BBS.Model.TO;
using HYTD.BBS.BLL;

public class serach : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        ShowList(context);

    }
    /// <summary>
    /// 搜索到的内容
    /// </summary>
    private void ShowList(HttpContext context)
    {
        string txt = context.Request.Form["txt"];
        PostsBLL bll = new PostsBLL();
        StringBuilder sb = new StringBuilder();
        if (txt != null)
        {
            System.Collections.Generic.List<Posts> list = bll.GetPostsList().Where(c => c.Title.Contains(txt)).ToList();

            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    sb.Append("<tbody>");
                    sb.AppendFormat(" <tr><th class='new'><em>[<a href='#'>问答精华</a>]</em><a href='#'class='xst'>{0}</a> - <a href='#' title='只看已解决的'>[已解决]</a> <span class='tps'>&nbsp;...<a href='#'  target='_blank'>2</a></span> </th>", list[i].Title);
                    sb.AppendFormat(" <td class='by'><cite><a target='_blank' href='#' c='1' mid='card_9492'>Bonri</a></cite> <em><span>{0}</span></em></td>", list[i].LastTime);
                    sb.AppendFormat("  <td class='num'> <a href='#' class='xi2'>22</a><em>1678</em></td>");

                    sb.AppendFormat("  <td class='by'> <cite><a target='_blank' href='#'c='1' mid='card_8929'>秦岭山脉</a></cite> <em><a target='_blank' href='#'> 2013-9-8 12:17</a></em> </td></tr>");
                    sb.Append("</tbody>");
                }
            }
            else
            {
                sb.Append("没有搜索到相关内容！");
            }
        }
        context.Response.Write(sb.ToString());

    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}