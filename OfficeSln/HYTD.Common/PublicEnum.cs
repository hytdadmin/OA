using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Web;

namespace HYTD.Common
{
    /// <summary>
    /// 枚举类
    /// </summary>
    public class PublicEnum
    {

        #region 呼叫中心
        public enum CallWorkBillType
        {
            /// <summary>
            /// 服务中
            /// </summary>
            [Description("来电")]
            Serviceing = 1,
            /// <summary>
            /// 已完成
            /// </summary>
            [Description("上门")]
            Complated = 2,
            /// <summary>
            /// 已回访
            /// </summary>
            [Description("回访")]
            Visited = 3
        }

        public enum CallWorkBillStatus
        {
            /// <summary>
            /// 服务中
            /// </summary>
            [Description("服务中")]
            Serviceing = 1,
            /// <summary>
            /// 已完成
            /// </summary>
            [Description("已完成")]
            Complated = 2,
            /// <summary>
            /// 已回访
            /// </summary>
            [Description("已回访")]
            Visited = 30
        }

        public enum CallCunstomerType
        {
            /// <summary>
            /// 大学 
            /// </summary>
            [Description("大学")]
            University = 1,
            /// <summary>
            /// 政府
            /// </summary>
            [Description("政府")]
            Government = 2
        }
        #endregion

        public enum PubliucIsVidicate
        {
            /// <summary>
            /// 是
            /// </summary>
            [Description("是")]
            Yes = 1,
            /// <summary>
            /// 否
            /// </summary>
            [Description("否")]
            No = 0
        }
        /// <summary>
        /// 根据Enum子元素获取子元素描述
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="val">枚举单个元素</param>
        /// <returns></returns>
        public static string GetEnumDescriptionFromEnum(Type enumType, object val)
        {

            string EnumValue = Enum.GetName(enumType, val);
            if (String.IsNullOrEmpty(EnumValue))
                return "";
            //反射访问字段属性
            FieldInfo finfo = enumType.GetField(EnumValue);
            object[] cAttr = finfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (cAttr.Length > 0)
            {
                DescriptionAttribute desc = cAttr[0] as DescriptionAttribute;
                if (desc != null)
                {
                    return desc.Description;
                }
            }
            return EnumValue;
        }
        #region 固定角色
        /// <summary>
        /// 固定角色
        /// </summary>
        public enum FixedRole
        {
            /// <summary>
            /// 招标办见证
            /// </summary>
            [Description("招标办见证")]
            TheTender = 1,
            /// <summary>
            /// 法律顾问
            /// </summary>
            [Description("法律顾问")]
            LegalAdviser = 2,
            /// <summary>
            /// 校领导
            /// </summary>
            [Description("校领导")]
            SchoolLeadership = 3,
            /// <summary>
            /// 部门领导
            /// </summary>
            [Description("部门领导")]
            DepartmentLeadership = 4,
            /// <summary>
            /// 超级管理员
            /// </summary>
            [Description("超级管理员")]
            SuperAdmin = 5,
            /// <summary>
            /// 维修工
            /// </summary>
            [Description("维修工")]
            MaintenanceWorker = 6,
            /// <summary>
            /// 摄像人员
            /// </summary>
            [Description("摄像人员")]
            CameraCrew = 7,
            /// <summary>
            /// 摄像审批管理员
            /// </summary>
            [Description("摄像审批管理员")]
            CameraApproval = 8
        }
        #endregion
        #region 合同登记完成情况
        public enum ContractComplate
        {
            /// <summary>
            /// 停用
            /// </summary>
            [Description("待登记")]
            toBeComplate = 0,
            /// <summary>
            /// 可用
            /// </summary>
            [Description("已登记")]
            Registed = 1,
            /// <summary>
            /// 可用
            /// </summary>
            [Description("已完成")]
            Complated = 2
        }
        #endregion
        #region 基本数据公用状态
        /// <summary>
        /// 基本数据公用状态
        /// </summary>
        public enum PublicStatus
        {
            /// <summary>
            /// 停用
            /// </summary>
            [Description("禁用")]
            Stop = 0,
            /// <summary>
            /// 可用
            /// </summary>
            [Description("启用")]
            Enable = 1,
            /// <summary>
            /// 删除
            /// </summary>
            [Description("删除")]
            Delete = 2
        }
        #endregion
        #region  调查问卷题型
        /// <summary>
        ///  公用状态
        /// </summary>
        public enum QuestionsType
        {
            /// <summary>
            /// 单选题
            /// </summary>
            [Description("单选题")]
            Radio = 1,
            /// <summary>
            /// 多选题
            /// </summary>
            [Description("多选题")]
            Checkbox = 2,
            /// <summary>
            /// 问答题
            /// </summary>
            [Description("问答题")]
            QA = 3

        }
        #endregion
        #region  公用状态
        /// <summary>
        ///  公用状态
        /// </summary>
        public enum PublicIsVindicate
        {
            /// <summary>
            /// 是
            /// </summary>
            [Description("是")]
            Yes = 1,
            /// <summary>
            /// 否
            /// </summary>
            [Description("否")]
            No = 0
        }
        #endregion

