using System.Collections.Generic;
using TRZ_WikimediaCount.Core.Entities;

namespace TRZ_WikimediaCount.Core.Repositories
{
    public interface IPrintController
    {
        void PrintResults(List<HourDetail> pageViews);
        void PrintRow(params string[] columns);
        string CenterAlign(string text, int width);
    }
}
