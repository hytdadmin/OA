using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Collections;

namespace HYTD.Common
{
    /// <summary>
    /// 缓存操作基类
    /// </summary>
   public class PublicCache
    {
       /// <summary>
       /// 是否启用缓存
       /// </summary>
       private static bool blEnableCache = true;
       private static readonly int intCacheTime = 60;



        /// <summary>
        /// 建立缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存内容</param>
        /// <param name="absoluteExpiration">绑存时间，如为null则按默认时间</param>
        public static void AddCache(string key, object value, DateTime absoluteExpiration)
        {
            if(blEnableCache)
            {
                if (HttpRuntime.Cache[key] == null && value != null)
                {
                    HttpRuntime.Cache.Add(key, value, null, absoluteExpiration, TimeSpan.Zero, CacheItemPriority.Default, null);
                }

            }
        }
        public static void AddCache(string key, object value)
        {
            if (blEnableCache)
            {
                if (HttpRuntime.Cache[key] == null && value != null)
                {
                    DateTime dtDefault = DateTime.Now.AddDays(intCacheTime);
                    HttpRuntime.Cache.Add(key, value, null, dtDefault, TimeSpan.Zero, CacheItemPriority.Default, null);
                }

            }
        }
       /// <summary>
       /// 判断缓存是否存在
       /// </summary>
       /// <param name="key"></param>
       /// <returns></returns>
        public static bool IsExistsCache(string key)
        {
            if (blEnableCache==false||HttpRuntime.Cache[key] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
       /// <summary>
       /// 获取Cache中的数据对象
       /// </summary>
       /// <param name="key"></param>
       /// <returns></returns>
        public static object GetCacheData(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        public static void RemoveCache(string key)
        {
            if (HttpRuntime.Cache[key] != null)
            {
                HttpRuntime.Cache.Remove(key);
            }
        }

        /// <summary>
        /// 移除键中带某关键字的缓存
        /// </summary>
        public static void RemoveMultiCache(string keyInclude)
        {
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                if (CacheEnum.Key.ToString().IndexOf(keyInclude.ToString()) >= 0)
                {
                    HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
                }
            }
        }

        /// <summary>
        /// 移除所有缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
    
}
