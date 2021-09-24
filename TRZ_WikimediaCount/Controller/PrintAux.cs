using System;
using System.Collections.Generic;
using TRZ_WikimediaCount.Controller.Interfaces;
using TRZ_WikimediaCount.Model;

namespace TRZ_WikimediaCount.Controller
{
    class PrintAux : IPrintAux
    {
        public void PrintResults(List<HourDetail> pageViews)
        {
            PrintRow("DOMAIN_CODE", "PAGE_TITLE", "CNT");
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
            {
                row += AlignCentre(column, width) + "|";
            }
            Console.WriteLine(row);
        }
        public string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

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
