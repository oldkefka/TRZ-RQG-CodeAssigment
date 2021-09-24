using Microsoft.Extensions.DependencyInjection;
using TRZ_WikimediaCount.Controller.Interfaces;

namespace TRZ_WikimediaCount.Controller
{
    class Initializer
    {
        public static IServiceCollection Set()
        {
            var services = new ServiceCollection();

            // SERVICES
            services.AddSingleton<IUrlFormatter, UrlFormatter>();
            services.AddSingleton<IPageViewLoader, PageViewLoader>();
            services.AddSingleton<IPrintAux, PrintAux>();
            services.AddSingleton<ICacheAux, CacheAux>();

            // UTILS
            services.AddSingleton<IStreamAux, StreamAux>();
            //// MAIN
            services.AddSingleton<Main>();

            return services;
        }

    }
}