        #region 假期类型
        /// <summary>
        /// 假期类型 1.年假  2.倒休 3.加班
        /// </summary>
        public enum HolidaysType
        { 
            /// <summary>
            /// 年假
            /// </summary>
            [Description("年假")]
            yearDay=1,
            /// <summary>
            /// 倒休
            /// </summary>
            [Description("倒休")]
            Swopped = 2,
            /// <summary>
            /// 加班
            /// </summary>
            [Description("加班")]
            workDay = 3
        }
        #endregion
        #region 具体要求

        public enum Require
        {
            /// <summary>
            /// 存档
            /// </summary>
            [Description("存档")]
            File = 1,
            /// <summary>
            /// 送审
            /// </summary>
            [Description("送审")]
            Examine = 2,
            /// <summary>
            /// 网上发布
            /// </summary>
            [Description("网上发布")]
            Release = 3,
            /// <summary>
            /// 其它
            /// </summary>
            [Description("其它")]
            Other = 4,



        }

        #endregion


        #region 摄像申请审核状态
        public enum ExamineStatus
        {
            [Description("审批中")]
            Submit = 0,
            [Description("审批通过")]
            Pass = 1,
            [Description("拒绝")]
            NotPass = 2
        }


        #endregion

        #region 会议室预约状态

        public enum LectureHallStatus
        {
            [Description("已申请")]
            Apply = 1,
            [Description("已批准")]
            Approve = 2,
            [Description("拒绝")]
            Refuse = -1,
            [Description("取消")]
            Cancel = -2

        }

        #endregion

        #region 收付类型
        public enum IncExpType
        {
            [Description("收入")]
            income = 1,
            [Description("支出")]
            expend = 2,
            [Description("其它")]
            other = 3

        }
        #endregion

        #region  客户类型
        /// <summary>
        /// 客户类型
        /// </summary>
        public enum CustomerType
        {
            /// <summary>
            /// 客户
            /// </summary>
            [Description("客户")]
            Common = 1,
            /// <summary>
            /// 供应商
            /// </summary>
            [Description("供应商")]
            Supplier = 2,
            /// <summary>
            /// VIP客户
            /// </summary>
            [Description("其它")]
            Other = 3,

        }
        #endregion

        #region 资源附件类型
        /// <summary>
        /// 资源附件类型
        /// </summary>
        public enum ResourceAnnex
        {
            [Description("文档")]
            Docmentation = 1,
            [Description("视频")]
            Video = 2,
            [Description("其它")]
            Other = 3,
        }

        #endregion
        /// <summary>
        /// 考勤弹性时间类型
        /// </summary>
        public enum AttendanceElasticType 
        {
            /// <summary>
            /// 正常时间
            /// </summary>
            [Description("正常时间")]
            NoElastic=0,
            /// <summary>
            /// 弹性时间
            /// </summary>
            [Description("弹性时间")]
            elastic=1
        }



        #region 移动端类型
        /// <summary>
        /// 意见反馈
        /// </summary>
        public enum FeedbackType 
        { 
            /// <summary>
            /// 提建议
            /// </summary>
            [Description("提建议")]
            advice = 0,
            /// <summary>
            /// 有错误
            /// </summary>
            [Description("有错误")]
            error = 1,
            /// <summary>
            /// 不会用
            /// </summary>
            [Description("不会用")]
            Nouse = 2,
            /// <summary>
            /// 其他
            /// </summary>
            [Description("其他")]
            other = 3
        }
        /// <summary>
        /// 应用类型
        /// </summary>
        public enum FbAppType
        { 
            /// <summary>
            /// 移动端
            /// </summary>
            [Description("移动端")]
            phoneApp=0,
            /// <summary>
            /// 客服端
            /// </summary>
            [Description("客服端")]
            pcApp=1
        }
        #endregion
        /// <summary>
        /// 得到enum值描述列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> EnumValueDescription(Type enumType)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Type type = typeof(DescriptionAttribute);
            FieldInfo[] fields = enumType.GetFields();

