using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace AspNetCorePlugins
{
    public static class PluginApplicationBuilderExtensions
    {

        public static IApplicationBuilder UsePlugins(this IApplicationBuilder app, string pluginFolder)
        {
            foreach (var folder in Directory.GetDirectories(pluginFolder))
            {
                var requestPath = $"/_content/{Path.GetFileName(folder)}";
                var fileProvider = new PhysicalFileProvider(Path.Combine(folder, "wwwroot"));
                app.UseStaticFiles(
                    new StaticFileOptions
                    {
                        RequestPath = requestPath,
                        FileProvider = fileProvider
                    });
            }
            return app;
        }

    }
}
