using Jane;
using JY.CSharp.Common.String;
using System;

namespace JY.CSharp.Common.IdCard
{
    public static class IdValidate
    {
        private static readonly string Address =
            "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

        public static IResult CheckIdCard(string idNumber)
        {
            if (idNumber.IsNullOrEmpty())
                return Result.Failure("身份证号不能为空");
            if (idNumber.Length != 15 && idNumber.Length != 18)
                return Result.Failure("身份证号长度错误");
            return idNumber.Length == 18 ? CheckIdCard18(idNumber) : CheckIdCard15(idNumber);
        }

        /// <summary>
        /// 18位身份证号验证
        /// </summary>
        /// <param name="idNumber">身份证号</param>
        /// <returns></returns>
        private static IResult CheckIdCard18(string idNumber)
        {
            if (!long.TryParse(idNumber.Remove(17), out var n) || n < Math.Pow(10, 16) || !long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n))
            {
                return Result.Failure("数字验证失败");
            }

            if (Address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return Result.Failure("省份验证失败");
            }
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");

            if (!DateTime.TryParse(birth, out var date))
            {
                return Result.Failure("出生日期验证失败");
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }

            Math.DivRem(sum, 11, out var y);
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                return Result.Failure("校验码验证失败");
            }

            return Result.Success();
        }

        /// <summary>
        /// 15位身份证号验证
        /// </summary>
        /// <param name="idNumber">身份证号</param>
        /// <returns></returns>
        private static IResult CheckIdCard15(string idNumber)
        {
            if (long.TryParse(idNumber, out var n) == false || n < Math.Pow(10, 14))
            {
                return Result.Failure("数字验证失败");
            }

            if (Address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return Result.Failure("省份验证失败");
            }
            string birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");

            if (!DateTime.TryParse(birth, out var time))
            {
                return Result.Failure("生日验证失败");
            }
            return Result.Success();
        }
    }
}