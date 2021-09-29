using System;
using System.Collections.Generic;
using System.Net;
using TRZ_WikimediaCount.Core.Repositories;
using TRZ_WikimediaCount.Core.Entities;

namespace TRZ_WikimediaCount.Application
{
    public class UrlFormatter : IUrlFormatter
    {
        private const string FILENAME_DATEFORMAT= "yyyyMMdd-HH0000";
        private const string URL_DATEFORMAT = "yyyy/yyyy-MM";
        public List<HourFile> GetListUrls(string urlBase, DateTime time, int hours)
        {
            //Variables
            var urlList = new List<HourFile>();
            int count = 0;
            for (int i = 0; i < hours; i++)
            {
                HourFile file = new HourFile();
                file = UrlGenerator(urlBase, time.AddHours(-count));
                if (i == 0 && UrlConectionValidator(file.Url) == false)//Check if file of hour is created
                {
                    count++;
                    file = UrlGenerator(urlBase, time.AddHours(-count));
                }
                count++;
                urlList.Add(file);
            }
            return urlList;
        }

        public HourFile UrlGenerator(string urlBase, DateTime time)
        {
            HourFile item = new HourFile
            {
                FileName = $"{time.ToString(FILENAME_DATEFORMAT)}"
            };
            item.Url = new Uri($"{urlBase}{time.ToString(URL_DATEFORMAT)}/pageviews-{item.FileName}.gz");
            return item;
        }

        public bool UrlConectionValidator(Uri url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                int statusCode = (int)response.StatusCode;
                if (statusCode >= 100 && statusCode < 400)
                    return true;
                else if (statusCode >= 500 && statusCode <= 510) //Server Errors
                    Console.WriteLine($"-The file is not created yet. Url invalid: {url}, jump to the next hour");

                return false;
            }
            catch (WebException)
            {
                Console.WriteLine($"-The file is not created yet. Url invalid: {url}, jump to the next hour");
                return false;
            }
        }
    }
}
