using GIF.Core.Services;
using GIF.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GIF.Core
{
    public static class DependencyInjection
    {
        public static void AddCoreServices(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.Configure<Ds0Setting>(configuration.GetSection(nameof(Ds0Setting)));
            services.Configure<MasterData>(configuration.GetSection(nameof(MasterData)));

            services.AddScoped<CsvFileService>();
            services.AddScoped<Ds0FileService>();
        }
    }
}
