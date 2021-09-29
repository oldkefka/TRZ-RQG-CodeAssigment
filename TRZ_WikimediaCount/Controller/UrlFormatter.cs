using System;
using System.Collections.Generic;
using System.Net;
using TRZ_WikimediaCount.Controller.Interfaces;
using TRZ_WikimediaCount.Model;

namespace TRZ_WikimediaCount.Controller
{
    public class UrlFormatter : IUrlFormatter
    {
        public List<HourFile> GetListUrls(string urlBase, DateTime time, int hours)
        {
            //Variables
            List<HourFile> links = new List<HourFile>();

            int count = 0;
            for (int i = 0; i < hours; i++)
            {
                HourFile item = new HourFile();
                item = UrlGenerator(urlBase, time.AddHours(-count));
                count++;
                if (i == 0 && UrlValidator(item.Url) == false)//Check if file of hour is created
                {
                    count++;
                    item = UrlGenerator(urlBase, time.AddHours(-count));
                }
                links.Add(item);
            }
            return links;
        }

        public HourFile UrlGenerator(string urlBase, DateTime time)
        {
            HourFile item = new HourFile
            {
                FileName = $"{time:yyyyMMdd-HH0000}"
            };
            item.Url = new Uri($"{urlBase}{time:yyyy/yyyy-MM}/pageviews-{item.FileName}.gz");
            return item;
        }

        public bool UrlValidator(Uri url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                int statusCode = (int)response.StatusCode;
                if (statusCode >= 100 && statusCode < 400)
                    return true;

                else if (statusCode >= 500 && statusCode <= 510) //Server Errors
                    Console.WriteLine($"The remote server has thrown an internal error. Url is not valid: {url}");

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
