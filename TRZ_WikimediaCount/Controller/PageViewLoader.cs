using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using TRZ_WikimediaCount.Controller.Interfaces;
using TRZ_WikimediaCount.Model;

namespace TRZ_WikimediaCount.Controller
{
    public class PageViewLoader : IPageViewLoader
    {
        private readonly IUrlFormatter UrlFormatter;
        private readonly IStreamAux StreamAux;
        private readonly ICacheAux CacheAux;
        public PageViewLoader(IUrlFormatter urlFormatter, IStreamAux streamUtil, ICacheAux cacheAux)
        {
            UrlFormatter = urlFormatter;
            StreamAux = streamUtil;
            CacheAux = cacheAux;

            //CacheAux.LoadPageView();
        }

        public List<HourDetail> PageViewMainProcess(Config config)
        {
            Console.WriteLine("-----01. Inicio proceso-----");
            List<HourFile> UrlList = UrlFormatter.GetListUrls(config.URLBASE, config.BASETIME, (int)config.HOURS);
            List<HourDetail> totalLines = new List<HourDetail>();
            Console.WriteLine("-----02. Itera urls-----");
            foreach (var file in UrlList)
            {
                List<HourDetail> pvLines = new List<HourDetail>();
                List<string> listFile = CacheAux.GetPageView(file.FileName);
                if (listFile == null)
                {
                    Console.WriteLine($"-----03.* {file.FileName} Inicio Lectura y descarga -----");
                    using (MemoryStream unzip = StreamAux.ReadFromUrlandDecompress(file.Url))
                        listFile = StreamAux.ReadLines(unzip, Encoding.UTF8);
                    //CacheAux.Refresh(file.FileName, listFile);

                }
                Console.WriteLine($"-----04.* {file.FileName} Formateo con {listFile.Count}-----");
                foreach (string pv in listFile)
                {
                    pvLines.Add(FormatToDetail(pv));
                }
                listFile = null;
                //totalLines.AddRange(pvLines);
                totalLines = SumConcatenate(totalLines, pvLines);
                pvLines = null;
            }

            CacheAux.SavePageView();
            return GetTopSum(totalLines, config.SIZERESULT);

        }
        public List<HourDetail> SumConcatenate(List<HourDetail> listBase, List<HourDetail> listAdd)
        {
            var ab = listBase.Concat(listAdd).GroupBy(x => new
            {
                x.DomainCode,
                x.PageTitle
            });

            return ab.Select(x => new HourDetail
            {
                DomainCode = x.Key.DomainCode,
                PageTitle = x.Key.PageTitle,
                CountView = x.Sum(i => i.CountView)
            }).ToList();

        }
        public HourDetail FormatToDetail(string line)
        {
            string[] words = line.Replace("\t", " ").Split(' ');
            var hourDetail = new HourDetail();
            hourDetail.DomainCode = words[0];
            hourDetail.PageTitle = words[1];
            hourDetail.CountView = int.Parse(words[2]);
            return hourDetail;
        }

        public List<HourDetail> GetTopSum(List<HourDetail> list, int top)
        {
            Console.WriteLine($"-----05 Inicio calculo de top-----");

            //return list
            //   .GroupBy(l => new { l.DomainCode, l.PageTitle })
            //   .Select(cl => new HourDetail
            //   (
            //       cl.First().DomainCode,
            //       cl.First().PageTitle,
            //       cl.Sum(c => c.CountView)
            //   )).OrderByDescending(x => x.CountView).Take(top).ToList();
            //var lista= list.Take(top).ToList();



            var lista = list.OrderByDescending(x => x.CountView).Take(top).ToList();
            Console.WriteLine($"-----05 Fin calculo de top-----");

            return lista;

        }
        //public PageViewLoader(Config config)
        //{


        //}



    }
}