            foreach (FieldInfo field in fields)
            {
                object[] arr = field.GetCustomAttributes(type, true);
                if (arr.Length > 0)
                {
                    dic.Add((int)Enum.Parse(enumType, field.Name), ((DescriptionAttribute)arr[0]).Description);
                }
            }

            return dic;
        }
        /// <summary>
        /// 得到enum字段描述列表
        /// </summary>
        /// <returns>返回字典</returns>
        public static Dictionary<string, string> EnumFieldDescription(Type enumType)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Type type = typeof(DescriptionAttribute);
            FieldInfo[] fields = enumType.GetFields();

            foreach (FieldInfo field in fields)
            {
                object[] arr = field.GetCustomAttributes(type, true);
                if (arr.Length > 0)
                {
                    dic.Add(field.Name, ((DescriptionAttribute)arr[0]).Description);
                }
            }

            return dic;
        }
        /// <summary>
        /// 由枚举值得到枚举描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>g.
        /// <returns></returns>
        public static string GetEnumDescription<T>(string value)
        {
            Type type = typeof(T);
            Dictionary<string, string> dic = EnumFieldDescription(type);
            string description = string.Empty;
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (dic.ContainsKey(Convert.ToString((T)Enum.Parse(type, value))))
                {
                    description = dic[Convert.ToString((T)Enum.Parse(type, value))];
                }
            }
            return description;
        }


        /// <summary>
        /// 由枚举描述得到枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static int GetEnumDescriptionvalue<T>(string description)
        {
            Type type = typeof(T);
            Dictionary<int, string> dic = EnumValueDescription(type);
            int value = 0;
            if (!string.IsNullOrWhiteSpace(description))
            {
                foreach (KeyValuePair<int, string> pair in dic)
                {
                    if (pair.Value == description)
                    {
                        value = pair.Key;
                        return value;
                    }
                }
            }
            return value;
        }

        /// <summary>
        /// 绑定枚举的描述列表-客户端控件
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="blnIsAll">是否填充“全部”</param>
        /// <param name="blnIsLine">是否填充“请选择”</param>
        public static string EnumBindList_Client<T>(bool blnIsAll, bool blnIsLine)
        {

            return EnumBindList_Client<T>(blnIsAll, blnIsLine, null);
        }

        /// <summary>
        /// 绑定枚举的描述列表并选中相应结果
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="blnIsAll">是否填充“全部”</param>
        /// <param name="blnIsLine">是否填充“请选择”</param>
        public static string EnumBindList_Client<T>(bool blnIsAll, bool blnIsLine, int? strEnumValue)
        {
            //注意：‘--’和‘全部’不要同时添加，即两个bool值不能同时为true
            StringBuilder dropDownListHtml = new StringBuilder();
            if (blnIsAll)
            {
                dropDownListHtml.Append("<option  value=\"").Append(PublicConst.PUBLICSTATUSALL_VALUE).Append("\">").Append("全部").Append("</option>");
            }
            if (blnIsLine)
            {
                dropDownListHtml.Append("<option  value=\"").Append(PublicConst.PUBLICSTATUSLINE_VALUE).Append("\">").Append("请选择").Append("</option>");
            }
            Dictionary<int, string> enumDic = EnumValueDescription(typeof(T));
            foreach (KeyValuePair<int, string> pair in enumDic)
            {

                if (strEnumValue != null && strEnumValue.ToString() == pair.Key.ToString())
                {
                    dropDownListHtml.Append("<option selected='selected' value=\"").Append(pair.Key.ToString()).Append("\">").Append(pair.Value).Append("</option>");
                }

                else
                {
                    dropDownListHtml.Append("<option value=\"").Append(pair.Key.ToString()).Append("\">").Append(pair.Value).Append("</option>");
                }
            }
            return dropDownListHtml.ToString();
        }

       

    }

}
