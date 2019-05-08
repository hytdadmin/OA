using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections;

/// <summary>
///Check 的摘要说明
/// </summary>
public class Check
{
    //
    //TODO: 在此处添加构造函数逻辑
    //
    public static int GetInt32(object o)
    {
        if (o == null)
            return 0;

        Regex reg = new Regex("^[0-9]+$", RegexOptions.IgnoreCase);

        if (reg.IsMatch(o.ToString()))
        {
            return Convert.ToInt32(o);
        }
        else
        {
            return 0;
        }
    }


    //带有默认值
    public static int GetInt32(object o, int defaultvalue)
    {
        if (o == null)
            return defaultvalue;

        Regex reg = new Regex("^([-]?)+([0-9]+)$", RegexOptions.IgnoreCase);

        if (reg.IsMatch(o.ToString()))
        {
            return Convert.ToInt32(o);
        }
        else
        {
            return defaultvalue;
        }
    }
    public static float GetFloat(object o)
    {
        if (o == null)
            return 0;

        Regex reg = new Regex("^([0-9]+)(.)+([0-9]*)$", RegexOptions.IgnoreCase);

        if (reg.IsMatch(o.ToString()))
        {
            return Convert.ToSingle(o);
        }
        else
        {
            return 0;
        }
    }
    //带有默认值
    public static float GetFloat(object o, float defaultvalue)
    {
        if (o == null)
            return defaultvalue;

        //Regex reg = new Regex("^([0-9]+)+([.]?)+([0-9]*)$", RegexOptions.IgnoreCase);

        //if (reg.IsMatch(o.ToString()))
        //{
        //    return Convert.ToSingle(o);
        //}
        //else
        //{
        //    return defaultvalue;
        //}

        try
        {
            return Convert.ToSingle(o);
        }
        catch
        {
            return defaultvalue;
        }
    }

    //带有默认值
    public static string GetString(object o)
    {
        if (o == null)
            return string.Empty;
        try
        {
            return Convert.ToString(o).Replace("undefined", "").Trim();
        }
        catch
        {
            return string.Empty;
        }
    }
    //带有默认值
    public static string GetString(object o, string defaultvalue)
    {
        if (o == null)
            return defaultvalue;
        try
        {
            return Convert.ToString(o).Replace("undefined", "").Trim();
        }
        catch
        {
            return defaultvalue;
        }
    }
    public static bool GetBool(object o, bool defaultvalue)
    {
        if (o == null)
            return defaultvalue;
        try
        {
            return Convert.ToBoolean(o);
        }
        catch
        {
            return defaultvalue;
        }
    }
    public static bool GetBool(object o)
    {
        if (o == null)
            return false;
        try
        {
            return o.ToString() == "1";
        }
        catch
        {
            return false;
        }
    }

    public static string GetReturnStringByDateTime(object o)
    {
        try
        {
            return Convert.ToDateTime(o).ToString();
        }
        catch
        {
            return "";
        }
    }
    public static DateTime GetDateTime(object o)
    {
        try
        {
            return Convert.ToDateTime(o);
        }
        catch
        {
            return DateTime.Now;
        }
    }
    public static DateTime GetDateTime(object o, DateTime defaultvalue)
    {
        if (o == null)
            return defaultvalue;
        try
        {
            return Convert.ToDateTime(o);
        }
        catch
        {
            return defaultvalue;
        }
    }


    /// <summary>
    /// 如果是null，获得默认值，否则返回自身
    /// </summary>
    /// <param name="o"></param>
    /// <param name="defaultvalue"></param>
    /// <returns></returns>
    public static object GetValueIfNull(object o, int defaultvalue)
    {
        if (o == null) return defaultvalue;

        try
        {
            return o;
        }
        catch
        {
            return defaultvalue;
        }
    }


    /// <summary>
    /// 获得百分比
    /// </summary>
    /// <param name="number1"></param>
    /// <param name="number2"></param>
    /// <returns></returns>
    public static float GetPercent(object number1, object number2)
    {
        float numb1 = Convert.ToSingle(number1);
        float numb2 = Convert.ToSingle(number2);


        if (numb2 == 0 || numb1 == 0) return 0f;

        try
        {
            return 100 * numb1 / numb2;
        }
        catch
        {
            return 0f;
        }
    }

    /// <summary>
    /// 获得百分比
    /// </summary>
    /// <param name="number1"></param>
    /// <param name="number2"></param>
    /// <returns></returns>
    public static string GetPercentString(object number1, object number2)
    {

        try
        {
            float numb1 = Convert.ToSingle(number1);
            float numb2 = Convert.ToSingle(number2);


            if (numb2 == 0 || numb1 == 0) return "0";

            return string.Format("{0:F2}", 100 * numb1 / numb2);
        }
        catch
        {
            return "0";
        }
    }

    /// <summary>
    /// 分割参数
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public static ArrayList SpliteToGetAL(object o)
    {
        ArrayList al = new ArrayList();
        try
        {
            string tmp = Convert.ToString(o);

            string[] arr = tmp.Split(new char[] { ',' });
            if (arr != null)
            {
                foreach (string s in arr)
                {
                    if (!string.IsNullOrEmpty(s))
                        al.Add(s);
                }
            }
        }
        catch { }

        return al;
    }

    /// <summary>
    /// 分割参数
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public static ArrayList SpliteToGetIntAL(object o)
    {
        ArrayList al = new ArrayList();
        try
        {
            string tmp = Convert.ToString(o);

            string[] arr = tmp.Split(new char[] { ',' });
            if (arr != null)
            {
                foreach (string s in arr)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        if (Convert.ToInt32(s) > 0)
                            al.Add(s);
                    }
                }
            }
        }
        catch { }

        return al;
    }
}