namespace TRZ_WikimediaCount.Core.Entities
{
    public class HourDetail
    {
        public HourDetail()
        {

        }
        public HourDetail(string domainCode, string pageTitle, int countView)
        {
            DomainCode = domainCode;
            PageTitle = pageTitle;
            CountView = countView;
        }

        public string DomainCode { get; set; }
        public string PageTitle { get; set; }
        public int CountView { get; set; }
    }

}
