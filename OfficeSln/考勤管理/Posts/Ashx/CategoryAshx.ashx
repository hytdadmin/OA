<%@ WebHandler Language="C#" Class="CategoryAshx" %>

using System;
using System.Web;
using System.Data;
using Models;
using HYTD.BBS.Model.TO;
using HYTD.BBS.BLL;
using System.Collections.Generic;

public class CategoryAshx : PageBase, IHttpHandler
{

    string opt = string.Empty;
    string name = string.Empty;
    int id = 0;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.ContentType = "text/plain";
        PageBase.ClearClientPageCache();
        if (!string.IsNullOrEmpty(context.Request["opt"]))
        {
            opt = context.Request["opt"].ToString();
        }
        if (!string.IsNullOrEmpty(context.Request["id"]))
        {
            id = Convert.ToInt32(context.Request["id"].ToString());
        }
        if (!string.IsNullOrEmpty(context.Request["name"]))
        {
            name = context.Request["name"].ToString();
        }
        if (opt == "delete")
        {
            DeleteCategory(context, id);
        }
        else
            if (opt == "add")
            {
                AddCategory(context, name);
            }
            else if (opt == "change_order")
            {
                ChangeCategoryOrder(context);
            }
    }

    /// <summary>
    /// 改变显示顺序 by liulei 2013年11月11日15:28:10
    /// </summary>
    /// <param name="ctx"></param>
    protected void ChangeCategoryOrder(HttpContext ctx)
    {
        try
        {
            int id = Convert.ToInt32(ctx.Request.QueryString["id"]);//编号
            int displayorder = Convert.ToInt32(ctx.Request.QueryString["displayorder"]);//显示顺序
            CategoryBLL catBll = new CategoryBLL();
            Category entity = catBll.GetCategoryEntity(id);
            entity.DisplayOrder = displayorder;
            catBll.UpdateCategory(entity);
            ctx.Response.Write("1");
        }
        catch (Exception ex)
        {
            ctx.Response.Write(ex.ToString());
        }
    }

    protected void DeleteCategory(HttpContext context, int id)
    {
        int intRet = 0;
        if (!string.IsNullOrEmpty(id.ToString()))
        {
            CategoryBLL catBll = new CategoryBLL();
            Category catModel = new Category();
            try
            {
                catModel = catBll.GetCategoryEntity(id);
                catModel.satatus = 0;
                catBll.UpdateCategory(catModel);
                intRet = 1;
            }
            catch (Exception ex)
            {

            }
        }
        context.Response.Write(intRet);

    }
    protected void AddCategory(HttpContext context, string name)
    {
        int intRet = 0;
        string strWord = string.Empty;
        if (!string.IsNullOrEmpty(name))
        {
            CategoryBLL catBll = new CategoryBLL();
            Category catModel = new Category();
            try
            {
                catModel.Name = name;
                catModel.satatus = 1;
                catBll.AddCategory(catModel);
                strWord = string.Format("<li><span>{0}</span><a id=\"{1}\" href=\"javascript:;\">X</a></li>"
                    , catModel.Name, catModel.ID);
            }
            catch (Exception ex)
            {
                strWord = "0";
            }
        }
        context.Response.Write(strWord);
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}