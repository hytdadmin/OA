<%@ WebHandler Language="C#" Class="sensitivewords" %>

using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using Models;
using HYTD.BBS.Model.TO;
using HYTD.BBS.BLL;

public class sensitivewords : IHttpHandler
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
                    addWorks(context);
                    break;
                case "del":
                    delWorks(context);
                    break;
            }
        }

    }
    private void delWorks(HttpContext context)
    {
        int intRet = 0;
        int intID = 0;
        if (!string.IsNullOrEmpty(context.Request["id"]))
        {
            int.TryParse(context.Request["id"].Trim(), out intID);
            HYTD.BBS.BLL.SensitiveWordsBLL swBll = new HYTD.BBS.BLL.SensitiveWordsBLL();
            SensitiveWords model = new SensitiveWords();
            try
            {
                model = swBll.GetSensitiveWordsEntity(intID);
                model.status = 0;
                swBll.UpdateSensitiveWords(model);
                intRet = 1;
            }
            catch (Exception ex)
            {

            }
        }
        context.Response.Write(intRet);
    }
    private void addWorks(HttpContext context)
    {
        int intRet = 0;
        string strWord = string.Empty;
        if (!string.IsNullOrEmpty(context.Request["kw"]))
        {

            HYTD.BBS.BLL.SensitiveWordsBLL swBll = new HYTD.BBS.BLL.SensitiveWordsBLL();
            SensitiveWords model = new SensitiveWords();
            try
            {
                model.sensitiveWord = context.Request["kw"].Trim();
                model.status = 1;
                swBll.AddSensitiveWords(model);
                strWord = string.Format("<li><span>{0}</span><a id=\"{1}\" href=\"javascript:;\">X</a></li>"
                    , model.sensitiveWord, model.ID);
            }
            catch (Exception ex)
            {
                strWord = "0";
            }
        }
        context.Response.Write(strWord);
    }

    private bool WordsIsEx()
    {

        return true;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}