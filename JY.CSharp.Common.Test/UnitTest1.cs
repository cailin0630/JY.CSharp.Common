using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JY.CSharp.Common.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetIdCardInfo()
        {
            var idCard = "342901199110312010";
            var result1 = JY.CSharp.Common.IdCard.IdValidate.CheckIdCard(idCard);
           var result= JY.CSharp.Common.IdCard.IdCardUtility.GetIdCardInfo(idCard);
           
        }
    }
}
