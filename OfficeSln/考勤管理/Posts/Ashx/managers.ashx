<%@ WebHandler Language="C#" Class="managers" %>

using System;
using System.Web;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
public class managers : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string strOption = string.Empty;
        if (!string.IsNullOrEmpty(context.Request["option"]))
        {
            strOption = context.Request["option"].Trim();
            switch (strOption)
            {
                case "add":
                    addAdmin(context);
                    break;
                case "del":
                    delAdmin(context);
                    break;
            }
        }

    }
    private string getStr(string[] strList, string strName)
    {
        string stt = string.Empty;
        foreach (var v in strList)
        {
            if (!v.Trim().ToLower().Equals(strName.Trim().ToLower()))
            {
                stt += "|" + v;
            }
        }
        return stt.Trim('|');
    }
    private void delAdmin(HttpContext context)
    {
        int intID = 0;
        string strName = string.Empty;
        string strRet = string.Empty;
        string[] strAdminListNoDel = { };
        string[] strFeedbackListNoDel = { };
        if (!string.IsNullOrEmpty(context.Request["id"]))
        {
            int.TryParse(context.Request["id"].Trim(), out intID);
            strName = context.Request["n"].Trim();
            //判断能否删除


            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                string strP = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["mangers"]);
                xmlDoc.Load(strP);
                if (xmlDoc != null)
                {

                    //************判断能不能被删除******************
                    string nodepath = "";
                    if (intID == 1002)
                        nodepath = "/managers/item/AdminListNoDelete";
                    else if (intID == 1003)
                        nodepath = "/managers/item/FeedbackListNoDelete";
                    string strNodel = xmlDoc.SelectSingleNode(nodepath).InnerText;
                    string[] noDelList = strNodel.Split('|');
                    if (noDelList.Contains(strName))//不能被删除
                    {
                        context.Response.Write("3");
                        return;
                    }
                    //************判断能不能被删除*****************


                    if (intID == 1002)
                    {
                        strRet = xmlDoc.SelectSingleNode("/managers/item/AdminList").InnerText;
                        if (strRet.Split('|').Where(c => c.Trim().ToLower().Equals(strName.Trim().ToLower())).Count() > 0)
                        {
                            strRet = getStr(strRet.Split('|'), strName);
                        }
                        xmlDoc.SelectSingleNode("/managers/item/AdminList").InnerText = strRet;
                    }
                    else if (intID == 1003)
                    {
                        strRet = xmlDoc.SelectSingleNode("/managers/item/FeedbackList").InnerText;
                        if (strRet.Split('|').Where(c => c.Trim().ToLower().Equals(strName.Trim().ToLower())).Count() > 0)
                        {
                            strRet = getStr(strRet.Split('|'), strName);
                        }
                        xmlDoc.SelectSingleNode("/managers/item/FeedbackList").InnerText = strRet;
                    }

                    xmlDoc.Save(strP);//保存。
                    strAdminListNoDel = xmlDoc.SelectSingleNode("/managers/item/AdminListNoDelete").InnerText.Trim().Split('|');
                    strFeedbackListNoDel = xmlDoc.SelectSingleNode("/managers/item/FeedbackListNoDelete").InnerText.Trim().Split('|');
                }

              

            }
            catch (Exception ex)
            {
                context.Response.Write("2");
            }
            string str = string.Empty;

            foreach (var v in strRet.Split('|'))
            {
                if (intID == 1002)
                {
                    if (strAdminListNoDel.Where(c => c.ToLower().Equals(v.ToLower())).Count() > 0)
                    {
                        str += string.Format("<li><span>{0}</span></li>",
               v);
                    }
                    else
                    {
                        str += string.Format("<li><span>{0}</span><a href=\"javascript:;\"><sup>x</sup></a></li>", v);
                    }
                }
                else if (intID == 1003)
                {

                    if (strFeedbackListNoDel.Where(c => c.ToLower().Equals(v.ToLower())).Count() > 0)
                    {
                        str += string.Format("<li><span>{0}</span></li>",
               v);
                    }
                    else
                    {
                        str += string.Format("<li><span>{0}</span><a href=\"javascript:;\"><sup>x</sup></a></li>", v);
                    }
                }
            }
            context.Response.Write(str);
        }
        else
            context.Response.Write("2");


    }
    private void addAdmin(HttpContext context)
    {
        int intID = 0;
        string strName = string.Empty;
        string strRet = string.Empty;
        bool isHas = false;
        string[] strAdminListNoDel = { };
        string[] strFeedbackListNoDel = { };
        if (!string.IsNullOrEmpty(context.Request["id"]))
        {
            int.TryParse(context.Request["id"].Trim(), out intID);
            strName = context.Request["n"].Trim();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                string strP = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["mangers"]);
                xmlDoc.Load(strP);
                if (xmlDoc != null)
                {
                    if (intID == 1002)
                    {
                        strRet = xmlDoc.SelectSingleNode("/managers/item/AdminList").InnerText;
                        if (strRet.Split('|').Where(c => c.Trim().ToLower().Equals(strName.Trim().ToLower())).Count() > 0)
                        {
                            isHas = true;
                        }
                        xmlDoc.SelectSingleNode("/managers/item/AdminList").InnerText += "|" + strName;
                        strRet = xmlDoc.SelectSingleNode("/managers/item/AdminList").InnerText;
                    }
                    else if (intID == 1003)
                    {
                        strRet = xmlDoc.SelectSingleNode("/managers/item/FeedbackList").InnerText;
                        if (strRet.Split('|').Where(c => c.Trim().ToLower().Equals(strName.Trim().ToLower())).Count() > 0)
                        {
                            isHas = true;
                        }
                        xmlDoc.SelectSingleNode("/managers/item/FeedbackList").InnerText += "|" + strName;
                        strRet = xmlDoc.SelectSingleNode("/managers/item/FeedbackList").InnerText;
                    }
                    if (!isHas)
                    {
                        xmlDoc.Save(strP);//保存。
                    }
                    strAdminListNoDel = xmlDoc.SelectSingleNode("/managers/item/AdminListNoDelete").InnerText.Trim().Split('|');
                    strFeedbackListNoDel = xmlDoc.SelectSingleNode("/managers/item/FeedbackListNoDelete").InnerText.Trim().Split('|');
                }

            }
            catch (Exception ex)
            {
                context.Response.Write("2");
            }
            if (isHas)
            {
                context.Response.Write("1");
            }
            else
            {
                string str = string.Empty;
                foreach (var v in strRet.Split('|'))
                {
                    if (intID == 1002)
                    {
                        if (strAdminListNoDel.Where(c => c.ToLower().Equals(v.ToLower())).Count() > 0)
                        {
                            str += string.Format("<li><span>{0}</span></li>",
                   v);
                        }
                        else
                        {
                            str += string.Format("<li><span>{0}</span><a href=\"javascript:;\"><sup>x</sup></a></li>", v);
                        }
                    }
                    else if (intID == 1003)
                    {

                        if (strFeedbackListNoDel.Where(c => c.ToLower().Equals(v.ToLower())).Count() > 0)
                        {
                            str += string.Format("<li><span>{0}</span></li>",
                   v);
                        }
                        else
                        {
                            str += string.Format("<li><span>{0}</span><a href=\"javascript:;\"><sup>x</sup></a></li>", v);
                        }
                    }
                }
                context.Response.Write(str);
            }
        }
        else
        {
            context.Response.Write("2");
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}