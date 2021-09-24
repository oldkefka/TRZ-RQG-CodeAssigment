using System.Collections.Generic;
using TRZ_WikimediaCount.Core.Entities;

namespace TRZ_WikimediaCount.Core.Repositories
{
    public interface IPageViewLoader
    {
        List<HourDetail> PageViewMainProcess();
        List<HourDetail> FormatToDetail(IEnumerable<string> list);
        List<HourDetail> GetTopSum(List<HourDetail> list, int top );
    }
}
