using JY.CSharp.Common.String;
using System.Collections.Generic;
using System.IO;

namespace JY.CSharp.Common.Data
{
    public class IdCardAddressData
    {
        private static readonly string TxtPath = "Resource\\IdCardAddress.txt";
        private static Dictionary<string, string> _idCardAddressKeyValuePair;

        public static Dictionary<string, string> IdCardAddressKeyValuePair =>
            _idCardAddressKeyValuePair ?? (_idCardAddressKeyValuePair = InitIdCardAddressKeyValuePair());

        private static Dictionary<string, string> InitIdCardAddressKeyValuePair()
        {
            var result = new Dictionary<string, string>();
            var text = File.ReadAllLines(TxtPath);
            foreach (var value in text)
            {
                if (value.IsNullOrEmpty())
                    continue;
                var keyValue = value.Split(' ');
                result.Add(keyValue[0], keyValue[1]);
            }

            return result;
        }
    }
}