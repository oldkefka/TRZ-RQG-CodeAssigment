using System;
using System.Collections.Generic;
using TRZ_WikimediaCount.Model;

namespace TRZ_WikimediaCount.Controller.Interfaces
{
    public interface IUrlFormatter
    {
        List<HourFile> GetListUrls(string urlBase, DateTime time, int hours);
        HourFile UrlGenerator(string urlBase, DateTime time);
        bool UrlValidator(Uri url);
    }
}
