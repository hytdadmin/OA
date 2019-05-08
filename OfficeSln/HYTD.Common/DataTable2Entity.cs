using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace HYTD.Common
{
    /// <summary>
    /// 将一个DataTable转换成实体类List
    /// </summary>
    public class DataTable2Entity
    {

        /// <summary>
        /// 将一个DataTable转换成实体类List
        /// </summary>
        /// <param name="dt">要转换的DataTable</param>
        /// <returns>返回一个实体类列表</returns>
        public static List<T> DataTable2EntityList<T>(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            List<T> entityList = new List<T>();
            T entity = default(T);
            foreach (DataRow dr in dt.Rows)
            {
                entity = Activator.CreateInstance<T>();
                PropertyInfo[] pis = entity.GetType().GetProperties();
                foreach (PropertyInfo pi in pis)
                {
                    if (dt.Columns.Contains(pi.Name))
                    {
                        if (!pi.CanWrite)
                        {
                            continue;
                        }
                        if (dr[pi.Name] != DBNull.Value)
                        {
                            pi.SetValue(entity, dr[pi.Name], null);
                        }
                    }
                }
                entityList.Add(entity);
            }
            return entityList;
        }


    }
}
