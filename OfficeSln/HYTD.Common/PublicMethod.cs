using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYTD.Common
{
  public class PublicMethod
    {
        #region 获取系统配置文件值
        /// <summary>
        /// 获取系统配置文件值
        /// </summary>
        /// <param name="key">键</param>
        public static string GetConfigValueByKey(string key)
        {
            string strValue = System.Configuration.ConfigurationSettings.AppSettings[key] ?? "";
            return strValue;
        }
        #endregion
    }
}
