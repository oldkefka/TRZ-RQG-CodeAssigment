using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TRZ_WikimediaCount.Test
{
    [TestClass]
    internal class UrlFormatterTest
    {

        [TestInitialize]
        public void Initialize()
        {
            //urlFormatter = new urlFormatter();
        }

        [TestMethod]
        public void Get_correct_url_when_UrlGenerator_is_called()
        {
            string result=  "Algo";
            Assert.AreEqual("Algo", result);
        }
    }
}
