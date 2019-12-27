using AspNetCorePlugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace WebPlugin2
{
    public class PluginStartup : IPluginStartup
    {
        public string AreaName => "Plugin2";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        public void Configure(IApplicationBuilder app)
        {
        }

    }
}
