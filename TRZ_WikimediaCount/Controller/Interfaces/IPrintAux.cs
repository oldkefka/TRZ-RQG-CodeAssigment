using System.Collections.Generic;
using TRZ_WikimediaCount.Model;

namespace TRZ_WikimediaCount.Controller.Interfaces
{
    public interface IPrintAux
    {
        void PrintResults(List<HourDetail> pageViews);
        void PrintRow(params string[] columns);
        string AlignCentre(string text, int width);
    }
}
