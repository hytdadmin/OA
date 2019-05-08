using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.Result
{
    /// <summary>
    /// 公告详情实体类
    /// </summary>
    public class BulletinDetailResult
    {
        /// <summary>
        /// 公告id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 公告标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 公告内容
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// 公告发布时间
        /// </summary>
        public DateTime? PublishTime { get; set; }

        /// <summary>
        /// 公告发布部门
        /// </summary>
        public string DepartmentName { get; set; }

    }
}
