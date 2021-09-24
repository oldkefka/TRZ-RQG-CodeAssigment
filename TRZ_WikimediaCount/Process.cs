using Microsoft.Extensions.DependencyInjection;
using TRZ_WikimediaCount.Core;
using TRZ_WikimediaCount.Core.Repositories;

namespace TRZ_WikimediaCount.Command
{
    public class Process
    {

        public static void Exec( ServiceProvider services)
        {
            services.GetService<Main>().Run();
        }

    }
    public class Main
    {
        private readonly IPageViewLoader PageViewLoader;
        private readonly IPrintController PrintAux;
        public Main(IPageViewLoader pageViewService, IPrintController printAux)
        {
            PageViewLoader = pageViewService;
            PrintAux = printAux;
        }

        public void Run()
        {
            PrintAux.PrintResults(PageViewLoader.PageViewMainProcess());
        }
    }
}
