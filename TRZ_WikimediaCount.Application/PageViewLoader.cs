using System;
using System.Collections.Generic;
using System.Linq;
using TRZ_WikimediaCount.Core.Repositories;
using TRZ_WikimediaCount.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace TRZ_WikimediaCount.Application
{
    public class PageViewLoader : IPageViewLoader
    {
        private const string BaseURL = "BaseURL";
        private readonly IUrlFormatter urlFormatter;
        private readonly IStreamHandler streanHandler;
        private readonly ITempHandler tempHandler;
        private readonly IConfiguration config;
        public PageViewLoader(IUrlFormatter urlFormatter, IStreamHandler streanHandler, ITempHandler tempHandler, IConfiguration config)
        {
            this.urlFormatter = urlFormatter;
            this.streanHandler = streanHandler;
            this.tempHandler = tempHandler;
            this.config = config;
            //CacheAux.LoadPageView();
        }

        public List<HourDetail> PageViewMainProcess()
        {
            Console.WriteLine("01. Process Begin (it may take more than 5 minutes)");
            DateTime baseDate = Util.ConvertUTCDate(config["Datetime"]);
            int hoursRequest = int.Parse(config["HoursRequest"]);
            Console.WriteLine("02. Get url list");
            var UrlList = urlFormatter.GetListUrls(config[BaseURL], baseDate, hoursRequest);
            List<HourDetail> totalLines = new List<HourDetail>();
            Console.WriteLine("03. Iterate urls");

            foreach (var file in UrlList)
            {
                IEnumerable<HourDetail> pvLines = null;
                List<string> listFile = tempHandler.GetPageView(file.FileName);//Check if a previous process is in temp file (not obligatory)
                if (listFile == null)
                {
                    Console.WriteLine($"04.* {file.FileName} Begin read and download ");

                    listFile = (List<string>)streanHandler.ReadFromUrlandDecompress(file.Url);
                    if (config["Temps:Use"].Equals("Y")) tempHandler.Refresh(file.FileName, listFile);

                }
                Console.WriteLine($"05.* {file.FileName} Formatting with {listFile.Count} registers");
                 pvLines = FormatToDetail(listFile);
                listFile = null;
                totalLines = IntegrateLists(totalLines, pvLines.ToList());
                pvLines = null;
            }

            if (config["Temps:Use"].Equals("Y")) tempHandler.SavePageView();
            return GetTopSum(totalLines, int.Parse(config["SizeResult"]));

        }

        public List<HourDetail> IntegrateLists(List<HourDetail> listBase, List<HourDetail> listAdd)
        {
            var consolidateList = listBase.Concat(listAdd).GroupBy(x => new
            {
                x.DomainCode,
                x.PageTitle
            });
            return consolidateList.Select(x => new HourDetail
            {
                DomainCode = x.Key.DomainCode,
                PageTitle = x.Key.PageTitle,
                CountView = x.Sum(i => i.CountView)
            }).ToList();
        }
        public List<HourDetail> FormatToDetail(IEnumerable<string> list)
        {
            var rows = new List<HourDetail>();
            foreach (var line in list)
            {
                string[] words = line.Replace("\t", " ").Split(' ');
                rows.Add(new HourDetail()
                {
                    DomainCode = words[0],
                    PageTitle = words[1],
                    CountView = int.Parse(words[2])
                }
            );
            }
            return rows;
        }

        public List<HourDetail> GetTopSum(List<HourDetail> list, int top)
        {
            Console.WriteLine($"06. Begin top {top} calculation");
            var lista = list.OrderByDescending(x => x.CountView).Take(top).ToList();
            Console.WriteLine($"07. End calculation");
            return lista;
        }


    }
}
