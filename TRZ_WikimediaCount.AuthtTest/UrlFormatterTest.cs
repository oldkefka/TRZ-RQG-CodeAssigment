using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TRZ_WikimediaCount.Application;

namespace TRZ_WikimediaCount.AuthtTest
{
    [TestClass]
    public class UrlFormatterTest
    {
        private UrlFormatter urlFormatter = null;
        private IConfigurationRoot config;
        [TestInitialize]
        public void Initialize()
        {
            urlFormatter = new UrlFormatter();
            config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();
        }

        [TestMethod]
        public void Get_correct_Url_from_UrlGenerator_NOW()
        {
            //is not UTC
            var date = DateTime.Now;
            var result = urlFormatter.UrlGenerator(config["BaseURL"], date);
            var expected = new Uri($"{config["BaseURL"]}{date:yyyy/yyyy-MM}/pageviews-{date:yyyyMMdd-HH0000}.gz");
            Assert.AreEqual(expected, result.Url);
        }

        [TestMethod]
        public void Get_correct_Url_from_UrlGenerator_FixedDate()
        {
            var date = new DateTime(2015, 5, 1, 1, 0, 0);
            var result = urlFormatter.UrlGenerator(config["BaseURL"], date);
            var expected = new Uri($"https://dumps.wikimedia.org/other/pageviews/2015/2015-05/pageviews-20150501-010000.gz");//Example date
            Assert.AreEqual(expected, result.Url);
        }

        [TestMethod]
        public void Check_existence_Url_from_UrlConectionValidator_OK()
        {
            var result = urlFormatter.UrlConectionValidator(new Uri($"https://dumps.wikimedia.org/other/pageviews/2015/2015-05/pageviews-20150501-010000.gz"));//Example date
            var expected =true;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Check_existence_Url_from_UrlConectionValidator_Fail()
        {
            var result = urlFormatter.UrlConectionValidator(new Uri($"https://dumps.wikimedia.org/other/pageviews/1900/1900-05/pageviews-19000101-010000.gz"));//Imposible date
            var expected = false;
            Assert.AreEqual(expected, result);
        }
    }
}
