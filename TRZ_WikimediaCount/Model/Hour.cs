using System;
using System.Collections.Generic;

namespace TRZ_WikimediaCount.Model
{
    public class HourBase
    {
        public List<HourDetail> Details { get; set; }
    }
    public class HourFile : HourBase
    {
        public string FileName { get; set; }
        public Uri Url { get; set; }
    }
}
