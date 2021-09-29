using System;
using System.Collections.Generic;
using TRZ_WikimediaCount.Core.Repositories;
using TRZ_WikimediaCount.Core.Entities;

namespace TRZ_WikimediaCount.Application
{
    public class PrintController : IPrintController
    {
        public void PrintResults(List<HourDetail> pageViews)
        {
            PrintRow("DOMAIN_CODE", "PAGE_TITLE", "CNT");
            PrintRow("-----------", "----------", "----");
            foreach (var pv in pageViews)
            {
                PrintRow(pv.DomainCode, pv.PageTitle, pv.CountView.ToString());
            }
        }
        public void PrintRow(params string[] columns)
        {
            int width = (100 - columns.Length) / columns.Length;
            string row = "|";
            foreach (string column in columns)
                row += CenterAlign(column, width) + "|";
            Console.WriteLine(row);
        }
        public string CenterAlign(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + ".." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
