using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace HYTD.Common
{
    /// <summary>
    /// 字符串帮助类
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// Compare string ignore case
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns></returns>
        public static bool EqualIgnoreCase(string strA, string strB)
        {
            if (strA == null || strB == null)
            {
                return false;
            }
            return string.Compare(strA.ToUpper(), strB.ToUpper()) == 0;
        }

        /// <summary>
        /// Format the string with parameters.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetStringValue(object source, params object[] parameters)
        {
            if (source == null)
            {
                return string.Empty;
            }
            string msg = source as string;
            if (string.IsNullOrEmpty(msg))
            {
                return string.Empty;
            }
            if (parameters == null || parameters.Length == 0)
            {
                return msg;
            }
            return string.Format(msg, parameters);
        }

        /// <summary>
        /// Get sub string from source if the string length is larger than maxLength
        /// </summary>
        /// <param name="source"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string SubString(string source, int maxLength)
        {
            return source.Length > maxLength ? source.Substring(0, maxLength - 3) + "..." : source;
        }

        public static bool ContainString(string strA, string strB)
        {
            if (strA.IndexOf(strB) >= 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get string after last chars
        /// </summary>
        /// <param name="source"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string GetStringAfterLast(string source, string chars)
        {
            int index = source.LastIndexOf(chars);
            return source.Substring(index + 1);
        }

        public static string GetNotNullString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            string value = obj as string;
            return value == null ? string.Empty : value;
        }

        /// <summary>
        /// if str is null or empty, not attach it the des,
        /// otherwise. concat the str behind the des.
        /// </summary>
        /// <param name="des"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConcatString(string des, string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return des += str;
            }
            else
            {
                return des;
            }
        }

        /// <summary>
        /// get md5 hash
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        ///  Verify a hash against a string.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        static bool VerifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMD5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// SQL过滤
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SQLFilter(string str)
        {
            if (!string.IsNullOrEmpty(str))
                return str.Replace("'", "").Replace("--", "");
            else
                return "";
        
        }


        #region 截取字符串长度 包括中文

        private static bool ISChinese(char C)
        {
            return (int)C >= 0x4E00 && (int)C <= 0x9FA5;
        }

        public static int GetStrByteLength(string str)
        {
            return System.Text.Encoding.Default.GetByteCount(str);
        }
        /// <summary>
        /// 截取中文字符串长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStrLenthNew(string str, int startIndex, int length)
        {
            int strlen = GetStrByteLength(str);
            if (startIndex + 1 > strlen)
            {
                return "";
            }
            int j = 0;//记录遍历的字节数
            int L = 0;//记录每次截取开始，遍历到开始的字节位，才开始记字节数
            int strW = 0;//字符宽度
            bool b = false;//当每次截取时，遍历到开始截取的位置才为true
            string restr = string.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                char C = str[i];
                if (ISChinese(C))
                {
                    strW = 2;
                }
                else
                {
                    strW = 1;
                }
                if ((L == length - 1) && (L + strW > length))
                {
                    b = false;
                    break;
                }
                if (j >= startIndex)
                {
                    restr += C;
                    b = true;
                }

                j += strW;

                if (b)
                {
                    L += strW;
                    if (((L + 1) > length))
                    {
                        b = false;
                        break;
                    }
                }

            }
            return restr+"...";
        }
        /// <summary>
        /// 截取中文字符串长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStrLenth(string str, int startIndex, int length)
        {
            int strlen = GetStrByteLength(str);
            if (startIndex + 1 > strlen)
            {
                return "";
            }
            int j = 0;//记录遍历的字节数
            int L = 0;//记录每次截取开始，遍历到开始的字节位，才开始记字节数
            int strW = 0;//字符宽度
            bool b = false;//当每次截取时，遍历到开始截取的位置才为true
            string restr = string.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                char C = str[i];
                if (ISChinese(C))
                {
                    strW = 2;
                }
                else
                {
                    strW = 1;
                }
                if ((L == length - 1) && (L + strW > length))
                {
                    b = false;
                    break;
                }
                if (j >= startIndex)
                {
                    restr += C;
                    b = true;
                }

                j += strW;

                if (b)
                {
                    L += strW;
                    if (((L + 1) > length))
                    {
                        b = false;
                        break;
                    }
                }

            }
            return restr;
        }
        #endregion


        #region 数字转换为中文大写
        /// 
        /// 转换数字金额主函数（包括小数）
        /// 
        /// 数字字符串
        /// 转换成中文大写后的字符串或者出错信息提示字符串
        public static string ConvertSum(string str)
        {
            if (!IsPositveDecimal(str))
                return "输入的不是正数字！";
            if (Double.Parse(str) > 999999999999.99)
                return "数字太大，无法换算，请输入一万亿元以下的金额";
            char[] ch = new char[1];
            ch[0] = '.'; //小数点
            string[] splitstr = null; //定义按小数点分割后的字符串数组
            splitstr = str.Split(ch[0]);//按小数点分割字符串
            if (splitstr.Length == 1) //只有整数部分
                return ConvertData(str) + "圆整";
            else //有小数部分
            {
                string rstr;
                rstr = ConvertData(splitstr[0]) + "圆";//转换整数部分
                rstr += ConvertXiaoShu(splitstr[1]);//转换小数部分
                return rstr;
            }

        }
        /// 
        /// 判断是否是正数字字符串
        /// 
        /// 判断字符串
        /// 如果是数字，返回true，否则返回false
        public static bool IsPositveDecimal(string str)
        {
            Decimal d;
            try
            {
                d = Decimal.Parse(str);

            }
            catch (Exception)
            {
                return false;
            }
            if (d > 0)
                return true;
            else
                return false;
        }
        /// 
        /// 转换数字（整数）
        /// 
        /// 需要转换的整数数字字符串
        /// 转换成中文大写后的字符串
        public static string ConvertData(string str)
        {
            string tmpstr = "";
            string rstr = "";
            int strlen = str.Length;
            if (strlen <= 4)//数字长度小于四位
            {
                rstr = ConvertDigit(str);

            }
            else
            {

                if (strlen <= 8)//数字长度大于四位，小于八位
                {
                    tmpstr = str.Substring(strlen - 4, 4);//先截取最后四位数字
                    rstr = ConvertDigit(tmpstr);//转换最后四位数字
                    tmpstr = str.Substring(0, strlen - 4);//截取其余数字
                    //将两次转换的数字加上萬后相连接
                    rstr = String.Concat(ConvertDigit(tmpstr) + "萬", rstr);
                    rstr = rstr.Replace("零萬", "萬");
                    rstr = rstr.Replace("零零", "零");

                }
                else
                    if (strlen <= 12)//数字长度大于八位，小于十二位
                    {
                        tmpstr = str.Substring(strlen - 4, 4);//先截取最后四位数字
                        rstr = ConvertDigit(tmpstr);//转换最后四位数字
                        tmpstr = str.Substring(strlen - 8, 4);//再截取四位数字
                        rstr = String.Concat(ConvertDigit(tmpstr) + "萬", rstr);
                        tmpstr = str.Substring(0, strlen - 8);
                        rstr = String.Concat(ConvertDigit(tmpstr) + "億", rstr);
                        rstr = rstr.Replace("零億", "億");
                        rstr = rstr.Replace("零萬", "零");
                        rstr = rstr.Replace("零零", "零");
                        rstr = rstr.Replace("零零", "零");
                    }
            }
            strlen = rstr.Length;
            if (strlen >= 2)
            {
                switch (rstr.Substring(strlen - 2, 2))
                {
                    case "佰零": rstr = rstr.Substring(0, strlen - 2) + "佰"; break;
                    case "仟零": rstr = rstr.Substring(0, strlen - 2) + "仟"; break;
                    case "萬零": rstr = rstr.Substring(0, strlen - 2) + "萬"; break;
                    case "億零": rstr = rstr.Substring(0, strlen - 2) + "億"; break;

                }
            }

            return rstr;
        }
        /// 
        /// 转换数字（小数部分）
        /// 
        /// 需要转换的小数部分数字字符串
        /// 转换成中文大写后的字符串
        public static string ConvertXiaoShu(string str)
        {
            int strlen = str.Length;
            string rstr;
            if (strlen == 1)
            {
                rstr = ConvertChinese(str) + "角";
                return rstr;
            }
            else
            {
                string tmpstr = str.Substring(0, 1);
                rstr = ConvertChinese(tmpstr) + "角";
                tmpstr = str.Substring(1, 1);
                rstr += ConvertChinese(tmpstr) + "分";
                rstr = rstr.Replace("零分", "");
                rstr = rstr.Replace("零角", "");
                return rstr;
            }


        }

        /// 
        /// 转换数字
        /// 
        /// 转换的字符串（四位以内）
        /// 
        public static string ConvertDigit(string str)
        {
            int strlen = str.Length;
            string rstr = "";
            switch (strlen)
            {
                case 1: rstr = ConvertChinese(str); break;
                case 2: rstr = Convert2Digit(str); break;
                case 3: rstr = Convert3Digit(str); break;
                case 4: rstr = Convert4Digit(str); break;
            }
            rstr = rstr.Replace("拾零", "拾");
            strlen = rstr.Length;

            return rstr;
        }


        /// 
        /// 转换四位数字
        /// 
        public static string Convert4Digit(string str)
        {
            string str1 = str.Substring(0, 1);
            string str2 = str.Substring(1, 1);
            string str3 = str.Substring(2, 1);
            string str4 = str.Substring(3, 1);
            string rstring = "";
            rstring += ConvertChinese(str1) + "仟";
            rstring += ConvertChinese(str2) + "佰";
            rstring += ConvertChinese(str3) + "拾";
            rstring += ConvertChinese(str4);
            rstring = rstring.Replace("零仟", "零");
            rstring = rstring.Replace("零佰", "零");
            rstring = rstring.Replace("零拾", "零");
            rstring = rstring.Replace("零零", "零");
            rstring = rstring.Replace("零零", "零");
            rstring = rstring.Replace("零零", "零");
            return rstring;
        }
        /// 
        /// 转换三位数字
        /// 
        public static string Convert3Digit(string str)
        {
            string str1 = str.Substring(0, 1);
            string str2 = str.Substring(1, 1);
            string str3 = str.Substring(2, 1);
            string rstring = "";
            rstring += ConvertChinese(str1) + "佰";
            rstring += ConvertChinese(str2) + "拾";
            rstring += ConvertChinese(str3);
            rstring = rstring.Replace("零佰", "零");
            rstring = rstring.Replace("零拾", "零");
            rstring = rstring.Replace("零零", "零");
            rstring = rstring.Replace("零零", "零");
            return rstring;
        }
        /// 
        /// 转换二位数字
        /// 
        public static string Convert2Digit(string str)
        {
            string str1 = str.Substring(0, 1);
            string str2 = str.Substring(1, 1);
            string rstring = "";
            rstring += ConvertChinese(str1) + "拾";
            rstring += ConvertChinese(str2);
            rstring = rstring.Replace("零拾", "零");
            rstring = rstring.Replace("零零", "零");
            return rstring;
        }
        /// 
        /// 将一位数字转换成中文大写数字
        /// 
        public static string ConvertChinese(string str)
        {
            //"零壹贰叁肆伍陆柒捌玖拾佰仟萬億圆整角分"
            string cstr = "";
            switch (str)
            {
                case "0": cstr = "零"; break;
                case "1": cstr = "壹"; break;
                case "2": cstr = "贰"; break;
                case "3": cstr = "叁"; break;
                case "4": cstr = "肆"; break;
                case "5": cstr = "伍"; break;
                case "6": cstr = "陆"; break;
                case "7": cstr = "柒"; break;
                case "8": cstr = "捌"; break;
                case "9": cstr = "玖"; break;
            }
            return (cstr);
        }
        #endregion
        #region 移除Html标签的方法
        /// <summary>
        /// 移除html标记
        /// </summary>
        /// <param name="source">移除Html标签之前的字符串</param>
        /// <returns>移除Html标签之后的字符串</returns>
        public static string RemoveHtmlTag(string source)
        {
            return Regex.Replace(source, "<[^>]*>", "");
        }
        #endregion

        /// <summary>
        /// 格式化日期显示
        /// </summary>
        /// <param name="obj">日期对象</param>
        /// <param name="format">格式</param>
        /// <returns>格式化后的日期字符串</returns>
        public static string DateTimeFormat(object obj,string format)
        {
            if (obj == null) return "";
            if (obj is DBNull) return "";
            DateTime dt = DateTime.Parse(obj.ToString());
            return dt.ToString(format);
        }
        /// <summary>
        /// 返回带人民币符号的字符串
        /// </summary>
        /// <param name="dcl"></param>
        /// <returns></returns>
        public static string getMoneyFormat(decimal dcl)
        {
            return "￥"+ dcl.ToString("N");
        }

        public static string FilterHTML(string html)
        {
            System.Text.RegularExpressions.Regex regex1 =
                  new System.Text.RegularExpressions.Regex(@"<script[sS]+</script *>",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 =
                  new System.Text.RegularExpressions.Regex(@" href *= *[sS]*script *:",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 =
                  new System.Text.RegularExpressions.Regex(@" no[sS]*=",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 =
                  new System.Text.RegularExpressions.Regex(@"<iframe[sS]+</iframe *>",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 =
                  new System.Text.RegularExpressions.Regex(@"<frameset[sS]+</frameset *>",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 =
                  new System.Text.RegularExpressions.Regex(@"<img[^>]+>",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 =
                  new System.Text.RegularExpressions.Regex(@"</p>",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 =
                  new System.Text.RegularExpressions.Regex(@"<p>",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 =
                  new System.Text.RegularExpressions.Regex(@"<[^>]*>",
                  System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记   
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性   
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件   
            html = regex4.Replace(html, ""); //过滤iframe   
            html = regex5.Replace(html, ""); //过滤frameset   
            html = regex6.Replace(html, ""); //过滤frameset   
            html = regex7.Replace(html, ""); //过滤frameset   
            html = regex8.Replace(html, ""); //过滤frameset   
            html = regex9.Replace(html, "");
            //html = html.Replace(" ", "");  
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            html = Regex.Replace(html, "[\f\n\r\t\v]", "");  //过滤回车换行制表符  
            return html;
        }  
    }
}
