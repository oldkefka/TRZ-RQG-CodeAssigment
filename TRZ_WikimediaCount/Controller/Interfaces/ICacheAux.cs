using System.Collections.Generic;
using TRZ_WikimediaCount.Model;

namespace TRZ_WikimediaCount.Controller.Interfaces
{
    public interface ICacheAux
    {
        void SavePageView();
        void LoadPageView();
        List<string> GetPageView(string id);
        void Refresh(string id, List<string> detalle );

    }
}
