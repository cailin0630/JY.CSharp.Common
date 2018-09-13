using JY.CSharp.Common.Enums;
using System;
using System.Collections.Generic;

namespace JY.CSharp.Common.Data
{
    public class ConstellationData
    {
        public static List<Tuple<string, string, Constellation>> ConstellationDic = new List<Tuple<string, string, Constellation>>
        {
            new Tuple<string, string, Constellation>("01-20","02-18",Constellation.水瓶座),
            new Tuple<string, string, Constellation>("02-19","03-20",Constellation.双鱼座),
            new Tuple<string, string, Constellation>("03-21","04-19",Constellation.白羊座),
            new Tuple<string, string, Constellation>("04-20","05-20",Constellation.金牛座),
            new Tuple<string, string, Constellation>("05-21","06-21",Constellation.双子座),
            new Tuple<string, string, Constellation>("06-22","07-22",Constellation.巨蟹座),
            new Tuple<string, string, Constellation>("07-23","08-22",Constellation.狮子座),
            new Tuple<string, string, Constellation>("08-23","09-22",Constellation.处女座),
            new Tuple<string, string, Constellation>("09-23","10-23",Constellation.天秤座),
            new Tuple<string, string, Constellation>("10-24","11-22",Constellation.天蝎座),
            new Tuple<string, string, Constellation>("11-23","12-21",Constellation.射手座),
            new Tuple<string, string, Constellation>("12-22","01-19",Constellation.摩羯座)
        };
    }
}