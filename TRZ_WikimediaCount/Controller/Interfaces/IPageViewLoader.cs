using System.Collections.Generic;
using TRZ_WikimediaCount.Model;

namespace TRZ_WikimediaCount.Controller.Interfaces
{
    public interface IPageViewLoader
    {
        List<HourDetail> PageViewMainProcess(Config config);
        HourDetail FormatToDetail(string lines);

        List<HourDetail> GetTopSum(List<HourDetail> list, int top );
    }
}
