using AspNetCorePlugins;
using EFCorePlugin.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFCorePlugin
{
    public class PluginStartup : IPluginStartup
    {
        public string AreaName => "EFCore";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbEFCorePlugin>(options =>
                options.UseSqlServer("server=.\\sql2017;database=EFCorePlugin;Trusted_Connection=True;MultipleActiveResultSets=true"));

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        public void Configure(IApplicationBuilder app)
        {
        }

    }
}
