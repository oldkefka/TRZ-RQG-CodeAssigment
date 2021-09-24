using System.Collections.Generic;

namespace TRZ_WikimediaCount.Core.Repositories
{
    public interface ITempHandler
    {
        void SavePageView();
        void LoadPageView();
        List<string> GetPageView(string id);
        void Refresh(string id, List<string> detalle );

    }
}
