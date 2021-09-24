using System;
using System.Collections.Generic;
using TRZ_WikimediaCount.Core.Entities;

namespace TRZ_WikimediaCount.Core.Repositories
{
    public interface IUrlFormatter
    {
        List<HourFile> GetListUrls(string urlBase, DateTime time, int hours);
        HourFile UrlGenerator(string urlBase, DateTime time);
        bool UrlConectionValidator(Uri url);
    }
}
