using Microsoft.Extensions.DependencyInjection;
using TRZ_WikimediaCount.Controller;
using TRZ_WikimediaCount.Controller.Interfaces;

namespace TRZ_WikimediaCount
{
    public class Process
    {

        public static void Exec(Config config, IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<Main>().Run(config);

        }

    }
    public class Main
    {
        private readonly IPageViewLoader PageViewLoader;
        private readonly IPrintAux PrintAux;
        public Main(IPageViewLoader pageViewService, IPrintAux printAux)
        {
            PageViewLoader = pageViewService;
            PrintAux = printAux;
        }

        public void Run(Config config)
        {
            PrintAux.PrintResults(PageViewLoader.PageViewMainProcess(config));
        }
    }
}
