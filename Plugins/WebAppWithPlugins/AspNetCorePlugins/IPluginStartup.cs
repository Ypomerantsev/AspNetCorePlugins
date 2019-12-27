using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCorePlugins
{
    public interface IPluginStartup
    {

        public string AreaName { get; }

        void ConfigureServices(IServiceCollection services);
        void Configure(IApplicationBuilder app);

    }
}
