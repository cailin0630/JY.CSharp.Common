using Jane;
using JY.CSharp.Common.Data;
using JY.CSharp.Common.Enums;
using System;
using System.Linq;

namespace JY.CSharp.Common.IdCard
{
    public class IdCardUtility
    {
        public static IResult<IdCardInfo> GetIdCardInfo(string idNumber)
        {
            var getIdCardAddressResult = GetIdCardAddress(idNumber);
            if (!getIdCardAddressResult.Ok)
                return Result.Failure<IdCardInfo>(getIdCardAddressResult.Reason);

            var getIdCardBirthdayResult = GetIdCardBirthday(idNumber);
            if (!getIdCardBirthdayResult.Ok)
                return Result.Failure<IdCardInfo>(getIdCardBirthdayResult.Reason);

            var getIdCardAgeResult = GetIdCardAge(idNumber);
            if (!getIdCardAgeResult.Ok)
                return Result.Failure<IdCardInfo>(getIdCardAgeResult.Reason);

            var getIdCardSexResult = GetIdCardSex(idNumber);
            if (!getIdCardSexResult.Ok)
                return Result.Failure<IdCardInfo>(getIdCardSexResult.Reason);

            var getIdCardConstellationResult = GetIdCardConstellation(idNumber);
            if (!getIdCardConstellationResult.Ok)
                return Result.Failure<IdCardInfo>(getIdCardConstellationResult.Reason);

            var getIdCardChineseZodiacResult = GetIdCardChineseZodiac(idNumber);
            if (!getIdCardChineseZodiacResult.Ok)
                return Result.Failure<IdCardInfo>(getIdCardChineseZodiacResult.Reason);

            return Result.Success(new IdCardInfo
            {
                Address = getIdCardAddressResult.Value,
                Birthday = getIdCardBirthdayResult.Value,
                Age = getIdCardAgeResult.Value,
                Sex = getIdCardSexResult.Value,
                Constellation = getIdCardConstellationResult.Value,
                ChineseZodiac = getIdCardChineseZodiacResult.Value
            });
        }

        private static IResult<string> GetIdCardAddress(string idNumber)
        {
            var addressCode = idNumber.Substring(0, 6);
            if (!IdCardAddressData.IdCardAddressKeyValuePair.ContainsKey(addressCode))
                return Result.Failure<string>("未找到符合条件的地址");
            return Result.Success(IdCardAddressData.IdCardAddressKeyValuePair[addressCode]);
        }

        private static IResult<DateTime> GetIdCardBirthday(string idNumber)
        {
            var temp = string.Empty;
            if (idNumber.Length == 15)
            {
                temp = $"19{idNumber.Substring(6, 6)}";
            }
            if (idNumber.Length == 18)
            {
                temp = $"{idNumber.Substring(6, 8)}";
            }
            var date = $"{temp.Substring(0, 4)}-{temp.Substring(4, 2)}-{temp.Substring(6, 2)}";
            var birthday = DateTime.Parse(date);
            return Result.Success(birthday.Date);
        }

        private static IResult<int> GetIdCardAge(string idNumber)
        {
            var result = GetIdCardBirthday(idNumber);
            if (!result.Ok)
                return Result.Failure<int>("获取出生日期失败");
            var birthday = result.Value;
            var nowDate = DateTime.Now;
            var year = nowDate.Year - birthday.Year;
            if (nowDate.Month < birthday.Month)
            {
                year--;
            }
            if (nowDate.Month == birthday.Month && nowDate.Day < birthday.Day)
            {
                year--;
            }
            //DateTime.Now.Subtract().Year
            return Result.Success(year);
        }

        private static IResult<Sex> GetIdCardSex(string idNumber)
        {
            var sexInt = 0;
            if (idNumber.Length == 15)
            {
                sexInt = int.Parse(idNumber.Substring(14, 1));
            }
            if (idNumber.Length == 18)
            {
                sexInt = int.Parse(idNumber.Substring(16, 1));
            }
            var sex = sexInt % 2 == 0 ? Sex.Female : Sex.Male;
            return Result.Success(sex);
        }

        private static IResult<Constellation> GetIdCardConstellation(string idNumber)
        {
            var result = GetIdCardBirthday(idNumber);
            if (!result.Ok)
                return Result.Failure<Constellation>("获取出生日期失败");
            var birthday = result.Value;
            var nowBirthday = DateTime.Parse($"{DateTime.Now.Year}-{birthday.Month}-{birthday.Day}");
            var constellationValue = ConstellationData.ConstellationDic.FirstOrDefault(p =>
                 nowBirthday >= DateTime.Parse(p.Item1) && nowBirthday <= DateTime.Parse(p.Item2));
            if (constellationValue == null)
                return Result.Failure<Constellation>("未找到符合条件的星座");
            return Result.Success(constellationValue.Item3);
        }

        private static IResult<ChineseZodiac> GetIdCardChineseZodiac(string idNumber)
        {
            var result = GetIdCardBirthday(idNumber);
            if (!result.Ok)
                return Result.Failure<ChineseZodiac>("获取出生日期失败");
            var birthday = result.Value;
            var temp = birthday.Year - 2008;
            var value = temp % 12;
            Enum.TryParse<ChineseZodiac>(Enum.GetValues(typeof(ChineseZodiac)).GetValue(birthday.Year < 2008?value+12:value).ToString(),out var chineseZodiac);
            return Result.Success(chineseZodiac);
        }
    }
}