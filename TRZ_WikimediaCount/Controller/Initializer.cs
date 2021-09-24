using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TRZ_WikimediaCount.Application;
using TRZ_WikimediaCount.Core.Repositories;

namespace TRZ_WikimediaCount.Command.Controller
{
    class Initializer
    {
        public static ServiceProvider Set()
        {
            var services = new ServiceCollection()
                //GET CONFIG
                .AddSingleton(StartConfig())
                //INSTANTIATE CONTROLLERS AND HANDLERS
                .AddSingleton<ITempHandler, TempHandler>()
                .AddSingleton<IPageViewLoader, PageViewLoader>()
                .AddSingleton<IStreamHandler, StreamHandler>()
                .AddScoped<IPrintController, PrintController>()
                .AddScoped<IUrlFormatter, UrlFormatter>()
                .AddHttpClient()
                //// MAIN
                .AddSingleton<Main>()
                .BuildServiceProvider();

            return services;
        }

        public static IConfiguration StartConfig()
        {
            return new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .Build();
        }

    }
}
