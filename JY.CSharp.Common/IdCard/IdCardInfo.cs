using JY.CSharp.Common.Enums;
using System;

namespace JY.CSharp.Common.IdCard
{
    public class IdCardInfo
    {
        /// <summary>
        /// 地址 省市区
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex { get; set; }

        /// <summary>
        /// 星座
        /// </summary>
        public Constellation Constellation { get; set; }

        /// <summary>
        /// 生肖
        /// </summary>
        public ChineseZodiac ChineseZodiac { get; set; }
    }
}