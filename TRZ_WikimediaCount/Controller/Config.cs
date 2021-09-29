using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;

namespace TRZ_WikimediaCount.Controller
{
    public  class Config
    {
        public string URLBASE { get; set; }
        public int? HOURS { get; set; }
        public int SIZERESULT { get; set; }
        public DateTime BASETIME { get; set; }
 
        public Config()
        {  
            var config = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true).Build();
            URLBASE = config["BaseURL"];
            HOURS = int.Parse(config["HoursRequest"]);
            SIZERESULT = int.Parse(config["SizeResult"]);
            BASETIME = string.IsNullOrEmpty(config["Datetime"]) ? DateTime.UtcNow.AddHours(-1) : DateTime.ParseExact(config["Datetime"], "YYYYMMDD-HH:mm", CultureInfo.InvariantCulture).ToUniversalTime();//Using UTC because the wikipedia server is neutral
        }

    }
}
